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
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace Car_Sales_Management_System
{
    public partial class Servicespage : Form
    {
        SqlConnection scon;
        SqlCommand cmd, cmd2;

        public void conn()
        {
            String con = Properties.Settings.Default.connection;
            scon = new SqlConnection(con);
            cmd = scon.CreateCommand();
            cmd2 = scon.CreateCommand();
            scon.Open();
        }


        public Servicespage()
        {
            InitializeComponent();
            menuStrip1.BackColor = Color.Transparent;
            groupBox1.BackColor = Color.Transparent;
            groupBox2.BackColor = Color.Transparent;
            groupBox3.BackColor = Color.Transparent;
            label13.BackColor = Color.Transparent;
            label13.Visible = false;

            uname.Visible = true;
            uname.Text = uname.Text + LoginPage.Username;
        }

        private void Servicespage_Load(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
            monthCalendar1.Visible = false;
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
            new Employeepage().Show();
            this.Hide();
        }

        private void invoiceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Invoicepage().Show();
            this.Hide();
        }

        private void serviceToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            if(saveFileDialog1.ShowDialog()==System.Windows.Forms.DialogResult.OK)
            {
                File.WriteAllText(saveFileDialog1.FileName, textBox1.Text+Environment.NewLine+textBox2.Text);
            }
            SaveDone sd = new SaveDone();
            sd.Show();
        }

        private void pictureBox1_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(pictureBox1, "Logout");
        }


        private void pictureBox1_Click(object sender, EventArgs e)
        {
            new LoginPage().Show();
            this.Hide();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                errorProvider1.Clear();
                errorProvider1.SetError(textBox1, "Please Enter Serial Number");
            }
            else if (textBox2.Text == "")
            {
                errorProvider1.Clear();
                errorProvider1.SetError(textBox2, "Please Select Service Date");
            }
            else if (textBox3.Text == "")
            {
                errorProvider1.Clear();
                errorProvider1.SetError(textBox3, "Please Enter Customer Name");
            }
            else if (textBox4.Text == "")
            {
                errorProvider1.Clear();
                errorProvider1.SetError(textBox4, "Please Enter Parts Cost");
            }
            else if (textBox5.Text == "")
            {
                errorProvider1.Clear();
                errorProvider1.SetError(textBox5, "Please Enter Labor Cost");
            }
            else if (textBox6.Text == "")
            {
                errorProvider1.Clear();
                errorProvider1.SetError(textBox6, "Please Enter Total Price");
            }
            else if (richTextBox2.Text == "")
            {
                errorProvider1.Clear();
                errorProvider1.SetError(richTextBox2, "Please Enter Service Description");
            }
            else
            {
                errorProvider1.Clear();
                conn();

                double taxPercent = 1.13;

                double partscost = double.Parse(textBox4.Text);
                double laborcost = double.Parse(textBox5.Text);

                double total = (partscost + laborcost) * taxPercent;

                double tax = (partscost + laborcost) * (taxPercent - 1);

                cmd2.CommandText = "select Carid,Cid from Car where Serial=@serial";
                cmd2.Parameters.AddWithValue("@serial", textBox1.Text);


                string Cid = "";
                string Carid = "";
                SqlDataReader dr = cmd2.ExecuteReader();
                while (dr.Read())
                {
                    Carid = dr[0].ToString();
                    Cid = dr[1].ToString();
                }
                dr.Close();
                cmd2.ExecuteNonQuery();

                string s = "insert into Service(Cid,Carid,Serdate,Cname,Serial,Partscost,Laborcost,Tax,Totalcost,Description)values(@cid,@carid,@sd,@cn,@serno,@pc,@lc,@tax,@total,@desc)";


                cmd.CommandText = s;

                cmd.Parameters.AddWithValue("@carid", Carid);
                cmd.Parameters.AddWithValue("@cid", Cid);
                cmd.Parameters.AddWithValue("@serno", textBox1.Text);
                cmd.Parameters.AddWithValue("@sd", textBox2.Text);
                cmd.Parameters.AddWithValue("@cn", textBox3.Text);
                cmd.Parameters.AddWithValue("@pc", textBox4.Text);
                cmd.Parameters.AddWithValue("@lc", textBox5.Text);
                cmd.Parameters.AddWithValue("@tax", tax);
                cmd.Parameters.AddWithValue("@total", total);
                cmd.Parameters.AddWithValue("@desc", richTextBox2.Text);

                cmd.ExecuteNonQuery();

                scon.Close();

                richTextBox1.Text = richTextBox1.Text + " Serial No:" + textBox1.Text +
                                    "\n Service Date:" + textBox2.Text +
                                    "\n Customer Name:" + textBox3.Text +
                                    "\n Parts Cost:" + textBox4.Text +
                                    "\n Labor Cost:" + textBox5.Text +
                                    "\n Service Price:" + total +
                                    "\n Service Description:" + richTextBox2.Text;


                //MessageBox.Show("Service queued!!!!");
                AddDone ad = new AddDone();
                ad.Show();

                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                textBox4.Clear();
                textBox5.Clear();
                textBox6.Clear();

                richTextBox2.Clear();
                richTextBox1.Clear();

            }

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
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

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                errorProvider1.Clear();
                errorProvider1.SetError(textBox1, "Please Enter Serial Number");
            }
            else
            {
                errorProvider1.Clear();
                conn();
                cmd.CommandText = "Delete from Service where Serial=@sn";

                cmd.Parameters.AddWithValue("@sn", textBox1.Text);

                cmd.ExecuteNonQuery();

                DeleteDone dd = new DeleteDone();
                dd.Show();

                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                textBox4.Clear();
                textBox5.Clear();
                textBox6.Clear();

                richTextBox2.Clear();
                richTextBox1.Clear();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                errorProvider1.Clear();
                errorProvider1.SetError(textBox1, "Please Enter Serial Number");
            }
            else if (textBox2.Text == "")
            {
                errorProvider1.Clear();
                errorProvider1.SetError(textBox2, "Please Select Service Date");
            }
            else if (textBox3.Text == "")
            {
                errorProvider1.Clear();
                errorProvider1.SetError(textBox3, "Please Enter Customer Name");
            }
            else if (textBox4.Text == "")
            {
                errorProvider1.Clear();
                errorProvider1.SetError(textBox4, "Please Enter Parts Cost");
            }
            else if (textBox5.Text == "")
            {
                errorProvider1.Clear();
                errorProvider1.SetError(textBox5, "Please Enter Labor Cost");
            }
            else if (textBox6.Text == "")
            {
                errorProvider1.Clear();
                errorProvider1.SetError(textBox6, "Please Enter Total Price");
            }
            else if (richTextBox2.Text == "")
            {
                errorProvider1.Clear();
                errorProvider1.SetError(richTextBox2, "Please Enter Service Description");
            }
            else
            {
                errorProvider1.Clear();
                conn();

                double taxPercent = 1.13;

                double partscost = double.Parse(textBox4.Text);
                double laborcost = double.Parse(textBox5.Text);

                double total = (partscost + laborcost) * taxPercent;

                double tax = (partscost + laborcost) * (taxPercent - 1);

                cmd.CommandText = " Update Service SET Serdate=@sd,Cname=@cn,Partscost=@pc,Laborcost=@lc,Tax=@tax,Totalcost=@total,Description=@desc where Serial=@serno";

                cmd.Parameters.AddWithValue("@serno", textBox1.Text);
                cmd.Parameters.AddWithValue("@sd", textBox2.Text);
                cmd.Parameters.AddWithValue("@cn", textBox3.Text);
                cmd.Parameters.AddWithValue("@pc", textBox4.Text);
                cmd.Parameters.AddWithValue("@lc", textBox5.Text);
                cmd.Parameters.AddWithValue("@tax", tax);
                cmd.Parameters.AddWithValue("@total", total);
                cmd.Parameters.AddWithValue("@desc", richTextBox2.Text);


                cmd.ExecuteNonQuery();
                //MessageBox.Show("Service Info updated!!!!");
                UpdateDone ud = new UpdateDone();
                ud.Show();

                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                textBox4.Clear();
                textBox5.Clear();
                textBox6.Clear();

                richTextBox2.Clear();
                richTextBox1.Clear();

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                errorProvider1.Clear();
                errorProvider1.SetError(textBox1, "Please Enter Serial Number");
            }
            else
            {

                errorProvider1.Clear();
                conn();

                string s = "Select * from Service where Serial=@sn";

                cmd.CommandText = s;

                cmd.Parameters.AddWithValue("@sn", textBox1.Text);

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {


                    while (dr.Read())
                    {
                        textBox1.Text = dr[5].ToString();
                        textBox2.Text = dr[3].ToString();
                        textBox3.Text = dr[4].ToString();
                        textBox4.Text = dr[6].ToString();
                        textBox5.Text = dr[7].ToString();
                        textBox6.Text = dr[9].ToString();
                        richTextBox2.Text = dr[10].ToString();
                    }


                    scon.Close();
                    SearchDone sd = new SearchDone();
                    sd.Show();

                }
                else
                {
                    RecordDone rd = new RecordDone();
                    rd.Show();
                }

            }
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

        private void textBox1_Leave(object sender, EventArgs e)
        {
            Regex pattern = new Regex("^([0-9]+[a-zA-Z]+|[a-zA-Z]+[0-9]+)[0-9a-zA-Z]*$");


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
                label13.Text = "Please Enter 13 Digit Serial Number In Combination Of String And Number ";
                textBox1.Focus();
                textBox1.BackColor = Color.Pink;
            }
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            Regex pattern = new Regex("^[A-Za-z\\s]+$");

            if (pattern.IsMatch(textBox3.Text))
            {

                textBox4.Focus();
                label13.Visible = false;
                textBox3.BackColor = Color.White;
                errorProvider1.Clear();


            }
            else
            {
                label13.Visible = true;
                label13.Text = "Please Enter Customer Name In String Only.";
                textBox3.Focus();
                textBox3.BackColor = Color.Pink;
            }
        }

        private void textBox4_Leave(object sender, EventArgs e)
        {
            Regex pattern = new Regex("^[0-9]{3,5}$");

            if (pattern.IsMatch(textBox4.Text))
            {

                textBox5.Focus();
                label13.Visible = false;
                textBox4.BackColor = Color.White;
                errorProvider1.Clear();


            }
            else
            {
                label13.Visible = true;
                label13.Text = "Please Enter Parts Cost In Number Only.";
                textBox4.Focus();
                textBox4.BackColor = Color.Pink;
            }
        }

        private void textBox5_Leave(object sender, EventArgs e)
        {
            Regex pattern = new Regex("^[0-9]{3,5}$");

            if (pattern.IsMatch(textBox5.Text))
            {

                textBox6.Focus();
                label13.Visible = false;
                textBox5.BackColor = Color.White;
                errorProvider1.Clear();

            }
            else
            {
                label13.Visible = true;
                label13.Text = "Please Enter Labor Cost In Number Only.";
                textBox5.Focus();
                textBox5.BackColor = Color.Pink;
            }
        }

        private void textBox6_Leave(object sender, EventArgs e)
        {
            Regex pattern = new Regex("^[0-9]{3,6}$");

            if (pattern.IsMatch(textBox6.Text))
            {

                richTextBox2.Focus();
                label13.Visible = false;
                textBox6.BackColor = Color.White;
                errorProvider1.Clear();


            }
            else
            {
                label13.Visible = true;
                label13.Text = "Please Enter Total Price In Number Only.";
                textBox6.Focus();
                textBox6.BackColor = Color.Pink;
            }
        }

        
    }
}
