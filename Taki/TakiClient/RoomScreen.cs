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
using System.Threading;

namespace newGUI_Taki
{
    public partial class RoomScreen : Form
    {
        private Form parent;
        private NetworkStream sock;
        private Thread thread;
        private bool shutdown = false;
        private int numOfPlayers = 1;
        private Dictionary<string, Image> map;
        private List<string> playerCards;
        private PictureBox[] Shapes;
        private bool is_admin;
        private string top_card;
        public RoomScreen(Form parent, NetworkStream sock, bool is_admin, string username)
        {
            this.Icon = Properties.Resources.TakiIcon;
            this.parent = parent;
            this.sock = sock;
            this.is_admin = is_admin;
            InitializeComponent();
            initMap();
            this.thread = new Thread(recvHandlingThread);
            this.thread.Start();
            this.Text = username;
            if(!is_admin)
            {
                butStartGame.Visible = false;
            }
        }
        private void tbEndTurn_Click(object sender, EventArgs e)
        {
            byte[] buffer;
            buffer = new ASCIIEncoding().GetBytes(String.Format("@{0}||", status_code.GAM_SCC_TURN));
            this.sock.Write(buffer, 0, buffer.Length);
            this.sock.Flush();
        }
        private void butStartGame_Click(object sender, EventArgs e)
        {
            byte[] buffer;
            if (is_admin && numOfPlayers >= status_code.MIN_PLAYERS_FOR_GAME)
            {
                buffer = new ASCIIEncoding().GetBytes(String.Format("@{0}||\0", status_code.RM_START_GAME));//start game
                this.sock.Write(buffer, 0, buffer.Length);
                this.sock.Flush();
            }
            else if (numOfPlayers < status_code.MIN_PLAYERS_FOR_GAME)
            {
                ErrorLabel.Visible = true;
                ErrorLabel.Text = "Missing players";
            }
        }
        private void StopThreads()
        {
            shutdown = true;
        }
        private void recvHandlingThread(object obj)
        {
            byte[] buffer;
            while (!shutdown)
            {
                this.sock.Flush();
                buffer = new byte[status_code.MSG_LEN];
                updatErrorLabel("");
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
                    updateChatBox(string.Format("[{0}][{1}] {2}\r\n", DateTime.Now.ToShortTimeString(), sender, chat));
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

                else if (msg.Contains(String.Format("@{0}", status_code.GAM_SCC_TURN)))
                {
                    updateChatBox("Turn complete\n");
                    updateCards(this.top_card);
                }

                else if (msg.Contains(String.Format("@{0}", status_code.PGM_CTR_GAME_STARTED)))
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

                else if (msg.Contains(String.Format("@{0}", status_code.GAM_CTR_TURN_COMPLETE)))
                {
                    if (!(CurrPlayerLabel.Text == this.Text))
                    {
                        int i = msg.IndexOf("|");
                        int j = msg.IndexOf("||");
                        this.top_card = msg.Substring(i + 1, j - i - 1);
                        if (this.top_card.Contains(string.Format(status_code.CARD_CHANGE_COLOR)) || this.top_card.Contains(string.Format(status_code.CARD_SUPER_TAKI)))
                        {
                            if (this.top_card.Contains(string.Format(status_code.CARD_CHANGE_COLOR)))
                            {
                                if (this.top_card[1] == 'r')
                                {
                                    updateChatBox("color change to red");
                                }
                                if (this.top_card[1] == 'b')
                                {
                                    updateChatBox("color change to blue");
                                }
                                if (this.top_card[1] == 'y')
                                {
                                    updateChatBox("color change to yellow");
                                }
                                if (this.top_card[1] == 'g')
                                {
                                    updateChatBox("color change to green");
                                }
                            }
                            this.top_card = this.top_card[0] + " ";
                        }
                        updateTopCard(this.top_card);
                    }
                }

                else if (msg.Contains(String.Format("@{0}", status_code.GAM_CTR_DRAW_CARDS)))
                {
                    int i = msg.IndexOf("|");
                    int j = msg.IndexOf("||");
                    updateChatBox(this.CurrPlayerLabel.Text + "drawed " + msg.Substring(i + 1, j - i - 1) + "cards" + "\r\n");
                }

                else if (msg.Contains(String.Format("@{0}", status_code.GAM_CTR_TURN_ENDED)))
                {
                    int i = msg.IndexOf("|");
                    int j = msg.IndexOf("||");
                    updateCurrPlayer(msg.Substring(i + 1, j - i - 1));
                }

                else if (msg.Contains(String.Format("@{0}", status_code.GAM_CTR_GAME_ENDED)))
                {
                    int i = msg.IndexOf("|");
                    int j = msg.IndexOf("||");
                    updateCurrPlayer("Winner: " + msg.Substring(i + 1, j - i - 1));
                }

                else if (msg.Contains(String.Format("@{0}", status_code.GAM_ERR_ILLEGAL_CARD)))
                {
                    updatErrorLabel("Illegal card");
                }

                else if (msg.Contains(String.Format("@{0}", status_code.GAM_ERR_ILLEGAL_ORDER)))
                {
                    updatErrorLabel("Illegal order");
                }

                else if (msg.Contains(String.Format("@{0}", status_code.GAM_ERR_LAST_CARD)))
                {
                    updatErrorLabel("Cant end a turn with this card");
                }

                else if (msg.Contains(String.Format("@{0}", status_code.PGM_MER_ACCESS)))
                {
                    updatErrorLabel("No access");
                }

                else if (msg.Contains(String.Format("@{0}", status_code.PGM_MER_MESSAGE)))
                {
                    updatErrorLabel("Message compatibility");
                }
                msg = "";
            }
        }

