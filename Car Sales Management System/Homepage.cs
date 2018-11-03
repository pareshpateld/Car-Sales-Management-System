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
    public partial class Homepage : Form
    {
        SqlConnection scon;
        SqlCommand cmd1, cmd2, cmd3;

        public void conn()
        {
            String con = Properties.Settings.Default.connection;
            scon = new SqlConnection(con);
            cmd1 = scon.CreateCommand();
            cmd2 = scon.CreateCommand();
            cmd3 = scon.CreateCommand();
            scon.Open();
        }

        public Homepage()
        {
            InitializeComponent();
            menuStrip1.BackColor = Color.Transparent;
            pictureBox1.BackColor = Color.Transparent;
            groupBox1.BackColor = Color.Transparent;
            groupBox2.BackColor = Color.Transparent;
            groupBox3.BackColor = Color.Transparent;
            linkLabel1.BackColor = Color.Transparent;
            label13.BackColor = Color.Transparent;

            uname.Visible = true;
            uname.Text = uname.Text + LoginPage.Username;
            label13.Visible = false;

        }

        private void Homepage_Load(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;

            conn();
            cmd1.CommandText = "Select Serial from Car where Sold=@sold";
            cmd1.Parameters.AddWithValue("sold", "NO");
            SqlDataReader dr = cmd1.ExecuteReader();
            while (dr.Read())
            {
                comboBox5.Items.Add(dr[0]);
            }
            dr.Close();
            scon.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            new LoginPage().Show();
            this.Hide();
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

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                errorProvider1.Clear();
                errorProvider1.SetError(textBox1, "Please Enter Customer name");
            }
            else if (textBox2.Text == "")
            {
                errorProvider1.Clear();
                errorProvider1.SetError(textBox2, "Please Enter Street");
            }
            else if (textBox3.Text == "")
            {
                errorProvider1.Clear();
                errorProvider1.SetError(textBox3, "Please Enter City");
            }
            else if (textBox4.Text == "")
            {
                errorProvider1.Clear();
                errorProvider1.SetError(textBox4, "Please Enter Province");
            }
            else if (textBox5.Text == "")
            {
                errorProvider1.Clear();
                errorProvider1.SetError(textBox5, "Please Enter Postalcode");
            }
            else if (textBox6.Text == "")
            {
                errorProvider1.Clear();
                errorProvider1.SetError(textBox6, "Please Enter Phone number");
            }

            else if (textBox9.Text == "")
            {
                errorProvider1.Clear();
                errorProvider1.SetError(textBox9, "Please Enter Purchase Date");
            }
            else if (comboBox5.SelectedIndex == -1 && comboBox5.Text == "")
            {
                errorProvider1.Clear();
                errorProvider1.SetError(comboBox5, "Please Select Car Serial Number");
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

            else if (textBox8.Text == "")
            {
                errorProvider1.Clear();
                errorProvider1.SetError(textBox8, "Please Enter Total Price");
            }
            else if (textBox10.Text == "")
            {
                errorProvider1.Clear();
                errorProvider1.SetError(textBox10, "Please Enter Purchase Invoice");
            }

            else
            {
                errorProvider1.Clear();

                conn();

                string Cid = "";

                cmd1.CommandText = "insert into Customer(Name,Street,City,Prov,Pcode,Phone)values(@name,@street,@city,@prov,@pcode,@pno)";

                cmd1.Parameters.AddWithValue("@name", textBox1.Text);
                cmd1.Parameters.AddWithValue("@street", textBox2.Text);
                cmd1.Parameters.AddWithValue("@city", textBox3.Text);
                cmd1.Parameters.AddWithValue("@prov", textBox4.Text);
                cmd1.Parameters.AddWithValue("@pcode", textBox5.Text);
                cmd1.Parameters.AddWithValue("@pno", textBox6.Text);
                cmd1.ExecuteNonQuery();


                cmd2.CommandText = "select ID from Customer where Name=@cname";
                cmd2.Parameters.AddWithValue("@cname", textBox1.Text);

                SqlDataReader dr = cmd2.ExecuteReader();
                while (dr.Read())
                {
                    Cid = dr[0].ToString();
                }
                dr.Close();
                cmd2.ExecuteNonQuery();

                cmd3.CommandText = "Update Car SET Cid=@cid,Purchinv=@pinv,Purchdate=@pdate,Sold=@sold where Serial = @serial";
                cmd3.Parameters.AddWithValue("@cid", Cid);
                cmd3.Parameters.AddWithValue("@serial", comboBox5.SelectedItem.ToString());
                cmd3.Parameters.AddWithValue("@pinv", textBox10.Text);
                cmd3.Parameters.AddWithValue("@pdate", textBox9.Text);
                cmd3.Parameters.AddWithValue("@sold", "YES");

                cmd3.ExecuteNonQuery();

                scon.Close();

                AddDone ad = new AddDone();
                ad.Show();

                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                textBox4.Clear();
                textBox5.Clear();
                textBox6.Clear();
                comboBox5.Text = "";
                textBox8.Clear();
                textBox9.Clear();
                textBox10.Clear();
                comboBox1.Text = "";
                comboBox2.Text = "";
                comboBox3.Text = "";
                comboBox4.Text = "";

                scon.Close();
            }
        }

        private void customerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //do nothing
           // Homepage hp = new Homepage();
            //hp.Show();
           // this.Hide();
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
            new Servicespage().Show();
            this.Hide();
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


        private void pictureBox1_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(pictureBox1, "Logout");

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ChangePassword cp = new ChangePassword();
            cp.Show();

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            monthCalendar1.Show();
        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            textBox9.Text = monthCalendar1.SelectionStart.ToShortDateString();
        }

        private void monthCalendar1_MouseLeave(object sender, EventArgs e)
        {
            monthCalendar1.Visible = false;
        }

        

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                errorProvider1.Clear();
                errorProvider1.SetError(textBox1, "Please Enter Customer name");
            }
            else if (textBox2.Text == "")
            {
                errorProvider1.Clear();
                errorProvider1.SetError(textBox2, "Please Enter Street");
            }
            else if (textBox3.Text == "")
            {
                errorProvider1.Clear();
                errorProvider1.SetError(textBox3, "Please Enter City");
            }
            else if (textBox4.Text == "")
            {
                errorProvider1.Clear();
                errorProvider1.SetError(textBox4, "Please Enter Province");
            }
            else if (textBox5.Text == "")
            {
                errorProvider1.Clear();
                errorProvider1.SetError(textBox5, "Please Enter Postalcode");
            }
            else if (textBox6.Text == "")
            {
                errorProvider1.Clear();
                errorProvider1.SetError(textBox6, "Please Enter Phone number");
            }
           
            else if (textBox9.Text == "")
            {
                errorProvider1.Clear();
                errorProvider1.SetError(textBox9, "Please Enter Purchase Date");
            }
            else if (comboBox5.SelectedIndex == -1 && comboBox5.Text == "")
            {
                errorProvider1.Clear();
                errorProvider1.SetError(comboBox5, "Please Select Car Serial Number");
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
            
            else if (textBox8.Text == "")
            {
                errorProvider1.Clear();
                errorProvider1.SetError(textBox8, "Please Enter Total Price");
            }
            else if (textBox10.Text == "")
            {
                errorProvider1.Clear();
                errorProvider1.SetError(textBox10, "Please Enter Purchase Invoice");
            }
            else
            {

                errorProvider1.Clear();

                conn();

                cmd1.CommandText = " Update Car SET Model=@mod,Trim=@trim,Color=@color,Year=@year,Price=@price,Purchinv=@piv,Purchdate=@pdate where Serial=@serial";

                cmd1.Parameters.AddWithValue("@serial", comboBox5.SelectedItem.ToString());
                cmd1.Parameters.AddWithValue("@mod", comboBox1.SelectedItem.ToString());
                cmd1.Parameters.AddWithValue("@trim", comboBox2.SelectedItem.ToString());
                cmd1.Parameters.AddWithValue("@color", comboBox3.SelectedItem.ToString());
                cmd1.Parameters.AddWithValue("@year", comboBox4.SelectedItem.ToString());
                cmd1.Parameters.AddWithValue("@price", textBox8.Text);
                cmd1.Parameters.AddWithValue("@piv", textBox10.Text);
                cmd1.Parameters.AddWithValue("@pdate", textBox9.Text);


                cmd1.ExecuteNonQuery();

                scon.Close();
                UpdateDone ud = new UpdateDone();
                ud.Show();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" && comboBox5.Text == "")
            {

                errorProvider1.SetError(textBox1, "Please Enter Customer Name OR Serial Number");
                errorProvider1.SetError(comboBox5, "Please Enter Customer Name OR Serial Number");
            }
            else
            {
                errorProvider1.Clear();


                if (textBox1.Text != "")
                {
                    conn();
                    string s_name = "Select c.Name,c.Street,c.City,c.Prov,c.Pcode,c.Phone,cr.Serial,cr.Model,cr.Trim,cr.Color,cr.Year,cr.Price,cr.Purchinv,cr.Purchdate from Customer c,Car cr WHERE c.ID=cr.Cid AND c.Name=@cname";
                    cmd1.CommandText = s_name;
                    cmd1.Parameters.AddWithValue("@cname", textBox1.Text);

                    SqlDataReader dr = cmd1.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            textBox1.Text = dr[0].ToString();
                            textBox2.Text = dr[1].ToString();
                            textBox3.Text = dr[2].ToString();
                            textBox4.Text = dr[3].ToString();
                            textBox5.Text = dr[4].ToString();
                            textBox6.Text = dr[5].ToString();
                            comboBox5.Text = dr[6].ToString();
                            comboBox1.Text = dr[7].ToString();
                            comboBox2.Text = dr[8].ToString();
                            comboBox3.Text = dr[9].ToString();
                            comboBox4.Text = dr[10].ToString();
                            textBox8.Text = dr[11].ToString();
                            textBox9.Text = dr[13].ToString();
                            textBox10.Text = dr[12].ToString();
                        }
                        SearchDone sd = new SearchDone();
                        sd.Show();
                    }
                    else
                    {
                        RecordDone rdd = new RecordDone();
                        rdd.Show();
                    }

                }
                else if (comboBox5.Text != "")
                {
                    conn();
                    string s_cid = "Select DISTINCT c.Name,c.Street,c.City,c.Prov,c.Pcode,c.Phone,cr.Serial,cr.Model,cr.Trim,cr.Color,cr.Year,cr.Price,cr.Purchinv,cr.Purchdate from Customer c,Car cr where c.ID=cr.Cid AND cr.Serial=@serial";

                    cmd2.CommandText = s_cid;
                    cmd2.Parameters.AddWithValue("@serial", comboBox5.Text);
                    SqlDataReader dr = cmd2.ExecuteReader();
                    if (dr.HasRows)
                    {
                        int count = 0;
                        while (dr.Read())
                        {
                            textBox1.Text = dr[0].ToString();
                            textBox2.Text = dr[1].ToString();
                            textBox3.Text = dr[2].ToString();
                            textBox4.Text = dr[3].ToString();
                            textBox5.Text = dr[4].ToString();
                            textBox6.Text = dr[5].ToString();
                            comboBox5.Text = dr[6].ToString();
                            comboBox1.Text = dr[7].ToString();
                            comboBox2.Text = dr[8].ToString();
                            comboBox3.Text = dr[9].ToString();
                            comboBox4.Text = dr[10].ToString();
                            textBox8.Text = dr[11].ToString();
                            textBox9.Text = dr[13].ToString();
                            textBox10.Text = dr[12].ToString();
                            count++;
                        }
                        SearchDone sd = new SearchDone();
                        sd.Show();
                    }
                    else
                    {
                        RecordDone rdd = new RecordDone();
                        rdd.Show();
                    }
                }

                scon.Close();

                
            }
        }

        private void textBox6_Leave(object sender, EventArgs e)
        {
            Regex pattern = new Regex("^[0-9]{10,10}$");

            if (pattern.IsMatch(textBox6.Text))
            {

                textBox9.Focus();
                label13.Visible = false;
                textBox6.BackColor = Color.White;
                errorProvider1.Clear();
                

            }
            else
            {
                label13.Visible = true;
                label13.Text = "Please Enter 10 Digit Only.";
                textBox6.Focus();
                textBox6.BackColor = Color.Pink;
            }

        }

        private void textBox5_Leave(object sender, EventArgs e)
        {
            
            
            if (System.Text.RegularExpressions.Regex.IsMatch(textBox5.Text, "^[A-Z]{1}[0-9]{1}[A-Z]{1}[ ]?[0-9]{1}[A-Z]{1}[0-9]{1}$"))
            {
                label13.Visible = false;
                textBox5.BackColor = Color.White;
            }
            else
            {
                label13.Visible = true;
                label13.Text = "Please Enter Combination of Character and Number. Ex-L6Y 3T9";
                textBox5.Focus();
                textBox5.BackColor = Color.Pink;
            }

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            conn();
            cmd1.CommandText = "Select Model,Trim,Color,Year,Price from Car where Serial=@serial";
            cmd1.Parameters.AddWithValue("@serial", comboBox5.SelectedItem.ToString());
            SqlDataReader dr = cmd1.ExecuteReader();
            while (dr.Read())
            {
                comboBox1.Text = dr[0].ToString();
                comboBox2.Text = dr[1].ToString();
                comboBox3.Text = dr[2].ToString();
                comboBox4.Text = dr[3].ToString();
                textBox8.Text = dr[4].ToString();
            }
            dr.Close();
            scon.Close();
        }

        private void backgroundToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog dg = new ColorDialog();
            if (dg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                panel1.BackColor = dg.Color;
                

            }
        }

        private void textBox8_Leave(object sender, EventArgs e)
        {
            Regex pattern = new Regex("^[0-9]{6,6}$");

            if (pattern.IsMatch(textBox8.Text))
            {

                textBox9.Focus();
                label13.Visible = false;
                textBox8.BackColor = Color.White;
                errorProvider1.Clear();


            }
            else
            {
                label13.Visible = true;
                label13.Text = "Please Enter Total Price In 6 Digit Only.";
                textBox8.Focus();
                textBox8.BackColor = Color.Pink;
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
                label13.Text = "Please Enter Customer Name In String Only.";
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
                label13.Text = "Please Enter City Name In String Only.";
                textBox3.Focus();
                textBox3.BackColor = Color.Pink;
            }
        }

        private void textBox4_Leave(object sender, EventArgs e)
        {
            Regex pattern = new Regex("^[A-Za-z\\s]+$");

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
                label13.Text = "Please Enter Province Name In String Only.";
                textBox4.Focus();
                textBox4.BackColor = Color.Pink;
            }
        }
    }
}
