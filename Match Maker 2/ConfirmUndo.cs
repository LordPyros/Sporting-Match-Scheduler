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
    public partial class ConfirmUndo : Form
    {
        public ConfirmUndo()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            // copy all files back from backup folder
            if (Directory.Exists(@Program.filePath + Program.currentSport + "\\" + Program.currentLeague + "\\backup"))
            {
                Array.ForEach(Directory.GetFiles(@Program.filePath + Program.currentSport + "\\" + Program.currentLeague + "\\"), File.Delete);
                string[] txtFiles = Directory.GetFiles(@Program.filePath + Program.currentSport + "\\" + Program.currentLeague + "\\backup\\", "*.csv");
                foreach (var item in txtFiles)
                {
                    File.Copy(item, Path.Combine(@Program.filePath + Program.currentSport + "\\" + Program.currentLeague + "\\", Path.GetFileName(item)));
                }
            }
            this.Close();
        }
    }
}
