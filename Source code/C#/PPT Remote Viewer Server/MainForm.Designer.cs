namespace PPTRemoteViewerServer
{
    partial class MainForm
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다.
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.startServer = new System.Windows.Forms.Button();
            this.stopServer = new System.Windows.Forms.Button();
            this.tray = new System.Windows.Forms.NotifyIcon(this.components);
            this.ipAddresses = new System.Windows.Forms.ComboBox();
            this.label = new System.Windows.Forms.Label();
            this.serverState = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // startServer
            // 
            this.startServer.Location = new System.Drawing.Point(12, 140);
            this.startServer.Name = "startServer";
            this.startServer.Size = new System.Drawing.Size(227, 23);
            this.startServer.TabIndex = 1;
            this.startServer.Text = "서버 시작";
            this.startServer.UseVisualStyleBackColor = true;
            this.startServer.Click += new System.EventHandler(this.startServer_Click);
            // 
            // stopServer
            // 
            this.stopServer.Location = new System.Drawing.Point(245, 140);
            this.stopServer.Name = "stopServer";
            this.stopServer.Size = new System.Drawing.Size(227, 23);
            this.stopServer.TabIndex = 2;
            this.stopServer.Text = "서버 종료";
            this.stopServer.UseVisualStyleBackColor = true;
            this.stopServer.Click += new System.EventHandler(this.stopServer_Click);
            // 
            // tray
            // 
            this.tray.Icon = ((System.Drawing.Icon)(resources.GetObject("tray.Icon")));
            this.tray.Text = "PPT Remote Viewer Server";
            this.tray.Visible = true;
            this.tray.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.tray_MouseDoubleClick);
            // 
            // ipAddresses
            // 
            this.ipAddresses.FormattingEnabled = true;
            this.ipAddresses.Items.AddRange(new object[] {
            "오른쪽의 화살표를 클릭하여 아이피 목록을 확인하세요."});
            this.ipAddresses.Location = new System.Drawing.Point(95, 36);
            this.ipAddresses.Name = "ipAddresses";
            this.ipAddresses.Size = new System.Drawing.Size(377, 20);
            this.ipAddresses.TabIndex = 3;
            // 
            // label
            // 
            this.label.AutoSize = true;
            this.label.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.label.Location = new System.Drawing.Point(12, 40);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(77, 12);
            this.label.TabIndex = 4;
            this.label.Text = "아이피 목록 :";
            // 
            // serverState
            // 
            this.serverState.AutoSize = true;
            this.serverState.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.serverState.Location = new System.Drawing.Point(12, 64);
            this.serverState.Name = "serverState";
            this.serverState.Size = new System.Drawing.Size(84, 12);
            this.serverState.TabIndex = 5;
            this.serverState.Text = "서버 상태 : Off";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BackgroundImage = global::PPTRemoteViewerServer.Properties.Resources.Background;
            this.ClientSize = new System.Drawing.Size(484, 195);
            this.Controls.Add(this.serverState);
            this.Controls.Add(this.label);
            this.Controls.Add(this.ipAddresses);
            this.Controls.Add(this.stopServer);
            this.Controls.Add(this.startServer);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(500, 234);
            this.MinimumSize = new System.Drawing.Size(290, 234);
            this.Name = "MainForm";
            this.Text = "PPT Remote Viewer Server";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button startServer;
        private System.Windows.Forms.Button stopServer;
        private System.Windows.Forms.NotifyIcon tray;
        private System.Windows.Forms.ComboBox ipAddresses;
        private System.Windows.Forms.Label label;
        private System.Windows.Forms.Label serverState;
    }
}

