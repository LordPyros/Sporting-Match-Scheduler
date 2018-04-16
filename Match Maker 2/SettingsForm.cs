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
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
            ReadGameTimesFile.readGameTimesFile();
            if (Program.settingsList.Count > 0)
            {
                textBox1.Text = Program.settingsList[0].GameTime;
                textBox2.Text = Program.settingsList[0].NumberOfGameAtThatTime.ToString();
            }
            if (Program.settingsList.Count >= 2)
            {
                textBox3.Text = Program.settingsList[1].GameTime;
                textBox4.Text = Program.settingsList[1].NumberOfGameAtThatTime.ToString();
            }
            if (Program.settingsList.Count >= 3)
            {
                textBox5.Text = Program.settingsList[2].GameTime;
                textBox6.Text = Program.settingsList[2].NumberOfGameAtThatTime.ToString();
            }
            if (Program.settingsList.Count >= 4)
            {
                textBox7.Text = Program.settingsList[3].GameTime;
                textBox8.Text = Program.settingsList[3].NumberOfGameAtThatTime.ToString();
            }
            if (Program.settingsList.Count >= 5)
            {
                textBox9.Text = Program.settingsList[4].GameTime;
                textBox10.Text = Program.settingsList[4].NumberOfGameAtThatTime.ToString();
            }
            if (Program.settingsList.Count >= 6)
            {
                textBox11.Text = Program.settingsList[5].GameTime;
                textBox12.Text = Program.settingsList[5].NumberOfGameAtThatTime.ToString();
            }
            if (Program.settingsList.Count >= 7)
            {
                textBox13.Text = Program.settingsList[6].GameTime;
                textBox14.Text = Program.settingsList[6].NumberOfGameAtThatTime.ToString();
            }
            if (Program.settingsList.Count >= 8)
            {
                textBox15.Text = Program.settingsList[7].GameTime;
                textBox16.Text = Program.settingsList[7].NumberOfGameAtThatTime.ToString();
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
            int fail = 0;
            try
            {
                
                List<Settings> tmpLst = new List<Settings>();
                if (textBox1.Text != "" && textBox2.Text == "" || textBox1.Text == "" && textBox2.Text != "")
                {
                    SettingsError settingsError = new SettingsError();
                    settingsError.ShowDialog();
                    fail++;
                }
                if (textBox1.Text != "" && textBox2.Text != "")
                {
                    Settings value = new Settings();
                    value.GameTime = textBox1.Text;
                    value.NumberOfGameAtThatTime = Int32.Parse(textBox2.Text);
                    tmpLst.Add(value);
                }
                if (fail == 0)
                {
                    if (textBox3.Text != "" && textBox4.Text == "" || textBox3.Text == "" && textBox4.Text != "")
                    {
                        SettingsError settingsError = new SettingsError();
                        settingsError.ShowDialog();
                        fail++;
                    }
                }
                if (textBox3.Text != "" && textBox4.Text != "")
                {
                    Settings value = new Settings();
                    value.GameTime = textBox3.Text;
                    value.NumberOfGameAtThatTime = Int32.Parse(textBox4.Text);
                    tmpLst.Add(value);
                }
                if (fail == 0)
                {
                    if (textBox5.Text != "" && textBox6.Text == "" || textBox5.Text == "" && textBox6.Text != "")
                    {
                        SettingsError settingsError = new SettingsError();
                        settingsError.ShowDialog();
                        fail++;
                    }
                }
                if (textBox5.Text != "" && textBox6.Text != "")
                {
                    Settings value = new Settings();
                    value.GameTime = textBox5.Text;
                    value.NumberOfGameAtThatTime = Int32.Parse(textBox6.Text);
                    tmpLst.Add(value);
                }
                if (fail == 0)
                {
                    if (textBox7.Text != "" && textBox8.Text == "" || textBox7.Text == "" && textBox8.Text != "")
                    {
                        SettingsError settingsError = new SettingsError();
                        settingsError.ShowDialog();
                        fail++;
                    }
                }
                if (textBox7.Text != "" && textBox8.Text != "")
                {
                    Settings value = new Settings();
                    value.GameTime = textBox7.Text;
                    value.NumberOfGameAtThatTime = Int32.Parse(textBox8.Text);
                    tmpLst.Add(value);
                }
                if (fail == 0)
                {
                    if (textBox9.Text != "" && textBox10.Text == "" || textBox9.Text == "" && textBox10.Text != "")
                    {
                        SettingsError settingsError = new SettingsError();
                        settingsError.ShowDialog();
                        fail++;
                    }
                }
                if (textBox9.Text != "" && textBox10.Text != "")
                {
                    Settings value = new Settings();
                    value.GameTime = textBox9.Text;
                    value.NumberOfGameAtThatTime = Int32.Parse(textBox10.Text);
                    tmpLst.Add(value);
                }
                if (fail == 0)
                {
                    if (textBox11.Text != "" && textBox12.Text == "" || textBox11.Text == "" && textBox12.Text != "")
                    {
                        SettingsError settingsError = new SettingsError();
                        settingsError.ShowDialog();
                        fail++;
                    }
                }
                if (textBox11.Text != "" && textBox12.Text != "")
                {
                    Settings value = new Settings();
                    value.GameTime = textBox11.Text;
                    value.NumberOfGameAtThatTime = Int32.Parse(textBox12.Text);
                    tmpLst.Add(value);
                }
                if (fail == 0)
                {
                    if (textBox13.Text != "" && textBox14.Text == "" || textBox13.Text == "" && textBox14.Text != "")
                    {
                        SettingsError settingsError = new SettingsError();
                        settingsError.ShowDialog();
                        fail++;
                    }
                }
                if (textBox13.Text != "" && textBox14.Text != "")
                {
                    Settings value = new Settings();
                    value.GameTime = textBox13.Text;
                    value.NumberOfGameAtThatTime = Int32.Parse(textBox14.Text);
                    tmpLst.Add(value);
                }
                if (fail == 0)
                {
                    if (textBox15.Text != "" && textBox16.Text == "" || textBox15.Text == "" && textBox16.Text != "")
                    {
                        SettingsError settingsError = new SettingsError();
                        settingsError.ShowDialog();
                        fail++;
                    }
                }
                if (textBox15.Text != "" && textBox16.Text != "")
                {
                    Settings value = new Settings();
                    value.GameTime = textBox15.Text;
                    value.NumberOfGameAtThatTime = Int32.Parse(textBox16.Text);
                    tmpLst.Add(value);
                }
                
                if (fail == 0)
                {
                    // check if a timeslot has been removed
                    // remove timeslot from cantplay files
                    LoadTeamData.loadTeamData();
                    foreach (Settings value in Program.settingsList)
                    {
                        if (value.GameTime != textBox1.Text && value.GameTime != textBox3.Text && value.GameTime != textBox5.Text && value.GameTime != textBox7.Text && value.GameTime != textBox9.Text && value.GameTime != textBox11.Text && value.GameTime != textBox13.Text && value.GameTime != textBox15.Text)
                        {
                            Program.currentGameTime = value.GameTime;
                            foreach (Team team in Program.itemList)
                            {
                                List<string> temp = new List<string>();
                                foreach (string str in team.CantPlay)
                                {
                                    if (str != Program.currentGameTime)
                                    {
                                        temp.Add(str);
                                    }
                                }
                                team.CantPlay = temp;
                            }
                        }
                    }
                    WriteBackData.writeBackData();
                    List<Settings> settingsTemp = tmpLst.OrderBy(o => o.GameTime).ToList();
                    Program.settingsList = settingsTemp;
                    WriteSettingsData.writeSettingsData();
                    this.Close();
                }
            }
            catch (Exception)
            {
                if (fail == 0)
                {
                    SettingsError settingsError = new SettingsError();
                    settingsError.ShowDialog();
                }
            }
            
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void textBox14_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
