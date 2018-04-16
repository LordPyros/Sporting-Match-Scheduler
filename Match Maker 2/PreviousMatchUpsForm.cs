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
    public partial class PreviousMatchUpsForm : Form
    {
        public PreviousMatchUpsForm()
        {
            InitializeComponent();
            label1.Text = "WEEK " + Program.weekStats + " MATCHES";
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
                var source = new BindingSource();
                List<MatchUpFinal> matchUpListFinalSorted = Program.matchUpFinal.OrderBy(o => o.Time).ToList();
                source.DataSource = matchUpListFinalSorted;
                dataGridView1.DataSource = source;
                dataGridView1.AutoGenerateColumns = true;
                dataGridView1.Columns[0].Width = 100;
                dataGridView1.Columns[1].Width = 242;
                dataGridView1.Columns[2].Width = 60;
                dataGridView1.Columns[3].Width = 242;
                this.Controls.Add(dataGridView1);
                dataGridView1.Refresh();
                filestream.Close();
            }
            // read previous byes file to listbox
            List<string> bye = new List<string>();
            if (File.Exists(@Program.filePath + Program.currentSport + "\\" + Program.currentLeague + "\\week" + Program.weekStats + "Byes.csv"))
            {
                //Reads csv file
                var filestream = new FileStream(@Program.filePath + Program.currentSport + "\\" + Program.currentLeague + "\\week" + Program.weekStats + "Byes.csv", FileMode.Open,
                FileAccess.Read, FileShare.ReadWrite);
                var file = new StreamReader(filestream, Encoding.UTF8, true, 128);

                while ((lineOfText = file.ReadLine()) != null)
                {
                    // add to main list
                    bye.Add(lineOfText);
                }
                foreach (string value in bye)
                {
                    listBox1.Items.Add(value);
                }
                filestream.Close();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (Program.weekStats != 1)
            {
                Program.weekStats--;
                PreviousMatchUpsForm previousMatchUpsForm2 = new PreviousMatchUpsForm();
                previousMatchUpsForm2.ShowDialog();
                this.Close();
            }     
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            if (Program.weekStats < Program.week)
            {
                Program.weekStats++;
                PreviousMatchUpsForm previousMatchUpsForm = new PreviousMatchUpsForm();
                previousMatchUpsForm.ShowDialog();
                this.Close();
            }
            
            
        }
    }
}
