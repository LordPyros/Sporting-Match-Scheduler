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
    public partial class RemoveLeague : Form
    {
        public RemoveLeague()
        {
            InitializeComponent();
            foreach (string value in Program.leagueNames)
            {
                listBox1.Items.Add(value);
            }
        }
        // cancel button
        private void pictureBox4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        // remove team button
        private void pictureBox5_Click(object sender, EventArgs e)
        {
            
            string temp = listBox1.SelectedItem.ToString();
            List<string> tempList = new List<string>();
            foreach (string value in Program.leagueNames)
            {
                if (value != temp)
                {
                    tempList.Add(value);
                }
            }
            Program.leagueNames = tempList;
            this.Close();
        }
    }
}
