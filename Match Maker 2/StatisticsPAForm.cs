using System;
using System.Windows.Forms;

namespace Match_Maker_2
{
    public partial class StatisticsPAForm : Form
    {
        public StatisticsPAForm()
        {
            InitializeComponent();
            // create 2d played against array
            int[,] matrix = new int[Program.itemList.Count, Program.itemList.Count];
            int count2 = 0;
            foreach (Team value in Program.itemList)
            {
                Program.currentTeamName = value.TeamName;
                foreach (string str in value.PlayedAgainst)
                {
                    int count = 0;
                    foreach (Team team in Program.itemList)
                    {

                        if (str == team.TeamName)
                        {
                            matrix[count,count2]++;
                        }
                        count++;
                    }
                }
                count2++;
            }
            var rowCount = matrix.GetLength(0);
            var rowLength = matrix.GetLength(1);
            dataGridView1.ColumnCount = Program.itemList.Count;
            
            // binds data to grid
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
                dataGridView1.Columns[count3].HeaderText = value.TeamName;
                dataGridView1.Rows[count3].HeaderCell.Value = value.TeamName;
                count3++;
            }
            dataGridView1.RowHeadersWidth = 200;
            //dataGridView1.Refresh();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
            StatisticsTPForm statTP = new StatisticsTPForm();
            statTP.Show();
            
        }
        
    }
}
