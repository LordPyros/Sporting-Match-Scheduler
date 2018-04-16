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
    public partial class CreateNewLeague : Form
    {
        public CreateNewLeague()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            try 
	        {
                Program.currentLeague = textBox1.Text;
                Program.leagueNames.Add(Program.currentLeague);
                if (!Directory.Exists(@Program.filePath + Program.currentSport + "\\" + Program.currentLeague))
                {
                    Directory.CreateDirectory(@Program.filePath + Program.currentSport + "\\" + Program.currentLeague);
                }
                SaveLeagueList.saveLeagueList();
                this.Close();
                
            }
	        catch (Exception)
            {
                ErrorForm notString = new ErrorForm();
                notString.Show();
            }
            
            
            
        
        }

        

        private void pictureBox2_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
