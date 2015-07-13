namespace newGUI_Taki
{
    partial class EnterIPScreen
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EnterIPScreen));
            this.EnterIPLabel = new System.Windows.Forms.Label();
            this.EnterIPBox = new System.Windows.Forms.TextBox();
            this.ErrorLabel = new System.Windows.Forms.Label();
            this.ExitBut = new System.Windows.Forms.Button();
            this.EnterBut = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // EnterIPLabel
            // 
            this.EnterIPLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.EnterIPLabel.AutoSize = true;
            this.EnterIPLabel.BackColor = System.Drawing.Color.GreenYellow;
            this.EnterIPLabel.Font = new System.Drawing.Font("Comic Sans MS", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EnterIPLabel.ForeColor = System.Drawing.Color.Indigo;
            this.EnterIPLabel.Location = new System.Drawing.Point(55, 9);
            this.EnterIPLabel.Name = "EnterIPLabel";
            this.EnterIPLabel.Size = new System.Drawing.Size(485, 45);
            this.EnterIPLabel.TabIndex = 0;
            this.EnterIPLabel.Text = "Enter the server\'s IP address:";
            // 
            // EnterIPBox
            // 
            this.EnterIPBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.EnterIPBox.BackColor = System.Drawing.Color.GreenYellow;
            this.EnterIPBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.EnterIPBox.Font = new System.Drawing.Font("Comic Sans MS", 24F);
            this.EnterIPBox.ForeColor = System.Drawing.Color.Fuchsia;
            this.EnterIPBox.Location = new System.Drawing.Point(98, 63);
            this.EnterIPBox.Name = "EnterIPBox";
            this.EnterIPBox.Size = new System.Drawing.Size(367, 52);
            this.EnterIPBox.TabIndex = 1;
            this.EnterIPBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.EnterIPBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.keyPressed);
            // 
            // ErrorLabel
            // 
            this.ErrorLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.ErrorLabel.AutoSize = true;
            this.ErrorLabel.Font = new System.Drawing.Font("Comic Sans MS", 24F);
            this.ErrorLabel.ForeColor = System.Drawing.Color.Red;
            this.ErrorLabel.Location = new System.Drawing.Point(43, 123);
            this.ErrorLabel.Name = "ErrorLabel";
            this.ErrorLabel.Size = new System.Drawing.Size(497, 45);
            this.ErrorLabel.TabIndex = 2;
            this.ErrorLabel.Text = "Could not connect to the server";
            this.ErrorLabel.Visible = false;
            // 
            // ExitBut
            // 
            this.ExitBut.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.ExitBut.BackColor = System.Drawing.Color.LightCoral;
            this.ExitBut.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ExitBut.Font = new System.Drawing.Font("Comic Sans MS", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ExitBut.ForeColor = System.Drawing.Color.Maroon;
            this.ExitBut.Location = new System.Drawing.Point(88, 186);
            this.ExitBut.Name = "ExitBut";
            this.ExitBut.Size = new System.Drawing.Size(180, 55);
            this.ExitBut.TabIndex = 3;
            this.ExitBut.Text = "Exit";
            this.ExitBut.UseVisualStyleBackColor = false;
            this.ExitBut.Click += new System.EventHandler(this.ExitBut_Click);
            // 
            // EnterBut
            // 
            this.EnterBut.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.EnterBut.BackColor = System.Drawing.Color.DarkGreen;
            this.EnterBut.Cursor = System.Windows.Forms.Cursors.Hand;
            this.EnterBut.Font = new System.Drawing.Font("Comic Sans MS", 24F);
            this.EnterBut.ForeColor = System.Drawing.Color.LightSeaGreen;
            this.EnterBut.Location = new System.Drawing.Point(295, 186);
            this.EnterBut.Name = "EnterBut";
            this.EnterBut.Size = new System.Drawing.Size(180, 55);
            this.EnterBut.TabIndex = 4;
            this.EnterBut.Text = "Enter";
            this.EnterBut.UseVisualStyleBackColor = false;
            this.EnterBut.Click += new System.EventHandler(this.EnterBut_Click);
            // 
            // EnterIPScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.GreenYellow;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(571, 252);
            this.Controls.Add(this.EnterBut);
            this.Controls.Add(this.ExitBut);
            this.Controls.Add(this.ErrorLabel);
            this.Controls.Add(this.EnterIPBox);
            this.Controls.Add(this.EnterIPLabel);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "EnterIPScreen";
            this.Text = "EnterIPScreen";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label EnterIPLabel;
        private System.Windows.Forms.TextBox EnterIPBox;
        private System.Windows.Forms.Label ErrorLabel;
        private System.Windows.Forms.Button ExitBut;
        private System.Windows.Forms.Button EnterBut;
    }
}