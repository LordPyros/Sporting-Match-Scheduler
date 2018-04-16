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
    public partial class ConfirmNewSeason : Form
    {
        public ConfirmNewSeason()
        {
            InitializeComponent();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
            LoadTeamData.loadTeamData();
            foreach (Team value in Program.itemList)
            {
                value.LSOP = 0;
                value.GamesPlayed = 0;
                value.Byes = 0;
            }
            Program.week = 0;
            WriteBackData.writeBackData();
            // if not exist create a folder
            if (!Directory.Exists(@Program.filePath + Program.currentSport + "\\" + Program.currentLeague + "\\temp"))
            {
                Directory.CreateDirectory(@Program.filePath + Program.currentSport + "\\" + Program.currentLeague + "\\temp");
            }
            File.Copy(@Program.filePath + Program.currentSport + "\\" + Program.currentLeague + "\\teams.csv", @Program.filePath + Program.currentSport + "\\" + Program.currentLeague + "\\temp\\teams.csv");
            File.Copy(@Program.filePath + Program.currentSport + "\\" + Program.currentLeague + "\\settings.csv", @Program.filePath + Program.currentSport + "\\" + Program.currentLeague + "\\temp\\settings.csv");
            foreach (var sourceFilePath in Directory.GetFiles(@Program.filePath + Program.currentSport + "\\" + Program.currentLeague + "\\", "*.csp"))
            {
                string fileName = Path.GetFileName(sourceFilePath);
                string destinationFilePath = Path.Combine(@Program.filePath + Program.currentSport + "\\" + Program.currentLeague + "\\temp\\", fileName);

                System.IO.File.Copy(sourceFilePath, destinationFilePath, true);
            }
            Array.ForEach(Directory.GetFiles(@Program.filePath + Program.currentSport + "\\" + Program.currentLeague + "\\"), File.Delete);
            File.Copy(@Program.filePath + Program.currentSport + "\\" + Program.currentLeague + "\\temp\\teams.csv", @Program.filePath + Program.currentSport + "\\" + Program.currentLeague + "\\teams.csv");
            File.Copy(@Program.filePath + Program.currentSport + "\\" + Program.currentLeague + "\\temp\\settings.csv", @Program.filePath + Program.currentSport + "\\" + Program.currentLeague + "\\settings.csv");
            foreach (var sourceFilePath in Directory.GetFiles(@Program.filePath + Program.currentSport + "\\" + Program.currentLeague + "\\temp\\", "*.csp"))
            {
                string fileName = Path.GetFileName(sourceFilePath);
                string destinationFilePath = Path.Combine(@Program.filePath + Program.currentSport + "\\" + Program.currentLeague + "\\", fileName);

                System.IO.File.Copy(sourceFilePath, destinationFilePath, true);
            }
            if (Directory.Exists(@Program.filePath + Program.currentSport + "\\" + Program.currentLeague + "\\temp"))
            {
                Array.ForEach(Directory.GetFiles(@Program.filePath + Program.currentSport + "\\" + Program.currentLeague + "\\backup\\"), File.Delete);
            }

            System.IO.DirectoryInfo di = new DirectoryInfo(@Program.filePath + Program.currentSport + "\\" + Program.currentLeague + "\\temp\\");

            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }
            foreach (DirectoryInfo dir in di.GetDirectories())
            {
                dir.Delete(true);
            }


            // copy team and settings
            // del all .csv
            // copy back files
            this.Close();
        }
    }
}
