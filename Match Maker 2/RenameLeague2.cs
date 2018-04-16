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
    public partial class RenameLeague2 : Form
    {
        public RenameLeague2()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        // Done button
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            try
            {
                string temp = textBox1.Text;
                // can't continue until folder storage is designed
                // check name is not same as any other
            }
            catch (Exception)
            {
                // not a string
              
            }
        }
    }
}
