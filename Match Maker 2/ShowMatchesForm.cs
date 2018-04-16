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
    public partial class ShowMatchesForm : Form
    {
        public ShowMatchesForm()
        {
            InitializeComponent();
            foreach (Team value in Program.byeList)
            {
                listView1.Items.Add(value.TeamName);
            }
            label2.Text = "WEEK " + Program.week + " MATCHES";
            Program.matchUpFinal.Clear();
            List<MatchUp> matchUpListFinalSorted = Program.matchUpListFinal.OrderBy(o => o.Time).ToList();
            foreach (MatchUp value in matchUpListFinalSorted)
            {
                MatchUpFinal temp = new MatchUpFinal();
                temp.Time = value.Time;
                temp.TeamName = value.TeamName;
                temp.OpponentTeamName = value.OpponentTeamName;
                Program.matchUpFinal.Add(temp);
            }
            var source = new BindingSource();
            source.DataSource = Program.matchUpFinal;
            dataGridView1.DataSource = source;
            dataGridView1.AutoGenerateColumns = true;
            dataGridView1.Columns[0].Width = 100;
            dataGridView1.Columns[1].Width = 242;
            dataGridView1.Columns[2].Width = 60;
            dataGridView1.Columns[3].Width = 242;
            this.Controls.Add(dataGridView1);
            dataGridView1.Refresh();
        }
        

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
