using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace newGUI_Taki
{
    public partial class ChangeColor : Form
    {
        public string returnVal { get; set; }
        public ChangeColor()
        {
            this.Text = "Change coloe windows";
            InitializeComponent();
        }
        private void butColorRed_Click(object sender, EventArgs e)
        {
            this.returnVal = "r";
            this.Close();
        }

        private void butColorYellow_Click(object sender, EventArgs e)
        {
            this.returnVal = "y";
            this.Close();
        }

        private void butColorGreen_Click(object sender, EventArgs e)
        {
            this.returnVal = "g";
            this.Close();
        }

        private void butColorBlue_Click(object sender, EventArgs e)
        {
            this.returnVal = "b";
            this.Close();
        }

        private void BackBut_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
