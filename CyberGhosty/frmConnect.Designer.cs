using System;
using System.Drawing;
using System.Windows.Forms;

namespace Cyberghosty {
    partial class frmConnect {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmConnect));
            this.cbxServers = new System.Windows.Forms.ComboBox();
            this.tcMain = new System.Windows.Forms.TabControl();
            this.tbMain = new System.Windows.Forms.TabPage();
            this.lblIP = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.btnConnect = new System.Windows.Forms.Button();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.lblUsername = new System.Windows.Forms.Label();
            this.lblServer = new System.Windows.Forms.Label();
            this.tbLog = new System.Windows.Forms.TabPage();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.bwGetIP = new System.ComponentModel.BackgroundWorker();
            this.noCyberGhosty = new System.Windows.Forms.NotifyIcon(this.components);
            this.cmuNotifyIcon = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.showToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tcMain.SuspendLayout();
            this.tbMain.SuspendLayout();
            this.tbLog.SuspendLayout();
            this.cmuNotifyIcon.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbxServers
            // 
            this.cbxServers.FormattingEnabled = true;
            this.cbxServers.Location = new System.Drawing.Point(97, 102);
            this.cbxServers.Name = "cbxServers";
            this.cbxServers.Size = new System.Drawing.Size(173, 21);
            this.cbxServers.TabIndex = 0;
            // 
            // tcMain
            // 
            this.tcMain.Controls.Add(this.tbMain);
            this.tcMain.Controls.Add(this.tbLog);
            this.tcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcMain.Location = new System.Drawing.Point(0, 0);
            this.tcMain.Name = "tcMain";
            this.tcMain.SelectedIndex = 0;
            this.tcMain.Size = new System.Drawing.Size(284, 261);
            this.tcMain.TabIndex = 1;
            this.tcMain.SelectedIndexChanged += new System.EventHandler(this.TcMainOnSelectedIndexChanged);
            // 
            // tbMain
            // 
            this.tbMain.Controls.Add(this.lblIP);
            this.tbMain.Controls.Add(this.lblStatus);
            this.tbMain.Controls.Add(this.btnConnect);
            this.tbMain.Controls.Add(this.txtPassword);
            this.tbMain.Controls.Add(this.txtUsername);
            this.tbMain.Controls.Add(this.lblPassword);
            this.tbMain.Controls.Add(this.lblUsername);
            this.tbMain.Controls.Add(this.lblServer);
            this.tbMain.Controls.Add(this.cbxServers);
            this.tbMain.Location = new System.Drawing.Point(4, 22);
            this.tbMain.Name = "tbMain";
            this.tbMain.Padding = new System.Windows.Forms.Padding(3);
            this.tbMain.Size = new System.Drawing.Size(276, 235);
            this.tbMain.TabIndex = 0;
            this.tbMain.Text = "Main";
            this.tbMain.UseVisualStyleBackColor = true;
            // 
            // lblIP
            // 
            this.lblIP.AutoSize = true;
            this.lblIP.Location = new System.Drawing.Point(71, 51);
            this.lblIP.Name = "lblIP";
            this.lblIP.Size = new System.Drawing.Size(109, 13);
            this.lblIP.TabIndex = 8;
            this.lblIP.Text = "Public IP: Unresolved";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(71, 30);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(109, 13);
            this.lblStatus.TabIndex = 7;
            this.lblStatus.Text = "Status: Disconnected";
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(9, 191);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(261, 33);
            this.btnConnect.TabIndex = 6;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // txtPassword
            // 
            this.txtPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtPassword.Location = new System.Drawing.Point(97, 162);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '•';
            this.txtPassword.Size = new System.Drawing.Size(173, 23);
            this.txtPassword.TabIndex = 5;
            // 
            // txtUsername
            // 
            this.txtUsername.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtUsername.Location = new System.Drawing.Point(97, 132);
            this.txtUsername.MinimumSize = new System.Drawing.Size(173, 24);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(173, 24);
            this.txtUsername.TabIndex = 4;
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(6, 165);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(56, 13);
            this.lblPassword.TabIndex = 3;
            this.lblPassword.Text = "Password:";
            // 
            // lblUsername
            // 
            this.lblUsername.AutoSize = true;
            this.lblUsername.Location = new System.Drawing.Point(6, 135);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(58, 13);
            this.lblUsername.TabIndex = 2;
            this.lblUsername.Text = "Username:";
            // 
            // lblServer
            // 
            this.lblServer.AutoSize = true;
            this.lblServer.Location = new System.Drawing.Point(6, 105);
            this.lblServer.Name = "lblServer";
            this.lblServer.Size = new System.Drawing.Size(41, 13);
            this.lblServer.TabIndex = 1;
            this.lblServer.Text = "Server:";
            // 
            // tbLog
            // 
            this.tbLog.Controls.Add(this.txtLog);
            this.tbLog.Location = new System.Drawing.Point(4, 22);
            this.tbLog.Name = "tbLog";
            this.tbLog.Padding = new System.Windows.Forms.Padding(3);
            this.tbLog.Size = new System.Drawing.Size(276, 235);
            this.tbLog.TabIndex = 1;
            this.tbLog.Text = "Log";
            this.tbLog.UseVisualStyleBackColor = true;
            // 
            // txtLog
            // 
            this.txtLog.BackColor = System.Drawing.SystemColors.InfoText;
            this.txtLog.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLog.ForeColor = System.Drawing.Color.White;
            this.txtLog.Location = new System.Drawing.Point(3, 3);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.Size = new System.Drawing.Size(270, 229);
            this.txtLog.TabIndex = 0;
            // 
            // bwGetIP
            // 
            this.bwGetIP.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwGetIP_DoWork);
            this.bwGetIP.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwGetIP_RunWorkerCompleted);
            // 
            // noCyberGhosty
            // 
            this.noCyberGhosty.ContextMenuStrip = this.cmuNotifyIcon;
            this.noCyberGhosty.Icon = ((System.Drawing.Icon)(resources.GetObject("noCyberGhosty.Icon")));
            this.noCyberGhosty.Text = "CyberGhosty";
            this.noCyberGhosty.Visible = true;
            this.noCyberGhosty.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.noCyberGhosty_MouseDoubleClick);
            // 
            // cmuNotifyIcon
            // 
            this.cmuNotifyIcon.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.cmuNotifyIcon.Name = "cmuNotifyIcon";
            this.cmuNotifyIcon.Size = new System.Drawing.Size(104, 48);
            // 
            // showToolStripMenuItem
            // 
            this.showToolStripMenuItem.Name = "showToolStripMenuItem";
            this.showToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.showToolStripMenuItem.Text = "Show";
            this.showToolStripMenuItem.Click += new System.EventHandler(this.showToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // frmConnect
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.tcMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmConnect";
            this.Text = "CyberGhosty";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmConnect_FormClosing);
            this.Load += new System.EventHandler(this.frmConnect_Load);
            this.tcMain.ResumeLayout(false);
            this.tbMain.ResumeLayout(false);
            this.tbMain.PerformLayout();
            this.tbLog.ResumeLayout(false);
            this.tbLog.PerformLayout();
            this.cmuNotifyIcon.ResumeLayout(false);
            this.ResumeLayout(false);

        }

     



        #endregion

        private System.Windows.Forms.ComboBox cbxServers;
        private System.Windows.Forms.TabControl tcMain;
        private System.Windows.Forms.TabPage tbMain;
        private System.Windows.Forms.Label lblIP;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.Label lblServer;
        private System.Windows.Forms.TabPage tbLog;
        private System.Windows.Forms.TextBox txtLog;
        private System.ComponentModel.BackgroundWorker bwGetIP;
        private NotifyIcon noCyberGhosty;
        private ContextMenuStrip cmuNotifyIcon;
        private ToolStripMenuItem showToolStripMenuItem;
        private ToolStripMenuItem exitToolStripMenuItem;
    }
}