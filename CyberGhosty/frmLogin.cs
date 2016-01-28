using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using WindowsFormsApplication1;

namespace Cyberghosty
{
    public partial class frmLogin : Form
    {

        private const uint ECM_FIRST = 5376U;
        private const uint EM_SETCUEBANNER = 5377U;

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, uint wParam, [MarshalAs(UnmanagedType.LPWStr)] string lParam);
        

        public frmLogin() {
            InitializeComponent();
        }

        private void frmLogin_Load(object sender, EventArgs e) {
            SendMessage(txtUsername.Handle, 5377U, 0U, "Enter username");
            SendMessage(txtPassword.Handle, 5377U, 0U, "Enter password");
        }

        private void btnLogin_Click(object sender, EventArgs e) {
            if (frmConnect.instance.login(txtUsername.Text, txtPassword.Text)){
                this.Hide();
                System.IO.File.WriteAllText(Application.StartupPath + @"\credentials", Base64Encode(txtUsername.Text + @":" + txtPassword.Text));
            } else{
                MessageBox.Show(@"Login failed. This may be due to incorrect username/password, or the need to fill out a Captcha.");
            }
        
        }


        public static string Base64Encode(string plainText) {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(plainTextBytes);
        }

        private void txtUsername_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter){
                txtPassword.Focus();
            }
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) {
                btnLogin.PerformClick();
            }
        }
    }
 }

