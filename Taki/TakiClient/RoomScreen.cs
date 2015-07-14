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
using System.Timers;

namespace newGUI_Taki
{
    public partial class RoomScreen : Form
    {

        private LobbyScreen parent;
        private NetworkStream sock;
        private Thread thread;
        private int numOfPlayers = 1;
        private Dictionary<string, Image> map;
        private List<string> playerCards;
        private List<PictureBox> Shapes;
        private bool shutdown;
        private bool is_admin;
        private string top_card;
        private string currPlayer;
        private Tuple<PictureBox, Image> blinkCard;
        private System.Timers.Timer blinkTimer;
        private PictureBox lastClick;

        public RoomScreen(LobbyScreen parent, NetworkStream sock, bool is_admin, string username)
        {
            this.parent = parent;
            this.sock = sock;
            this.is_admin = is_admin;
            this.shutdown = false;
            InitializeComponent();
            initMap();
            this.thread = new Thread(recvHandlingThread);
            this.thread.Start();
            this.Text = username;
            if (!is_admin)
            {
                butStartGame.Visible = false;
            }
        }

        private delegate void RoomScreenViewCallback(string msg);

        public void RoomScreenView(string msg)
        {
            if (this.InvokeRequired)
            {
                RoomScreenViewCallback d = new RoomScreenViewCallback(RoomScreenView);
                this.Invoke(d, new object[] { msg });
            }
            else
            {
                this.playerCards = new List<string>();
                this.Shapes = new List<PictureBox>();
                this.butEndTurn.Visible = true;
                this.ErrorLabel.Visible = true;
                this.butStartGame.Visible = false;
                this.pbTopCard.Visible = true;
                this.pbBankCards.Visible = true;
                this.CurrPlayerLabel.Visible = true;
                this.CardsPanel.Visible = true;
                this.pbBankCards.Visible = true;
                this.CurrPlayerLabel.Text = "";

                int i = msg.IndexOf("|");
                int j = msg.IndexOf("|", i + 1);
                this.numOfPlayers = int.Parse(msg.Substring(i + 1, j - i - 1));

                i = j;
                j = msg.IndexOf(",", i + 1);
                while (j != -1)
                {
                    this.playerCards.Add(msg.Substring(i + 1, j - i - 1));
                    i = j;
                    j = msg.IndexOf(",", i + 1);
                }
                j = msg.IndexOf("|", i + 1);
                this.playerCards.Add(msg.Substring(i + 1, j - i - 1));
                updateCards();

                i = j;
                j = msg.IndexOf("|", i + 1);
                this.top_card = msg.Substring(i + 1, j - i - 1);

                updateTopCard();

                i = j;
                j = msg.IndexOf("|", i + 1);
                this.currPlayer = msg.Substring(i + 1, j - i - 1);
                updateCurrPlayer();

                string txt = string.Format("[{0}] Admin started the game:\r\n", DateTime.Now.ToShortTimeString());
                updateChatBox(txt, Color.Blue);
            }
        }

        private void tbEndTurn_Click(object sender, EventArgs e)
        {
            byte[] buffer;
            buffer = new ASCIIEncoding().GetBytes(String.Format("@{0}||", status_code.GAM_SCC_TURN));
            this.sock.Write(buffer, 0, buffer.Length);
            this.sock.Flush();
            this.lastClick = null;
        }

        private void butStartGame_Click(object sender, EventArgs e)
        {
            byte[] buffer;
            buffer = new ASCIIEncoding().GetBytes(String.Format("@{0}||", status_code.RM_START_GAME));
            this.sock.Write(buffer, 0, buffer.Length);
            this.sock.Flush();
        }


        private void recvHandlingThread(object obj)
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
                    string txt = string.Format("[{0}] User joined: {1}\r\n", DateTime.Now.ToShortTimeString(),
                        msg.Substring(i + 1, j - i - 1));
                    updateChatBox(txt, Color.Green);
                }

