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
    public partial class RenameLeague : Form
    {
        public RenameLeague()
        {
            InitializeComponent();
        }
            

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                Program.currentLeague = listBox1.SelectedItem.ToString();
                RenameLeague2 renameLeague2 = new RenameLeague2();
                renameLeague2.ShowDialog();

            }
            else
            {
                // need not null error box
            }
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                Program.currentLeague = listBox1.SelectedItem.ToString();
                RenameLeague2 renameLeague2 = new RenameLeague2();
                renameLeague2.ShowDialog();

            }
            else
            {
                // need not null error box
            }
        }
    }
}
