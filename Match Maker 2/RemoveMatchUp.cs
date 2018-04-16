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
    public partial class RemoveMatchUp : Form
    {
        public RemoveMatchUp()
        {
            InitializeComponent();
            LoadTeamData.loadTeamData();
            label2.Text = "WEEK " + Program.weekStats + " MATCHES";
            // read previous matches file to datagridview
            Program.matchUpFinal.Clear();
            string lineOfText;
            if (File.Exists(@Program.filePath + Program.currentSport + "\\" + Program.currentLeague + "\\week" + Program.weekStats + ".csv"))
            {
                //Reads csv file
                var filestream = new FileStream(@Program.filePath + Program.currentSport + "\\" + Program.currentLeague + "\\week" + Program.weekStats + ".csv", FileMode.Open,
                FileAccess.Read, FileShare.ReadWrite);
                var file = new StreamReader(filestream, Encoding.UTF8, true, 128);

                while ((lineOfText = file.ReadLine()) != null)
                {
                    // splits the values        
                    MatchUpFinal newItem = new MatchUpFinal();
                    string[] elements = lineOfText.Split(',');
                    //assign values
                    newItem.Time = elements[0];
                    newItem.TeamName = elements[1];
                    newItem.OpponentTeamName = elements[2];

                    // add to main list
                    Program.matchUpFinal.Add(newItem);
                }
                filestream.Close();
                List<MatchUpFinal> temp = Program.matchUpFinal.OrderBy(o => o.Time).ToList();
                foreach (MatchUpFinal value in temp)
                {
                    listBox1.Items.Add(value.Time + " - " + value.TeamName + " VS " + value.OpponentTeamName);
                }
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
            if (listBox1.SelectedItem != null)
            {
                List<MatchUpFinal> temp = new List<MatchUpFinal>();
                foreach (MatchUpFinal value in Program.matchUpFinal)
                {
                    if (listBox1.SelectedItem.ToString() != (value.Time + " - " + value.TeamName + " VS " + value.OpponentTeamName))
                    {
                        temp.Add(value);   
                    }
                }
                Program.matchUpFinal = temp;
                // write matchup file

                if (Program.week != 0)
                {
                    using (TextWriter tw = new StreamWriter(@Program.filePath + Program.currentSport + "\\" + Program.currentLeague + "\\week" + Program.weekStats + ".csv"))
                    {
                        foreach (MatchUpFinal value in Program.matchUpFinal)
                        {
                            tw.WriteLine(value.Time + "," + value.TeamName + "," + value.OpponentTeamName);
                        }
                    }
                    this.Close();
                }
            }
        }
        // previous week button
        private void pictureBox5_Click(object sender, EventArgs e)
        {
            if (Program.weekStats != 1)
            {
                Program.weekStats--;
                RemoveMatchUp removeMatchUp = new RemoveMatchUp();
                removeMatchUp.ShowDialog();
                this.Close();
            }
        }
        // next week button
        private void pictureBox4_Click(object sender, EventArgs e)
        {
            if (Program.weekStats < Program.week)
            {
                Program.weekStats++;
                RemoveMatchUp removeMatchUp = new RemoveMatchUp();
                removeMatchUp.ShowDialog();
                this.Close();
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                List<MatchUpFinal> temp = new List<MatchUpFinal>();
                foreach (MatchUpFinal value in Program.matchUpFinal)
                {
                    if (listBox1.SelectedItem.ToString() != (value.Time + " - " + value.TeamName + " VS " + value.OpponentTeamName))
                    {
                        temp.Add(value);
                    }
                }
                Program.matchUpFinal = temp;
                // write matchup file

                if (Program.week != 0)
                {
                    using (TextWriter tw = new StreamWriter(@Program.filePath + Program.currentSport + "\\" + Program.currentLeague + "\\week" + Program.weekStats + ".csv"))
                    {
                        foreach (MatchUpFinal value in Program.matchUpFinal)
                        {
                            tw.WriteLine(value.Time + "," + value.TeamName + "," + value.OpponentTeamName);
                        }
                    }
                }
                RemoveMatchUp removeMatchUp = new RemoveMatchUp();
                removeMatchUp.ShowDialog();
                this.Close();
            }
        }
    }
}
