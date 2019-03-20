using System.Collections.Generic;
using System.Drawing;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;
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

        private Thread acceptingThread = null;
        private Thread receivingThread = null;
        private ScreenshotThread screenshotThread = null;

        private ScreenRenewalSubject subject = null;
        private int port = -1;

        public ConnectionManager(ScreenRenewalSubject subject, int port)
        {
            this.subject = subject;
            this.port = port;

            subject.AddObserver(this);
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
            try { client.Send(PacketFactory.CreateScreenPacket(screen)); }
            catch { }
        }

        public void StartServer()
        {
            if (!isRunning)
            {
                isRunning = true;

                server = new TcpListener(IPAddress.Any, port);
                server.Start(1);

                acceptingThread = new Thread(new ThreadStart(RunAccepting)) { IsBackground = true };
                receivingThread = new Thread(new ThreadStart(RunReceiving)) { IsBackground = true };
                screenshotThread = new ScreenshotThread(subject);

                acceptingThread.Start();
                receivingThread.Start();
                screenshotThread.Start();
            }
        }

        private void RunAccepting()
        {
            while (isRunning)
            {
                try { client = server.AcceptSocket(); }
                catch { }
            }
        }

        private void RunReceiving()
        {
            byte[] buffer = new byte[2];

            while (isRunning)
            {
                try 
                { 
                    client.Receive(buffer);
                    Packet packet = PacketReader.Read(buffer);

                    if (!packet.Key.Equals(Keys.None))
                        Win32.SendKey(packet.Key);
                }
                catch 
                { 
                }
            }
        }

        public void StopServer()
        {
            if (isRunning)
            {
                isRunning = false;
                server.Stop();

                acceptingThread.Abort();
                receivingThread.Abort();
                screenshotThread.Stop();
            }
        }
    }
}
