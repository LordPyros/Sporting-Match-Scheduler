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
    public partial class RenameTeam2 : Form
    {
        public RenameTeam2()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                if (File.Exists(@Program.filePath + Program.currentSport + "\\" + Program.currentLeague + "\\" + Program.currentTeamName + "PA.csv"))
                {
                    File.Move(@Program.filePath + Program.currentSport + "\\" + Program.currentLeague + "\\" + Program.currentTeamName + "PA.csv", @Program.filePath + Program.currentSport + "\\" + Program.currentLeague + "\\" + textBox1.Text + "PA.csv");
                }
                if (File.Exists(@Program.filePath + Program.currentSport + "\\" + Program.currentLeague + "\\" + Program.currentTeamName + "TP.csv"))
                {
                    File.Move(@Program.filePath + Program.currentSport + "\\" + Program.currentLeague + "\\" + Program.currentTeamName + "TP.csv", @Program.filePath + Program.currentSport + "\\" + Program.currentLeague + "\\" + textBox1.Text + "TP.csv");
                }
                if (File.Exists(@Program.filePath + Program.currentSport + "\\" + Program.currentLeague + "\\" + Program.currentTeamName + "CP.csp"))
                {
                    File.Move(@Program.filePath + Program.currentSport + "\\" + Program.currentLeague + "\\" + Program.currentTeamName + "CP.csp", @Program.filePath + Program.currentSport + "\\" + Program.currentLeague + "\\" + textBox1.Text + "CP.csp");
                }
                if (File.Exists(@Program.filePath + Program.currentSport + "\\" + Program.currentLeague + "\\backup\\" + Program.currentTeamName + "PA.csv"))
                {
                    File.Move(@Program.filePath + Program.currentSport + "\\" + Program.currentLeague + "\\backup\\" + Program.currentTeamName + "PA.csv", @Program.filePath + Program.currentSport + "\\" + Program.currentLeague + "\\backup\\" + textBox1.Text + "PA.csv");
                }
                if (File.Exists(@Program.filePath + Program.currentSport + "\\" + Program.currentLeague + "\\backup\\" + Program.currentTeamName + "TP.csv"))
                {
                    File.Move(@Program.filePath + Program.currentSport + "\\" + Program.currentLeague + "\\backup\\" + Program.currentTeamName + "TP.csv", @Program.filePath + Program.currentSport + "\\" + Program.currentLeague + "\\backup\\" + textBox1.Text + "TP.csv");
                }
                if (File.Exists(@Program.filePath + Program.currentSport + "\\" + Program.currentLeague + "\\backup\\" + Program.currentTeamName + "CP.csp"))
                {
                    File.Move(@Program.filePath + Program.currentSport + "\\" + Program.currentLeague + "\\backup\\" + Program.currentTeamName + "CP.csp", @Program.filePath + Program.currentSport + "\\" + Program.currentLeague + "\\backup\\" + textBox1.Text + "CP.csp");
                }
                // address problem if team is on forced bye list
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
                }
                // check if team is in bye list
                // if so change team name
                foreach (Team value in Program.forcedByeList)
                {

                    if (value.TeamName == Program.currentTeamName)
                    {
                        value.TeamName = textBox1.Text.ToString();
                    }
                }
                using (TextWriter tw = new StreamWriter(@Program.filePath + Program.currentSport + "\\" + Program.currentLeague + "\\bye.csv"))
                {
                    foreach (Team value in Program.forcedByeList)
                    {
                        tw.WriteLine(value.TeamName);
                    }
                }
                foreach (Team value in Program.itemList)
                {
                    if (Program.currentTeamName == value.TeamName)
                    {
                        value.TeamName = textBox1.Text.ToString();
                    }
                }
                WriteBackData.writeBackData();

                this.Close();
                // still need to update teams file in backup with renamed teams name? meh
            }
        }
    }
}
