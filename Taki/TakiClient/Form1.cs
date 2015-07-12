using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Net;
using System.Net.Sockets;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace newGUI_Taki
{
    public partial class Form1 : Form
    {
        private NetworkStream sock;
        bool is_admin;
        bool is_register;
        private Thread myThread;
        private bool shutdown = false;
        private int numOfPlayers = 1;
        private Dictionary<string, Image> map;
        List<string> playerCards;
        PictureBox[] Shapes;
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
            dgvRoomList.Visible = true;
            butRefresh.Visible = true;
            butCreateRoom.Visible = true;
        }
        private void updateRoomList(string msg)
        {
            int i = msg.IndexOf('|'), j, num_players, row_index;
            string is_open, name, admin;
            dgvRoomList.Rows.Clear();
            while (msg[i + 1] != '|')
            {
                msg.Substring(i + 1).Contains('|');
                j = msg.Substring(i + 1).IndexOf("|") + i + 1;
                name = msg.Substring(i + 1, j - (i + 1));
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
                row_index = dgvRoomList.Rows.Add();
                dgvRoomList["name", row_index].Value = name;
                dgvRoomList["admin", row_index].Value = admin;
                dgvRoomList["players", row_index].Value = num_players.ToString();
                dgvRoomList["state", row_index].Value = is_open;
                i = j;
            }
        }
        private void butEnterDetails_Click(object sender, EventArgs e)
        {
            byte[] buffer;
            string username = tbUsername.Text;
            string password = tbPassword.Text;
            string recv = "";
            TcpClient client = new TcpClient();
            IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Parse("10.0.0.142"), 10113);
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
                    updateRoomList(recv);
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
                    updateRoomList(recv);
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
                this.Text = tbUsername.Text;
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
                if (this.myThread.IsAlive)
                {
                    StopThreads();
                }
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
            if (is_admin && option.Equals("2") && numOfPlayers >= status_code.MIN_PLAYERS_FOR_GAME)
            {
                buffer = new ASCIIEncoding().GetBytes(String.Format("@{0}||\0", status_code.RM_START_GAME));//start game
                this.sock.Write(buffer, 0, buffer.Length);
                this.sock.Flush();
            }
            else if (numOfPlayers < status_code.MIN_PLAYERS_FOR_GAME)
            {
                lblError.Visible = true;
                lblError.Text = "missing players";
                tbEnterChose.Text = "";
            }
        }
        private void in_room(bool admin)
        {
            this.myThread = new Thread(secThread);
            myThread.Start();
            is_admin = admin;
            string str = "";
            str += "1. leave room\n";
            if (is_admin)
            {
                str += "2. start game\n";
            }
            butEnterChoseLobby.Visible = false;
            dgvRoomList.Visible = false;
            lblScreen.Visible = true;
            lblScreen.Text = str + "\nEnter your chose:";
            tbEnterChose.Text = "";
            butEnterChoseInRoom.Visible = true;
            ChatShowBox.Visible = true;
            ChatSendBox.Visible = true;
            butSendChat.Visible = true;
            butRefresh.Visible = false;
            butCreateRoom.Visible = false;
        }
        private void StopThreads()
        {
            shutdown = true;
        }
        private void secThread(object obj)
        {
            byte[] buffer;
            while (!shutdown)
            {
                this.sock.Flush();
                buffer = new byte[status_code.MSG_LEN];
                int bytesRead = this.sock.Read(buffer, 0, status_code.MSG_LEN);
                string msg = new ASCIIEncoding().GetString(buffer, 0, bytesRead);
                this.sock.Flush();
                if (msg.Contains(String.Format("@{0}|", status_code.PGM_CTR_NEW_USER)))
                {
                    int i = msg.IndexOf("|");
                    int j = msg.IndexOf("||");
                    this.numOfPlayers++;
                    updateChatBox("User joined: " + msg.Substring(i + 1, j - i - 1) + "\r\n");
                }
                else if (msg.Contains(String.Format("@{0}|", status_code.PGM_CTR_REMOVE_USER)))
                {
                    int i = msg.IndexOf("|");
                    int j = msg.IndexOf("||");
                    this.numOfPlayers--;
                    updateChatBox("User left: " + msg.Substring(i + 1, j - i - 1) + "\r\n");
                }
                else if (msg.Contains(String.Format("@{0}|", status_code.CH_SEND)))
                {
                    int i = msg.IndexOf("|");
                    int j = msg.IndexOf("|", i + 1);
                    string sender = msg.Substring(i + 1, j - i - 1);
                    i = msg.IndexOf("||");
                    string chat = msg.Substring(j + 1, i - j - 1);
                    updateChatBox(sender + ": " + chat + "\r\n");
                }
                if (msg.Contains(String.Format("@{0}", status_code.PGM_CTR_GAME_STARTED)))
                {
                    updateStringCards(msg);
                    int i = 0, j;
                    for (j = 0; j < 4; ++j)
                    {
                        i = msg.IndexOf("|", i + 1);
                    }
                    j = msg.IndexOf("|", i + 1);
                    string player = msg.Substring(i + 1, j - i - 1);
                    updateCurrPlayer(player);
                }
                else if (msg.Contains(String.Format("@{0}", status_code.GAM_SCC_DRAW)))
                {
                    updateDraw(msg);
                }
                else if (msg.Contains(String.Format("@{0}", status_code.GAM_CTR_TURN_COMPLETE)))
                {
                    int i = msg.IndexOf("|");
                    int j = msg.IndexOf("||");
                    string player = msg.Substring(i + 1, j - i - 1);
                    updateCurrPlayer(player);
                }
                msg = "";
            }
        }

        delegate void updateCurrPlayerCallback(string player);
        private void updateCurrPlayer(string player)
        {
            if (this.lblScreen.InvokeRequired)
            {
                updateCurrPlayerCallback d = new updateCurrPlayerCallback(updateCurrPlayer);
                this.Invoke(d, new object[] { player });
            }
            else
            {
                lblScreen.Text = "Current player: " + player;
            }
        }

        private void parseInfo(string msg)
        {
            string[] s = new string[status_code.NUM_OF_CARDS + 1];
            msg = msg.Substring(6);
            int i = msg.IndexOf('|');
            int j = 0, k = 0;
            while (k < status_code.NUM_OF_CARDS - 1)
            {
                msg.Substring(i + 1).Contains(',');
                j = msg.Substring(i + 1).IndexOf(",") + i + 1;
                s[k] = msg.Substring(i + 1, j - (i + 1));
                i = j;
                k++;
            }
            msg.Substring(i + 1).Contains(',');
            j = msg.Substring(i + 1).IndexOf("|") + i + 1;
            s[k] = msg.Substring(i + 1, j - (i + 1));
            i = j;
            k++;
            s[k] = msg.Substring(i + 1, 2);
            printCardsInPB(s);
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

        private void pbTopCard_Click(object sender, EventArgs e)
        {

        }

        private void pbBankCards_Click(object sender, EventArgs e)
        {
            byte[] buffer;
            buffer = new ASCIIEncoding().GetBytes(String.Format("@{0}||", status_code.GM_DRAW));
            this.sock.Write(buffer, 0, buffer.Length);
            this.sock.Flush();
        }
        delegate void updateStringCardsCallback(string msg);
        private void updateStringCards(string msg)
        {
            if (this.InvokeRequired)
            {
                updateStringCardsCallback d = new updateStringCardsCallback(updateStringCards);
                this.Invoke(d, new object[] { msg });
            }
            else
            {
                parseInfo(msg);
            }
        }
        delegate void updateChatBoxCallback(string msg);

        private void updateChatBox(string msg)
        {
            if (this.ChatShowBox.InvokeRequired)
            {
                updateChatBoxCallback d = new updateChatBoxCallback(updateChatBox);
                this.Invoke(d, new object[] { msg });
            }
            else
            {
                ChatShowBox.Text += msg;
            }
        }

        delegate void updateDrawCallback(string msg);
        private void updateDraw(string msg)
        {
            if (this.InvokeRequired)
            {
                updateDrawCallback d = new updateDrawCallback(updateDraw);
                this.Invoke(d, new object[] { msg });
            }
            else
            {
                List<string> drawn_cards = new List<string>();
                int i, j, x = 50, y = 250;
                if (msg.Contains(String.Format("{0}", status_code.GAM_SCC_DRAW)))
                {
                    i = msg.IndexOf("|");
                    j = msg.IndexOf(',', i + 1);
                    while (j != -1)
                    {
                        drawn_cards.Add(msg[i + 1].ToString() + msg[i + 2].ToString());
                        i = j;
                        j = msg.IndexOf(',', i + 1);
                    }
                    drawn_cards.Add(msg[i + 1].ToString() + msg[i + 2].ToString());
                    for (int k = 0; k < drawn_cards.Count; k++)
                    {
                        this.playerCards.Add(drawn_cards[k]);
                    }

                    this.Shapes = new PictureBox[this.playerCards.Count];
                    for (i = 0; i < this.playerCards.Count; i++)
                    {
                        Shapes[i] = new PictureBox();
                        Shapes[i].Name = "ItemNum_" + i.ToString();
                        x += 50;
                        Shapes[i].Location = new Point(x, y);
                        Shapes[i].Size = new Size(100, 100);
                        Shapes[i].BackColor = Color.Yellow;

                        Shapes[i].SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                        Shapes[i].Visible = true;

                        this.Controls.Add(Shapes[i]);
                    }
                    for (i = 0; i < this.playerCards.Count; i++)
                    {
                        Shapes[i].Image = this.map[this.playerCards[i]];
                    }
                }
                else if (msg.Contains(String.Format("{0}", status_code.GAM_ERROR_WRONG_DRAW)))
                {
                    lblError.Visible = true;
                    lblError.Text = "wrong draw";
                }
                else if (msg.Contains(String.Format("{0}", status_code.PGM_MER_ACCESS)))
                {
                    lblError.Visible = true;
                    lblError.Text = "access error";
                }
            }
        }
        private void printCardsInPB(string[] msg)
        {
            pbTopCard.Visible = true;
            pbBankCards.Visible = true;
            int j, x = 50, y = 250;
            tbEnterChose.Visible = false;
            tbEndTurn.Visible = true;
            lblScreen.Visible = true;
            pbBankCards.Visible = true;
            lblScreen.Text = "";
            butEnterChoseInRoom.Visible = false;
            this.playerCards = new List<string>();
            this.Shapes = new PictureBox[status_code.NUM_OF_CARDS];
            for (int i = 0; i < status_code.NUM_OF_CARDS; i++)
            {
                Shapes[i] = new PictureBox();
                Shapes[i].Name = "ItemNum_" + i.ToString();
                x += 50;
                Shapes[i].Location = new Point(x, y);
                Shapes[i].Size = new Size(100, 100);
                Shapes[i].BackColor = Color.Yellow;

                Shapes[i].SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                Shapes[i].Visible = true;
                this.Controls.Add(Shapes[i]);
            }

            map = new Dictionary<string, Image>(53);
            map["1b"] = newGUI_Taki.Properties.Resources._1_B;
            map["1g"] = newGUI_Taki.Properties.Resources._1_G;
            map["1r"] = newGUI_Taki.Properties.Resources._1_R;
            map["1y"] = newGUI_Taki.Properties.Resources._1_Y;
            map["3b"] = newGUI_Taki.Properties.Resources._3_B;
            map["3g"] = newGUI_Taki.Properties.Resources._3_G;
            map["3r"] = newGUI_Taki.Properties.Resources._3_R;
            map["3y"] = newGUI_Taki.Properties.Resources._3_Y;
            map["4b"] = newGUI_Taki.Properties.Resources._4_B;
            map["4g"] = newGUI_Taki.Properties.Resources._4_G;
            map["4r"] = newGUI_Taki.Properties.Resources._4_R;
            map["4y"] = newGUI_Taki.Properties.Resources._4_Y;
            map["5b"] = newGUI_Taki.Properties.Resources._5_B;
            map["5g"] = newGUI_Taki.Properties.Resources._5_G;
            map["5r"] = newGUI_Taki.Properties.Resources._5_R;
            map["5y"] = newGUI_Taki.Properties.Resources._5_Y;
            map["6b"] = newGUI_Taki.Properties.Resources._6_B;
            map["6g"] = newGUI_Taki.Properties.Resources._6_G;
            map["6r"] = newGUI_Taki.Properties.Resources._6_R;
            map["6y"] = newGUI_Taki.Properties.Resources._6_Y;
            map["7b"] = newGUI_Taki.Properties.Resources._7_B;
            map["7g"] = newGUI_Taki.Properties.Resources._7_G;
            map["7r"] = newGUI_Taki.Properties.Resources._7_R;
            map["7y"] = newGUI_Taki.Properties.Resources._7_Y;
            map["8b"] = newGUI_Taki.Properties.Resources._8_B;
            map["8g"] = newGUI_Taki.Properties.Resources._8_G;
            map["8r"] = newGUI_Taki.Properties.Resources._8_R;
            map["8y"] = newGUI_Taki.Properties.Resources._8_Y;
            map["9b"] = newGUI_Taki.Properties.Resources._9_B;
            map["9g"] = newGUI_Taki.Properties.Resources._9_G;
            map["9r"] = newGUI_Taki.Properties.Resources._9_R;
            map["9y"] = newGUI_Taki.Properties.Resources._9_Y;
            map["+b"] = newGUI_Taki.Properties.Resources.Plus_B;
            map["+g"] = newGUI_Taki.Properties.Resources.Plus_G;
            map["+r"] = newGUI_Taki.Properties.Resources.Plus_R;
            map["+y"] = newGUI_Taki.Properties.Resources.Plus_Y;
            map["!b"] = newGUI_Taki.Properties.Resources.Stop_B;
            map["!g"] = newGUI_Taki.Properties.Resources.Stop_G;
            map["!r"] = newGUI_Taki.Properties.Resources.Stop_R;
            map["!y"] = newGUI_Taki.Properties.Resources.Stop_Y;
            map["<b"] = newGUI_Taki.Properties.Resources.ChangeDirect_B;
            map["<g"] = newGUI_Taki.Properties.Resources.ChangeDirect_G;
            map["<r"] = newGUI_Taki.Properties.Resources.ChangeDirect_R;
            map["<y"] = newGUI_Taki.Properties.Resources.ChangeDirect_Y;
            map["$b"] = newGUI_Taki.Properties.Resources.Plus2_B;
            map["$g"] = newGUI_Taki.Properties.Resources.Plus2_G;
            map["$r"] = newGUI_Taki.Properties.Resources.Plus2_R;
            map["$y"] = newGUI_Taki.Properties.Resources.Plus2_Y;
            map["^b"] = newGUI_Taki.Properties.Resources.Taki_B;
            map["^g"] = newGUI_Taki.Properties.Resources.Taki_G;
            map["^r"] = newGUI_Taki.Properties.Resources.Taki_R;
            map["^y"] = newGUI_Taki.Properties.Resources.Taki_Y;
            map["% "] = newGUI_Taki.Properties.Resources.ChangeColor;
            map["* "] = newGUI_Taki.Properties.Resources.SuperTaki;

            for (j = 0; j < status_code.NUM_OF_CARDS; j++)
            {
                Shapes[j].Image = map[msg[j]];
                this.playerCards.Add(msg[j]);
            }
            pbTopCard.Image = map[msg[j]];
        }

        private void tbEndTurn_Click(object sender, EventArgs e)
        {
            byte[] buffer;
            buffer = new ASCIIEncoding().GetBytes(String.Format("@{0}||", status_code.GAM_SCC_TURN));
            this.sock.Write(buffer, 0, buffer.Length);
            this.sock.Flush();
        }

        private void butSendChat_Click(object sender, EventArgs e)
        {
            byte[] buffer;
            buffer = new ASCIIEncoding().GetBytes(String.Format("@{0}|{1}||", status_code.CH_SEND, ChatSendBox.Text));
            this.sock.Write(buffer, 0, buffer.Length);
            this.sock.Flush();
            updateChatBox(ChatSendBox.Text + "\r\n");
            ChatSendBox.Text = "";
        }

        private void joinRoom_click(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView sender1 = (DataGridView)sender;
            byte[] buffer;
            buffer = new ASCIIEncoding().GetBytes(String.Format("@{0}|{1}||", status_code.RM_JOIN_GAME,
                sender1["admin", e.RowIndex].Value.ToString()));
            this.sock.Write(buffer, 0, buffer.Length);
            this.sock.Flush();
            buffer = new byte[status_code.MSG_LEN];
            int bytesRead = this.sock.Read(buffer, 0, status_code.MSG_LEN);
            string msg = new ASCIIEncoding().GetString(buffer, 0, bytesRead);
            if (msg.Contains(String.Format("@{0}|", status_code.PGM_SCC_GAME_JOIN)))
            {
                in_room(false);
            }
        }

        private void butCreateRoom_Click(object sender, EventArgs e)
        {
            byte[] buffer;
            string name = Microsoft.VisualBasic.Interaction.InputBox("Enter room name", "", "");
            buffer = new ASCIIEncoding().GetBytes(String.Format("@{0}|{1}||", status_code.RM_CREATE_GAME, name));
            this.sock.Write(buffer, 0, buffer.Length);
            this.sock.Flush();
            buffer = new byte[status_code.MSG_LEN];
            int bytesRead = this.sock.Read(buffer, 0, status_code.MSG_LEN);
            string msg = new ASCIIEncoding().GetString(buffer, 0, bytesRead);
            if (msg.Contains(String.Format("@{0}|", status_code.PGM_SCC_GAME_CREATED)))
            {
                in_room(true);
            }
            else if (msg.Contains(String.Format("@{0}|", status_code.PGM_ERR_INFO_TOO_LONG)))
            {
                MessageBox.Show("Name too long", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void butRefresh_Click(object sender, EventArgs e)
        {
            byte[] buffer;
            buffer = new ASCIIEncoding().GetBytes(String.Format("@{0}||", status_code.RM_ROOM_LIST));
            this.sock.Write(buffer, 0, buffer.Length);
            this.sock.Flush();
            buffer = new byte[status_code.MSG_LEN];
            int bytesRead = this.sock.Read(buffer, 0, status_code.MSG_LEN);
            string msg = new ASCIIEncoding().GetString(buffer, 0, bytesRead);
            if (msg.Contains(String.Format("@{0}|", status_code.PGM_CTR_ROOM_LIST)))
            {
                updateRoomList(msg);
            }
        }
    }
}