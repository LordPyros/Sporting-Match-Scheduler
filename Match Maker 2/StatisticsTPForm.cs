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
    public partial class StatisticsTPForm : Form
    {
        public StatisticsTPForm()
        {
            InitializeComponent();
            // create 2d played against array
            int[,] matrix = new int[Program.numberOfTeams, Program.settingsList.Count];
            int count2 = 0;
            foreach (Team value in Program.itemList)
            {
                Program.currentTeamName = value.TeamName;
                foreach (string str in value.TimesPlayed)
                {
                    int count = 0;
                    foreach (Settings time in Program.settingsList)
                    {

                        if (str == time.GameTime)
                        {
                            matrix[count2, count]++;
                        }
                        count++;
                    }
                }
                count2++;
            }
            var rowCount = matrix.GetLength(0);
            var rowLength = matrix.GetLength(1);
            dataGridView1.ColumnCount = Program.settingsList.Count;
            //dataGridView1.RowCount = Program.numberOfTeams;

            // binds data
            for (int rowIndex = 0; rowIndex < rowCount; ++rowIndex)
            {
                var row = new DataGridViewRow();
                for (int columnIndex = 0; columnIndex < rowLength; ++columnIndex)
                {
                    row.Cells.Add(new DataGridViewTextBoxCell()
                    {
                        Value = matrix[rowIndex, columnIndex]
                    });
                }
                dataGridView1.Rows.Add(row);
            }

            // set column names
            int count3 = 0;
            foreach (Team value in Program.itemList)
            {
                
                dataGridView1.Rows[count3].HeaderCell.Value = value.TeamName;
                count3++;
            }
            count3 = 0;
            foreach (Settings set in Program.settingsList)
            {
                dataGridView1.Columns[count3].HeaderText = set.GameTime;
                count3++;
            }
            dataGridView1.RowHeadersWidth = 200;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            StatisticsPAForm statPA = new StatisticsPAForm();
            statPA.Show();
            this.Close();
        }
    }
}