                else if (msg.Contains(String.Format("@{0}|", status_code.PGM_CTR_REMOVE_USER)))
                {
                    int i = msg.IndexOf("|");
                    int j = msg.IndexOf("||");
                    string txt = string.Format("[{0}] User left: {1}\r\n", DateTime.Now.ToShortTimeString(),
                        msg.Substring(i + 1, j - i - 1));
                    updateChatBox(txt, Color.Red);
                }

                else if (msg.Contains(String.Format("@{0}", status_code.PGM_CTR_GAME_STARTED)))
                {
                    RoomScreenView(msg);
                }

                else if (msg.Contains(String.Format("@{0}|", status_code.PGM_CTR_ROOM_CLOSED)))
                {
                    shutdown = true;
                    exitRoom();
                }

                else if (msg.Contains(String.Format("@{0}|", status_code.PGM_SCC_GAME_LEAVE)))
                {
                    shutdown = true;
                }

                else if (msg.Contains(String.Format("@{0}|", status_code.PGM_SCC_GAME_CLOSE)))
                {
                    shutdown = true;
                }


                else if (msg.Contains(String.Format("@{0}|", status_code.CH_SEND)))
                {
                    int i = msg.IndexOf("|");
                    int j = msg.IndexOf("|", i + 1);
                    string sender = msg.Substring(i + 1, j - i - 1);
                    i = msg.IndexOf("||");
                    string chat = msg.Substring(j + 1, i - j - 1);
                    updateChatBox(string.Format("[{0}] {1}: {2}\r\n", DateTime.Now.ToShortTimeString(), sender, chat),
                        Color.Black);
                }

                else if (msg.Contains(String.Format("@{0}", status_code.GAM_SCC_DRAW)))
                {
                    updateDraw(msg);
                }