        delegate void updateCardsCallback(string card);
        private void updateCards(string card)
        {
            if (this.ErrorLabel.InvokeRequired)
            {
                updateCardsCallback d = new updateCardsCallback(updateCards);
                this.Invoke(d, new object[] { card });
            }
            else
            {
                int x = 10, y = 250;
                pbTopCard.Image = this.map[card];
                for (int i = 0; i < this.playerCards.Count; i++)
                {
                    this.Shapes[i].Visible = false;
                }
                this.playerCards.Remove(card);
                this.Shapes = new PictureBox[this.playerCards.Count];
                for (int i = 0; i < this.playerCards.Count; i++)
                {
                    Shapes[i] = new PictureBox();
                    Shapes[i].Name = "ItemNum_" + i.ToString();
                    x += 50;
                    Shapes[i].Location = new Point(x, y);
                    Shapes[i].Size = new Size(100, 100);
                    Shapes[i].BackColor = Color.Yellow;
                    Shapes[i].SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                    this.Shapes[i].Click += new System.EventHandler(this.pbCard_Click);
                    this.Controls.Add(Shapes[i]);
                }
                for (int i = 0; i < this.playerCards.Count; i++)
                {
                    Shapes[i].Image = this.map[this.playerCards[i]];
                    Shapes[i].Visible = true;
                }
            }
        }
        delegate void updateErrorLabelCallback(string msg);
        private void updatErrorLabel(string msg)
        {
            if (this.ErrorLabel.InvokeRequired)
            {
                updateErrorLabelCallback d = new updateErrorLabelCallback(updatErrorLabel);
                this.Invoke(d, new object[] { msg });
            }
            else
            {
                ErrorLabel.Text = msg;
            }
        }

        delegate void updateTopCardCallback(string msg);
        private void updateTopCard(string top_card)
        {
            if (this.ErrorLabel.InvokeRequired)
            {
                updateTopCardCallback d = new updateTopCardCallback(updateTopCard);
                this.Invoke(d, new object[] { top_card });
            }
            else
            {
                pbTopCard.Image = this.map[top_card];
            }
        }
        delegate void updateCurrPlayerCallback(string player);
        private void updateCurrPlayer(string player)
        {
            if (this.CurrPlayerLabel.InvokeRequired)
            {
                updateCurrPlayerCallback d = new updateCurrPlayerCallback(updateCurrPlayer);
                this.Invoke(d, new object[] { player });
            }
            else
            {
                CurrPlayerLabel.Text = player;
            }
        }

