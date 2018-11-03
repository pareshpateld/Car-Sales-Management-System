using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Car_Sales_Management_System
{
    public partial class Employeepage : Form
    {
        SqlConnection scon;
        SqlCommand cmd;

        public void conn()
        {
            String con = Properties.Settings.Default.connection;
            scon = new SqlConnection(con);
            cmd = scon.CreateCommand();
            scon.Open();
        }

        public Employeepage()
        {
            InitializeComponent();
            menuStrip1.BackColor = Color.Transparent;
            pictureBox1.BackColor = Color.Transparent;
            groupBox2.BackColor = Color.Transparent;
            groupBox3.BackColor = Color.Transparent;
            label13.BackColor = Color.Transparent;
            label13.Visible = false;

            uname.Visible = true;
            uname.Text = uname.Text + LoginPage.Username;
            textBox2.Enabled = false;

        }

        public void getEmployee()
        {

            conn();

            string s = "select Firstname,Lastname,Position,Bdate,Commision from Employee where Username=@uname";

            cmd.CommandText = s;

            cmd.Parameters.AddWithValue("@uname", LoginPage.Username);

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBox1.Text = rd[0].ToString() + " " + rd[1].ToString();
                comboBox1.Text = rd[2].ToString();
                textBox2.Text = rd[3].ToString();
                textBox3.Text = rd[4].ToString();

            }

            scon.Close();
        }

        private void Employeepage_Load(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
            getEmployee();
        }

        private void customerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Homepage().Show();
            this.Hide();
        }

        private void carToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Carpage().Show();
            this.Hide();
        }

        private void employeeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            getEmployee();
        }

        private void invoiceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Invoicepage().Show();
            this.Hide();
        }

        private void serviceToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            new Servicespage().Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(pictureBox1, "Logout");
            
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            monthCalendar1.Show();
        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            textBox2.Text = monthCalendar1.SelectionStart.ToShortDateString();
        }

        private void monthCalendar1_MouseLeave(object sender, EventArgs e)
        {
            monthCalendar1.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            new LoginPage().Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {

            if (textBox1.Text == "")
            {
                errorProvider1.Clear();
                errorProvider1.SetError(textBox1, "Please Enter Employee Name");
            }
            else if (comboBox1.SelectedIndex == -1)
            {
                errorProvider1.Clear();
                errorProvider1.SetError(comboBox1, "Please Select Position");
            }
            else if (textBox2.Text == "")
            {
                errorProvider1.Clear();
                errorProvider1.SetError(textBox2, "Please Select Birthdate");
            }
            else if (textBox3.Text == "")
            {
                errorProvider1.Clear();
                errorProvider1.SetError(textBox3, "Please Enter Commission");
            }
            else
            {
                errorProvider1.Clear();
                conn();

                cmd.CommandText = " Update Employee SET Firstname=@fn,Position=@pos,Bdate=@bd,Commision=@com where Username=@uname";
                cmd.Parameters.AddWithValue("@fn", textBox1.Text);
                cmd.Parameters.AddWithValue("@pos", comboBox1.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@bd", textBox2.Text);
                cmd.Parameters.AddWithValue("@com", textBox3.Text);
                cmd.Parameters.AddWithValue("@uname", LoginPage.Username);

                cmd.ExecuteNonQuery();

                UpdateDone ud = new UpdateDone();
                ud.Show();
            }
        }

        private void foreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog dg = new ColorDialog();
            if (dg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                customerToolStripMenuItem.ForeColor = dg.Color;
                carToolStripMenuItem.ForeColor = dg.Color;
                employeeToolStripMenuItem.ForeColor = dg.Color;
                invoiceToolStripMenuItem.ForeColor = dg.Color;
                serviceToolStripMenuItem.ForeColor = dg.Color;
                uname.ForeColor = dg.Color;

            }
        }

        private void backgroundToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog dg = new ColorDialog();
            if (dg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                panel1.BackColor = dg.Color;


            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            Regex pattern = new Regex("^[A-Za-z\\s]+$");

            if (pattern.IsMatch(textBox1.Text))
            {

                textBox2.Focus();
                label13.Visible = false;
                textBox1.BackColor = Color.White;
                errorProvider1.Clear();


            }
            else
            {
                label13.Visible = true;
                label13.Text = "Please Enter Only String.";
                textBox1.Focus();
                textBox1.BackColor = Color.Pink;
            }
        }

        private void textBox3_MouseLeave(object sender, EventArgs e)
        {
            Regex pattern = new Regex("^[0-9]{1,1}$");

            if (pattern.IsMatch(textBox3.Text))
            {


                label13.Visible = false;
                textBox3.BackColor = Color.White;
                errorProvider1.Clear();


            }
            else
            {
                label13.Visible = true;
                label13.Text = "Please Enter Only Number";
                textBox3.Focus();
                textBox3.BackColor = Color.Pink;
            }
        }
    }
}