                else if (msg.Contains(String.Format("@{0}", status_code.GAM_SCC_TURN)))
                {
                    updateChatBox("Turn completed\r\n", Color.Black);
                    if (top_card[0].ToString() == status_code.CARD_CHANGE_COLOR ||
                        top_card[0].ToString() == status_code.CARD_SUPER_TAKI)
                    {
                        top_card = top_card[0] + " ";
                    }
                    this.playerCards.Remove(top_card);
                    updateCards();
                    updateTopCard();
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
                                updateChatBox(string.Format("[{0}] Color changed to: ", DateTime.Now.ToShortTimeString()),
                                    Color.Black);
                                if (this.top_card[1] == 'r')
                                {
                                    updateChatBox("RED\r\n", Color.Red);
                                }
                                if (this.top_card[1] == 'b')
                                {
                                    updateChatBox("BLUE\r\n", Color.Blue);
                                }
                                if (this.top_card[1] == 'y')
                                {
                                    updateChatBox("YELLOW\r\n", Color.Yellow);
                                }
                                if (this.top_card[1] == 'g')
                                {
                                    updateChatBox("GREEN\r\n", Color.Green);
                                }
                            }
                            this.top_card = this.top_card[0] + " ";
                        }
                        updateTopCard();
                    }
                }

                else if (msg.Contains(String.Format("@{0}", status_code.GAM_CTR_DRAW_CARDS)))
                {
                    int i = msg.IndexOf("|");
                    int j = msg.IndexOf("||");
                    updateChatBox(string.Format("[{0}] [{1}] drawed {2} cards\r\n", DateTime.Now.ToShortTimeString(),
                        this.CurrPlayerLabel.Text, msg.Substring(i + 1, j - i - 1)), Color.Black);
                }

                else if (msg.Contains(String.Format("@{0}", status_code.GAM_CTR_TURN_ENDED)))
                {
                    int i = msg.IndexOf("|");
                    int j = msg.IndexOf("||");
                    this.currPlayer = msg.Substring(i + 1, j - i - 1);
                    updateCurrPlayer();
                }

                else if (msg.Contains(String.Format("@{0}", status_code.GAM_CTR_GAME_ENDED)))
                {
                    int i = msg.IndexOf("|");
                    int j = msg.IndexOf("||");
                    updateErrorLabel("Winner: " + msg.Substring(i + 1, j - i - 1));
                }

                else if (msg.Contains(String.Format("@{0}", status_code.PGM_ERR_TOO_FEW_USERS)))
                {
                    updateErrorLabel("Not Enough Players");
                }

                else if (msg.Contains(String.Format("@{0}", status_code.GAM_ERR_ILLEGAL_CARD)))
                {
                    blinkBegin(this.lastClick);
                    this.lastClick = null;
                }

                else if (msg.Contains(String.Format("@{0}", status_code.GAM_ERROR_WRONG_DRAW)))
                {
                    blinkBegin(this.lastClick);
                    this.lastClick = null;
                }

                else if (msg.Contains(String.Format("@{0}", status_code.PGM_MER_ACCESS)))
                {
                    if (this.lastClick != null)
                    {
                        blinkBegin(this.lastClick);
                        this.lastClick = null;
                    }
                }

                else if (msg.Contains(String.Format("@{0}", status_code.PGM_MER_MESSAGE)))
                {
                    updateErrorLabel("Message compatibility");
                }
                msg = "";
            }
            this.thread.Abort();
        }

        private delegate void updateCardsCallback();

        private void updateCards()
        {
            if (this.ErrorLabel.InvokeRequired)
            {
                updateCardsCallback d = new updateCardsCallback(updateCards);
                this.Invoke(d, new object[] { });
            }
            else
            {
                this.Shapes.Clear();
                this.CardsPanel.Controls.Clear();
                int i = 0, x = 0;
                PictureBox currPB;
                foreach (string str in this.playerCards)
                {
                    currPB = new PictureBox();
                    currPB.Name = "ItemNum_" + i.ToString();
                    currPB.Location = new Point(x, 0);
                    currPB.Size = new Size(100, 100);
                    currPB.BackColor = Color.Yellow;
                    currPB.Click += new System.EventHandler(this.pbCard_Click);
                    currPB.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                    currPB.Visible = true;
                    currPB.Image = map[str];
                    this.CardsPanel.Controls.Add(currPB);
                    this.Shapes.Add(currPB);
                }
            }
        }

        private delegate void updateErrorLabelCallback(string msg);

        private void updateErrorLabel(string msg)
        {
            if (this.ErrorLabel.InvokeRequired)
            {
                updateErrorLabelCallback d = new updateErrorLabelCallback(updateErrorLabel);
                this.Invoke(d, new object[] { msg });
            }
            else
            {
                this.ErrorLabel.Text = msg;
            }
        }

        private delegate void updateTopCardCallback();

        private void updateTopCard()
        {
            if (this.ErrorLabel.InvokeRequired)
            {
                updateTopCardCallback d = new updateTopCardCallback(updateTopCard);
                this.Invoke(d, new object[] { });
            }
            else
            {
                pbTopCard.Image = this.map[this.top_card];
            }
        }

        private delegate void updateCurrPlayerCallback();

        private void updateCurrPlayer()
        {
            if (this.CurrPlayerLabel.InvokeRequired)
            {
                updateCurrPlayerCallback d = new updateCurrPlayerCallback(updateCurrPlayer);
                this.Invoke(d, new object[] { });
            }
            else
            {
                CurrPlayerLabel.Text = "Curr player: " + this.currPlayer;
            }
        }

        private void parseInfo(string msg)
        {
            this.playerCards = new List<string>(status_code.NUM_OF_CARDS);
            int i = msg.IndexOf("|");
            int j = msg.IndexOf("|", i + 1), k = 0;
            this.numOfPlayers = int.Parse(msg.Substring(i + 1, j - i - 1));
            i = j;
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
            //printCardsInPB();
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
            this.lastClick = p;
        }

        private void pbBankCards_Click(object sender, EventArgs e)
        {
            byte[] buffer;
            buffer = new ASCIIEncoding().GetBytes(String.Format("@{0}||", status_code.GM_DRAW));
            this.sock.Write(buffer, 0, buffer.Length);
            this.sock.Flush();
            this.lastClick = pbBankCards;
        }

        private delegate void updateStringCardsCallback(string msg);

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

        private delegate void updateChatBoxCallback(string msg, Color color);

        private void updateChatBox(string txt, Color color)
        {
            if (this.ChatShowBox.InvokeRequired)
            {
                updateChatBoxCallback d = new updateChatBoxCallback(updateChatBox);
                this.Invoke(d, new object[] { txt, color });
            }
            else
            {
                ChatShowBox.SelectionColor = color;
                ChatShowBox.AppendText(txt);
            }
        }

        private void ChatShowBox_TextChanged(object sender, EventArgs e)
        {
            ChatShowBox.SelectionStart = ChatShowBox.Text.Length;
            ChatShowBox.ScrollToCaret();
        }

        private void ChatSendBox_TextChanged(object sender, EventArgs e)
        {
            ChatSendBox.SelectionStart = ChatSendBox.Text.Length;
            ChatSendBox.ScrollToCaret();
        }

        private void ChatSendBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                this.SendChatBut.PerformClick();
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
                int i = msg.IndexOf("|");
                int j = msg.IndexOf(",");
                while (j != -1)
                {
                    this.playerCards.Add(msg.Substring(i + 1, j - i - 1));
                    i = j;
                    j = msg.IndexOf(",", i + 1);
                }
                j = msg.IndexOf("|", i + 1);
                this.playerCards.Add(msg.Substring(i + 1, j - i - 1));
                updateCards();
            }
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

        private void SendChatBut_Click(object sender, EventArgs e)
        {
            byte[] buffer;
            buffer = new ASCIIEncoding().GetBytes(String.Format("@{0}|{1}||", status_code.CH_SEND, ChatSendBox.Text));
            this.sock.Write(buffer, 0, buffer.Length);
            this.sock.Flush();
            updateChatBox(string.Format("[{0}] {1}\r\n", DateTime.Now.ToShortTimeString(), ChatSendBox.Text), Color.Black);
            ChatSendBox.Text = "";
        }

        private void butLeaveRoom_Click(object sender, EventArgs e)
        {
            byte[] buffer;
            if (!is_admin)
            {
                buffer = new ASCIIEncoding().GetBytes(String.Format("@{0}||", status_code.RM_LEAVE_GAME));
            }
            else
            {
                buffer = new ASCIIEncoding().GetBytes(String.Format("@{0}||", status_code.RM_CLOSE_GAME));
            }
            this.sock.Write(buffer, 0, buffer.Length);
            this.sock.Flush();
            exitRoom();
        }

        delegate void exitRoomCallback();

        private void exitRoom()
        {
            if (this.parent.InvokeRequired)
            {
                exitRoomCallback d = new exitRoomCallback(exitRoom);
                this.Invoke(d, new object[] { });
            }
            else
            {
                this.parent.showAndRefresh();
                this.Close();
            }
        }

        private void pbTopCard_Click(object sender, EventArgs e)
        {

        }


        private delegate void blinkBeginCallback(PictureBox blinkCard);

        private void blinkBegin(PictureBox blinkCard)
        {
            if (this.InvokeRequired)
            {
                blinkBeginCallback d = new blinkBeginCallback(blinkBegin);
                this.Invoke(d, new object[] { blinkCard });
            }
            else
            {
                Image img = blinkCard.Image;
                blinkCard.Image = newGUI_Taki.Properties.Resources.redBlink;
                this.blinkTimer = new System.Timers.Timer(200);
                this.blinkTimer.Enabled = true;
                this.blinkTimer.Elapsed += new ElapsedEventHandler(blinkEnd);
                this.blinkCard = new Tuple<PictureBox, Image>(blinkCard, img);
            }
        }

        private delegate void blinkEndCallback(object sender, ElapsedEventArgs e);

        private void blinkEnd(object sender, ElapsedEventArgs e)
        {
            if (this.InvokeRequired)
            {
                blinkEndCallback d = new blinkEndCallback(blinkEnd);
                this.Invoke(d, new object[] { sender, e } );
            }
            else
            {
                this.blinkTimer.Close();
                this.blinkCard.Item1.Image = this.blinkCard.Item2;
            }
        }

        private delegate void activateBlinkCallback(string blinkCard);
    }
}
