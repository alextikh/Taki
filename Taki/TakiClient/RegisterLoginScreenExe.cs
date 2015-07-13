using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;

namespace newGUI_Taki
{
    public partial class RegisterLoginScreenExe : Form
    {
        private Form parent;
        private NetworkStream sock;
        private string reg_log;
        private Dictionary<string, Image> captcha;
        private string captcha_value;
        private TextBox focusedBox;

        public RegisterLoginScreenExe(Form parent, NetworkStream sock, string reg_log)
        {
            this.Icon = Properties.Resources.TakiIcon;
            this.parent = parent;
            this.sock = sock;
            this.reg_log = reg_log.ToLower();
            InitializeComponent();

            if (this.reg_log == "register")
            {
                initCaptcha();
                this.BackColor = Color.Aquamarine;
                this.Text = "Register";
            }
            else
            {
                this.BackColor = Color.PaleGreen;
                this.Text = "Login";
            }
        }

        private void initCaptcha()
        {
            this.captcha = new Dictionary<string, Image>();

            this.captcha.Add("czchjiav", newGUI_Taki.Properties.Resources.captcha_czchjiav);
            this.captcha.Add("dpbaiajz", newGUI_Taki.Properties.Resources.captcha_dpbaiajz);
            this.captcha.Add("lucytpft", newGUI_Taki.Properties.Resources.captcha_lucytpft);
            this.captcha.Add("nrtgdkwn", newGUI_Taki.Properties.Resources.captcha_nrtgdkwn);
            this.captcha.Add("nvhoxdm", newGUI_Taki.Properties.Resources.captcha_nvhoxdm);
            this.captcha.Add("phxxjdrk", newGUI_Taki.Properties.Resources.captcha_phxxjdrk);
            this.captcha.Add("plbhxzxl", newGUI_Taki.Properties.Resources.captcha_plbhxzxl);
            this.captcha.Add("udbbgxls", newGUI_Taki.Properties.Resources.captcha_udbbgxls);
            this.captcha.Add("wvvjcfua", newGUI_Taki.Properties.Resources.captcha_wvvjcfua);
            this.captcha.Add("yhykemwr", newGUI_Taki.Properties.Resources.captcha_yhykemwr);
            this.captcha.Add("zagxtwdx", newGUI_Taki.Properties.Resources.captcha_zagxtwdx);

            Random r = new Random();
            int captcha_index = r.Next(1, captcha.Count);
            this.captcha_value = captcha.ElementAt(captcha_index).Key;
            this.CaptchaPBox.Image = captcha.ElementAt(captcha_index).Value;
            this.CaptchaPBox.Visible = true;
            this.ProveHumanBox.Visible = true;
        }

        private void UsernameBox_GotFocus(object sender, EventArgs e)
        {
            if (this.UsernameBox.Text == "username ")
            {
                this.UsernameBox.Text = "";
            }
            this.focusedBox = this.UsernameBox;
            fillEmptyBoxes();
        }

        private void PasswordBox_GotFocus(object sender, EventArgs e)
        {
            if (this.PasswordBox.Text == "password ")
            {
                this.PasswordBox.Text = "";
                if (!this.checkBox1.Checked)
                {
                    this.PasswordBox.UseSystemPasswordChar = true;
                }
            }
            this.focusedBox = this.PasswordBox;
            fillEmptyBoxes();
        }

        private void ProveHumanBox_GotFocus(object sender, EventArgs e)
        {
            if (this.ProveHumanBox.Text == "please prove that you are a human")
            {
                this.ProveHumanBox.Text = "";
            }
            this.focusedBox = this.ProveHumanBox;
            fillEmptyBoxes();
        }

        private void BackBut_Click(object sender, EventArgs e)
        {
            this.parent.Show();
            this.Close();
        }

