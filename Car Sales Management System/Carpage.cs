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
    public partial class Carpage : Form
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


        public Carpage()
        {
            InitializeComponent();
            menuStrip1.BackColor = Color.Transparent;
            label13.Visible = false;
            
            groupBox2.BackColor = Color.Transparent;
            groupBox3.BackColor = Color.Transparent;
            label13.BackColor = Color.Transparent;

            uname.Visible = true;
            uname.Text = uname.Text + LoginPage.Username;
        }

        private void Carpage_Load(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
        }

        private void customerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Homepage().Show();
            this.Hide();
        }

        private void carToolStripMenuItem_Click(object sender, EventArgs e)
        {

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
            new Servicespage().Show();
            this.Hide();
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
            else if (comboBox1.Text == "")
            {
                errorProvider1.Clear();
                errorProvider1.SetError(comboBox1, "Please Select Model");
            }
            else if (comboBox2.Text == "")
            {
                errorProvider1.Clear();
                errorProvider1.SetError(comboBox2, "Please Select Trim");
            }
            else if (comboBox3.Text == "")
            {
                errorProvider1.Clear();
                errorProvider1.SetError(comboBox3, "Please Select Color");
            }
            else if (comboBox4.Text == "")
            {
                errorProvider1.Clear();
                errorProvider1.SetError(comboBox4, "Please Select Year");
            }
            else if (textBox2.Text == "")
            {
                errorProvider1.Clear();
                errorProvider1.SetError(textBox2, "Please Enter Total Price");
            }

            else
            {

                errorProvider1.Clear();

                conn();

                string s = "insert into Car(Serial,Model,Trim,Color,Year,Price,Sold)values(@sn,@model,@trim,@color,@year,@price,@sold)";


                cmd.CommandText = s;

                cmd.Parameters.AddWithValue("@sn", textBox1.Text);
                cmd.Parameters.AddWithValue("@model", comboBox1.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@trim", comboBox2.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@color", comboBox3.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@year", int.Parse(comboBox4.SelectedItem.ToString()));
                cmd.Parameters.AddWithValue("@price", int.Parse(textBox2.Text));
                cmd.Parameters.AddWithValue("@sold", "NO");

                cmd.ExecuteNonQuery();

                scon.Close();

                AddDone ad = new AddDone();
                ad.Show();

                textBox1.Clear();
                textBox2.Clear();
                comboBox1.Text = "";
                comboBox2.Text = "";
                comboBox3.Text = "";
                comboBox4.Text = "";
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string model = comboBox1.SelectedItem.ToString();

            if (model.Equals("HURACAN"))
            {
                comboBox2.Items.Clear();
                comboBox2.Items.Add("PERFORMANTE");
                comboBox2.Items.Add("COUPE");
                comboBox2.Items.Add("SPYDER");
                comboBox2.Items.Add("AVIO");
            }
            else if (model.Equals("AVENTADOR"))
            {
                comboBox2.Items.Clear();
                comboBox2.Items.Add("ROADSTER");
                comboBox2.Items.Add("PIRELLI");
                comboBox2.Items.Add("COUPE");
                comboBox2.Items.Add("MIURA HOMAGE");
            }
            else if (model.Equals("VENENO"))
            {
                comboBox2.Items.Clear();
                comboBox2.Items.Add("ROADSTER");
            }
            else if (model.Equals("GALLARDO"))
            {
                comboBox2.Items.Clear();
                comboBox2.Items.Add("POLIZIA");
                comboBox2.Items.Add("BALBONI");
                comboBox2.Items.Add("SUPER TROFOE");
                comboBox2.Items.Add("AVIO");
            }
            else if (model.Equals("MURCEILAGO"))
            {
                comboBox2.Items.Clear();
                comboBox2.Items.Add("ROADSTER");
                comboBox2.Items.Add("SUPERVELOCE");
                comboBox2.Items.Add("VERSACE");
                comboBox2.Items.Add("ROADSTER VERSACE");
            }
        }

        

        private void button3_Click(object sender, EventArgs e)
        {

            if (textBox1.Text == "")
            {
                errorProvider1.Clear();
                errorProvider1.SetError(textBox1, "Please Enter Serial Number");
            }
            else if (comboBox1.SelectedIndex == -1)
            {
                errorProvider1.Clear();
                errorProvider1.SetError(comboBox1, "Please Select Model");
            }
            else if (comboBox2.SelectedIndex == -1)
            {
                errorProvider1.Clear();
                errorProvider1.SetError(comboBox2, "Please Select Trim");
            }
            else if (comboBox3.SelectedIndex == -1)
            {
                errorProvider1.Clear();
                errorProvider1.SetError(comboBox3, "Please Select Color");
            }
            else if (comboBox4.SelectedIndex == -1)
            {
                errorProvider1.Clear();
                errorProvider1.SetError(comboBox4, "Please Select Year");
            }
            else if (textBox2.Text == "")
            {
                errorProvider1.Clear();
                errorProvider1.SetError(textBox2, "Please Enter Total Price");
            }
            else
            {

                errorProvider1.Clear();
                conn();

                cmd.CommandText = " Update Car SET Model=@mod,Trim=@trim,Color=@color,Year=@year,Price=@price where Serial=@serial";

                cmd.Parameters.AddWithValue("@serial", textBox1.Text);
                cmd.Parameters.AddWithValue("@mod", comboBox1.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@trim", comboBox2.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@color", comboBox3.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@year", comboBox4.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@price", textBox2.Text);


                cmd.ExecuteNonQuery();
                scon.Close();
                UpdateDone ud = new UpdateDone();
                ud.Show();

                textBox1.Clear();
                textBox2.Clear();
                comboBox1.Text = "";
                comboBox2.Text = "";
                comboBox3.Text = "";
                comboBox4.Text = "";
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if(textBox1.Text == "")
            {
                
                errorProvider1.SetError(textBox1, "Please Enter Serial Number");
            }
            else
            {
                errorProvider1.Clear();

                conn();
                string s = "Select * from Car where Serial=@sn";

                cmd.CommandText = s;

                cmd.Parameters.AddWithValue("@sn", textBox1.Text);

                SqlDataReader rd = cmd.ExecuteReader();

                if (rd.HasRows)
                {
                    int count = 0;
                    while (rd.Read())
                    {
                        textBox1.Text = rd[2].ToString();
                        comboBox1.Text = rd[3].ToString();
                        comboBox2.Text = rd[4].ToString();
                        comboBox3.Text = rd[5].ToString();
                        comboBox4.Text = rd[6].ToString();
                        textBox2.Text = rd[7].ToString();
                        count++;
                    }


                    scon.Close();
                    SearchDone sd = new SearchDone();
                    sd.Show();
                }
                else
                {
                    RecordDone rdd = new RecordDone();
                    rdd.Show();
                }
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
           Regex pattern = new Regex("^([0-9]+[a-zA-Z]+|[a-zA-Z]+[0-9]+)[0-9a-zA-Z]*$");
            //Regex pattern = new Regex("^[A-Z0-9]{13,13}$");

            if (pattern.IsMatch(textBox1.Text))
            {
                comboBox1.Focus();
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

        private void textBox2_MouseLeave(object sender, EventArgs e)
        {
            Regex pattern = new Regex("^[0-9]{6,6}$");

            if (pattern.IsMatch(textBox2.Text))
            {

                
                label13.Visible = false;
                textBox2.BackColor = Color.White;
                errorProvider1.Clear();


            }
            else
            {
                label13.Visible = true;
                label13.Text = "Please Enter Total Price In Number Only.";
                textBox2.Focus();
                textBox2.BackColor = Color.Pink;
            }
        }
    }
}
