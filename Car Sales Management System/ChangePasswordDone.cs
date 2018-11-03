using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Car_Sales_Management_System
{
    public partial class ChangePasswordDone : Form
    {
        public ChangePasswordDone()
        {
            InitializeComponent();
            pictureBox1.BackColor = Color.Transparent;
            label1.BackColor = Color.Transparent;
        }

        private void ChangePasswordDone_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            ChangePassword cp = new ChangePassword();
            cp.Close();
           // Homepage hp = new Homepage();
            //hp.Show();
        }
    }
}
