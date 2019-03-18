using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Threading;

using System.Runtime.InteropServices;
using System.Reflection;

using System.Diagnostics;
using System.Net;

namespace PPTRemoteViewerServer
{
    public delegate void progressUpdateDelegate(int index); 

    public partial class MainForm : Form
    {
        [DllImport("user32.dll")]
        static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, UIntPtr dwExtraInfo);
        
        private ThreadStart ts = null;                 
        private Thread t = null;

        TcpListener server = new TcpListener(IPAddress.Any, 1828);
        Socket socket_Tcp;

        Process myProcess;

        bool isStarted = false;

        public MainForm()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (isStarted)
            {
                txtbox1.Text = Dns.GetHostByName(Dns.GetHostName()).AddressList[0].ToString();

                isStarted = false;
                myProcess.Kill();

                server.Stop();
                if (t.IsAlive)
                    t.Abort();
            }

        }

        private void btn_serverstart_Click(object sender, EventArgs e)
        {
            if (!isStarted)
            {
                try { server.Stop(); }
                catch { }
                server.Start(1);
                txtbox1.Text = "server start!!";

                ts = new ThreadStart(con);
                t = new Thread(ts);

                isStarted = true;
                t.IsBackground = true;
                t.Start();


                myProcess = new Process();
                myProcess.StartInfo.FileName = @"RemotePT_View.exe";
                myProcess.StartInfo.Arguments = "9999";
                myProcess.StartInfo.UseShellExecute = false;
                myProcess.StartInfo.CreateNoWindow = true;
                //string myDocumentsPath =   Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                //myProcess.StartInfo.FileName = myDocumentsPath + @"\MyFile.doc";
                //myProcess.StartInfo.Verb = "Print";
                //myProcess.StartInfo.CreateNoWindow = true;
                myProcess.Start();
            }
        }   
            
        private void Form1_Load(object sender, EventArgs e){
            txtbox1.Text = Dns.GetHostByName(Dns.GetHostName()).AddressList[0].ToString();


            string hostName = Dns.GetHostName();
            IPAddress[] d = Dns.GetHostAddresses(Dns.GetHostName());
            // ipv4는 AddressFamily.Internetwork이다.
        }

        public void run_Accept()
        {            
            while (isStarted)
            {
                try { socket_Tcp = server.AcceptSocket(); }
                catch { }
            }
        }

        public void con()
        {
            socket_Tcp = server.AcceptSocket();
            Thread socket_Accept = new Thread(new ThreadStart(run_Accept));
            socket_Accept.Start();

            while(isStarted){
                try
                {
                    byte[] bytes = new byte[1];
                    socket_Tcp.Receive(bytes, bytes.Length, 0);

                    if (bytes[0] == (byte)0x2)
                    {
                        keybd_event((byte)Keys.Left, 0x00, (uint)0, (UIntPtr)1);
                        keybd_event((byte)Keys.Left, 0x00, 0x0002, (UIntPtr)1);
                    }
                    else if (bytes[0] == (byte)0x1)
                    {
                        keybd_event((byte)Keys.Right, (byte)0, (uint)0, (UIntPtr)1);
                        keybd_event((byte)Keys.Right, (byte)0, 0x0002, (UIntPtr)1);
                    }
                    Thread.Sleep(1);
                }
                catch { }
            }

            socket_Accept.Abort();
            socket_Accept = null;
        }

        private void min_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
            WindowState = FormWindowState.Normal; //제일 처음 실행시킨 상태로 보여준다고 해야하나?!
            //이것을 사용하지 않으면 최소화된 상태로 상태표시줄에 뜬다.
            
            this.Visible = true; //폼의 형태, 작업표시줄에 나타냄
        }

        private void Form1_Resize(object sender, System.EventArgs e)
        {   //resize는 크기를 변경시
            if (this.WindowState == FormWindowState.Minimized) { //form이 최소화되었을 때
                this.Visible = false;
                this.Hide();
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (isStarted)
            {
                isStarted = false;
                myProcess.Kill();

                server.Stop();
                if (t.IsAlive)
                    t.Abort();
            }
        }
    
    }
}
