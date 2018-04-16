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
    public partial class SelectLeague : Form
    {
        public SelectLeague()
        {
            InitializeComponent();
            Program.leagueNames.Clear();
            string lineOfText;
            if (File.Exists(@Program.filePath + Program.currentSport + "\\Leagues.csv"))
            {
                //Reads csv file
                var filestream = new FileStream(@Program.filePath + Program.currentSport + "\\Leagues.csv", FileMode.Open,
                FileAccess.Read, FileShare.ReadWrite);
                var file = new StreamReader(filestream, Encoding.UTF8, true, 128);
                while ((lineOfText = file.ReadLine()) != null)
                {
                    Program.leagueNames.Add(lineOfText);
                }
                filestream.Close();
            }
            foreach (string value in Program.leagueNames)
            {
                listBox1.Items.Add(value);
            }
            listBox1.Update();

        }
        // main menu button
        private void pictureBox4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        // "create new league" button
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            CreateNewLeague createNewLeague = new CreateNewLeague();
            createNewLeague.FormClosing += new FormClosingEventHandler(this.CreateNewLeague_FormClosing);
            createNewLeague.ShowDialog();

        }
        private void CreateNewLeague_FormClosing(object sender, FormClosingEventArgs e)
        {
            listBox1.Items.Clear();
            foreach (string value in Program.leagueNames)
            {
                listBox1.Items.Add(value);
            }
            listBox1.Update();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            RemoveLeague removeLeague = new RemoveLeague();
            removeLeague.FormClosing += new FormClosingEventHandler(this.RemoveLeague_FormClosing);
            removeLeague.Show();
        }
        private void RemoveLeague_FormClosing(object sender, FormClosingEventArgs e)
        {
            listBox1.Items.Clear();
            foreach (string value in Program.leagueNames)
            {
                listBox1.Items.Add(value);
            }
            listBox1.Update();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            foreach (string value in Program.leagueNames)
            {
                listBox1.Items.Add(value);
            }
            listBox1.Update();
        }
        // Done Button
        private void pictureBox5_Click(object sender, EventArgs e)
        {
            Program.currentLeague = listBox1.SelectedItem.ToString();
            MainMatchMake mainMatchMake = new MainMatchMake();
            mainMatchMake.ShowDialog();

        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            Program.currentLeague = listBox1.SelectedItem.ToString();
            MainMatchMake mainMatchMake = new MainMatchMake();
            mainMatchMake.ShowDialog();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            RenameLeague renameLeague = new RenameLeague();
            renameLeague.ShowDialog();
        }
        
    }
}
