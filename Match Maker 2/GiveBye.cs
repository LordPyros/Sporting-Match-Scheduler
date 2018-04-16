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
    public partial class GiveBye : Form
    {
        public GiveBye()
        {
            InitializeComponent();
            LoadTeamData.loadTeamData();
            foreach (Team value in Program.itemList)
            {
                listBox1.Items.Add(value.TeamName);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Close();
        }
        
        // Done button
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            AddBye();
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            AddBye();
        }

        private void AddBye()
        {
            if (listBox1.SelectedItem != null)
            {
                // read bye file
                Program.forcedByeList.Clear();
                Program.currentTeamName = listBox1.SelectedItem.ToString();
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
                }
                // check if team already byed
                int bust = 0;
                foreach (Team item in Program.forcedByeList)
                {
                    if (item.TeamName == Program.currentTeamName)
                    {
                        bust++;
                    }
                }
                // add team to bye list
                if (bust == 0)
                {
                    foreach (Team value in Program.itemList)
                    {
                        if (value.TeamName == Program.currentTeamName)
                        {
                            Program.forcedByeList.Add(value);
                        }
                    }
                }
                // write back bye file
                using (TextWriter tw = new StreamWriter(@Program.filePath + Program.currentSport + "\\" + Program.currentLeague + "\\bye.csv"))
                {
                    foreach (Team value in Program.forcedByeList)
                    {
                        tw.WriteLine(value.TeamName);
                    }
                }
                Close();
            }
        }

    }
}
