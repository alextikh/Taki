using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;

namespace newGUI_Taki
{
    public partial class LobbyScreen : Form
    {
        private NetworkStream sock;
        private Form parent;
        private string admin_join;
        public LobbyScreen(Form parent, NetworkStream sock, string msg, string username)
        {
            this.sock = sock;
            this.parent = parent;
            InitializeComponent();
            updateRoomList(msg);
            this.Text = username;
            this.Icon = newGUI_Taki.Properties.Resources.TakiIcon;
        }

        private void updateRoomList(string msg)
        {
            int i = msg.IndexOf('|'), j, num_players, row_index;
            string is_open, name, admin;
            dgvRoomList.Rows.Clear();

            while (msg[i + 1] != '|')
            {
                msg.Substring(i + 1).Contains('|');
                j = msg.Substring(i + 1).IndexOf("|") + i + 1;
                name = msg.Substring(i + 1, j - (i + 1));
                i = j;
                j = msg.Substring(i + 1).IndexOf("|") + i + 1;
                admin = msg.Substring(i + 1, j - (i + 1));
                i = j;
                j = msg.Substring(i + 1).IndexOf("|") + i + 1;
                num_players = int.Parse(msg.Substring(i + 1, j - (i + 1)));
                i = j;
                j = msg.Substring(i + 1).IndexOf("|") + i + 1;
                msg.Substring(i + 1, j - (i + 1));
                if (msg.Substring(i + 1, j - (i + 1)) == status_code.ROOM_OPEN)
                {
                    is_open = "open";
                }
                else
                {
                    is_open = "close";
                }

                row_index = this.dgvRoomList.Rows.Add();
                this.dgvRoomList["name", row_index].Value = name;
                this.dgvRoomList["admin", row_index].Value = admin;
                this.dgvRoomList["players", row_index].Value = num_players.ToString();
                this.dgvRoomList["state", row_index].Value = is_open;
                i = j;
            }

            dgvRoomList.ClearSelection();
            dgvRoomList.CurrentCell = null;
        }

        private void dgvRoomList_selectionChanged(object sender, EventArgs e)
        {
            if (this.dgvRoomList.SelectedRows.Count > 0 && this.dgvRoomList.SelectedRows[0].Cells["admin"].Value != null)
            {
                this.admin_join = this.dgvRoomList.SelectedRows[0].Cells["admin"].Value.ToString();
            }
        }

        private void ExitBut_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void BackBut_Click(object sender, EventArgs e)
        {
            parent.Show();
            this.Close();
        }

        private void RefreshBut_Click(object sender, EventArgs e)
        {
            byte[] buffer;
            buffer = new ASCIIEncoding().GetBytes(String.Format("@{0}||", status_code.RM_ROOM_LIST));
            this.sock.Write(buffer, 0, buffer.Length);
            this.sock.Flush();
            buffer = new byte[status_code.MSG_LEN];
            int bytesRead = this.sock.Read(buffer, 0, status_code.MSG_LEN);
            string msg = new ASCIIEncoding().GetString(buffer, 0, bytesRead);
            if (msg.Contains(String.Format("@{0}|", status_code.PGM_CTR_ROOM_LIST)))
            {
                updateRoomList(msg);
            }
        }

        private void JoinBut_Click(object sender, EventArgs e)
        {
            byte[] buffer;
            buffer = new ASCIIEncoding().GetBytes(String.Format("@{0}|{1}||", status_code.RM_JOIN_GAME, this.admin_join));
            this.sock.Write(buffer, 0, buffer.Length);
            this.sock.Flush();
            buffer = new byte[status_code.MSG_LEN];
            int bytesRead = this.sock.Read(buffer, 0, status_code.MSG_LEN);
            string msg = new ASCIIEncoding().GetString(buffer, 0, bytesRead);
            if (msg.Contains(String.Format("@{0}|", status_code.PGM_SCC_GAME_JOIN)))
            {
                RoomScreen form = new RoomScreen(this, this.sock, false, this.Text); form.Show();
                this.Close();
            }
        }

        private void CreateRoomBut_Click(object sender, EventArgs e)
        {
            byte[] buffer;
            RoomNameScreen RoomNameForm = new RoomNameScreen();

            if (RoomNameForm.ShowDialog() == DialogResult.OK)
            {
                string name = RoomNameForm.RoomName;
                buffer = new ASCIIEncoding().GetBytes(String.Format("@{0}|{1}||", status_code.RM_CREATE_GAME, name));
                this.sock.Write(buffer, 0, buffer.Length);
                this.sock.Flush();
                buffer = new byte[status_code.MSG_LEN];
                int bytesRead = this.sock.Read(buffer, 0, status_code.MSG_LEN);
                string msg = new ASCIIEncoding().GetString(buffer, 0, bytesRead);
                if (msg.Contains(String.Format("@{0}|", status_code.PGM_SCC_GAME_CREATED)))
                {
                    RoomScreen form = new RoomScreen(this, sock, true, this.Text);
                    form.Show();
                    this.Hide();
                }
                else if (msg.Contains(String.Format("@{0}|", status_code.PGM_ERR_INFO_TOO_LONG)))
                {
                    MessageBox.Show("Name too long", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