        private void fillEmptyBoxes()
        {
            if (this.UsernameBox.Text == "" && this.focusedBox != this.UsernameBox)
            {
                this.UsernameBox.Text = "username ";
            }
            if (this.PasswordBox.Text == "" && this.focusedBox != this.PasswordBox)
            {
                this.PasswordBox.Text = "password ";
                this.PasswordBox.UseSystemPasswordChar = false;
            }
            if (this.ProveHumanBox.Text == "" && this.focusedBox != this.ProveHumanBox)
            {
                this.ProveHumanBox.Text = "please prove that you are a human";
            }
        }

        private void TextBox_keyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                this.EnterBut.PerformClick();
            }
        }

        private void sendDetails()
        {
            this.ErrorLabel.Visible = false;

            byte[] buffer = null;

            if (this.reg_log == "register")
            {
                if (this.ProveHumanBox.Text != captcha_value)
                {
                    this.ErrorLabel.Text = "Are you a fucking robot?";
                    this.ErrorLabel.Visible = true;
                    return;
                }
                buffer = new ASCIIEncoding().GetBytes(String.Format("@{0}|{1}|{2}||\0", status_code.EN_REGISTER,
                    this.UsernameBox.Text, this.PasswordBox.Text));

            }
            else if (this.reg_log == "login")
            {
                buffer = new ASCIIEncoding().GetBytes(String.Format("@{0}|{1}|{2}||\0", status_code.EN_LOGIN,
                    this.UsernameBox.Text, this.PasswordBox.Text));
            }

            this.sock.Write(buffer, 0, buffer.Length);
            this.sock.Flush();
            buffer = new byte[status_code.MSG_LEN];
            int bytesRead = this.sock.Read(buffer, 0, status_code.MSG_LEN);
            string msg = new ASCIIEncoding().GetString(buffer, 0, bytesRead);

            if (this.reg_log == "register")
            {
                if (msg.Contains(String.Format("@{0}|", status_code.PGM_SCC_REGISTER)))
                {
                    LobbyScreen form = new LobbyScreen(this.parent, this.sock, msg, this.UsernameBox.Text);
                    form.Show();
                    this.Hide();
                }

                else if (msg.Contains(String.Format("@{0}|", status_code.PGM_ERR_NAME_TAKEN)))
                {
                    this.ErrorLabel.Text = "Username already taken";
                    this.ErrorLabel.Visible = true;
                }

                else if (msg.Contains(String.Format("@{0}|", status_code.PGM_ERR_INFO_TOO_LONG)))
                {
                    this.ErrorLabel.Text = "The info exceeds the maximum length possible.";
                    this.ErrorLabel.Visible = true;
                }
            }

            else if (reg_log == "login")
            {
                if (msg.Contains(String.Format("@{0}|", status_code.PGM_SCC_LOGIN)))
                {
                    LobbyScreen form = new LobbyScreen(this.parent, this.sock, msg, this.UsernameBox.Text);
                    form.Show();
                    this.Hide();
                }

                else if (msg.Contains(String.Format("@{0}|", status_code.PGM_ERR_LOGIN)))
                {
                    this.ErrorLabel.Text = "Invalid username or password";
                    this.ErrorLabel.Visible = true;
                }

                else if (msg.Contains(String.Format("@{0}|", status_code.PGM_ERR_INFO_TOO_LONG)))
                {
                    this.ErrorLabel.Text = "The info exceeds the maximum length possible.";
                    this.ErrorLabel.Visible = true;
                }
            }
        }

        private void EnterBut_Click(object sender, EventArgs e)
        {
            if (this.focusedBox == this.UsernameBox)
            {
                this.PasswordBox.Focus();
            }
            else if (this.focusedBox == this.PasswordBox)
            {
                if (this.reg_log == "register")
                {
                    this.ProveHumanBox.Focus();
                }
                else if (this.reg_log == "login")
                {
                    sendDetails();
                }
            }
            else if (this.focusedBox == this.ProveHumanBox)
            {
                sendDetails();
            }
        }

        private void ExitBut_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.PasswordBox.Text != "password ")
            {
                if (checkBox1.Checked)
                {
                    PasswordBox.UseSystemPasswordChar = false;
                }
                else
                {
                    PasswordBox.UseSystemPasswordChar = true;
                }
            }
        }
    }
}