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
    public partial class AddDone : Form
    {
        public AddDone()
        {
            InitializeComponent();
            pictureBox1.BackColor = Color.Transparent;
            label1.BackColor = Color.Transparent;
        }

        private void AddDone_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
