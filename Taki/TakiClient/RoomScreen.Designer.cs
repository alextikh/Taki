namespace newGUI_Taki
{
    partial class RoomScreen
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RoomScreen));
            this.ChatSendBox = new System.Windows.Forms.TextBox();
            this.SendChatBut = new System.Windows.Forms.Button();
            this.CurrPlayerLabel = new System.Windows.Forms.Label();
            this.ErrorLabel = new System.Windows.Forms.Label();
            this.butEndTurn = new System.Windows.Forms.Button();
            this.pbTopCard = new System.Windows.Forms.PictureBox();
            this.pbBankCards = new System.Windows.Forms.PictureBox();
            this.butStartGame = new System.Windows.Forms.Button();
            this.butLeaveRoom = new System.Windows.Forms.Button();
            this.ChatShowBox = new System.Windows.Forms.RichTextBox();
            this.CardsPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.enemyPanelLeft = new System.Windows.Forms.FlowLayoutPanel();
            this.enemyPanelTop = new System.Windows.Forms.FlowLayoutPanel();
            this.enemyPanelRight = new System.Windows.Forms.FlowLayoutPanel();
            this.nameLabelTop = new System.Windows.Forms.Label();
            this.cardsNumLabelTop = new System.Windows.Forms.Label();
            this.nameLabelLeft = new System.Windows.Forms.Label();
            this.cardsNumLabelLeft = new System.Windows.Forms.Label();
            this.nameLabelRight = new System.Windows.Forms.Label();
            this.cardsNumLabelRight = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.pbTopCard)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbBankCards)).BeginInit();
            this.CardsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // ChatSendBox
            // 
            this.ChatSendBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ChatSendBox.BackColor = System.Drawing.Color.Khaki;
            this.ChatSendBox.Font = new System.Drawing.Font("Comic Sans MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChatSendBox.Location = new System.Drawing.Point(403, 352);
            this.ChatSendBox.Multiline = true;
            this.ChatSendBox.Name = "ChatSendBox";
            this.ChatSendBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ChatSendBox.Size = new System.Drawing.Size(280, 56);
            this.ChatSendBox.TabIndex = 23;
            this.ChatSendBox.TextChanged += new System.EventHandler(this.ChatSendBox_TextChanged);
            this.ChatSendBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ChatSendBox_KeyDown);
            // 
            // SendChatBut
            // 
            this.SendChatBut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SendChatBut.BackColor = System.Drawing.Color.ForestGreen;
            this.SendChatBut.Font = new System.Drawing.Font("Comic Sans MS", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SendChatBut.ForeColor = System.Drawing.Color.MidnightBlue;
            this.SendChatBut.Location = new System.Drawing.Point(576, 414);
            this.SendChatBut.Name = "SendChatBut";
            this.SendChatBut.Size = new System.Drawing.Size(107, 56);
            this.SendChatBut.TabIndex = 22;
            this.SendChatBut.Text = "send";
            this.SendChatBut.UseVisualStyleBackColor = false;
            this.SendChatBut.Click += new System.EventHandler(this.SendChatBut_Click);
            // 
            // CurrPlayerLabel
            // 
            this.CurrPlayerLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.CurrPlayerLabel.AutoSize = true;
            this.CurrPlayerLabel.Font = new System.Drawing.Font("Comic Sans MS", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CurrPlayerLabel.ForeColor = System.Drawing.Color.Green;
            this.CurrPlayerLabel.Location = new System.Drawing.Point(3, 0);
            this.CurrPlayerLabel.Name = "CurrPlayerLabel";
            this.CurrPlayerLabel.Text = "Your turn!";
            this.CurrPlayerLabel.Size = new System.Drawing.Size(0, 38);
            this.CurrPlayerLabel.TabIndex = 24;
            this.CurrPlayerLabel.Visible = false;
            // 
            // ErrorLabel
            // 
            this.ErrorLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.ErrorLabel.AutoSize = true;
            this.ErrorLabel.Font = new System.Drawing.Font("Comic Sans MS", 16F, System.Drawing.FontStyle.Bold);
            this.ErrorLabel.ForeColor = System.Drawing.Color.Red;
            this.ErrorLabel.Location = new System.Drawing.Point(207, 16);
            this.ErrorLabel.Name = "ErrorLabel";
            this.ErrorLabel.Size = new System.Drawing.Size(0, 31);
            this.ErrorLabel.TabIndex = 25;
            this.ErrorLabel.Visible = false;
            // 
            // butEndTurn
            // 
            this.butEndTurn.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.butEndTurn.BackColor = System.Drawing.Color.LightPink;
            this.butEndTurn.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.butEndTurn.ForeColor = System.Drawing.Color.DarkRed;
            this.butEndTurn.Location = new System.Drawing.Point(228, 297);
            this.butEndTurn.Name = "butEndTurn";
            this.butEndTurn.Size = new System.Drawing.Size(88, 45);
            this.butEndTurn.TabIndex = 26;
            this.butEndTurn.Text = "End turn";
            this.butEndTurn.UseVisualStyleBackColor = false;
            this.butEndTurn.Visible = false;
            this.butEndTurn.Click += new System.EventHandler(this.butEndTurn_Click);
            // 
            // pbTopCard
            // 
            this.pbTopCard.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pbTopCard.Location = new System.Drawing.Point(159, 148);
            this.pbTopCard.Name = "pbTopCard";
            this.pbTopCard.Size = new System.Drawing.Size(106, 135);
            this.pbTopCard.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbTopCard.TabIndex = 27;
            this.pbTopCard.TabStop = false;
            // 
            // pbBankCards
            // 
            this.pbBankCards.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pbBankCards.Image = global::newGUI_Taki.Properties.Resources.backCard;
            this.pbBankCards.Location = new System.Drawing.Point(281, 148);
            this.pbBankCards.Name = "pbBankCards";
            this.pbBankCards.Size = new System.Drawing.Size(106, 135);
            this.pbBankCards.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbBankCards.TabIndex = 28;
            this.pbBankCards.TabStop = false;
            this.pbBankCards.Visible = false;
            this.pbBankCards.Click += new System.EventHandler(this.pbBankCards_Click);
            // 
            // butStartGame
            // 
            this.butStartGame.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.butStartGame.BackColor = System.Drawing.Color.Blue;
            this.butStartGame.Font = new System.Drawing.Font("Comic Sans MS", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.butStartGame.ForeColor = System.Drawing.Color.Cornsilk;
            this.butStartGame.Location = new System.Drawing.Point(188, 62);
            this.butStartGame.Name = "butStartGame";
            this.butStartGame.Size = new System.Drawing.Size(176, 68);
            this.butStartGame.TabIndex = 29;
            this.butStartGame.Text = "Start game";
            this.butStartGame.UseVisualStyleBackColor = false;
            this.butStartGame.Click += new System.EventHandler(this.butStartGame_Click);
            // 
            // butLeaveRoom
            // 
            this.butLeaveRoom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.butLeaveRoom.BackColor = System.Drawing.Color.Red;
            this.butLeaveRoom.Font = new System.Drawing.Font("Comic Sans MS", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.butLeaveRoom.ForeColor = System.Drawing.Color.PaleGreen;
            this.butLeaveRoom.Location = new System.Drawing.Point(402, 414);
            this.butLeaveRoom.Name = "butLeaveRoom";
            this.butLeaveRoom.Size = new System.Drawing.Size(170, 56);
            this.butLeaveRoom.TabIndex = 31;
            this.butLeaveRoom.Text = "Leave room";
            this.butLeaveRoom.UseVisualStyleBackColor = false;
            this.butLeaveRoom.Click += new System.EventHandler(this.butLeaveRoom_Click);
            // 
            // ChatShowBox
            // 
            this.ChatShowBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ChatShowBox.BackColor = System.Drawing.Color.MistyRose;
            this.ChatShowBox.Font = new System.Drawing.Font("Comic Sans MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChatShowBox.Location = new System.Drawing.Point(403, 6);
            this.ChatShowBox.Name = "ChatShowBox";
            this.ChatShowBox.Size = new System.Drawing.Size(280, 340);
            this.ChatShowBox.TabIndex = 32;
            this.ChatShowBox.Text = "";
            this.ChatShowBox.TextChanged += new System.EventHandler(this.ChatShowBox_TextChanged);
            // 
            // CardsPanel
            // 
            this.CardsPanel.AutoScroll = true;
            this.CardsPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.CardsPanel.Controls.Add(this.CurrPlayerLabel);
            this.CardsPanel.Location = new System.Drawing.Point(12, 403);
            this.CardsPanel.Name = "CardsPanel";
            this.CardsPanel.Size = new System.Drawing.Size(691, 67);
            this.CardsPanel.TabIndex = 33;
            this.CardsPanel.Visible = false;
            this.CardsPanel.WrapContents = false;
            // 
            // enemyPanelLeft
            // 
            this.enemyPanelLeft.AutoScroll = true;
            this.enemyPanelLeft.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.enemyPanelLeft.Location = new System.Drawing.Point(12, 94);
            this.enemyPanelLeft.Name = "enemyPanelLeft";
            this.enemyPanelLeft.Size = new System.Drawing.Size(140, 402);
            this.enemyPanelLeft.TabIndex = 34;
            this.enemyPanelLeft.Visible = false;
            this.enemyPanelLeft.WrapContents = false;
            // 
            // enemyPanelTop
            // 
            this.enemyPanelTop.AutoScroll = true;
            this.enemyPanelTop.Location = new System.Drawing.Point(213, 12);
            this.enemyPanelTop.Name = "enemyPanelTop";
            this.enemyPanelTop.Size = new System.Drawing.Size(140, 140);
            this.enemyPanelTop.TabIndex = 35;
            this.enemyPanelTop.Visible = false;
            this.enemyPanelTop.WrapContents = false;
            this.enemyPanelTop.Paint += new System.Windows.Forms.PaintEventHandler(this.enemyPanelTop_Paint);
            // 
            // enemyPanelRight
            // 
            this.enemyPanelRight.AutoScroll = true;
            this.enemyPanelRight.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.enemyPanelRight.Location = new System.Drawing.Point(539, 39);
            this.enemyPanelRight.Name = "enemyPanelRight";
            this.enemyPanelRight.Size = new System.Drawing.Size(140, 410);
            this.enemyPanelRight.TabIndex = 0;
            this.enemyPanelRight.Visible = false;
            this.enemyPanelRight.WrapContents = false;
            this.enemyPanelRight.Paint += new System.Windows.Forms.PaintEventHandler(this.enemyPanelRight_Paint);
            // 
            // nameLabelTop
            // 
            this.nameLabelTop.AutoSize = true;
            this.nameLabelTop.Font = new System.Drawing.Font("Comic Sans MS", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nameLabelTop.Location = new System.Drawing.Point(171, 180);
            this.nameLabelTop.Name = "nameLabelTop";
            this.nameLabelTop.Size = new System.Drawing.Size(0, 30);
            this.nameLabelTop.TabIndex = 36;
            this.nameLabelTop.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.nameLabelTop.Visible = false;
            // 
            // cardsNumLabelTop
            // 
            this.cardsNumLabelTop.AutoSize = true;
            this.cardsNumLabelTop.Font = new System.Drawing.Font("Comic Sans MS", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cardsNumLabelTop.Location = new System.Drawing.Point(236, 180);
            this.cardsNumLabelTop.Name = "cardsNumLabelTop";
            this.cardsNumLabelTop.Size = new System.Drawing.Size(0, 30);
            this.cardsNumLabelTop.TabIndex = 37;
            this.cardsNumLabelTop.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cardsNumLabelTop.Visible = false;
            // 
            // nameLabelLeft
            // 
            this.nameLabelLeft.AutoSize = true;
            this.nameLabelLeft.Font = new System.Drawing.Font("Comic Sans MS", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nameLabelLeft.Location = new System.Drawing.Point(171, 274);
            this.nameLabelLeft.Name = "nameLabelLeft";
            this.nameLabelLeft.Size = new System.Drawing.Size(0, 30);
            this.nameLabelLeft.TabIndex = 38;
            this.nameLabelLeft.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.nameLabelLeft.Visible = false;
            // 
            // cardsNumLabelLeft
            // 
            this.cardsNumLabelLeft.AutoSize = true;
            this.cardsNumLabelLeft.Font = new System.Drawing.Font("Comic Sans MS", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cardsNumLabelLeft.Location = new System.Drawing.Point(174, 302);
            this.cardsNumLabelLeft.Name = "cardsNumLabelLeft";
            this.cardsNumLabelLeft.Size = new System.Drawing.Size(0, 30);
            this.cardsNumLabelLeft.TabIndex = 39;
            this.cardsNumLabelLeft.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cardsNumLabelLeft.Visible = false;
            // 
            // nameLabelRight
            // 
            this.nameLabelRight.AutoSize = true;
            this.nameLabelRight.Font = new System.Drawing.Font("Comic Sans MS", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nameLabelRight.Location = new System.Drawing.Point(446, 181);
            this.nameLabelRight.Name = "nameLabelRight";
            this.nameLabelRight.Size = new System.Drawing.Size(0, 30);
            this.nameLabelRight.TabIndex = 40;
            this.nameLabelRight.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.nameLabelRight.Visible = false;
            // 
            // cardsNumLabelRight
            // 
            this.cardsNumLabelRight.AutoSize = true;
            this.cardsNumLabelRight.Font = new System.Drawing.Font("Comic Sans MS", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cardsNumLabelRight.Location = new System.Drawing.Point(428, 233);
            this.cardsNumLabelRight.Name = "cardsNumLabelRight";
            this.cardsNumLabelRight.Size = new System.Drawing.Size(0, 30);
            this.cardsNumLabelRight.TabIndex = 41;
            this.cardsNumLabelRight.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cardsNumLabelRight.Visible = false;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(13, 13);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(140, 140);
            this.flowLayoutPanel1.TabIndex = 35;
            this.flowLayoutPanel1.Visible = false;
            this.flowLayoutPanel1.WrapContents = false;
            this.flowLayoutPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.enemyPanelTop_Paint);
            // 
            // RoomScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(691, 479);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.enemyPanelTop);
            this.Controls.Add(this.pbTopCard);
            this.Controls.Add(this.cardsNumLabelRight);
            this.Controls.Add(this.CardsPanel);
            this.Controls.Add(this.nameLabelRight);
            this.Controls.Add(this.cardsNumLabelLeft);
            this.Controls.Add(this.nameLabelLeft);
            this.Controls.Add(this.cardsNumLabelTop);
            this.Controls.Add(this.nameLabelTop);
            this.Controls.Add(this.enemyPanelRight);
            this.Controls.Add(this.enemyPanelLeft);
            this.Controls.Add(this.butLeaveRoom);
            this.Controls.Add(this.pbBankCards);
            this.Controls.Add(this.butEndTurn);
            this.Controls.Add(this.ErrorLabel);
            this.Controls.Add(this.ChatSendBox);
            this.Controls.Add(this.SendChatBut);
            this.Controls.Add(this.butStartGame);
            this.Controls.Add(this.ChatShowBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "RoomScreen";
            this.Text = "RoomScreen";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.pbTopCard)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbBankCards)).EndInit();
            this.CardsPanel.ResumeLayout(false);
            this.CardsPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox ChatSendBox;
        private System.Windows.Forms.Button SendChatBut;
        private System.Windows.Forms.Label CurrPlayerLabel;
        private System.Windows.Forms.Label ErrorLabel;
        private System.Windows.Forms.Button butEndTurn;
        private System.Windows.Forms.PictureBox pbTopCard;
        private System.Windows.Forms.PictureBox pbBankCards;
        private System.Windows.Forms.Button butStartGame;
        private System.Windows.Forms.Button butLeaveRoom;
        private System.Windows.Forms.RichTextBox ChatShowBox;
        private System.Windows.Forms.FlowLayoutPanel CardsPanel;
        private System.Windows.Forms.FlowLayoutPanel enemyPanelLeft;
        private System.Windows.Forms.FlowLayoutPanel enemyPanelTop;
        private System.Windows.Forms.FlowLayoutPanel enemyPanelRight;
        private System.Windows.Forms.Label nameLabelTop;
        private System.Windows.Forms.Label cardsNumLabelTop;
        private System.Windows.Forms.Label nameLabelLeft;
        private System.Windows.Forms.Label cardsNumLabelLeft;
        private System.Windows.Forms.Label nameLabelRight;
        private System.Windows.Forms.Label cardsNumLabelRight;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
    }
}