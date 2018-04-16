using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
namespace Match_Maker_2
{
    public partial class RemoveTeam : Form
    {
        public RemoveTeam()
        {
            InitializeComponent();
            LoadTeamData.loadTeamData();
            foreach (Team value in Program.itemList)
            {
                listBox1.Items.Add(value.TeamName);
            }
            listBox1.Update();
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
                List<Team> temp = new List<Team>();
                foreach (Team value in Program.itemList)
                {
                    if (value.TeamName != Program.currentTeamName)
                    {
                        temp.Add(value);
                    }
                }
                File.Delete(@Program.filePath + Program.currentSport + "\\" + Program.currentLeague + "\\" + Program.currentTeamName + "PA.csv");
                File.Delete(@Program.filePath + Program.currentSport + "\\" + Program.currentLeague + "\\" + Program.currentTeamName + "TP.csv");
                File.Delete(@Program.filePath + Program.currentSport + "\\" + Program.currentLeague + "\\" + Program.currentTeamName + "CP.csp");
                Program.itemList = temp;
                WriteBackData.writeBackData();
                this.Close();
            }
        }
    }
}
