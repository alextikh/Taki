using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace newGUI_Taki
{
    class EnemyPanel
    {
        public FlowLayoutPanel Panel
        {
            get;
            set;
        }
        public string Player
        {
            get;
            set;
        }
        public int NumCards
        {
            get;
            set;
        }

        public Label NameLabel
        {
            get;
            set;
        }

        public Label NumCardsLabel
        {
            get;
            set;
        }

        public EnemyPanel(FlowLayoutPanel panel, string player, int numCards, Label nameLabel, Label numCardsLabel)
        {
            this.Panel = panel;
            this.Player = player;
            this.NumCards = numCards;
            this.NameLabel = nameLabel;
            this.NumCardsLabel = numCardsLabel;
            this.NameLabel.Text = player;
            this.NumCardsLabel.Text = numCards.ToString();
        }
    }
}
