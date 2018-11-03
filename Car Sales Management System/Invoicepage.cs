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
    public partial class Invoicepage : Form
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

        public Invoicepage()
        {
            InitializeComponent();
            menuStrip1.BackColor = Color.Transparent;
            groupBox2.BackColor = Color.Transparent;
            groupBox3.BackColor = Color.Transparent;
            label13.BackColor = Color.Transparent;
            label13.Visible = false;

            uname.Visible = true;
            uname.Text = uname.Text + LoginPage.Username;
        }

        private void Invoicepage_Load(object sender, EventArgs e)
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

        private void button5_Click(object sender, EventArgs e)
        {
            string insurance = "";
            if (radioButton1.Checked)
            {
                insurance = "YES";
            }
            else
            {
                insurance = "NO";
            }

            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                File.WriteAllText(saveFileDialog1.FileName,
                   "Serial No:          " + textBox1.Text + Environment.NewLine +
                   "Customer Name:      " + textBox2.Text + Environment.NewLine +
                   "Salesaman:          " + textBox3.Text + Environment.NewLine +
                   "Model:              " + comboBox1.Text + Environment.NewLine +
                   "Trim:               " + comboBox2.Text + Environment.NewLine +
                   "Color:              " + comboBox3.Text + Environment.NewLine +
                   "Year:               " + comboBox4.Text + Environment.NewLine +
                   "Delivery Date:      " + textBox5.Text + Environment.NewLine +
                   "Total Price:        " + textBox6.Text + Environment.NewLine +
                   "Insurance:          " + insurance);
            }
            SaveDone sd = new SaveDone();
            sd.Show();
        
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
                errorProvider1.SetError(textBox2, "Please Enter Customer Name");
            }
            else if (textBox3.Text == "")
            {
                errorProvider1.Clear();
                errorProvider1.SetError(textBox3, "Please Enter Salesman Name");
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
            else if (textBox5.Text == "")
            {
                errorProvider1.Clear();
                errorProvider1.SetError(textBox5, "Please Select Delivery Date");
            }
            else if (textBox6.Text == "")
            {
                errorProvider1.Clear();
                errorProvider1.SetError(textBox6, "Please Enter Total Amount");
            }
            else
            {
            errorProvider1.Clear();
            conn();

            string insurance = "";
            if (radioButton1.Checked)
            {
                insurance = "YES";
            }
            else
            {
                insurance = "NO";
            }

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


            cmd.CommandText = "Insert into Saleinv(Carid,Cid,Cname,Salesman,Saledate,Price,Insurance)values(@carid,@cid,@cname,@sname,@sdate,@price,@insurance)";
            cmd.Parameters.AddWithValue("@carid", Carid);
            cmd.Parameters.AddWithValue("@cid", Cid);
            cmd.Parameters.AddWithValue("@cname", textBox2.Text);
            cmd.Parameters.AddWithValue("@sname", LoginPage.Username);
            cmd.Parameters.AddWithValue("@sdate", textBox5.Text);
            cmd.Parameters.AddWithValue("@price", textBox6.Text);
            cmd.Parameters.AddWithValue("@insurance", insurance);

            cmd.ExecuteNonQuery();

            AddDone ad = new AddDone();
            ad.Show();

            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox5.Clear();
            textBox6.Clear();
            comboBox1.Text = "";
            comboBox2.Text = "";
            comboBox3.Text = "";
            comboBox4.Text = "";
            
        }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                errorProvider1.Clear();
                errorProvider1.SetError(textBox2, "Please Insert Customer Name");
            }
            else
            {
                errorProvider1.Clear();

                conn();
                cmd.CommandText = "Delete from Saleinv where Cname=@cname";

                cmd.Parameters.AddWithValue("@cname", textBox2.Text);

                cmd.ExecuteNonQuery();

                DeleteDone dd = new DeleteDone();
                dd.Show();

                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                textBox5.Clear();
                textBox6.Clear();
                comboBox1.Text = "";
                comboBox2.Text = "";
                comboBox3.Text = "";
                comboBox4.Text = "";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                errorProvider1.Clear();
                errorProvider1.SetError(textBox2, "Please Enter Customer Name");
            }
            else if (textBox3.Text == "")
            {
                errorProvider1.Clear();
                errorProvider1.SetError(textBox3, "Please Enter Salesman Name");
            }
            else if (textBox5.Text == "")
            {
                errorProvider1.Clear();
                errorProvider1.SetError(textBox5, "Please Select Delivery Date");
            }
            else if (textBox6.Text == "")
            {
                errorProvider1.Clear();
                errorProvider1.SetError(textBox6, "Please Enter Total Amount");
            }
            else
            {
                errorProvider1.Clear();
                conn();

                string insurance = "";
                if (radioButton1.Checked)
                {
                    insurance = "YES";
                }
                else
                {
                    insurance = "NO";
                }

                cmd.CommandText = " Update Saleinv SET Salesman=@sman,Saledate=@sdate,Price=@price,Insurance=@ins where Cname=@cname";

                cmd2.CommandText = " Update Car SET Price=@cprice where Serial=@serial";

                cmd.Parameters.AddWithValue("@cname", textBox2.Text);
                cmd.Parameters.AddWithValue("@sman", textBox3.Text);
                cmd.Parameters.AddWithValue("@sdate", textBox5.Text);
                cmd.Parameters.AddWithValue("@price", textBox6.Text);
                cmd.Parameters.AddWithValue("@ins", insurance);
                cmd2.Parameters.AddWithValue("@serial", textBox1.Text);
                cmd2.Parameters.AddWithValue("@cprice", textBox6.Text);


                cmd.ExecuteNonQuery();
                cmd2.ExecuteNonQuery();

                UpdateDone ud = new UpdateDone();
                ud.Show();

                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                textBox5.Clear();
                textBox6.Clear();
                comboBox1.Text = "";
                comboBox2.Text = "";
                comboBox3.Text = "";
                comboBox4.Text = "";
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            conn();

            if (textBox1.Text == "" && textBox2.Text=="")
            {
                errorProvider1.Clear();
                errorProvider1.SetError(textBox1, "Please Enter Serial Number OR Customer Name");
                errorProvider1.SetError(textBox2, "Please Enter Serial Number OR Customer Name");
            }
            else if (textBox2.Text != "" && textBox1.Text == "")
            {
                errorProvider1.Clear();
                string s_name = "Select cr.Serial,c.Name,cr.Model,cr.Trim,cr.Color,cr.Year,cr.Purchdate,cr.Price,s.Salesman,s.Insurance from Car cr,Customer c,Saleinv s where cr.Cid=c.ID AND cr.Cid=s.Cid AND c.Name=@cname";
                cmd.CommandText = s_name;
                cmd.Parameters.AddWithValue("@cname", textBox2.Text);

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        textBox1.Text = dr[0].ToString();
                        textBox2.Text = dr[1].ToString();
                        textBox3.Text = dr[8].ToString();
                        comboBox1.Text = dr[2].ToString();
                        comboBox2.Text = dr[3].ToString();
                        comboBox3.Text = dr[4].ToString();
                        comboBox4.Text = dr[5].ToString();
                        textBox5.Text = dr[6].ToString();
                        textBox6.Text = dr[7].ToString();
                        if (dr[9].ToString().Equals("YES"))
                        {
                            radioButton1.Checked = true;
                        }
                        else
                        {
                            radioButton2.Checked = true;
                        }
                    }
                }
                else
                {
                    RecordDone rd = new RecordDone();
                    rd.Show();
                }
            }
            else if (textBox2.Text == "" && textBox1.Text != "")
            {
                errorProvider1.Clear();
                string s_name = "Select cr.Serial,c.Name,cr.Model,cr.Trim,cr.Color,cr.Year,cr.Purchdate,cr.Price,s.Salesman,s.Insurance from Car cr,Customer c,Saleinv s where cr.Cid=c.ID AND cr.Cid=s.Cid AND cr.Serial=@serial";
                cmd.CommandText = s_name;
                cmd.Parameters.AddWithValue("@serial", textBox1.Text);

                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    textBox1.Text = dr[0].ToString();
                    textBox2.Text = dr[1].ToString();
                    textBox3.Text = dr[8].ToString();
                    comboBox1.Text = dr[2].ToString();
                    comboBox2.Text = dr[3].ToString();
                    comboBox3.Text = dr[4].ToString();
                    comboBox4.Text = dr[5].ToString();
                    textBox5.Text = dr[6].ToString();
                    textBox6.Text = dr[7].ToString();
                    if (dr[9].ToString().Equals("YES"))
                    {
                        radioButton1.Checked = true;
                    }
                    else
                    {
                        radioButton2.Checked = true;
                    }
                }
            }
            scon.Close();
        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            textBox5.Text = monthCalendar1.SelectionStart.ToShortDateString();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            monthCalendar1.Show();
        }

        private void monthCalendar1_MouseLeave(object sender, EventArgs e)
        {
            monthCalendar1.Visible = false;
        }

        private void textBox3_Enter(object sender, EventArgs e)
        {
            conn();
            cmd.CommandText = "Select cr.Model,cr.Trim,cr.Color,cr.Year,cr.Price from Car cr,Customer c where cr.Cid=c.ID AND cr.Serial=@serial";
            cmd.Parameters.AddWithValue("@serial", textBox1.Text);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                comboBox1.Text = dr[0].ToString();
                comboBox2.Text = dr[1].ToString();
                comboBox3.Text = dr[2].ToString();
                comboBox4.Text = dr[3].ToString();
                textBox6.Text = dr[4].ToString();
            }
            scon.Close();
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

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
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

        private void textBox2_Leave(object sender, EventArgs e)
        {
            Regex pattern = new Regex("^[A-Za-z\\s]+$");

            if (pattern.IsMatch(textBox2.Text))
            {

                textBox3.Focus();
                label13.Visible = false;
                textBox2.BackColor = Color.White;
                errorProvider1.Clear();


            }
            else
            {
                label13.Visible = true;
                label13.Text = "Please Enter Customer Name In String Only.";
                textBox2.Focus();
                textBox2.BackColor = Color.Pink;
            }
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            Regex pattern = new Regex("^[A-Za-z\\s]+$");

            if (pattern.IsMatch(textBox3.Text))
            {

                label13.Visible = false;
                textBox3.BackColor = Color.White;
                errorProvider1.Clear();

            }
            else
            {
                label13.Visible = true;
                label13.Text = "Please Enter Salesman Name In String Only.";
                textBox3.Focus();
                textBox3.BackColor = Color.Pink;
            }
        }

        private void textBox6_MouseLeave(object sender, EventArgs e)
        {
            Regex pattern = new Regex("^[0-9]{6,6}$");

            if (pattern.IsMatch(textBox6.Text))
            {
   
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
