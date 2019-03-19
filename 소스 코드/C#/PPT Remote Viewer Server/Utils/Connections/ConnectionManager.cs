using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Threading;
using PPTRemoteViewerServer.Utils.Observers;
using PPTRemoteViewerServer.Utils.Observers.Subjects;
using PPTRemoteViewerServer.Utils.Statics;

namespace PPTRemoteViewerServer.Utils.Connections
{
    public class ConnectionManager : ScreenRenewalObserver
    {
        private bool isRunning = false;
        private TcpListener server = null;
        private Socket client = null;

        private Thread connectionThread = null;
        private ScreenshotThread screenshotThread = null;

        public ConnectionManager(ScreenRenewalSubject subject, int port)
        {
            subject.AddObserver(this);

            server = new TcpListener(IPAddress.Any, port);
            connectionThread = new Thread(new ThreadStart(RunConnection)) { IsBackground = true };
            screenshotThread = new ScreenshotThread(subject);
        }

        public string[] GetIPAddresses()
        {
            List<string> ipAddresses = new List<string>();
            NetworkInterface[] interfaces = NetworkInterface.GetAllNetworkInterfaces();
            
            for (int i = 0; i < interfaces.Length; i++)
            {
                if (interfaces[i].OperationalStatus.Equals(OperationalStatus.Up))
                {
                    UnicastIPAddressInformationCollection infos = interfaces[i].GetIPProperties().UnicastAddresses;

                    for (int j = 0; j < infos.Count; j++)
                    {
                        if (infos[j].Address.AddressFamily.Equals(AddressFamily.InterNetwork))
                        {
                            string ipAddress = infos[j].Address.ToString();

                            if (!(ipAddress.Equals("127.0.0.1")) || ipAddress.ToLower().Equals("localhost"))
                                ipAddresses.Add(interfaces[i].Description + " : " + ipAddress);
                        }
                    }
                }
            }

            return ipAddresses.ToArray();
        }

        public void OnScreenChanged(Bitmap screen)
        {

        }

        public void StartServer()
        {
            if (!isRunning)
            {
                isRunning = true;

                server.Start(1);
                connectionThread.Start();
                screenshotThread.Start();
            }
        }

        private void RunConnection()
        {
            byte[] buffer = new byte[1];
            client = server.AcceptSocket();

            while (isRunning)
            {
                client.Receive(buffer);
                PacketType packetType = PacketReader.GetPacketType(buffer);

                if (packetType.Equals(PacketType.Key))
                {
                    client.Receive(buffer);
                    Win32.SendKey(PacketReader.GetKey(buffer));
                }
                else throw new Exception("Unexcepted Packet Type");
            }
        }

        public void StopServer()
        {
            if (isRunning)
            {
                isRunning = false;
                screenshotThread.Stop();

                server.Stop();
                connectionThread.Abort();
            }
        }
    }
}
