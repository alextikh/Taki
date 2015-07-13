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
    public partial class RoomNameScreen : Form
    {
        public string RoomName
        {
            get;
            set;
        }
        public RoomNameScreen()
        {
            InitializeComponent();
            this.Text = "Enter room name windows";
        }

        private void RoomNameBox_Click(object sender, EventArgs e)
        {
            this.RoomNameBox.Text = "";
        }

        private void RoomNameBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                this.CreateRoomBut.PerformClick();
            }
        }

        private void CreateRoomBut_Click(object sender, EventArgs e)
        {
            this.RoomName = RoomNameBox.Text;
            this.Close();
        }

        private void BackBut_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
