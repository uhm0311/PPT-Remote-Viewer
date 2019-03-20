namespace RemotePT_server
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.txtbox1 = new System.Windows.Forms.TextBox();
            this.btn_serverstart = new System.Windows.Forms.Button();
            this.btn_serverexit = new System.Windows.Forms.Button();
            this.min = new System.Windows.Forms.NotifyIcon(this.components);
            this.SuspendLayout();
            // 
            // txtbox1
            // 
            this.txtbox1.Location = new System.Drawing.Point(18, 39);
            this.txtbox1.Name = "txtbox1";
            this.txtbox1.Size = new System.Drawing.Size(238, 21);
            this.txtbox1.TabIndex = 0;
            // 
            // btn_serverstart
            // 
            this.btn_serverstart.Location = new System.Drawing.Point(42, 130);
            this.btn_serverstart.Name = "btn_serverstart";
            this.btn_serverstart.Size = new System.Drawing.Size(75, 23);
            this.btn_serverstart.TabIndex = 1;
            this.btn_serverstart.Text = "시작";
            this.btn_serverstart.UseVisualStyleBackColor = true;
            this.btn_serverstart.Click += new System.EventHandler(this.btn_serverstart_Click);
            // 
            // btn_serverexit
            // 
            this.btn_serverexit.Location = new System.Drawing.Point(154, 130);
            this.btn_serverexit.Name = "btn_serverexit";
            this.btn_serverexit.Size = new System.Drawing.Size(75, 23);
            this.btn_serverexit.TabIndex = 2;
            this.btn_serverexit.Text = "연결끊기";
            this.btn_serverexit.UseVisualStyleBackColor = true;
            this.btn_serverexit.Click += new System.EventHandler(this.button2_Click);
            // 
            // min
            // 
            this.min.Icon = ((System.Drawing.Icon)(resources.GetObject("min.Icon")));
            this.min.Text = "Server";
            this.min.Visible = true;
            this.min.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.min_MouseDoubleClick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BackgroundImage = global::RemotePT_server.Properties.Resources.색깔;
            this.ClientSize = new System.Drawing.Size(274, 196);
            this.Controls.Add(this.btn_serverexit);
            this.Controls.Add(this.btn_serverstart);
            this.Controls.Add(this.txtbox1);
            this.MaximumSize = new System.Drawing.Size(290, 234);
            this.MinimumSize = new System.Drawing.Size(290, 234);
            this.Name = "Form1";
            this.Text = "Server";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtbox1;
        private System.Windows.Forms.Button btn_serverstart;
        private System.Windows.Forms.Button btn_serverexit;
        private System.Windows.Forms.NotifyIcon min;
    }
}

