namespace newGUI_Taki
{
    partial class ChangeColor
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
            this.butColorBlue = new System.Windows.Forms.Button();
            this.butColorRed = new System.Windows.Forms.Button();
            this.butColorYellow = new System.Windows.Forms.Button();
            this.butColorGreen = new System.Windows.Forms.Button();
            this.BackBut = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // butColorBlue
            // 
            this.butColorBlue.BackColor = System.Drawing.Color.Blue;
            this.butColorBlue.Cursor = System.Windows.Forms.Cursors.Hand;
            this.butColorBlue.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.butColorBlue.Location = new System.Drawing.Point(32, 9);
            this.butColorBlue.Name = "butColorBlue";
            this.butColorBlue.Size = new System.Drawing.Size(110, 110);
            this.butColorBlue.TabIndex = 0;
            this.butColorBlue.UseVisualStyleBackColor = false;
            this.butColorBlue.Click += new System.EventHandler(this.butColorBlue_Click);
            // 
            // butColorRed
            // 
            this.butColorRed.BackColor = System.Drawing.Color.Red;
            this.butColorRed.Cursor = System.Windows.Forms.Cursors.Hand;
            this.butColorRed.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.butColorRed.Location = new System.Drawing.Point(180, 145);
            this.butColorRed.Name = "butColorRed";
            this.butColorRed.Size = new System.Drawing.Size(110, 110);
            this.butColorRed.TabIndex = 1;
            this.butColorRed.UseVisualStyleBackColor = false;
            this.butColorRed.Click += new System.EventHandler(this.butColorRed_Click);
            // 
            // butColorYellow
            // 
            this.butColorYellow.BackColor = System.Drawing.Color.Yellow;
            this.butColorYellow.Cursor = System.Windows.Forms.Cursors.Hand;
            this.butColorYellow.Location = new System.Drawing.Point(180, 9);
            this.butColorYellow.Name = "butColorYellow";
            this.butColorYellow.Size = new System.Drawing.Size(110, 110);
            this.butColorYellow.TabIndex = 2;
            this.butColorYellow.UseVisualStyleBackColor = false;
            this.butColorYellow.Click += new System.EventHandler(this.butColorYellow_Click);
            // 
            // butColorGreen
            // 
            this.butColorGreen.BackColor = System.Drawing.Color.Lime;
            this.butColorGreen.Cursor = System.Windows.Forms.Cursors.Hand;
            this.butColorGreen.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.butColorGreen.Location = new System.Drawing.Point(32, 145);
            this.butColorGreen.Name = "butColorGreen";
            this.butColorGreen.Size = new System.Drawing.Size(110, 110);
            this.butColorGreen.TabIndex = 3;
            this.butColorGreen.UseVisualStyleBackColor = false;
            this.butColorGreen.Click += new System.EventHandler(this.butColorGreen_Click);
            // 
            // BackBut
            // 
            this.BackBut.BackColor = System.Drawing.Color.Orchid;
            this.BackBut.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BackBut.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BackBut.Font = new System.Drawing.Font("Comic Sans MS", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BackBut.ForeColor = System.Drawing.Color.Maroon;
            this.BackBut.Location = new System.Drawing.Point(101, 272);
            this.BackBut.Name = "BackBut";
            this.BackBut.Size = new System.Drawing.Size(118, 51);
            this.BackBut.TabIndex = 4;
            this.BackBut.Text = "Back";
            this.BackBut.UseVisualStyleBackColor = false;
            this.BackBut.Click += new System.EventHandler(this.BackBut_Click);
            // 
            // ChangeColor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SkyBlue;
            this.ClientSize = new System.Drawing.Size(315, 335);
            this.Controls.Add(this.BackBut);
            this.Controls.Add(this.butColorGreen);
            this.Controls.Add(this.butColorYellow);
            this.Controls.Add(this.butColorRed);
            this.Controls.Add(this.butColorBlue);
            this.Name = "ChangeColor";
            this.Text = "CangeColor";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button butColorBlue;
        private System.Windows.Forms.Button butColorRed;
        private System.Windows.Forms.Button butColorYellow;
        private System.Windows.Forms.Button butColorGreen;
        private System.Windows.Forms.Button BackBut;
    }
}