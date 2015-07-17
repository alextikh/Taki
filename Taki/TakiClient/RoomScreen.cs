using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
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
        private Tuple<object, Image> blinkObj;
        private System.Timers.Timer blinkTimer;
        private System.Timers.Timer roomScreenTimer;
        private object lastClick;
        private List<EnemyPanel> enemyPanels;
        private List<Label> names;
        private List<Label> cardsNum;

        private int CARD_WIDTH = 100;
        private int CARD_HEIGHT = 100;
        private int PANEL_DISTANCE = 50;
        private int CARD_DISTANCE = 30;
        private int CARDS_IN_HORIZONTAL_PANEL = 9;
        private int CARDS_IN_VERTICAL_PANEL = 3;

        public RoomScreen(LobbyScreen parent, NetworkStream sock, bool is_admin, string username, List<string> players)
        {
            InitializeComponent();

            this.parent = parent;
            this.sock = sock;
            this.is_admin = is_admin;
            this.shutdown = false;
            this.Name = username;

            initMap();

            this.names = new List<Label>();
            this.names.Add(this.nameLabelTop);
            this.names.Add(this.nameLabelRight);
            this.names.Add(this.nameLabelLeft);

            this.cardsNum = new List<Label>();
            this.cardsNum.Add(this.cardsNumLabelTop);
            this.cardsNum.Add(this.cardsNumLabelRight);
            this.cardsNum.Add(this.cardsNumLabelLeft);

            this.enemyPanels = new List<EnemyPanel>();
            this.enemyPanels.Add(new EnemyPanel(this.enemyPanelTop, "", 0, this.nameLabelTop, this.cardsNumLabelTop));
            this.enemyPanels.Add(new EnemyPanel(this.enemyPanelRight, "", 0, this.nameLabelRight, this.cardsNumLabelRight));
            this.enemyPanels.Add(new EnemyPanel(this.enemyPanelLeft, "", 0, this.nameLabelLeft, this.cardsNumLabelLeft));

            this.thread = new Thread(recvHandlingThread);
            this.thread.Start();
            this.Text = username;
            if (!is_admin)
            {
                butStartGame.Visible = false;
            }

            if (players != null)
            {
                this.numOfPlayers = players.Count + 1;
                int index = 1;
                foreach (string str in players)
                {
                    addEnemyPanel(index, str);
                    ++index;
                }
            }

            setControls();
        }

        private void setControls()
        {
            Rectangle screenSize = Screen.PrimaryScreen.WorkingArea;

            this.CardsPanel.Location = new Point(this.CARD_HEIGHT + this.CARD_DISTANCE + this.PANEL_DISTANCE,
                screenSize.Height - this.CARD_HEIGHT - this.PANEL_DISTANCE);
            this.CardsPanel.Size = new Size(CARD_WIDTH * CARDS_IN_HORIZONTAL_PANEL, CARD_HEIGHT + CARD_DISTANCE);

            this.enemyPanelTop.Location = new Point(this.CARD_HEIGHT + this.PANEL_DISTANCE, this.PANEL_DISTANCE);
            this.enemyPanelTop.Size = new Size(CARD_WIDTH * CARDS_IN_HORIZONTAL_PANEL, CARD_HEIGHT + CARD_DISTANCE);

            this.nameLabelTop.Location = new Point(this.enemyPanelTop.Location.X + (int)(this.enemyPanelTop.Width * (3 / 8.0)),
                this.enemyPanelTop.Location.Y + this.enemyPanelTop.Height + 20);
            this.cardsNumLabelTop.Location = new Point(this.nameLabelTop.Location.X +
                (int)(this.enemyPanelTop.Width / 4.0), this.nameLabelTop.Location.Y);

            this.enemyPanelRight.Location = new Point(screenSize.Width - this.CARD_HEIGHT - this.PANEL_DISTANCE -
                this.ChatShowBox.Width, this.CARD_HEIGHT + this.PANEL_DISTANCE + this.CARD_DISTANCE);
            this.enemyPanelRight.Size = new Size(this.CARD_HEIGHT + CARD_DISTANCE,
                CARD_WIDTH * CARDS_IN_VERTICAL_PANEL);

            this.nameLabelRight.Location = new Point(this.enemyPanelRight.Location.X - (int)(this.enemyPanelRight.Width *
                (3 / 8.0)), this.enemyPanelRight.Location.Y + this.enemyPanelRight.Height + 10);
            this.cardsNumLabelRight.Location = new Point(this.enemyPanelRight.Location.X - 50,
                this.enemyPanelRight.Location.Y + (int)(this.enemyPanelRight.Height / 4.0));

            this.enemyPanelLeft.Location = new Point(this.CARD_DISTANCE + PANEL_DISTANCE, this.enemyPanelRight.Location.Y);
            this.enemyPanelLeft.Size = new Size(this.CARD_HEIGHT + CARD_DISTANCE,
                CARD_WIDTH * CARDS_IN_VERTICAL_PANEL);

            this.nameLabelLeft.Location = new Point(this.enemyPanelLeft.Location.X, this.nameLabelRight.Location.Y);
            this.cardsNumLabelLeft.Location = new Point(this.enemyPanelLeft.Location.X + this.enemyPanelRight.Width + 20,
                this.cardsNumLabelRight.Location.Y);
        }

        private delegate void playScreenViewCallback(string msg);

        private delegate void RoomScreenViewCallback();
        public void playScreenView(string msg)
        {
            if (this.InvokeRequired)
            {
                playScreenViewCallback d = new playScreenViewCallback(playScreenView);
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
                this.CardsPanel.Visible = true;
                this.pbBankCards.Visible = true;

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
                System.Media.SoundPlayer player = new System.Media.SoundPlayer(newGUI_Taki.Properties.Resources.startGame);
                player.Play();
                i = j;
                j = msg.IndexOf("|", i + 1);
                this.top_card = msg.Substring(i + 1, j - i - 1);

                updateTopCard();

                i = j;
                j = msg.IndexOf("|", i + 1);
                this.currPlayer = msg.Substring(i + 1, j - i - 1);

                string txt = string.Format("[{0}] Admin started the game:\r\n", DateTime.Now.ToShortTimeString());
                updateChatBox(txt, Color.Blue);

                for (int index = 1; index < this.numOfPlayers; ++index)
                {
                    string name = "";
                    switch (index)
                    {
                        case 1:
                            name = "enemyPanelTop";
                            break;
                        case 2:
                            name = "enemyPanelRight";
                            break;
                        case 3:
                            name = "enemyPanelLeft";
                            break;
                    }
                    foreach (EnemyPanel p in this.enemyPanels)
                    {
                        if (p.Panel.Name == name)
                        {
                            fillEnemyPanel(p, this.playerCards.Count);
                            p.Panel.Visible = true;
                            p.NameLabel.Visible = true;
                            p.NumCardsLabel.Visible = true;
                            break;
                        }
                    }
                }

                updateCurrPlayer();
            }
        }
        private void RoomScreenView()
        {
            if (this.InvokeRequired)
            {
                RoomScreenViewCallback d = new RoomScreenViewCallback(RoomScreenView);
                this.Invoke(d, new object[] { });
            }
            else
            {
                this.butEndTurn.Visible = false;
                if (is_admin)
                {
                    this.butStartGame.Visible = true;
                }
                this.pbTopCard.Visible = false;
                this.pbBankCards.Visible = false;
                this.CardsPanel.Visible = false;
                this.pbBankCards.Visible = false;

                int index = 1;
                foreach (EnemyPanel p in this.enemyPanels)
                {
                    p.Panel.Visible = false;
                    p.Panel.Controls.Clear();
                    p.NameLabel.Visible = false;
                    p.NumCardsLabel.Visible = false;
                    ++index;
                }
            }
        }


        private void tbEndTurn_Click(object sender, EventArgs e)
        {
            if (this.currPlayer == this.Name)
            {
                byte[] buffer;
                buffer = new ASCIIEncoding().GetBytes(String.Format("@{0}||", status_code.GAM_SCC_TURN));
                this.sock.Write(buffer, 0, buffer.Length);
                this.sock.Flush();
                this.lastClick = null;
            }
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
                    string name = msg.Substring(i + 1, j - i - 1);
                    string txt = string.Format("[{0}] User joined: {1}\r\n", DateTime.Now.ToShortTimeString(), name);
                    updateChatBox(txt, Color.Green);
                    addEnemyPanel(this.numOfPlayers, name);
                    ++this.numOfPlayers;
                }

                else if (msg.Contains(String.Format("@{0}|", status_code.PGM_CTR_REMOVE_USER)))
                {
                    int i = msg.IndexOf("|");
                    int j = msg.IndexOf("||");
                    string txt = string.Format("[{0}] User left: {1}\r\n", DateTime.Now.ToShortTimeString(),
                        msg.Substring(i + 1, j - i - 1));
                    updateChatBox(txt, Color.Red);
                    --this.numOfPlayers;
                    removeEnemyPanel(this.numOfPlayers);
                }

                else if (msg.Contains(String.Format("@{0}", status_code.PGM_CTR_GAME_STARTED)))
                {
                    playScreenView(msg);
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
                    if (this.currPlayer != this.Text)
                    {
                        int i = msg.IndexOf("|");
                        int j = msg.IndexOf("||");
                        this.top_card = msg.Substring(i + 1, j - i - 1);
                        if (this.top_card.Contains(string.Format(status_code.CARD_CHANGE_COLOR)) ||
                            this.top_card.Contains(string.Format(status_code.CARD_SUPER_TAKI)))
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
                        foreach (EnemyPanel p in this.enemyPanels)
                        {
                            if (p.Player == currPlayer)
                            {
                                fillEnemyPanel(p, p.NumCards - 1);
                                break;
                            }
                        }
                    }
                }

                else if (msg.Contains(String.Format("@{0}", status_code.GAM_CTR_DRAW_CARDS)))
                {
                    int i = msg.IndexOf("|");
                    int j = msg.IndexOf("||");
                    string numDraw = msg.Substring(i + 1, j - i - 1);
                    updateChatBox(string.Format("[{0}] [{1}] drawed {2} cards\r\n", DateTime.Now.ToShortTimeString(),
                        this.currPlayer, numDraw), Color.Black);
                    foreach (EnemyPanel p in this.enemyPanels)
                    {
                        if (p.Player == currPlayer)
                        {
                            fillEnemyPanel(p, p.NumCards + int.Parse(numDraw));
                            break;
                        }
                    }
                }

                else if (msg.Contains(String.Format("@{0}", status_code.GAM_CTR_TURN_ENDED)))
                {
                    int i = msg.IndexOf("|");
                    int j = msg.IndexOf("||");
                    this.currPlayer = msg.Substring(i + 1, j - i - 1);
                    updateCurrPlayer();
                    //RoomScreenView();   
                }

                else if (msg.Contains(String.Format("@{0}", status_code.GAM_CTR_GAME_ENDED)))
                {
                    int i = msg.IndexOf("|");
                    int j = msg.IndexOf("||");
                    string winner = msg.Substring(i + 1, j - i - 1);
                    if (winner == this.Name)
                    {
                        updateCurrPlayerLabel("You win!");
                        System.Media.SoundPlayer player = new System.Media.SoundPlayer(newGUI_Taki.Properties.Resources.winner);
                        player.Play();
                    }
                    else
                    {
                        updateCurrPlayerLabel(string.Format("Winner: {0}", msg.Substring(i + 1, j - i - 1)));
                        System.Media.SoundPlayer player = new System.Media.SoundPlayer(newGUI_Taki.Properties.Resources.loser);
                        player.Play();
                    }
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
                    if (this.lastClick == null)
                    {
                        updateErrorLabel("Message compatibility");
                    }
                    else
                    {
                        blinkBegin(this.lastClick);
                        this.lastClick = null;
                    }
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
                int i = 0;
                PictureBox currPB;
                foreach (string str in this.playerCards)
                {
                    currPB = new PictureBox();
                    currPB.Name = "ItemNum_" + i.ToString();
                    currPB.Size = new Size(this.CARD_WIDTH, this.CARD_HEIGHT);
                    currPB.BackColor = Color.Yellow;
                    currPB.Click += new System.EventHandler(this.pbCard_Click);
                    currPB.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                    currPB.Visible = true;
                    currPB.Image = map[str];
                    this.CardsPanel.Controls.Add(currPB);
                    this.Shapes.Add(currPB);
                }
                System.Media.SoundPlayer player = new System.Media.SoundPlayer(newGUI_Taki.Properties.Resources.draw);
                player.Play();
                player.Dispose();
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
                if (this.currPlayer == this.Name)
                {
                    this.CurrPlayerLabel.Visible = true;
                }
                else
                {
                    this.CurrPlayerLabel.Visible = false;
                }
                foreach (EnemyPanel p in this.enemyPanels)
                {
                    if (p.Player == this.currPlayer)
                    {
                        p.NameLabel.ForeColor = System.Drawing.Color.Green;
                    }
                    else
                    {
                        p.NameLabel.ForeColor = System.Drawing.Color.Black;
                    }
                }
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
            if (card != null)
            {
                if (currPlayer == this.Text)
                {
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
            this.lastClick = butEndTurn;
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

        private delegate void blinkBeginCallback(object blinkObject);

        private void blinkBegin(object blinkObject)
        {
            if (this.InvokeRequired)
            {
                blinkBeginCallback d = new blinkBeginCallback(blinkBegin);
                this.Invoke(d, new object[] { blinkObject });
            }
            else
            {
                if (blinkObject is PictureBox)
                {
                    PictureBox blinkCard = (PictureBox)blinkObject;
                    Image img = blinkCard.Image;
                    blinkCard.Image = newGUI_Taki.Properties.Resources.redBlink;
                    this.blinkTimer = new System.Timers.Timer(200);
                    this.blinkTimer.Enabled = true;
                    this.blinkTimer.Elapsed += new ElapsedEventHandler(blinkEnd);
                    this.blinkObj = new Tuple<object, Image>(blinkCard, img);
                    System.Media.SoundPlayer player = new System.Media.SoundPlayer(newGUI_Taki.Properties.Resources.error);
                    player.Play();
                    player.Dispose();
                }
                else if (blinkObject is Button)
                {
                    Button blinkButton = (Button)blinkObject;
                    Image img = blinkButton.Image;
                    blinkButton.Image = newGUI_Taki.Properties.Resources.redBlink;
                    this.blinkTimer = new System.Timers.Timer(200);
                    this.blinkTimer.Enabled = true;
                    this.blinkTimer.Elapsed += new ElapsedEventHandler(blinkEnd);
                    this.blinkObj = new Tuple<object, Image>(blinkButton, img);
                    System.Media.SoundPlayer player = new System.Media.SoundPlayer(newGUI_Taki.Properties.Resources.error);
                    player.Play();
                    player.Dispose();
                }
            }
        }

        private delegate void blinkEndCallback(object sender, ElapsedEventArgs e);

        private void blinkEnd(object sender, ElapsedEventArgs e)
        {
            if (this.InvokeRequired)
            {
                blinkEndCallback d = new blinkEndCallback(blinkEnd);
                this.Invoke(d, new object[] { sender, e });
            }
            else
            {
                this.blinkTimer.Close();
                if (this.blinkObj.Item1 is PictureBox)
                {
                    PictureBox blinkCard = (PictureBox)this.blinkObj.Item1;
                    blinkCard.Image = this.blinkObj.Item2;
                }
                else if (this.blinkObj.Item1 is Button)
                {
                    this.butEndTurn = (Button)this.blinkObj.Item1;
                    this.butEndTurn.Image = newGUI_Taki.Properties.Resources.lightpink;
                }
            }
        }

        private delegate void addEnemyPanelCallback(int index, string str);

        private void addEnemyPanel(int index, string str)
        {
            if (this.InvokeRequired)
            {
                addEnemyPanelCallback d = new addEnemyPanelCallback(addEnemyPanel);
                this.Invoke(d, new object[] { index, str });
            }
            else
            {
                string name = "";
                switch (index)
                {
                    case 1:
                        name = "enemyPanelTop";
                        break;
                    case 2:
                        name = "enemyPanelRight";
                        break;
                    case 3:
                        name = "enemyPanelLeft";
                        break;
                }
                foreach (EnemyPanel p in enemyPanels)
                {
                    if (p.Panel.Name == name)
                    {
                        p.Player = str;
                        p.NameLabel.Text = str;
                        break;
                    }
                }
            }
        }

        private delegate void fillEnemyPanelsCallback(EnemyPanel panel, int numCards);

        private void fillEnemyPanel(EnemyPanel panel, int numCards)
        {
            if (panel.Panel.InvokeRequired)
            {
                fillEnemyPanelsCallback d = new fillEnemyPanelsCallback(fillEnemyPanel);
                this.Invoke(d, new object[] { panel, numCards });
            }
            else
            {
                panel.Panel.Controls.Clear();
                for (int i = 0; i < numCards; ++i)
                {
                    PictureBox p = new PictureBox();
                    p.Size = new Size(this.CARD_WIDTH, this.CARD_HEIGHT);
                    p.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                    p.Image = newGUI_Taki.Properties.Resources.backCard;
                    if (panel.Panel.Name == "enemyPanelRight" || panel.Panel.Name == "enemyPanelRight")
                    {
                        p.Image.RotateFlip(RotateFlipType.Rotate90FlipNone);
                    }
                    panel.Panel.Controls.Add(p);
                    panel.NumCards = numCards;
                    panel.NumCardsLabel.Text = numCards.ToString();
                    p.Visible = true;
                }
            }
        }

        private delegate void removeEnemyPanelCallback(int index);

        private void removeEnemyPanel(int index)
        {
            if (this.InvokeRequired)
            {
                removeEnemyPanelCallback d = new removeEnemyPanelCallback(removeEnemyPanel);
                this.Invoke(d, new object[] { index });
            }
            else
            {
                string name = "";
                switch (index)
                {
                    case 1:
                        name = "enemyPanelTop";
                        break;
                    case 2:
                        name = "enemyPanelRight";
                        break;
                    case 3:
                        name = "enemyPanelLeft";
                        break;
                }
                foreach (EnemyPanel p in enemyPanels)
                {
                    if (p.Panel.Name == name)
                    {
                        p.Panel.Visible = false;
                        p.NumCardsLabel.Visible = false;
                        p.NameLabel.Visible = false;
                        break;
                    }
                }
            }
        }

        private delegate void updateCurrPlayerLabelCallback(string txt);

        private void updateCurrPlayerLabel(string txt)
        {
            if (this.CurrPlayerLabel.InvokeRequired)
            {
                updateCurrPlayerLabelCallback d = new updateCurrPlayerLabelCallback(updateCurrPlayerLabel);
                this.Invoke(d, new object[] { txt });
            }
            else
            {
                this.CurrPlayerLabel.Text = txt;
            }
        }
    }
}