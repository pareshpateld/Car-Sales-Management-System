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
    public partial class ChangePassword : Form
    {
        public ChangePassword()
        {
            InitializeComponent();
            label2.BackColor = Color.Transparent;
            label4.BackColor = Color.Transparent;
            label5.BackColor = Color.Transparent;

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text == "")
            {
                errorProvider1.Clear();
                errorProvider1.SetError(textBox1, "Please Enter Old Password");
            }
            else if(textBox2.Text == "")
            {
                errorProvider1.Clear();
                errorProvider1.SetError(textBox2, "Please Enter New Password");
            }
            else if (textBox3.Text == "")
            {
                errorProvider1.Clear();
                errorProvider1.SetError(textBox3, "Please Confirm New Password");
            }
            else
            {
                errorProvider1.Clear();
                ChangePasswordDone cp = new ChangePasswordDone();
                cp.Show();
                this.Close();
            }
            
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
