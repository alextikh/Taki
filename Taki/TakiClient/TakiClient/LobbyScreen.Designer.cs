namespace newGUI_Taki
{
    partial class LobbyScreen
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvRoomList = new System.Windows.Forms.DataGridView();
            this.name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.admin = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.players = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.state = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ExitBut = new System.Windows.Forms.Button();
            this.BackBut = new System.Windows.Forms.Button();
            this.RefreshBut = new System.Windows.Forms.Button();
            this.JoinBut = new System.Windows.Forms.Button();
            this.CreateRoomBut = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRoomList)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvRoomList
            // 
            this.dgvRoomList.AllowUserToAddRows = false;
            this.dgvRoomList.AllowUserToDeleteRows = false;
            this.dgvRoomList.AllowUserToResizeColumns = false;
            this.dgvRoomList.AllowUserToResizeRows = false;
            this.dgvRoomList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvRoomList.BackgroundColor = System.Drawing.Color.GreenYellow;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.GreenYellow;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Comic Sans MS", 14F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.GreenYellow;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvRoomList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvRoomList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRoomList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.name,
            this.admin,
            this.players,
            this.state});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.GreenYellow;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Times New Roman", 12F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Plum;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.DarkBlue;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvRoomList.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvRoomList.GridColor = System.Drawing.Color.GreenYellow;
            this.dgvRoomList.Location = new System.Drawing.Point(12, 13);
            this.dgvRoomList.MultiSelect = false;
            this.dgvRoomList.Name = "dgvRoomList";
            this.dgvRoomList.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.GreenYellow;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Times New Roman", 14F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.GreenYellow;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvRoomList.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.GreenYellow;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.Plum;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.DarkBlue;
            this.dgvRoomList.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvRoomList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvRoomList.Size = new System.Drawing.Size(474, 187);
            this.dgvRoomList.TabIndex = 0;
            this.dgvRoomList.SelectionChanged += new System.EventHandler(this.dgvRoomList_selectionChanged);
            // 
            // name
            // 
            this.name.HeaderText = "name";
            this.name.Name = "name";
            this.name.ReadOnly = true;
            // 
            // admin
            // 
            this.admin.HeaderText = "admin";
            this.admin.Name = "admin";
            this.admin.ReadOnly = true;
            // 
            // players
            // 
            this.players.HeaderText = "players";
            this.players.Name = "players";
            this.players.ReadOnly = true;
            // 
            // state
            // 
            this.state.HeaderText = "state";
            this.state.Name = "state";
            this.state.ReadOnly = true;
            // 
            // ExitBut
            // 
            this.ExitBut.BackColor = System.Drawing.Color.MediumVioletRed;
            this.ExitBut.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ExitBut.Font = new System.Drawing.Font("Comic Sans MS", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ExitBut.ForeColor = System.Drawing.Color.DarkRed;
            this.ExitBut.Location = new System.Drawing.Point(58, 217);
            this.ExitBut.Name = "ExitBut";
            this.ExitBut.Size = new System.Drawing.Size(180, 60);
            this.ExitBut.TabIndex = 1;
            this.ExitBut.Text = "Exit";
            this.ExitBut.UseVisualStyleBackColor = false;
            this.ExitBut.Click += new System.EventHandler(this.ExitBut_Click);
            // 
            // BackBut
            // 
            this.BackBut.BackColor = System.Drawing.Color.CornflowerBlue;
            this.BackBut.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BackBut.Font = new System.Drawing.Font("Comic Sans MS", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BackBut.ForeColor = System.Drawing.Color.MidnightBlue;
            this.BackBut.Location = new System.Drawing.Point(276, 217);
            this.BackBut.Name = "BackBut";
            this.BackBut.Size = new System.Drawing.Size(180, 60);
            this.BackBut.TabIndex = 2;
            this.BackBut.Text = "Back";
            this.BackBut.UseVisualStyleBackColor = false;
            this.BackBut.Click += new System.EventHandler(this.BackBut_Click);
            // 
            // RefreshBut
            // 
            this.RefreshBut.BackColor = System.Drawing.Color.MidnightBlue;
            this.RefreshBut.Cursor = System.Windows.Forms.Cursors.Hand;
            this.RefreshBut.Font = new System.Drawing.Font("Comic Sans MS", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RefreshBut.ForeColor = System.Drawing.Color.OliveDrab;
            this.RefreshBut.Location = new System.Drawing.Point(493, 12);
            this.RefreshBut.Name = "RefreshBut";
            this.RefreshBut.Size = new System.Drawing.Size(120, 50);
            this.RefreshBut.TabIndex = 3;
            this.RefreshBut.Text = "Refresh";
            this.RefreshBut.UseVisualStyleBackColor = false;
            this.RefreshBut.Click += new System.EventHandler(this.RefreshBut_Click);
            // 
            // JoinBut
            // 
            this.JoinBut.BackColor = System.Drawing.Color.ForestGreen;
            this.JoinBut.Cursor = System.Windows.Forms.Cursors.Hand;
            this.JoinBut.Font = new System.Drawing.Font("Comic Sans MS", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.JoinBut.ForeColor = System.Drawing.Color.Orange;
            this.JoinBut.Location = new System.Drawing.Point(493, 68);
            this.JoinBut.Name = "JoinBut";
            this.JoinBut.Size = new System.Drawing.Size(120, 50);
            this.JoinBut.TabIndex = 4;
            this.JoinBut.Text = "Join";
            this.JoinBut.UseVisualStyleBackColor = false;
            this.JoinBut.Click += new System.EventHandler(this.JoinBut_Click);
            // 
            // CreateRoomBut
            // 
            this.CreateRoomBut.BackColor = System.Drawing.Color.Magenta;
            this.CreateRoomBut.Cursor = System.Windows.Forms.Cursors.Hand;
            this.CreateRoomBut.Font = new System.Drawing.Font("Comic Sans MS", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CreateRoomBut.ForeColor = System.Drawing.Color.Snow;
            this.CreateRoomBut.Location = new System.Drawing.Point(493, 124);
            this.CreateRoomBut.Name = "CreateRoomBut";
            this.CreateRoomBut.Size = new System.Drawing.Size(120, 76);
            this.CreateRoomBut.TabIndex = 5;
            this.CreateRoomBut.Text = "Create room";
            this.CreateRoomBut.UseVisualStyleBackColor = false;
            this.CreateRoomBut.Click += new System.EventHandler(this.CreateRoomBut_Click);
            // 
            // LobbyScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.GreenYellow;
            this.ClientSize = new System.Drawing.Size(619, 289);
            this.Controls.Add(this.CreateRoomBut);
            this.Controls.Add(this.JoinBut);
            this.Controls.Add(this.RefreshBut);
            this.Controls.Add(this.BackBut);
            this.Controls.Add(this.ExitBut);
            this.Controls.Add(this.dgvRoomList);
            this.Name = "LobbyScreen";
            this.Text = "LobbyScreen";
            ((System.ComponentModel.ISupportInitialize)(this.dgvRoomList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button ExitBut;
        private System.Windows.Forms.Button BackBut;
        private System.Windows.Forms.Button RefreshBut;
        private System.Windows.Forms.Button JoinBut;
        private System.Windows.Forms.Button CreateRoomBut;
        private System.Windows.Forms.DataGridView dgvRoomList;
        private System.Windows.Forms.DataGridViewTextBoxColumn name;
        private System.Windows.Forms.DataGridViewTextBoxColumn admin;
        private System.Windows.Forms.DataGridViewTextBoxColumn players;
        private System.Windows.Forms.DataGridViewTextBoxColumn state;
    }
}