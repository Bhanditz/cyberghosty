using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using Newtonsoft.Json.Linq;

namespace Cyberghosty {

    public partial class frmConnect : Form {
        public static eagles13.CyberGhost.CyberGhostInstance instance = new eagles13.CyberGhost.CyberGhostInstance();
        public frmConnect() {
            InitializeComponent();
        }



        private void frmConnect_Load(object sender, EventArgs e) {
            
          loadConfig();
        }

        private void loadConfig() {
            if (File.Exists(Application.StartupPath + @"\credentials")) {
                var credentials = Base64Decode(File.ReadAllText(Application.StartupPath + @"\credentials"));

                if (!instance.login(credentials.Substring(0, credentials.IndexOf(":")), credentials.Substring(credentials.IndexOf(":") + 1, credentials.Length - credentials.IndexOf(":") - 1))) {
                    MessageBox.Show(@"Login failed. This may be due to incorrect username/password, or the need to fill out a Captcha.");
                    File.Delete(Application.StartupPath + @"\credentials");
                    Application.Restart();
                } else {
                    var vpnDetails = instance.getCredentials();
                    while (vpnDetails.Length == 2) {
                        if (instance.createNewCredentials()) {
                            vpnDetails = instance.getCredentials();
                        } else {
                            break;
                        }
                    }
                    if (vpnDetails.Length == 2) {
                        MessageBox.Show(@"There are currently no Linux devices on your account, and creating a new one failed. The program cannot continue. Please create a new Linux device in your CyberGhost control panel.");
                        return;
                    }
                    try {
                        var linuxMachine = vpnDetails.Substring(1, vpnDetails.Length - 2).Split(new string[] { "},{" }, StringSplitOptions.None).Single(str => str.ToLower().Contains("linux, router"));
                        JObject o = JObject.Parse(linuxMachine.StartsWith("{") ? linuxMachine + "}" : "{" + linuxMachine);
                        txtUsername.Text = (o["token"].ToString());
                        txtPassword.Text = (o["secret"].ToString());

                        var serverDetails = instance.getServers();
                        o = JObject.Parse(serverDetails);
                        foreach (var serverInfo in o["countries"]) {
                            try {
                                var server = JObject.Parse(serverInfo["servers"].ToString().Substring(1, serverInfo["servers"].ToString().Length - 2));
                                if (server["name"].ToString() == "Premium Servers") {
                                    cbxServers.Items.Add(server["url"].ToString());
                                }
                                cbxServers.SelectedIndex = 0;
                            } catch (Exception) {
                                txtLog.Text += @"[CyberGhosty] Failed to parse server list from CyberGhost website" + Environment.NewLine;
                            }

                        }
                    } catch (Exception) {
                        txtLog.Text += @"[CyberGhosty] Failed to download config files from CyberGhost website" + Environment.NewLine;

                    }

                    bwGetIP.RunWorkerAsync();


                }
            } else {
                var loginForm = new frmLogin();
                if (loginForm.ShowDialog() == DialogResult.OK){
                    loadConfig();
                }
            }
       

        }

  
        public static string Base64Decode(string base64EncodedData) {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }

        private void btnConnect_Click(object sender, EventArgs e) {
            foreach (var process1 in Process.GetProcessesByName("openvpn")) {
                process1.Kill();
            }
            var configFile = instance.configureConnection(cbxServers.SelectedItem.ToString(), txtUsername.Text, txtPassword.Text);
            var openVpn = getOpenVPNInstall();
            if (openVpn.Length != 0 && configFile.Length != 0){

                var startInfo = new ProcessStartInfo {WorkingDirectory = Path.GetDirectoryName(configFile), FileName = openVpn, Arguments = @"--pause-exit --config """ + configFile + @"""" , RedirectStandardOutput = true, UseShellExecute = false, CreateNoWindow = true};
                Process process = new Process {StartInfo = startInfo};
                process.OutputDataReceived += p_OutputDataReceived;
                process.Start();
                process.BeginOutputReadLine();
                lblStatus.Text = @"Status: Starting";
               
            }
        
        }


        void p_OutputDataReceived(object sender, DataReceivedEventArgs e) {
            try{

                Invoke(new Action(() => {
                    txtLog.Text += e.Data + Environment.NewLine;
                    txtLog.SelectionStart = txtLog.TextLength;
                    txtLog.ScrollToCaret();
                    if (e.Data != null && e.Data.ToLower().Contains("with errors")){
                        lblStatus.Text = @"Status: Failed to connect";
                    } else if (e.Data != null && e.Data.ToLower().Contains("initialization sequence completed")) {
                        lblStatus.Text = @"Status: Connected";

                    }
                }));
            } catch (Exception){
                txtLog.Text += @"[CyberGhosty] Failed to get output" + Environment.NewLine;

            }
            
        }

        private static string getOpenVPNInstall() {
            var initialPath = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles).Replace(" (x86)", "") + @"\OpenVPN\bin\openvpn.exe";
            if (File.Exists(initialPath)){
                return initialPath;
            } else{
                initialPath = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) + @"\OpenVPN\bin\openvpn.exe";
                if (File.Exists(initialPath)){
                    return initialPath;
                }
            }
            return "";
        }


        private void TcMainOnSelectedIndexChanged(object sender, EventArgs eventArgs) {
            this.Size = tcMain.SelectedIndex == 0 ? new Size(304, this.Height) : new Size(600, this.Height);

        }

        private string getExternalIp() {
            try{
                return (new Regex(@"\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}")).Matches(new WebClient().DownloadString("http://checkip.dyndns.org/"))[0].ToString();
            } catch{
                return null;
            }
        }

        private void bwGetIP_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e) {
            var IP = getExternalIp();
            setText("Public IP: " + (IP ?? "Unresolved"));
            System.Threading.Thread.Sleep(3000);
        }

        private void bwGetIP_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e) {
            bwGetIP.RunWorkerAsync();
        }

        public void setText(string text) {
            if (!InvokeRequired) {
                lblIP.Text = text;
            } else {
                lblIP.BeginInvoke((MethodInvoker)delegate () {lblIP.Text = text;});
            }
        }

        private void frmConnect_FormClosing(object sender, FormClosingEventArgs e) {
            if (e.CloseReason == CloseReason.UserClosing){
                e.Cancel = true;
                Hide();
            } else{
                foreach (var process1 in Process.GetProcessesByName("openvpn")) {
                    process1.Kill();
                }
            }

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e) {
            Application.Exit();
        }

        private void showToolStripMenuItem_Click(object sender, EventArgs e) {
            this.Show();
        }

        private void noCyberGhosty_MouseDoubleClick(object sender, MouseEventArgs e) {
            this.Show();
        }
    }
}
