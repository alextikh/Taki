using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Net;
using System.Net.Sockets;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace newGUI_Taki
{
    public partial class Form1 : Form
    {
        private NetworkStream sock;
        private string menu;
        bool is_admin;
        bool is_register;
        public Form1()
        {
            InitializeComponent();
            lblUsername.Visible = false;
            lblPassword.Visible = false;
            lblError.Visible = false;
            lblScreen.Visible = false;
            butEnterDetails.Visible = false;
            butEnterChoseLobby.Visible = false;
            butEnterChoseInRoom.Visible = false;
            tbUsername.Visible = false;
            tbPassword.Visible = false;
            tbEnterChose.Visible = false;
        }

        private void butRegistr_Click(object sender, EventArgs ee)
        {
            tbPassword.Text = "";
            tbUsername.Text = "";
            butExit.Visible = false;
            butRegistr.Visible = false;
            butLogin.Visible = false;
            tbPassword.Visible = true;
            tbUsername.Visible = true;
            lblPassword.Visible = true;
            lblUsername.Visible = true;
            lblError.Visible = false;
            lblPassword.Text = "Enter password";
            lblUsername.Text = "Enter username";
            butEnterDetails.Visible = true;
            is_register = true;
        }

        private void butLogin_Click(object sender, EventArgs ee)
        {
            tbPassword.Text = "";
            tbUsername.Text = "";
            butExit.Visible = false;
            butRegistr.Visible = false;
            butLogin.Visible = false;
            tbPassword.Visible = true;
            tbUsername.Visible = true;
            lblPassword.Visible = true;
            lblUsername.Visible = true;
            lblError.Visible = false;
            butEnterDetails.Visible = true;
            is_register = false;
        }

        private void butExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void lobby(bool open_rooms)
        {
            butEnterChoseInRoom.Visible = false;
            tbUsername.Visible = false;
            butEnterDetails.Visible = false;
            butEnterChoseLobby.Visible = true;
            butEnterChoseLobby.Text = "Enter option";
            tbEnterChose.Visible = true;
            tbEnterChose.Text = "";
            if (open_rooms)
            {
                menu = "1. get room list\n2. join room\n3. create room\n4. logout\n";
            }
            else
            {
                menu = "No open rooms!\n3. create room\n4. logout\n";
            }
            lblScreen.Visible = true;
            lblScreen.Text = menu;
        }
        private void print_room_list(string msg)
        {
            string str = "Room list:\nAdmin, Number of players, Is open\n";
            int i = msg.IndexOf('|');
            int j;
            int num_players;
            string is_open, room, admin;
            while (msg[i + 1] != '|')
            {
                msg.Substring(i + 1).Contains('|');
                j = msg.Substring(i + 1).IndexOf("|") + i + 1;
                room = msg.Substring(i + 1, j - (i + 1));
                i = j;
                j = msg.Substring(i + 1).IndexOf("|") + i + 1;
                admin = msg.Substring(i + 1, j - (i + 1));
                i = j;
                j = msg.Substring(i + 1).IndexOf("|") + i + 1;
                num_players = int.Parse(msg.Substring(i + 1, j - (i + 1)));
                i = j;
                j = msg.Substring(i + 1).IndexOf("|") + i + 1;
                msg.Substring(i + 1, j - (i + 1));
                if (msg.Substring(i + 1, j - (i + 1)) == status_code.ROOM_OPEN)
                {
                    is_open = "open";
                }
                else
                {
                    is_open = "close";
                }
                str += String.Format("{0}{1}{2}{3}\n", room, admin, num_players, is_open);
                i = j;
            }
            lblScreen.Text = str;
        }
        private void butEnterDetails_Click(object sender, EventArgs e)
        {
            byte[] buffer;
            string username = tbUsername.Text;
            string password = tbPassword.Text;
            string recv = "";
            TcpClient client = new TcpClient();
            IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Parse("10.0.0.11"), 10113);
            client.Connect(serverEndPoint);
            this.sock = client.GetStream();
            if (is_register)
            {
                buffer = new ASCIIEncoding().GetBytes(String.Format("@{0}|{1}|{2}||\0", status_code.EN_REGISTER, username, password));
                this.sock.Write(buffer, 0, buffer.Length);
            }
            else
            {
                buffer = new ASCIIEncoding().GetBytes(String.Format("@{0}|{1}|{2}||\0", status_code.EN_LOGIN, username, password));
                this.sock.Write(buffer, 0, buffer.Length);
            }
            this.sock.Flush();
            buffer = new byte[status_code.MSG_LEN];
            int bytesRead = this.sock.Read(buffer, 0, status_code.MSG_LEN);
            recv = new ASCIIEncoding().GetString(buffer, 0, bytesRead);
            if (is_register)
            {
                if (recv.Contains(String.Format("@{0}||", status_code.PGM_SCC_REGISTER)))
                {
                    lblError.Visible = false;
                    lblPassword.Visible = false;
                    lblUsername.Visible = false;
                    tbPassword.Visible = false;
                    tbUsername.Visible = true;
                    butLogin.Visible = false;
                    lobby(false);
                }
                else if (recv.Contains(String.Format("@{0}|", status_code.PGM_SCC_REGISTER)))
                {
                    lblError.Visible = false;
                    lblPassword.Visible = false;
                    lblUsername.Visible = false;
                    tbPassword.Visible = false;
                    tbUsername.Visible = true;
                    butLogin.Visible = false;
                    print_room_list(recv);
                    lobby(true);
                }
                else if (recv.Contains(String.Format("@{0}|", status_code.PGM_ERR_NAME_TAKEN)))
                {
                    lblError.Visible = true;
                    lblError.Text = "username already taken";
                }
                else if (recv.Contains(String.Format("@{0}|", status_code.PGM_ERR_INFO_TOO_LONG)))
                {
                    lblError.Visible = true;
                    lblError.Text = "The info exceeds the maximum length possible.";
                }
            }
            else
            {
                if (recv.Contains(String.Format("@{0}||", status_code.PGM_SCC_LOGIN)))
                {
                    lblError.Visible = false;
                    lblPassword.Visible = false;
                    lblUsername.Visible = false;
                    tbPassword.Visible = false;
                    tbUsername.Visible = true;
                    butLogin.Visible = false;
                    lobby(false);
                }
                else if (recv.Contains(String.Format("@{0}|", status_code.PGM_SCC_LOGIN)))
                {
                    lblError.Visible = false;
                    lblPassword.Visible = false;
                    lblUsername.Visible = false;
                    tbPassword.Visible = false;
                    tbUsername.Visible = true;
                    butLogin.Visible = false;
                    print_room_list(recv);
                    lobby(true);
                }
                else if (recv.Contains(String.Format("@{0}|", status_code.PGM_ERR_LOGIN)))
                {
                    lblError.Visible = true;
                    lblError.Text = "invalid username or password";
                }
                else if (recv.Contains(String.Format("@{0}|", status_code.PGM_ERR_INFO_TOO_LONG)))
                {
                    lblError.Visible = true;
                    lblError.Text = "The info exceeds the maximum length possible.";
                }
            }
        }

        private void butEnterChoseLobby_Click(object sender, EventArgs e)
        {
            byte[] buffer;
            string option;
            string msg, code;
            option = tbEnterChose.Text;
            if (option.Equals("") || option[0] > '4' || option[0] < '1')
            {
                lobby(true);
                return;
            }

            if (option.Equals("1")) ///get room list
            {
                buffer = new ASCIIEncoding().GetBytes(String.Format("@{0}||\0", status_code.RM_ROOM_LIST));
                this.sock.Write(buffer, 0, buffer.Length);
                this.sock.Flush();
                buffer = new byte[status_code.MSG_LEN];
                int bytesRead = this.sock.Read(buffer, 0, status_code.MSG_LEN);
                msg = new ASCIIEncoding().GetString(buffer, 0, bytesRead);
                print_room_list(msg);
            }
            else if (option.Equals("2"))///join room  by admin name
            {
                butEnterChoseLobby.Visible = false;
                butEnterChoseInRoom.Visible = false;
                butEnterRoomName.Visible = true;
                lblScreen.Text = "Enter room to join";
                tbEnterChose.Text = "";
            }
            else if (option.Equals("3")) ///create room 
            {
                buffer = new ASCIIEncoding().GetBytes(String.Format("@{0}|{1}||\0", status_code.RM_CREATE_GAME, tbUsername.Text));
                this.sock.Write(buffer, 0, buffer.Length);

                this.sock.Flush();
                buffer = new byte[status_code.MSG_LEN];
                int bytesRead = this.sock.Read(buffer, 0, status_code.MSG_LEN);
                msg = new ASCIIEncoding().GetString(buffer, 0, bytesRead);

                code = msg.Substring(1, msg.IndexOf("|") - 1);
                if (code.Equals(status_code.PGM_SCC_GAME_CREATED))
                {
                    lblError.Text = "room, created successfully";
                }
                else if (code.Equals(status_code.PGM_ERR_GAME_CREATED))
                {
                    lblError.Visible = true;
                    lblError.Text = "failed to create room";
                }
                in_room(true);
            }
            else if (option.Equals("4"))///logout
            {
                buffer = new ASCIIEncoding().GetBytes(String.Format("@{0}||", status_code.EN_LOGOUT));
                this.sock.Write(buffer, 0, buffer.Length);
                sock.Close();
                butLogin.Visible = true;
                mainPage();
            }
        }
        private void mainPage()
        {
            butRegistr.Visible = true;
            butExit.Visible = true;
            butLogin.Visible = true;
            lblScreen.Text = "";
            tbEnterChose.Visible = false;
            butEnterChoseLobby.Visible = false;
        }

        private void butEnterChoseInRoom_Click(object sender, EventArgs e)
        {
            byte[] buffer;
            string option;
            option = tbEnterChose.Text;
            if (option.Equals("") || option[0] > '4' || option[0] < '1')
            {
                in_room(is_admin);
                return;
            }

            if (option.Equals("1"))
            {
                if (!is_admin)
                {
                    buffer = new ASCIIEncoding().GetBytes(String.Format("@{0}||\0", status_code.RM_LEAVE_GAME));
                    this.sock.Write(buffer, 0, buffer.Length);
                    lobby(true);
                }
                else
                {
                    buffer = new ASCIIEncoding().GetBytes(String.Format("@{0}||\0", status_code.RM_CLOSE_GAME));
                    this.sock.Write(buffer, 0, buffer.Length);
                    lobby(true);
                }
            }
            if (is_admin && option.Equals("2"))
            {
                buffer = new ASCIIEncoding().GetBytes(String.Format("@{0}||\0", status_code.RM_START_GAME));//start game
                this.sock.Write(buffer, 0, buffer.Length);
                this.sock.Flush();
                buffer = new byte[status_code.MSG_LEN];
                int bytesRead = this.sock.Read(buffer, 0, status_code.MSG_LEN);
                string msg = new ASCIIEncoding().GetString(buffer, 0, bytesRead);
                parseInfo(msg);
            }
        }
        private void in_room(bool admin)
        {
            is_admin = admin;
            string str = "";
            str += "1. leave room\n";
            if (is_admin)
            {
                str += "2. start game\n";
            }
            butEnterChoseLobby.Visible = false;
            lblScreen.Visible = true;
            lblScreen.Text = str + "\nEnter your chose:";
            tbEnterChose.Text = "";
            butEnterChoseInRoom.Visible = true;
        } 
        private void parseInfo(string msg)
        {
            string[] s = new string[8];

            msg = msg.Substring(6);
            int i = msg.IndexOf('|');
            int j = 0, k = 0;
            while (k < 8)
            {
                msg.Substring(i + 1).Contains(',');
                j = msg.Substring(i + 1).IndexOf(",") + i + 1;
                s[k] = msg.Substring(i + 1, j - (i + 1));
                i = j;
                k++;
            }

            printCardsInPB(s);
        }
        private void printCardsInPB(string[] msg)
        {
            tbEnterChose.Visible = false;
            lblScreen.Visible = false;
            butEnterChoseInRoom.Visible = false;
            int x = 5, y = 5;
            PictureBox[] Shapes = new PictureBox[8];
            for (int i = 0; i < 8; i++)
            {
                Shapes[i] = new PictureBox();
                Shapes[i].Name = "ItemNum_" + i.ToString();
                x += 20;
                y += 20;
                Shapes[i].Location = new Point(x, y);
                Shapes[i].Size = new Size(100, 100);
                Shapes[i].BackColor = Color.Yellow;
                Shapes[i].SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                Shapes[i].Visible = true;
                this.Controls.Add(Shapes[i]);

            }

            int j = 0;
            while (j < 8)
            {
                if (msg[j].Equals("4998")) { Shapes[j].Image = newGUI_Taki.Properties.Resources._1_B; j++; }
                else if (msg[j].Equals("49103")) { Shapes[j].Image = newGUI_Taki.Properties.Resources._1_G; j++; }
                else if (msg[j].Equals("49114")) { Shapes[j].Image = newGUI_Taki.Properties.Resources._1_R; j++; }
                else if (msg[j].Equals("49121")) { Shapes[j].Image = newGUI_Taki.Properties.Resources._1_Y; j++; }
                else if (msg[j].Equals("5198")) { Shapes[j].Image = newGUI_Taki.Properties.Resources._3_B; j++; }
                else if (msg[j].Equals("51103")) { Shapes[j].Image = newGUI_Taki.Properties.Resources._3_G; j++; }
                else if (msg[j].Equals("51114")) { Shapes[j].Image = newGUI_Taki.Properties.Resources._3_R; j++; }
                else if (msg[j].Equals("51121")) { Shapes[j].Image = newGUI_Taki.Properties.Resources._3_Y; j++; }
                else if (msg[j].Equals("5298")) { Shapes[j].Image = newGUI_Taki.Properties.Resources._4_B; j++; }
                else if (msg[j].Equals("52103")) { Shapes[j].Image = newGUI_Taki.Properties.Resources._4_G; j++; }
                else if (msg[j].Equals("52114")) { Shapes[j].Image = newGUI_Taki.Properties.Resources._4_R; j++; }
                else if (msg[j].Equals("52121")) { Shapes[j].Image = newGUI_Taki.Properties.Resources._4_Y; j++; }
                else if (msg[j].Equals("5398")) { Shapes[j].Image = newGUI_Taki.Properties.Resources._5_B; j++; }
                else if (msg[j].Equals("53103")) { Shapes[j].Image = newGUI_Taki.Properties.Resources._5_G; j++; }
                else if (msg[j].Equals("53114")) { Shapes[j].Image = newGUI_Taki.Properties.Resources._5_R; j++; }
                else if (msg[j].Equals("53121")) { Shapes[j].Image = newGUI_Taki.Properties.Resources._5_Y; j++; }
                else if (msg[j].Equals("54498")) { Shapes[j].Image = newGUI_Taki.Properties.Resources._6_B; j++; }
                else if (msg[j].Equals("54103")) { Shapes[j].Image = newGUI_Taki.Properties.Resources._6_G; j++; }
                else if (msg[j].Equals("54114")) { Shapes[j].Image = newGUI_Taki.Properties.Resources._6_R; j++; }
                else if (msg[j].Equals("54121")) { Shapes[j].Image = newGUI_Taki.Properties.Resources._6_Y; j++; }
                else if (msg[j].Equals("5598")) { Shapes[j].Image = newGUI_Taki.Properties.Resources._7_B; j++; }
                else if (msg[j].Equals("55103")) { Shapes[j].Image = newGUI_Taki.Properties.Resources._7_G; j++; }
                else if (msg[j].Equals("55114")) { Shapes[j].Image = newGUI_Taki.Properties.Resources._7_R; j++; }
                else if (msg[j].Equals("55121")) { Shapes[j].Image = newGUI_Taki.Properties.Resources._7_Y; j++; }
                else if (msg[j].Equals("5698")) { Shapes[j].Image = newGUI_Taki.Properties.Resources._8_B; j++; }
                else if (msg[j].Equals("56103")) { Shapes[j].Image = newGUI_Taki.Properties.Resources._8_G; j++; }
                else if (msg[j].Equals("56114")) { Shapes[j].Image = newGUI_Taki.Properties.Resources._8_R; j++; }
                else if (msg[j].Equals("56121")) { Shapes[j].Image = newGUI_Taki.Properties.Resources._8_Y; j++; }
                else if (msg[j].Equals("5798")) { Shapes[j].Image = newGUI_Taki.Properties.Resources._9_B; j++; }
                else if (msg[j].Equals("57103")) { Shapes[j].Image = newGUI_Taki.Properties.Resources._9_G; j++; }
                else if (msg[j].Equals("57114")) { Shapes[j].Image = newGUI_Taki.Properties.Resources._9_R; j++; }
                else if (msg[j].Equals("57121")) { Shapes[j].Image = newGUI_Taki.Properties.Resources._9_Y; j++; }
                else if (msg[j].Equals("4398")) { Shapes[j].Image = newGUI_Taki.Properties.Resources.Plus_B; j++; }
                else if (msg[j].Equals("43103")) { Shapes[j].Image = newGUI_Taki.Properties.Resources.Plus_G; j++; }
                else if (msg[j].Equals("43114")) { Shapes[j].Image = newGUI_Taki.Properties.Resources.Plus_R; j++; }
                else if (msg[j].Equals("43121")) { Shapes[j].Image = newGUI_Taki.Properties.Resources.Plus_Y; j++; }
                else if (msg[j].Equals("3398")) { Shapes[j].Image = newGUI_Taki.Properties.Resources.Stop_B; j++; }
                else if (msg[j].Equals("33103")) { Shapes[j].Image = newGUI_Taki.Properties.Resources.Stop_G; j++; }
                else if (msg[j].Equals("33114")) { Shapes[j].Image = newGUI_Taki.Properties.Resources.Stop_R; j++; }
                else if (msg[j].Equals("33121")) { Shapes[j].Image = newGUI_Taki.Properties.Resources.Stop_Y; j++; }
                else if (msg[j].Equals("6098")) { Shapes[j].Image = newGUI_Taki.Properties.Resources.ChangeDirect_B; j++; }
                else if (msg[j].Equals("60103")) { Shapes[j].Image = newGUI_Taki.Properties.Resources.ChangeDirect_G; j++; }
                else if (msg[j].Equals("60114")) { Shapes[j].Image = newGUI_Taki.Properties.Resources.ChangeDirect_R; j++; }
                else if (msg[j].Equals("60121")) { Shapes[j].Image = newGUI_Taki.Properties.Resources.ChangeDirect_Y; j++; }
                else if (msg[j].Equals("3698")) { Shapes[j].Image = newGUI_Taki.Properties.Resources.Plus2_B; j++; }
                else if (msg[j].Equals("36103")) { Shapes[j].Image = newGUI_Taki.Properties.Resources.Plus2_G; j++; }
                else if (msg[j].Equals("36114")) { Shapes[j].Image = newGUI_Taki.Properties.Resources.Plus2_R; j++; }
                else if (msg[j].Equals("36121")) { Shapes[j].Image = newGUI_Taki.Properties.Resources.Plus2_Y; j++; }
                else if (msg[j].Equals("9498")) { Shapes[j].Image = newGUI_Taki.Properties.Resources.Taki_B; j++; }
                else if (msg[j].Equals("94103")) { Shapes[j].Image = newGUI_Taki.Properties.Resources.Taki_G; j++; }
                else if (msg[j].Equals("94114")) { Shapes[j].Image = newGUI_Taki.Properties.Resources.Taki_R; j++; }
                else if (msg[j].Equals("94121")) { Shapes[j].Image = newGUI_Taki.Properties.Resources.Taki_Y; j++; }
                else if (msg[j].Equals("370")) { Shapes[j].Image = newGUI_Taki.Properties.Resources.ChangeColor; j++; }
                else if (msg[j].Equals("420")) { Shapes[j].Image = newGUI_Taki.Properties.Resources.SuperTaki; j++; }
            }
        }

        private void butEnterRoomName_Click(object sender, EventArgs e)
        {
            string roomName = tbEnterChose.Text;
            byte[] buffer;
            string msg;
            int i, j;
            buffer = new ASCIIEncoding().GetBytes(String.Format("@{0}|{1}||\0", status_code.RM_JOIN_GAME, roomName));
            this.sock.Write(buffer, 0, buffer.Length);
            this.sock.Flush();
            buffer = new byte[status_code.MSG_LEN];
            int bytesRead = this.sock.Read(buffer, 0, status_code.MSG_LEN);
            msg = new ASCIIEncoding().GetString(buffer, 0, bytesRead);
            if (msg.Contains(status_code.PGM_ERR_ROOM_NOT_FOUND))
            {
                lblError.Visible = true;
                lblError.Text = "room not found";
            }
            else if (msg.Contains(status_code.PGM_ERR_ROOM_FULL))
            {
                lblError.Visible = true;
                lblError.Text = "room is full";
            }
            else if (msg.Contains(status_code.PGM_SCC_GAME_JOIN))
            {
                lblScreen.Text = "players:";
                i = msg.IndexOf('|');
                while (msg[i + 1] != '|')
                {
                    j = msg.Substring(i + 1).IndexOf("|") + i + 1;
                    lblScreen.Text = msg.Substring(i + 1, j - (i + 1));
                    i = j;
                }
                butEnterRoomName.Visible = false;
                tbEnterChose.Text = "";
                in_room(false);
            }
        }
    }
}