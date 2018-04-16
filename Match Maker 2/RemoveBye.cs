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
    public partial class RemoveBye : Form
    {
        public RemoveBye()
        {
            InitializeComponent();
            // add teams with byes to list box
            Program.forcedByeList.Clear();
            
            if (File.Exists(@Program.filePath + Program.currentSport + "\\" + Program.currentLeague + "\\bye.csv"))
            {
                var filestream = new FileStream(@Program.filePath + Program.currentSport + "\\" + Program.currentLeague + "\\bye.csv", FileMode.Open,
                FileAccess.Read, FileShare.ReadWrite);
                var file = new StreamReader(filestream, Encoding.UTF8, true, 128);
                string lineOfText;
                while ((lineOfText = file.ReadLine()) != null)
                {
                    foreach (Team value in Program.itemList)
                    {
                        if (lineOfText == value.TeamName)
                        {
                            Program.forcedByeList.Add(value);
                        }
                    }
                }
                filestream.Close();
                foreach (Team value in Program.forcedByeList)
                {
                    listBox1.Items.Add(value.TeamName);
                }
                
                
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                Program.currentTeamName = listBox1.SelectedItem.ToString();
                List<Team> temp = new List<Team>();
                foreach (Team value in Program.forcedByeList)
                {
                    if (value.TeamName != Program.currentTeamName)
                    {
                        temp.Add(value);
                    }
                }
                Program.forcedByeList = temp;
                // write back bye file
                using (TextWriter tw = new StreamWriter(@Program.filePath + Program.currentSport + "\\" + Program.currentLeague + "\\bye.csv"))
                {
                    foreach (Team value in Program.forcedByeList)
                    {
                        tw.WriteLine(value.TeamName);
                    }
                }
                this.Close();
            }
        }
    }
}
