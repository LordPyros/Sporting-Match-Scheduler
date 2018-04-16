using System;
using System.Windows.Forms;

namespace Match_Maker_2
{
    public partial class AddTeam : Form
    {
        public AddTeam()
        {
            InitializeComponent();
            DisplayGameTimeCheckboxes();
        }
        
        // Done Button
        private void doneBtn_Click(object sender, EventArgs e)
        {
            // Checks the new team name is not null or already exists
            // Adds a new team
            if (textBox1.Text != "")
            {
                if (!TeamAlreadyExists())
                {
                    AddNewTeam();
                    Close();
                }
            }
        }

        // Cancel Button
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Close();
        }

        // Add another team button
        private void addAnotherTeamBtn_Click(object sender, EventArgs e)
        {
            // Checks the new team name is not null or already exists
            // then adds new team, closes and reopens the form
            if (textBox1.Text != "")
            {
                if (!TeamAlreadyExists())
                {
                    AddNewTeam();
                    Close();
                    AddTeam addTeam = new AddTeam();
                    addTeam.ShowDialog();
                }
            }
        }

        private void DisplayGameTimeCheckboxes()
        {
            // Checks how many game times exist and displays a checkbox for each time
            // (So that the user can select times which the new team is unable to play)

            ReadGameTimesFile.readGameTimesFile();
            int i = Program.settingsList.Count;
            if (i > 0)
            {
                checkBox1.Text = Program.settingsList[0].GameTime;
            }
            else
            {
                checkBox1.Visible = false;
            }
            if (i >= 2)
            {
                checkBox2.Text = Program.settingsList[1].GameTime;
            }
            else
            {
                checkBox2.Visible = false;
            }
            if (i >= 3)
            {
                checkBox3.Text = Program.settingsList[2].GameTime;
            }
            else
            {
                checkBox3.Visible = false;
            }
            if (i >= 4)
            {
                checkBox4.Text = Program.settingsList[3].GameTime;
            }
            else
            {
                checkBox4.Visible = false;
            }
            if (i >= 5)
            {
                checkBox5.Text = Program.settingsList[4].GameTime;
            }
            else
            {
                checkBox5.Visible = false;
            }
            if (i >= 6)
            {
                checkBox6.Text = Program.settingsList[5].GameTime;
            }
            else
            {
                checkBox6.Visible = false;
            }
            if (i >= 7)
            {
                checkBox7.Text = Program.settingsList[6].GameTime;
            }
            else
            {
                checkBox7.Visible = false;
            }
            if (i >= 8)
            {
                checkBox8.Text = Program.settingsList[7].GameTime;
            }
            else
            {
                checkBox8.Visible = false;
            }
        }

        private void AddNewTeam()
        {
            // creates a new team and adds can't play times (if any)
            try
            {
                Program.currentTeamName = textBox1.Text.ToString();
                Team value = new Team();
                value.TeamName = Program.currentTeamName;
                if (checkBox1.Checked)
                {
                    value.CantPlay.Add(Program.settingsList[0].GameTime);
                }
                if (checkBox2.Checked)
                {
                    value.CantPlay.Add(Program.settingsList[1].GameTime);
                }
                if (checkBox3.Checked)
                {
                    value.CantPlay.Add(Program.settingsList[2].GameTime);
                }
                if (checkBox4.Checked)
                {
                    value.CantPlay.Add(Program.settingsList[4].GameTime);
                }
                if (checkBox5.Checked)
                {
                    value.CantPlay.Add(Program.settingsList[4].GameTime);
                }
                if (checkBox6.Checked)
                {
                    value.CantPlay.Add(Program.settingsList[5].GameTime);
                }
                if (checkBox7.Checked)
                {
                    value.CantPlay.Add(Program.settingsList[6].GameTime);
                }
                if (checkBox8.Checked)
                {
                    value.CantPlay.Add(Program.settingsList[7].GameTime);
                }
                Program.itemList.Add(value);
                WriteBackData.writeBackData();
                // Lsop still not added
            }
            catch (Exception)
            {
                // not a string

            }
        }

        private bool TeamAlreadyExists()
        {
            // check textbox text is not an existing team name
            LoadTeamData.loadTeamData();
            foreach (Team value in Program.itemList)
            {
                if (value.TeamName == textBox1.Text)
                    return true;
            }
            return false;
        }
    }
}
