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
using PPTRemoteViewerServer.Utils.Connections;
using PPTRemoteViewerServer.Utils.Observers.Subjects;

namespace PPTRemoteViewerServer
{
    public partial class MainForm : Form
    {
        private ConnectionManager connectionManager = null;
        private const int port = 1282;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            connectionManager = new ConnectionManager(new ScreenRenewalNotifier(), port);
            ipAddresses.Items.AddRange(connectionManager.GetIPAddresses());
            ipAddresses.SelectedIndex = 0;

            this.MaximizeBox = false;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
        }

        private void MainForm_Resize(object sender, System.EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Visible = false;
                this.Hide();
            }
        }

        private void startServer_Click(object sender, EventArgs e)
        {
            connectionManager.StartServer();
            serverState.Text = "서버 상태 : On";
        }

        private void stopServer_Click(object sender, EventArgs e)
        {
            connectionManager.StopServer();
            serverState.Text = "서버 상태 : Off";
        }

        private void tray_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
            WindowState = FormWindowState.Normal; //제일 처음 실행시킨 상태로 보여준다고 해야하나?!
            //이것을 사용하지 않으면 최소화된 상태로 상태표시줄에 뜬다.

            this.Visible = true; //폼의 형태, 작업표시줄에 나타냄
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            connectionManager.StopServer();
            tray.Visible = false;
        }
    }
}
