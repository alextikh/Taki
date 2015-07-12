namespace newGUI_Taki
{
    partial class RegisterLoginScreenChoice
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
            this.RegisterBut = new System.Windows.Forms.Button();
            this.LoginBut = new System.Windows.Forms.Button();
            this.ExitBut = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // RegisterBut
            // 
            this.RegisterBut.BackColor = System.Drawing.Color.SkyBlue;
            this.RegisterBut.Cursor = System.Windows.Forms.Cursors.Hand;
            this.RegisterBut.Font = new System.Drawing.Font("Comic Sans MS", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RegisterBut.ForeColor = System.Drawing.Color.Green;
            this.RegisterBut.Location = new System.Drawing.Point(185, 23);
            this.RegisterBut.Name = "RegisterBut";
            this.RegisterBut.Size = new System.Drawing.Size(190, 60);
            this.RegisterBut.TabIndex = 0;
            this.RegisterBut.Text = "Register";
            this.RegisterBut.UseVisualStyleBackColor = false;
            this.RegisterBut.Click += new System.EventHandler(this.RegisterBut_Click);
            // 
            // LoginBut
            // 
            this.LoginBut.BackColor = System.Drawing.Color.RoyalBlue;
            this.LoginBut.Cursor = System.Windows.Forms.Cursors.Hand;
            this.LoginBut.Font = new System.Drawing.Font("Comic Sans MS", 24F);
            this.LoginBut.ForeColor = System.Drawing.Color.LawnGreen;
            this.LoginBut.Location = new System.Drawing.Point(185, 99);
            this.LoginBut.Name = "LoginBut";
            this.LoginBut.Size = new System.Drawing.Size(190, 60);
            this.LoginBut.TabIndex = 1;
            this.LoginBut.Text = "Login";
            this.LoginBut.UseVisualStyleBackColor = false;
            this.LoginBut.Click += new System.EventHandler(this.LoginBut_Click);
            // 
            // ExitBut
            // 
            this.ExitBut.BackColor = System.Drawing.Color.IndianRed;
            this.ExitBut.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ExitBut.Font = new System.Drawing.Font("Comic Sans MS", 24F);
            this.ExitBut.ForeColor = System.Drawing.Color.DarkRed;
            this.ExitBut.Location = new System.Drawing.Point(185, 178);
            this.ExitBut.Name = "ExitBut";
            this.ExitBut.Size = new System.Drawing.Size(190, 60);
            this.ExitBut.TabIndex = 2;
            this.ExitBut.Text = "Exit";
            this.ExitBut.UseVisualStyleBackColor = false;
            this.ExitBut.Click += new System.EventHandler(this.ExitBut_Click);
            // 
            // RegisterLoginScreenChoice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Orange;
            this.ClientSize = new System.Drawing.Size(576, 261);
            this.Controls.Add(this.ExitBut);
            this.Controls.Add(this.LoginBut);
            this.Controls.Add(this.RegisterBut);
            this.Name = "RegisterLoginScreenChoice";
            this.Text = "RegisterLoginScreenChoice";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button RegisterBut;
        private System.Windows.Forms.Button LoginBut;
        private System.Windows.Forms.Button ExitBut;
    }
}