        private void parseInfo(string msg)
        {
            this.playerCards = new List<string>(status_code.NUM_OF_CARDS);
            msg = msg.Substring(6);
            int i = msg.IndexOf('|');
            int j = 0, k = 0;
            while (k < status_code.NUM_OF_CARDS - 1)
            {
                msg.Substring(i + 1).Contains(',');
                j = msg.Substring(i + 1).IndexOf(",") + i + 1;
                this.playerCards.Add(msg.Substring(i + 1, j - (i + 1)));
                i = j;
                k++;
            }
            msg.Substring(i + 1).Contains(',');
            j = msg.Substring(i + 1).IndexOf("|") + i + 1;
            this.playerCards.Add(msg.Substring(i + 1, j - (i + 1)));
            i = j;
            k++;
            this.top_card = msg.Substring(i + 1, 2);
            printCardsInPB();
        }
        private void pbCard_Click(object sender, EventArgs e)
        {
            byte[] buffer;
            PictureBox p = (PictureBox)sender;
            string card = this.map.FirstOrDefault(x => x.Value == p.Image).Key;
            if (card[0].ToString() == status_code.CARD_CHANGE_COLOR || card[0].ToString() == status_code.CARD_SUPER_TAKI)
            {
                ChangeColor ChangeColorForm = new ChangeColor();
                if (ChangeColorForm.ShowDialog() == DialogResult.OK)
                {
                    string color = ChangeColorForm.returnVal;
                    card = card[0] + color;
                }
                else
                {
                    return;
                }
            }
            buffer = new ASCIIEncoding().GetBytes(String.Format("@{0}|{1}||", status_code.GM_PLAY, card));
            this.sock.Write(buffer, 0, buffer.Length);
            this.sock.Flush();
            if (card[0].ToString() == status_code.CARD_CHANGE_COLOR || card[0].ToString() == status_code.CARD_SUPER_TAKI)
            {
                card = card[0] + " ";
            }
            this.top_card = card;
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

        private void ChatSendBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                this.butSendChat.PerformClick();
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
                int i, j, x = 10, y = 250;
                for (i = 0; i < this.playerCards.Count; i++)
                {
                    this.Shapes[i].Visible = false;
                }

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
                        this.Shapes[i].Click += new System.EventHandler(this.pbCard_Click);
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
                    ErrorLabel.Visible = true;
                    ErrorLabel.Text = "Wrong draw";
                }
                else if (msg.Contains(String.Format("{0}", status_code.PGM_MER_ACCESS)))
                {
                    ErrorLabel.Visible = true;
                    ErrorLabel.Text = "Access error";
                }
            }
        }
        private void printCardsInPB()
        {
            butEndTurn.Visible = true;
            butSurrender.Visible = true;
            butLeaveRoom.Visible = false;
            butStartGame.Visible = false;
            pbTopCard.Visible = true;
            pbBankCards.Visible = true;
            int j, x = 10, y = 250;
            CurrPlayerLabel.Visible = true;
            pbBankCards.Visible = true;
            CurrPlayerLabel.Text = "";

            this.Shapes = new PictureBox[this.playerCards.Count];
            for (int i = 0; i < this.playerCards.Count; i++)
            {
                Shapes[i] = new PictureBox();
                Shapes[i].Name = "ItemNum_" + i.ToString();
                x += 50;
                Shapes[i].Location = new Point(x, y);
                Shapes[i].Size = new Size(100, 100);
                Shapes[i].BackColor = Color.Yellow;
                this.Shapes[i].Click += new System.EventHandler(this.pbCard_Click);
                Shapes[i].SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                Shapes[i].Visible = true;
                this.Controls.Add(Shapes[i]);
            }
            for (j = 0; j < this.playerCards.Count; j++)
            {
                Shapes[j].Image = map[this.playerCards[j]];
            }
            pbTopCard.Image = map[this.top_card];
        }
        private void initMap()
        {
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
        }
        private void butEndTurn_Click(object sender, EventArgs e)
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

        private void butLeaveRoom_Click(object sender, EventArgs e)
        {
            byte[] buffer;
            if (this.thread.IsAlive)
            {
                StopThreads();
            }
            if (!is_admin)
            {
                buffer = new ASCIIEncoding().GetBytes(String.Format("@{0}||\0", status_code.RM_LEAVE_GAME));
                this.sock.Write(buffer, 0, buffer.Length);
                this.parent.Show();
                this.Close();
            }
            else
            {
                buffer = new ASCIIEncoding().GetBytes(String.Format("@{0}||\0", status_code.RM_CLOSE_GAME));
                this.sock.Write(buffer, 0, buffer.Length);
                this.parent.Show();
                this.Close();
            }
        }

        private void butSurrender_Click(object sender, EventArgs e)
        {
            StopThreads();
            byte[] buffer;
            buffer = new ASCIIEncoding().GetBytes(String.Format("@{0}||\0", status_code.RM_LEAVE_GAME));
            this.sock.Write(buffer, 0, buffer.Length);
            for (int i = 0; i < this.playerCards.Count; i++)
            {
                this.Shapes[i].Visible = false;
            }
            this.thread.Abort();
            this.pbTopCard.Visible = false;
            this.pbBankCards.Visible = false;
            butSurrender.Visible = false;
            butEndTurn.Visible = false;
            ChatShowBox.Visible = false;
            ChatSendBox.Visible = false;
            butSendChat.Visible = false;
            this.parent.Show();
            this.Close();
        }
    }
}
