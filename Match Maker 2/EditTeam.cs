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
    public partial class EditTeam : Form
    {
        public EditTeam()
        {
            InitializeComponent();

            LoadTeamData.loadTeamData();
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
            foreach (Team value in Program.itemList)
            {
                listBox1.Items.Add(value.TeamName);
            }
            listBox1.Update();
        }
        // cancel button
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        // done button
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                Program.currentTeamName = listBox1.SelectedItem.ToString();
                foreach (Team value in Program.itemList)
                {
                    if (value.TeamName == Program.currentTeamName)
                    {
                        value.CantPlay.Clear();
                        if (checkBox1.Checked && Program.settingsList[0].GameTime != null)
                        {
                            value.CantPlay.Add(Program.settingsList[0].GameTime);
                        }
                        if (checkBox2.Checked && Program.settingsList[1].GameTime != null)
                        {
                            value.CantPlay.Add(Program.settingsList[1].GameTime);
                        }
                        if (checkBox3.Checked && Program.settingsList[2].GameTime != null)
                        {
                            value.CantPlay.Add(Program.settingsList[2].GameTime);
                        }
                        if (checkBox4.Checked && Program.settingsList[3].GameTime != null)
                        {
                            value.CantPlay.Add(Program.settingsList[3].GameTime);
                        }
                        if (checkBox5.Checked && Program.settingsList[4].GameTime != null)
                        {
                            value.CantPlay.Add(Program.settingsList[4].GameTime);
                        }
                        if (checkBox6.Checked && Program.settingsList[5].GameTime != null)
                        {
                            value.CantPlay.Add(Program.settingsList[5].GameTime);
                        }
                        if (checkBox7.Checked && Program.settingsList[6].GameTime != null)
                        {
                            value.CantPlay.Add(Program.settingsList[6].GameTime);
                        }
                        if (checkBox8.Checked && Program.settingsList[7].GameTime != null)
                        {
                            value.CantPlay.Add(Program.settingsList[7].GameTime);
                        }
                        WriteBackData.writeBackData();
                    }
                }
                this.Close();
            }
            
        }
        // edit another team button
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Program.currentTeamName = listBox1.SelectedItem.ToString();
            foreach (Team value in Program.itemList)
            {
                if (value.TeamName == Program.currentTeamName)
                {
                    value.CantPlay.Clear();
                    if (checkBox1.Checked && Program.settingsList[0].GameTime != null)
                    {
                        value.CantPlay.Add(Program.settingsList[0].GameTime);
                    }
                    if (checkBox2.Checked && Program.settingsList[1].GameTime != null)
                    {
                        value.CantPlay.Add(Program.settingsList[1].GameTime);
                    }
                    if (checkBox3.Checked && Program.settingsList[2].GameTime != null)
                    {
                        value.CantPlay.Add(Program.settingsList[2].GameTime);
                    }
                    if (checkBox4.Checked && Program.settingsList[3].GameTime != null)
                    {
                        value.CantPlay.Add(Program.settingsList[3].GameTime);
                    }
                    if (checkBox5.Checked && Program.settingsList[4].GameTime != null)
                    {
                        value.CantPlay.Add(Program.settingsList[4].GameTime);
                    }
                    if (checkBox6.Checked && Program.settingsList[5].GameTime != null)
                    {
                        value.CantPlay.Add(Program.settingsList[5].GameTime);
                    }
                    if (checkBox7.Checked && Program.settingsList[6].GameTime != null)
                    {
                        value.CantPlay.Add(Program.settingsList[6].GameTime);
                    }
                    if (checkBox8.Checked && Program.settingsList[7].GameTime != null)
                    {
                        value.CantPlay.Add(Program.settingsList[7].GameTime);
                    }
                    WriteBackData.writeBackData();
                }

            }
            this.Close();
            EditTeam editTeam = new EditTeam();
            editTeam.ShowDialog();
        }

        private void listBox1_Click(object sender, EventArgs e)
        {
            checkBox1.Checked = false;
            checkBox2.Checked = false;
            checkBox3.Checked = false;
            checkBox4.Checked = false;
            checkBox5.Checked = false;
            checkBox6.Checked = false;
            checkBox7.Checked = false;
            checkBox8.Checked = false;
            foreach (Team value in Program.itemList)
            {
                if (value.TeamName == listBox1.SelectedItem.ToString())
                {
                    foreach (string str in value.CantPlay)
                    {
                        if (str == checkBox1.Text)
                        {
                            checkBox1.Checked = true;
                        }
                        if (str == checkBox2.Text)
                        {
                            checkBox2.Checked = true;
                        }
                        if (str == checkBox3.Text)
                        {
                            checkBox3.Checked = true;
                        }
                        if (str == checkBox4.Text)
                        {
                            checkBox4.Checked = true;
                        }
                        if (str == checkBox5.Text)
                        {
                            checkBox5.Checked = true;
                        }
                        if (str == checkBox6.Text)
                        {
                            checkBox6.Checked = true;
                        }
                        if (str == checkBox7.Text)
                        {
                            checkBox7.Checked = true;
                        }
                        if (str == checkBox8.Text)
                        {
                            checkBox8.Checked = true;
                        }
                    }
                }
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            checkBox1.Checked = false;
            checkBox2.Checked = false;
            checkBox3.Checked = false;
            checkBox4.Checked = false;
            checkBox5.Checked = false;
            checkBox6.Checked = false;
            checkBox7.Checked = false;
            checkBox8.Checked = false;
            foreach (Team value in Program.itemList)
            {
                if (value.TeamName == listBox1.SelectedItem.ToString())
                {
                    foreach (string str in value.CantPlay)
                    {
                        if (str == checkBox1.Text)
                        {
                            checkBox1.Checked = true;
                        }
                        if (str == checkBox2.Text)
                        {
                            checkBox2.Checked = true;
                        }
                        if (str == checkBox3.Text)
                        {
                            checkBox3.Checked = true;
                        }
                        if (str == checkBox4.Text)
                        {
                            checkBox4.Checked = true;
                        }
                        if (str == checkBox5.Text)
                        {
                            checkBox5.Checked = true;
                        }
                        if (str == checkBox6.Text)
                        {
                            checkBox6.Checked = true;
                        }
                        if (str == checkBox7.Text)
                        {
                            checkBox7.Checked = true;
                        }
                        if (str == checkBox8.Text)
                        {
                            checkBox8.Checked = true;
                        }
                    }
                }
            }
        }
    }
}
