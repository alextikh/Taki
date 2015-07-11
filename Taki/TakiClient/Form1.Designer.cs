using System;
namespace newGUI_Taki
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.butRegistr = new System.Windows.Forms.Button();
            this.butLogin = new System.Windows.Forms.Button();
            this.butExit = new System.Windows.Forms.Button();
            this.lblUsername = new System.Windows.Forms.Label();
            this.lblPassword = new System.Windows.Forms.Label();
            this.tbUsername = new System.Windows.Forms.TextBox();
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.butEnterDetails = new System.Windows.Forms.Button();
            this.lblError = new System.Windows.Forms.Label();
            this.lblScreen = new System.Windows.Forms.Label();
            this.butEnterChoseLobby = new System.Windows.Forms.Button();
            this.tbEnterChose = new System.Windows.Forms.TextBox();
            this.butEnterChoseInRoom = new System.Windows.Forms.Button();
            this.butEnterRoomName = new System.Windows.Forms.Button();
            this.dgvRoomList = new System.Windows.Forms.DataGridView();
            this.Admin = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NumberPlayers = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsOpen = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pbTopCard = new System.Windows.Forms.PictureBox();
            this.pbBankCards = new System.Windows.Forms.PictureBox();
            this.tbEndTurn = new System.Windows.Forms.Button();
            this.ChatShowBox = new System.Windows.Forms.TextBox();
            this.butSendChat = new System.Windows.Forms.Button();
            this.ChatSendBox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRoomList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbTopCard)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbBankCards)).BeginInit();
            this.SuspendLayout();
            // 
            // butRegistr
            // 
            this.butRegistr.BackColor = System.Drawing.Color.Red;
            this.butRegistr.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.butRegistr.Location = new System.Drawing.Point(276, 277);
            this.butRegistr.Margin = new System.Windows.Forms.Padding(4);
            this.butRegistr.Name = "butRegistr";
            this.butRegistr.Size = new System.Drawing.Size(152, 43);
            this.butRegistr.TabIndex = 0;
            this.butRegistr.Text = "Registr";
            this.butRegistr.UseVisualStyleBackColor = false;
            this.butRegistr.Click += new System.EventHandler(this.butRegistr_Click);
            // 
            // butLogin
            // 
            this.butLogin.BackColor = System.Drawing.Color.Yellow;
            this.butLogin.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.butLogin.Location = new System.Drawing.Point(276, 216);
            this.butLogin.Margin = new System.Windows.Forms.Padding(4);
            this.butLogin.Name = "butLogin";
            this.butLogin.Size = new System.Drawing.Size(152, 43);
            this.butLogin.TabIndex = 1;
            this.butLogin.Text = "Log-in";
            this.butLogin.UseVisualStyleBackColor = false;
            this.butLogin.Click += new System.EventHandler(this.butLogin_Click);
            // 
            // butExit
            // 
            this.butExit.BackColor = System.Drawing.Color.Blue;
            this.butExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.butExit.Location = new System.Drawing.Point(276, 340);
            this.butExit.Margin = new System.Windows.Forms.Padding(4);
            this.butExit.Name = "butExit";
            this.butExit.Size = new System.Drawing.Size(152, 43);
            this.butExit.TabIndex = 2;
            this.butExit.Text = "Exit";
            this.butExit.UseVisualStyleBackColor = false;
            this.butExit.Click += new System.EventHandler(this.butExit_Click);
            // 
            // lblUsername
            // 
            this.lblUsername.AutoSize = true;
            this.lblUsername.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUsername.Location = new System.Drawing.Point(134, 105);
            this.lblUsername.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(127, 20);
            this.lblUsername.TabIndex = 3;
            this.lblUsername.Text = "Enter username:";
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPassword.Location = new System.Drawing.Point(134, 148);
            this.lblPassword.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(124, 20);
            this.lblPassword.TabIndex = 4;
            this.lblPassword.Text = "Enter password:";
            // 
            // tbUsername
            // 
            this.tbUsername.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbUsername.Location = new System.Drawing.Point(269, 105);
            this.tbUsername.Margin = new System.Windows.Forms.Padding(4);
            this.tbUsername.Name = "tbUsername";
            this.tbUsername.Size = new System.Drawing.Size(160, 26);
            this.tbUsername.TabIndex = 5;
            // 
            // tbPassword
            // 
            this.tbPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbPassword.Location = new System.Drawing.Point(269, 148);
            this.tbPassword.Margin = new System.Windows.Forms.Padding(4);
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.Size = new System.Drawing.Size(160, 26);
            this.tbPassword.TabIndex = 6;
            // 
            // butEnterDetails
            // 
            this.butEnterDetails.BackColor = System.Drawing.Color.Lime;
            this.butEnterDetails.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.butEnterDetails.Location = new System.Drawing.Point(276, 232);
            this.butEnterDetails.Margin = new System.Windows.Forms.Padding(4);
            this.butEnterDetails.Name = "butEnterDetails";
            this.butEnterDetails.Size = new System.Drawing.Size(152, 43);
            this.butEnterDetails.TabIndex = 7;
            this.butEnterDetails.Text = "Enter details";
            this.butEnterDetails.UseVisualStyleBackColor = false;
            this.butEnterDetails.Click += new System.EventHandler(this.butEnterDetails_Click);
            // 
            // lblError
            // 
            this.lblError.AutoSize = true;
            this.lblError.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblError.ForeColor = System.Drawing.Color.Red;
            this.lblError.Location = new System.Drawing.Point(240, 24);
            this.lblError.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblError.Name = "lblError";
            this.lblError.Size = new System.Drawing.Size(0, 20);
            this.lblError.TabIndex = 8;
            // 
            // lblScreen
            // 
            this.lblScreen.AutoSize = true;
            this.lblScreen.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblScreen.Location = new System.Drawing.Point(272, 65);
            this.lblScreen.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblScreen.Name = "lblScreen";
            this.lblScreen.Size = new System.Drawing.Size(0, 24);
            this.lblScreen.TabIndex = 9;
            // 
            // butEnterChoseLobby
            // 
            this.butEnterChoseLobby.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.butEnterChoseLobby.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.butEnterChoseLobby.Location = new System.Drawing.Point(276, 250);
            this.butEnterChoseLobby.Margin = new System.Windows.Forms.Padding(4);
            this.butEnterChoseLobby.Name = "butEnterChoseLobby";
            this.butEnterChoseLobby.Size = new System.Drawing.Size(153, 43);
            this.butEnterChoseLobby.TabIndex = 10;
            this.butEnterChoseLobby.Text = "Enter chose";
            this.butEnterChoseLobby.UseVisualStyleBackColor = false;
            this.butEnterChoseLobby.Click += new System.EventHandler(this.butEnterChoseLobby_Click);
            // 
            // tbEnterChose
            // 
            this.tbEnterChose.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbEnterChose.Location = new System.Drawing.Point(269, 147);
            this.tbEnterChose.Margin = new System.Windows.Forms.Padding(4);
            this.tbEnterChose.Name = "tbEnterChose";
            this.tbEnterChose.Size = new System.Drawing.Size(160, 26);
            this.tbEnterChose.TabIndex = 11;
            // 
            // butEnterChoseInRoom
            // 
            this.butEnterChoseInRoom.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.butEnterChoseInRoom.Location = new System.Drawing.Point(276, 283);
            this.butEnterChoseInRoom.Margin = new System.Windows.Forms.Padding(4);
            this.butEnterChoseInRoom.Name = "butEnterChoseInRoom";
            this.butEnterChoseInRoom.Size = new System.Drawing.Size(152, 43);
            this.butEnterChoseInRoom.TabIndex = 12;
            this.butEnterChoseInRoom.Text = "Enter chose";
            this.butEnterChoseInRoom.UseVisualStyleBackColor = true;
            this.butEnterChoseInRoom.Click += new System.EventHandler(this.butEnterChoseInRoom_Click);
            // 
            // butEnterRoomName
            // 
            this.butEnterRoomName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.butEnterRoomName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.butEnterRoomName.Location = new System.Drawing.Point(269, 289);
            this.butEnterRoomName.Margin = new System.Windows.Forms.Padding(4);
            this.butEnterRoomName.Name = "butEnterRoomName";
            this.butEnterRoomName.Size = new System.Drawing.Size(170, 43);
            this.butEnterRoomName.TabIndex = 13;
            this.butEnterRoomName.Text = "Enter Room Name";
            this.butEnterRoomName.UseVisualStyleBackColor = false;
            this.butEnterRoomName.Visible = false;
            this.butEnterRoomName.Click += new System.EventHandler(this.butEnterRoomName_Click);
            // 
            // dgvRoomList
            // 
            this.dgvRoomList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRoomList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Admin,
            this.NumberPlayers,
            this.IsOpen});
            this.dgvRoomList.Location = new System.Drawing.Point(410, 195);
            this.dgvRoomList.Name = "dgvRoomList";
            this.dgvRoomList.Size = new System.Drawing.Size(345, 150);
            this.dgvRoomList.TabIndex = 14;
            this.dgvRoomList.Visible = false;
            // 
            // Admin
            // 
            this.Admin.HeaderText = "Admin";
            this.Admin.Name = "Admin";
            // 
            // NumberPlayers
            // 
            this.NumberPlayers.HeaderText = "number of players";
            this.NumberPlayers.Name = "NumberPlayers";
            // 
            // IsOpen
            // 
            this.IsOpen.HeaderText = "Is open";
            this.IsOpen.Name = "IsOpen";
            // 
            // pbTopCard
            // 
            this.pbTopCard.Location = new System.Drawing.Point(553, 217);
            this.pbTopCard.Name = "pbTopCard";
            this.pbTopCard.Size = new System.Drawing.Size(100, 109);
            this.pbTopCard.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbTopCard.TabIndex = 15;
            this.pbTopCard.TabStop = false;
            // 
            // pbBankCards
            // 
            this.pbBankCards.Image = ((System.Drawing.Image)(resources.GetObject("pbBankCards.Image")));
            this.pbBankCards.Location = new System.Drawing.Point(486, 65);
            this.pbBankCards.Name = "pbBankCards";
            this.pbBankCards.Size = new System.Drawing.Size(100, 124);
            this.pbBankCards.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbBankCards.TabIndex = 16;
            this.pbBankCards.TabStop = false;
            this.pbBankCards.Visible = false;
            this.pbBankCards.Click += new System.EventHandler(this.pbBankCards_Click);
            // 
            // tbEndTurn
            // 
            this.tbEndTurn.Location = new System.Drawing.Point(150, 216);
            this.tbEndTurn.Name = "tbEndTurn";
            this.tbEndTurn.Size = new System.Drawing.Size(75, 23);
            this.tbEndTurn.TabIndex = 17;
            this.tbEndTurn.Text = "End turn";
            this.tbEndTurn.UseVisualStyleBackColor = true;
            this.tbEndTurn.Visible = false;
            this.tbEndTurn.Click += new System.EventHandler(this.tbEndTurn_Click);
            // 
            // ChatShowBox
            // 
            this.ChatShowBox.Location = new System.Drawing.Point(592, 12);
            this.ChatShowBox.Multiline = true;
            this.ChatShowBox.Name = "ChatShowBox";
            this.ChatShowBox.ReadOnly = true;
            this.ChatShowBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ChatShowBox.Size = new System.Drawing.Size(210, 77);
            this.ChatShowBox.TabIndex = 18;
            this.ChatShowBox.UseWaitCursor = true;
            this.ChatShowBox.Visible = false;
            // 
            // butSendChat
            // 
            this.butSendChat.Location = new System.Drawing.Point(727, 108);
            this.butSendChat.Name = "butSendChat";
            this.butSendChat.Size = new System.Drawing.Size(75, 23);
            this.butSendChat.TabIndex = 19;
            this.butSendChat.Text = "send";
            this.butSendChat.UseVisualStyleBackColor = true;
            this.butSendChat.Visible = false;
            this.butSendChat.Click += new System.EventHandler(this.butSendChat_Click);
            // 
            // ChatSendBox
            // 
            this.ChatSendBox.Location = new System.Drawing.Point(592, 90);
            this.ChatSendBox.Multiline = true;
            this.ChatSendBox.Name = "ChatSendBox";
            this.ChatSendBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ChatSendBox.Size = new System.Drawing.Size(210, 20);
            this.ChatSendBox.TabIndex = 20;
            this.ChatSendBox.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(812, 422);
            this.Controls.Add(this.ChatSendBox);
            this.Controls.Add(this.butSendChat);
            this.Controls.Add(this.ChatShowBox);
            this.Controls.Add(this.tbEndTurn);
            this.Controls.Add(this.pbBankCards);
            this.Controls.Add(this.pbTopCard);
            this.Controls.Add(this.dgvRoomList);
            this.Controls.Add(this.butEnterRoomName);
            this.Controls.Add(this.butEnterChoseInRoom);
            this.Controls.Add(this.tbEnterChose);
            this.Controls.Add(this.butEnterChoseLobby);
            this.Controls.Add(this.lblScreen);
            this.Controls.Add(this.lblError);
            this.Controls.Add(this.butEnterDetails);
            this.Controls.Add(this.tbPassword);
            this.Controls.Add(this.tbUsername);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.lblUsername);
            this.Controls.Add(this.butExit);
            this.Controls.Add(this.butLogin);
            this.Controls.Add(this.butRegistr);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "Taki";
            ((System.ComponentModel.ISupportInitialize)(this.dgvRoomList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbTopCard)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbBankCards)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button butRegistr;
        private System.Windows.Forms.Button butLogin;
        private System.Windows.Forms.Button butExit;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox tbUsername;
        private System.Windows.Forms.TextBox tbPassword;
        private System.Windows.Forms.Button butEnterDetails;
        private System.Windows.Forms.Label lblError;
        private System.Windows.Forms.Label lblScreen;
        private System.Windows.Forms.Button butEnterChoseLobby;
        private System.Windows.Forms.TextBox tbEnterChose;
        private System.Windows.Forms.Button butEnterChoseInRoom;
        private System.Windows.Forms.Button butEnterRoomName;
        private System.Windows.Forms.DataGridView dgvRoomList;
        private System.Windows.Forms.DataGridViewTextBoxColumn Admin;
        private System.Windows.Forms.DataGridViewTextBoxColumn NumberPlayers;
        private System.Windows.Forms.DataGridViewTextBoxColumn IsOpen;
        private System.Windows.Forms.PictureBox pbTopCard;
        private System.Windows.Forms.PictureBox pbBankCards;
        private System.Windows.Forms.Button tbEndTurn;
        private System.Windows.Forms.TextBox ChatShowBox;
        private System.Windows.Forms.Button butSendChat;
        private System.Windows.Forms.TextBox ChatSendBox;
    }
}
