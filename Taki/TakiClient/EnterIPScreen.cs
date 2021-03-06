﻿using System;
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
    public partial class EnterIPScreen : Form
    {
        private NetworkStream sock;
        private const int PORT = 10113;
        public EnterIPScreen()
        {
            InitializeComponent();
            this.Text = "Enter IP windows";
        }

        private void EnterIPBox_KeyPressed(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                this.EnterBut.PerformClick();
            }
        }

        private void EnterIPBox_Click(object sender, EventArgs e)
        {

            if (this.EnterIPBox.Text == "Enter the server\'s IP address:")
            {
                this.EnterIPBox.Text = "";
            }
        }

        private void connect()
        {
            TcpClient client = null;
            ErrorLabel.Visible = false;
            bool success = true;
            try
            {
                client = new TcpClient(this.EnterIPBox.Text, PORT);
            }
            catch (System.Net.Sockets.SocketException)
            {
                ErrorLabel.Visible = true;
                success = false;
            }
            finally
            {
                if (success)
                {
                    sock = client.GetStream();
                    RegisterLoginScreenChoice form = new RegisterLoginScreenChoice(sock);
                    Hide();
                    form.Show();
                }
            }
        }

        private void ExitBut_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void EnterBut_Click(object sender, EventArgs e)
        {
            connect();
        }
    }
}
