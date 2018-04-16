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
using Match_Maker_2.Properties;
namespace Match_Maker_2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();  
            
            Program.filePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\mm2\\"; 
             
        }

        

        private void pictureBox3_MouseEnter(object sender, EventArgs e)
        {
            pictureBox3.Image = Resources.cricket2;
        }
        private void pictureBox3_MouseLeave(object sender, EventArgs e)
        {
            pictureBox3.Image = Resources.cricket1;
        }
        private void pictureBox4_MouseEnter(object sender, EventArgs e)
        {
            pictureBox4.Image = Resources.soccer2;
        }
        private void pictureBox4_MouseLeave(object sender, EventArgs e)
        {
            pictureBox4.Image = Resources.soccer1;
        }
        private void pictureBox2_MouseEnter(object sender, EventArgs e)
        {
            pictureBox2.Image = Resources.netball2;
        }
        private void pictureBox2_MouseLeave(object sender, EventArgs e)
        {
            pictureBox2.Image = Resources.netball1;
        }
        // soccer
        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Program.currentSport = "soccer";
            SelectLeague selectLeague = new SelectLeague();
            selectLeague.ShowDialog();
            if (!Directory.Exists(@Program.filePath + Program.currentSport))
            {
                Directory.CreateDirectory(@Program.filePath + Program.currentSport);
            }
            OpenLeagueList();
        }
        // netball
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Program.currentSport = "netball";
            SelectLeague selectLeague = new SelectLeague();
            selectLeague.ShowDialog();
            if (!Directory.Exists(@Program.filePath + Program.currentSport))
            {
                Directory.CreateDirectory(@Program.filePath + Program.currentSport);
            }
            OpenLeagueList();
        }
        // cricket
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Program.currentSport = "cricket";
            SelectLeague selectLeague = new SelectLeague();
            selectLeague.ShowDialog();
            if (!Directory.Exists(@Program.filePath + Program.currentSport))
            {
                Directory.CreateDirectory(@Program.filePath + Program.currentSport);
            }
            OpenLeagueList();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        void OpenLeagueList()
        {
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
        }
        
    }

}
