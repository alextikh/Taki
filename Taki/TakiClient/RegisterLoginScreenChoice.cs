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
    public partial class RegisterLoginScreenChoice : Form
    {
        private NetworkStream sock;
        public RegisterLoginScreenChoice(NetworkStream sock)
        {
            this.sock = sock;
            InitializeComponent();
            this.Text = "Taki";
        }

        private void ExitBut_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void RegisterBut_Click(object sender, EventArgs e)
        {
            RegisterLoginScreenExe form = new RegisterLoginScreenExe(this, sock, "register");
            form.Show();
            this.Hide();
        }

        private void LoginBut_Click(object sender, EventArgs e)
        {
            RegisterLoginScreenExe form = new RegisterLoginScreenExe(this, sock, "login");
            form.Show();
            this.Hide();
        }
    }
}
