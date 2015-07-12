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
            this.butSendChat = new System.Windows.Forms.Button();
            this.ChatShowBox = new System.Windows.Forms.TextBox();
            this.CurrPlayerLabel = new System.Windows.Forms.Label();
            this.ErrorLabel = new System.Windows.Forms.Label();
            this.butEndTurn = new System.Windows.Forms.Button();
            this.pbTopCard = new System.Windows.Forms.PictureBox();
            this.pbBankCards = new System.Windows.Forms.PictureBox();
            this.butStartGame = new System.Windows.Forms.Button();
            this.butSurrender = new System.Windows.Forms.Button();
            this.butLeaveRoom = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pbTopCard)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbBankCards)).BeginInit();
            this.SuspendLayout();
            // 
            // ChatSendBox
            // 
            this.ChatSendBox.Location = new System.Drawing.Point(246, 90);
            this.ChatSendBox.Multiline = true;
            this.ChatSendBox.Name = "ChatSendBox";
            this.ChatSendBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ChatSendBox.Size = new System.Drawing.Size(210, 20);
            this.ChatSendBox.TabIndex = 23;
            // 
            // butSendChat
            // 
            this.butSendChat.Location = new System.Drawing.Point(381, 108);
            this.butSendChat.Name = "butSendChat";
            this.butSendChat.Size = new System.Drawing.Size(75, 23);
            this.butSendChat.TabIndex = 22;
            this.butSendChat.Text = "send";
            this.butSendChat.UseVisualStyleBackColor = true;
            // 
            // ChatShowBox
            // 
            this.ChatShowBox.Location = new System.Drawing.Point(246, 12);
            this.ChatShowBox.Multiline = true;
            this.ChatShowBox.Name = "ChatShowBox";
            this.ChatShowBox.ReadOnly = true;
            this.ChatShowBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ChatShowBox.Size = new System.Drawing.Size(210, 77);
            this.ChatShowBox.TabIndex = 21;
            this.ChatShowBox.UseWaitCursor = true;
            // 
            // CurrPlayerLabel
            // 
            this.CurrPlayerLabel.AutoSize = true;
            this.CurrPlayerLabel.Location = new System.Drawing.Point(43, 13);
            this.CurrPlayerLabel.Name = "CurrPlayerLabel";
            this.CurrPlayerLabel.Size = new System.Drawing.Size(0, 13);
            this.CurrPlayerLabel.TabIndex = 24;
            // 
            // ErrorLabel
            // 
            this.ErrorLabel.AutoSize = true;
            this.ErrorLabel.Location = new System.Drawing.Point(60, 52);
            this.ErrorLabel.Name = "ErrorLabel";
            this.ErrorLabel.Size = new System.Drawing.Size(0, 13);
            this.ErrorLabel.TabIndex = 25;
            // 
            // butEndTurn
            // 
            this.butEndTurn.Location = new System.Drawing.Point(127, 108);
            this.butEndTurn.Name = "butEndTurn";
            this.butEndTurn.Size = new System.Drawing.Size(75, 23);
            this.butEndTurn.TabIndex = 26;
            this.butEndTurn.Text = "butEndTurn";
            this.butEndTurn.UseVisualStyleBackColor = true;
            this.butEndTurn.Visible = false;
            this.butEndTurn.Click += new System.EventHandler(this.butEndTurn_Click);
            // 
            // pbTopCard
            // 
            this.pbTopCard.Location = new System.Drawing.Point(42, 137);
            this.pbTopCard.Name = "pbTopCard";
            this.pbTopCard.Size = new System.Drawing.Size(100, 106);
            this.pbTopCard.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbTopCard.TabIndex = 27;
            this.pbTopCard.TabStop = false;
            // 
            // pbBankCards
            // 
            this.pbBankCards.Image = ((System.Drawing.Image)(resources.GetObject("pbBankCards.Image")));
            this.pbBankCards.Location = new System.Drawing.Point(293, 137);
            this.pbBankCards.Name = "pbBankCards";
            this.pbBankCards.Size = new System.Drawing.Size(100, 112);
            this.pbBankCards.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbBankCards.TabIndex = 28;
            this.pbBankCards.TabStop = false;
            // 
            // butStartGame
            // 
            this.butStartGame.Location = new System.Drawing.Point(42, 47);
            this.butStartGame.Name = "butStartGame";
            this.butStartGame.Size = new System.Drawing.Size(75, 23);
            this.butStartGame.TabIndex = 29;
            this.butStartGame.Text = "butStartGame";
            this.butStartGame.UseVisualStyleBackColor = true;
            this.butStartGame.Click += new System.EventHandler(this.butStartGame_Click);
            // 
            // butSurrender
            // 
            this.butSurrender.Location = new System.Drawing.Point(46, 108);
            this.butSurrender.Name = "butSurrender";
            this.butSurrender.Size = new System.Drawing.Size(75, 23);
            this.butSurrender.TabIndex = 30;
            this.butSurrender.Text = "butSurrender";
            this.butSurrender.UseVisualStyleBackColor = true;
            this.butSurrender.Click += new System.EventHandler(this.butSurrender_Click);
            // 
            // butLeaveRoom
            // 
            this.butLeaveRoom.Location = new System.Drawing.Point(136, 47);
            this.butLeaveRoom.Name = "butLeaveRoom";
            this.butLeaveRoom.Size = new System.Drawing.Size(75, 23);
            this.butLeaveRoom.TabIndex = 31;
            this.butLeaveRoom.Text = "butLeaveRoom";
            this.butLeaveRoom.UseVisualStyleBackColor = true;
            this.butLeaveRoom.Click += new System.EventHandler(this.butLeaveRoom_Click);
            // 
            // RoomScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(590, 335);
            this.Controls.Add(this.butLeaveRoom);
            this.Controls.Add(this.butSurrender);
            this.Controls.Add(this.butStartGame);
            this.Controls.Add(this.pbBankCards);
            this.Controls.Add(this.pbTopCard);
            this.Controls.Add(this.butEndTurn);
            this.Controls.Add(this.ErrorLabel);
            this.Controls.Add(this.CurrPlayerLabel);
            this.Controls.Add(this.ChatSendBox);
            this.Controls.Add(this.butSendChat);
            this.Controls.Add(this.ChatShowBox);
            this.Name = "RoomScreen";
            this.Text = "RoomScreen";
            ((System.ComponentModel.ISupportInitialize)(this.pbTopCard)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbBankCards)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox ChatSendBox;
        private System.Windows.Forms.Button butSendChat;
        private System.Windows.Forms.TextBox ChatShowBox;
        private System.Windows.Forms.Label CurrPlayerLabel;
        private System.Windows.Forms.Label ErrorLabel;
        private System.Windows.Forms.Button butEndTurn;
        private System.Windows.Forms.PictureBox pbTopCard;
        private System.Windows.Forms.PictureBox pbBankCards;
        private System.Windows.Forms.Button butStartGame;
        private System.Windows.Forms.Button butSurrender;
        private System.Windows.Forms.Button butLeaveRoom;
    }
}