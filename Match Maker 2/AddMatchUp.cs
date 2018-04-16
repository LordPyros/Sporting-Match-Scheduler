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
    public partial class AddMatchUp : Form
    {
        public AddMatchUp()
        {
            InitializeComponent();
            ReadGameTimesFile.readGameTimesFile();
            LoadTeamData.loadTeamData();
            foreach (Settings value in Program.settingsList)
            {
                listBox3.Items.Add(value.GameTime);
            }
            foreach (Team value in Program.itemList)
            {
                listBox1.Items.Add(value.TeamName);
                listBox2.Items.Add(value.TeamName);
            }
        }
        // cancel button
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        // done button
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null && listBox2.SelectedItem != null && listBox3.SelectedItem != null && listBox1.SelectedItem != listBox2.SelectedItem)
            {
                Program.matchUpListFinal.Clear();
                // add games played, times played and played against to both teams
                foreach (Team value in Program.itemList)
                {
                    if (value.TeamName == listBox1.SelectedItem.ToString())
                    {
                        value.GamesPlayed++;
                        value.TimesPlayed.Add(listBox3.SelectedItem.ToString());
                        value.PlayedAgainst.Add(listBox2.SelectedItem.ToString());
                    }
                    if (value.TeamName == listBox2.SelectedItem.ToString())
                    {
                        value.GamesPlayed++;
                        value.TimesPlayed.Add(listBox3.SelectedItem.ToString());
                        value.PlayedAgainst.Add(listBox1.SelectedItem.ToString());
                    }
                }
                
                if (checkBox1.Checked)
                {
                    Program.week++;
                }
                if (File.Exists(@Program.filePath + Program.currentSport + "\\" + Program.currentLeague + "\\week" + Program.week + ".csv"))
                {

                    string lineOfText;
                    var filestream = new FileStream(@Program.filePath + Program.currentSport + "\\" + Program.currentLeague + "\\week" + Program.week + ".csv", FileMode.Open,
                        FileAccess.Read, FileShare.ReadWrite);
                    var file = new StreamReader(filestream, Encoding.UTF8, true, 128);
                    while ((lineOfText = file.ReadLine()) != null)
                    {
                        // splits the values        
                        MatchUp newItem = new MatchUp();
                        string[] elements = lineOfText.Split(',');
                        //assign values
                        newItem.Time = elements[0];
                        newItem.TeamName = elements[1];
                        newItem.OpponentTeamName = elements[2];

                        // add to main list
                        Program.matchUpListFinal.Add(newItem);
                    }
                    filestream.Close();
                }
                
                // make some kind of match record
                MatchUp temp = new MatchUp();
                temp.Time = listBox3.SelectedItem.ToString();
                temp.TeamName = listBox1.SelectedItem.ToString();
                temp.OpponentTeamName = listBox2.SelectedItem.ToString();
                Program.matchUpListFinal.Add(temp);
                WriteBackData.writeBackData();
                this.Close();
            }
            
        }
    }
}
