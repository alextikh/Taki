namespace newGUI_Taki
{
    partial class RegisterLoginScreenExe
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RegisterLoginScreenExe));
            this.UsernameBox = new System.Windows.Forms.TextBox();
            this.PasswordBox = new System.Windows.Forms.TextBox();
            this.BackBut = new System.Windows.Forms.Button();
            this.ProveHumanBox = new System.Windows.Forms.TextBox();
            this.ExitBut = new System.Windows.Forms.Button();
            this.CaptchaPBox = new System.Windows.Forms.PictureBox();
            this.ErrorLabel = new System.Windows.Forms.Label();
            this.EnterBut = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.CaptchaPBox)).BeginInit();
            this.SuspendLayout();
            // 
            // UsernameBox
            // 
            this.UsernameBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.UsernameBox.BackColor = System.Drawing.Color.Gold;
            this.UsernameBox.Font = new System.Drawing.Font("Comic Sans MS", 24F);
            this.UsernameBox.ForeColor = System.Drawing.Color.Indigo;
            this.UsernameBox.Location = new System.Drawing.Point(51, 12);
            this.UsernameBox.Name = "UsernameBox";
            this.UsernameBox.Size = new System.Drawing.Size(215, 52);
            this.UsernameBox.TabIndex = 1;
            this.UsernameBox.Text = "username ";
            this.UsernameBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.UsernameBox.GotFocus += new System.EventHandler(this.UsernameBox_GotFocus);
            this.UsernameBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBox_keyDown);
            // 
            // PasswordBox
            // 
            this.PasswordBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.PasswordBox.BackColor = System.Drawing.Color.Gold;
            this.PasswordBox.Font = new System.Drawing.Font("Comic Sans MS", 24F);
            this.PasswordBox.ForeColor = System.Drawing.Color.Indigo;
            this.PasswordBox.Location = new System.Drawing.Point(302, 12);
            this.PasswordBox.Name = "PasswordBox";
            this.PasswordBox.Size = new System.Drawing.Size(215, 52);
            this.PasswordBox.TabIndex = 2;
            this.PasswordBox.Text = "password ";
            this.PasswordBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.PasswordBox.GotFocus += new System.EventHandler(this.PasswordBox_GotFocus);
            this.PasswordBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBox_keyDown);
            // 
            // BackBut
            // 
            this.BackBut.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.BackBut.BackColor = System.Drawing.Color.Crimson;
            this.BackBut.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BackBut.Font = new System.Drawing.Font("Comic Sans MS", 24F);
            this.BackBut.ForeColor = System.Drawing.Color.Orange;
            this.BackBut.Location = new System.Drawing.Point(30, 215);
            this.BackBut.Name = "BackBut";
            this.BackBut.Size = new System.Drawing.Size(150, 70);
            this.BackBut.TabIndex = 0;
            this.BackBut.Text = "Back";
            this.BackBut.UseVisualStyleBackColor = false;
            this.BackBut.Click += new System.EventHandler(this.BackBut_Click);
            // 
            // ProveHumanBox
            // 
            this.ProveHumanBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.ProveHumanBox.BackColor = System.Drawing.Color.Gold;
            this.ProveHumanBox.Font = new System.Drawing.Font("Comic Sans MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ProveHumanBox.ForeColor = System.Drawing.Color.Indigo;
            this.ProveHumanBox.Location = new System.Drawing.Point(163, 176);
            this.ProveHumanBox.Name = "ProveHumanBox";
            this.ProveHumanBox.Size = new System.Drawing.Size(221, 26);
            this.ProveHumanBox.TabIndex = 3;
            this.ProveHumanBox.Text = "please prove that you are a human";
            this.ProveHumanBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ProveHumanBox.Visible = false;
            this.ProveHumanBox.GotFocus += new System.EventHandler(this.ProveHumanBox_GotFocus);
            this.ProveHumanBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBox_keyDown);
            // 
            // ExitBut
            // 
            this.ExitBut.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.ExitBut.BackColor = System.Drawing.Color.Orchid;
            this.ExitBut.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ExitBut.Font = new System.Drawing.Font("Comic Sans MS", 24F);
            this.ExitBut.ForeColor = System.Drawing.Color.Maroon;
            this.ExitBut.Location = new System.Drawing.Point(205, 215);
            this.ExitBut.Name = "ExitBut";
            this.ExitBut.Size = new System.Drawing.Size(150, 70);
            this.ExitBut.TabIndex = 4;
            this.ExitBut.Text = "Exit";
            this.ExitBut.UseVisualStyleBackColor = false;
            this.ExitBut.Click += new System.EventHandler(this.ExitBut_Click);
            // 
            // CaptchaPBox
            // 
            this.CaptchaPBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.CaptchaPBox.Location = new System.Drawing.Point(181, 108);
            this.CaptchaPBox.Name = "CaptchaPBox";
            this.CaptchaPBox.Size = new System.Drawing.Size(185, 62);
            this.CaptchaPBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.CaptchaPBox.TabIndex = 7;
            this.CaptchaPBox.TabStop = false;
            this.CaptchaPBox.Visible = false;
            // 
            // ErrorLabel
            // 
            this.ErrorLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.ErrorLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ErrorLabel.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.ErrorLabel.Font = new System.Drawing.Font("Comic Sans MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ErrorLabel.ForeColor = System.Drawing.Color.Red;
            this.ErrorLabel.Location = new System.Drawing.Point(51, 75);
            this.ErrorLabel.Name = "ErrorLabel";
            this.ErrorLabel.Size = new System.Drawing.Size(367, 30);
            this.ErrorLabel.TabIndex = 0;
            this.ErrorLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ErrorLabel.Visible = false;
            // 
            // EnterBut
            // 
            this.EnterBut.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.EnterBut.BackColor = System.Drawing.Color.OliveDrab;
            this.EnterBut.Cursor = System.Windows.Forms.Cursors.Hand;
            this.EnterBut.Font = new System.Drawing.Font("Comic Sans MS", 24F);
            this.EnterBut.ForeColor = System.Drawing.Color.Aqua;
            this.EnterBut.Location = new System.Drawing.Point(384, 215);
            this.EnterBut.Name = "EnterBut";
            this.EnterBut.Size = new System.Drawing.Size(150, 70);
            this.EnterBut.TabIndex = 5;
            this.EnterBut.Text = "Enter";
            this.EnterBut.UseVisualStyleBackColor = false;
            this.EnterBut.Click += new System.EventHandler(this.EnterBut_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.checkBox1.AutoSize = true;
            this.checkBox1.BackColor = System.Drawing.Color.LightPink;
            this.checkBox1.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox1.ForeColor = System.Drawing.Color.Navy;
            this.checkBox1.Location = new System.Drawing.Point(424, 75);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(139, 27);
            this.checkBox1.TabIndex = 8;
            this.checkBox1.Text = "Show password";
            this.checkBox1.UseVisualStyleBackColor = false;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // RegisterLoginScreenExe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(575, 305);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.EnterBut);
            this.Controls.Add(this.ErrorLabel);
            this.Controls.Add(this.CaptchaPBox);
            this.Controls.Add(this.ExitBut);
            this.Controls.Add(this.ProveHumanBox);
            this.Controls.Add(this.BackBut);
            this.Controls.Add(this.PasswordBox);
            this.Controls.Add(this.UsernameBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "RegisterLoginScreenExe";
            this.Text = "RegisterLoginScreenExe";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.CaptchaPBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox UsernameBox;
        private System.Windows.Forms.TextBox PasswordBox;
        private System.Windows.Forms.Button BackBut;
        private System.Windows.Forms.TextBox ProveHumanBox;
        private System.Windows.Forms.Button ExitBut;
        private System.Windows.Forms.PictureBox CaptchaPBox;
        private System.Windows.Forms.Label ErrorLabel;
        private System.Windows.Forms.Button EnterBut;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}