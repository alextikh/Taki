namespace newGUI_Taki
{
    partial class RoomNameScreen
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
            this.RoomNameBox = new System.Windows.Forms.TextBox();
            this.CreateRoomBut = new System.Windows.Forms.Button();
            this.BackBut = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // RoomNameBox
            // 
            this.RoomNameBox.BackColor = System.Drawing.Color.Wheat;
            this.RoomNameBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.RoomNameBox.Font = new System.Drawing.Font("Comic Sans MS", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RoomNameBox.ForeColor = System.Drawing.Color.DarkGreen;
            this.RoomNameBox.Location = new System.Drawing.Point(71, 32);
            this.RoomNameBox.Name = "RoomNameBox";
            this.RoomNameBox.Size = new System.Drawing.Size(272, 52);
            this.RoomNameBox.TabIndex = 0;
            this.RoomNameBox.Text = "Room name";
            this.RoomNameBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.RoomNameBox.Click += new System.EventHandler(this.RoomNameBox_Click);
            this.RoomNameBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.RoomNameBox_KeyDown);
            // 
            // CreateRoomBut
            // 
            this.CreateRoomBut.BackColor = System.Drawing.Color.LightGreen;
            this.CreateRoomBut.Cursor = System.Windows.Forms.Cursors.Hand;
            this.CreateRoomBut.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.CreateRoomBut.Font = new System.Drawing.Font("Comic Sans MS", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CreateRoomBut.ForeColor = System.Drawing.Color.MediumBlue;
            this.CreateRoomBut.Location = new System.Drawing.Point(233, 161);
            this.CreateRoomBut.Name = "CreateRoomBut";
            this.CreateRoomBut.Size = new System.Drawing.Size(110, 70);
            this.CreateRoomBut.TabIndex = 1;
            this.CreateRoomBut.Text = "Create Room";
            this.CreateRoomBut.UseVisualStyleBackColor = false;
            this.CreateRoomBut.Click += new System.EventHandler(this.CreateRoomBut_Click);
            // 
            // BackBut
            // 
            this.BackBut.BackColor = System.Drawing.Color.Pink;
            this.BackBut.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BackBut.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BackBut.Font = new System.Drawing.Font("Comic Sans MS", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BackBut.ForeColor = System.Drawing.Color.DarkRed;
            this.BackBut.Location = new System.Drawing.Point(71, 161);
            this.BackBut.Name = "BackBut";
            this.BackBut.Size = new System.Drawing.Size(110, 70);
            this.BackBut.TabIndex = 2;
            this.BackBut.Text = "Back";
            this.BackBut.UseVisualStyleBackColor = false;
            this.BackBut.Click += new System.EventHandler(this.BackBut_Click);
            // 
            // RoomNameScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Wheat;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ClientSize = new System.Drawing.Size(417, 261);
            this.Controls.Add(this.BackBut);
            this.Controls.Add(this.CreateRoomBut);
            this.Controls.Add(this.RoomNameBox);
            this.ForeColor = System.Drawing.Color.DarkGreen;
            this.Name = "RoomNameScreen";
            this.Text = "RoomNameScreen";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox RoomNameBox;
        private System.Windows.Forms.Button CreateRoomBut;
        private System.Windows.Forms.Button BackBut;
    }
}