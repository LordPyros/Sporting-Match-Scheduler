using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Match_Maker_2
{
    public partial class RenameTeam : Form
    {
        public RenameTeam()
        {
            InitializeComponent();
            LoadTeamData.loadTeamData();
            foreach (Team value in Program.itemList)
            {
                listBox1.Items.Add(value.TeamName);
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                Program.currentTeamName = listBox1.SelectedItem.ToString();
                RenameTeam2 renameTeam2 = new RenameTeam2();
                renameTeam2.ShowDialog();
                this.Close();
            }
        }
    }
}
