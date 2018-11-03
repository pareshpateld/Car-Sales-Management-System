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
    public partial class LoginPage :Form
    {
        SqlConnection scon;
        SqlCommand cmd;

        public static String Username = "";

        public void conn()
        {
            String con = Properties.Settings.Default.connection;
            scon = new SqlConnection(con);
            cmd = scon.CreateCommand();
            scon.Open();
        }


        public LoginPage()
        {
            InitializeComponent();
            textBox2.PasswordChar = '*';
            textBox6.PasswordChar = '*';
            label13.Visible = false;
            label13.BackColor = Color.Transparent;

         
            pictureBox1.BackColor = Color.Transparent;
            
            pictureBox2.BackColor = Color.Transparent;
            
        }

        private void LoginPage_Load(object sender, EventArgs e)
        {
            //new WelcomeMessage().Show();
            this.ActiveControl = textBox1; // Auto Focus on username button
            WindowState = FormWindowState.Maximized;
        }

        private void login(object sender, EventArgs e)
        {
            if (textBox1.Text == "Enter Your Username")
            {
                errorProvider1.SetError(textBox1, "Please Enter Username");
            }
            else if (textBox2.Text == "Enter Your Password") 
           {
                errorProvider1.Clear();
                errorProvider1.SetError(textBox2, "Please Enter Password");
            }
            else
            {
                errorProvider1.Clear();
                conn();
                String s = "select Username,Password from Employee where Username= @uname AND  Password= @password";

                Username = textBox1.Text;
                cmd.CommandText = s;
                cmd.Parameters.AddWithValue("@uname", textBox1.Text);
                cmd.Parameters.AddWithValue("@password", textBox2.Text);

                SqlDataReader rd = cmd.ExecuteReader();

                if (rd.Read() == true)
                {
                    this.Hide();
                    new Homepage().Show();
                }
                else
                {
                    UserInvalid ui = new UserInvalid();
                    ui.Show();
                    textBox1.Clear();
                    textBox2.Clear();
                }
                scon.Close();
              
            }
        }

        private void signup(object sender, EventArgs e)
        {
            if(textBox3.Text=="First Name")
            {
                errorProvider1.SetError(textBox3, "Please Enter Firstname");
            }
            else if(textBox4.Text=="Last Name")
            {
                errorProvider1.Clear();
                errorProvider1.SetError(textBox4, "Please Enter Lastname");
            }
            else if(textBox5.Text=="Username")
            {
                errorProvider1.Clear();
                errorProvider1.SetError(textBox5, "Please Enter Username");
            }
            else if (textBox6.Text == "Password")
            {
                errorProvider1.Clear();
                errorProvider1.SetError(textBox6, "Please Enter Password");
            }
            else if (comboBox1.Text == "Select Your Position")
            {
                errorProvider1.Clear();
                errorProvider1.SetError(comboBox1, "Please Select Your Position");
            }
            else if (textBox7.Text == "Birth Date")
            {
                errorProvider1.Clear();
                errorProvider1.SetError(textBox7, "Please Select Date");
            }
            else if (textBox8.Text == "Commission")
            {
                errorProvider1.Clear();
                errorProvider1.SetError(textBox8, "Please Enter Commision Rate");
            }
            else
            {
                conn();
                string s = "insert into Employee(Username,Password,Firstname,Lastname,Position,Bdate,Commision) values(@uname,@password,@fname,@lname,@position,@bdate,@commision)";

                cmd.CommandText = s;

                cmd.Parameters.AddWithValue("@fname", textBox3.Text);
                cmd.Parameters.AddWithValue("@lname", textBox4.Text);
                cmd.Parameters.AddWithValue("@uname", textBox5.Text);
                cmd.Parameters.AddWithValue("@password", textBox6.Text);
                cmd.Parameters.AddWithValue("@position", comboBox1.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@bdate", textBox7.Text);
                cmd.Parameters.AddWithValue("@commision", textBox8.Text);

                cmd.ExecuteNonQuery();
                scon.Close();

                SignupDone sd = new SignupDone();
                sd.Show();

                textBox3.Clear();
                textBox4.Clear();
                textBox5.Clear();
                textBox6.Clear();
                textBox7.Clear();
                textBox8.Clear();
                comboBox1.Text = "";
            }
        }

        private void clear(object sender, EventArgs e)
        {
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            comboBox1.Text = "";
            textBox7.Text = "";
            textBox8.Text = "";

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
           SaveDone sd = new SaveDone();
            sd.Show();

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            monthCalendar1.Show();
        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            textBox7.Text = monthCalendar1.SelectionStart.ToShortDateString();
        }

        private void monthCalendar1_MouseLeave(object sender, EventArgs e)
        {
            monthCalendar1.Visible = false;
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            if (textBox1.Text == "Enter Your Username")
            {
                textBox1.Text = "";

                textBox1.ForeColor = Color.Black;
            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                textBox1.Text = "Enter Your Username";

                textBox1.ForeColor = Color.Gray;
            }
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            if (textBox2.Text == "Enter Your Password")
            {
                textBox2.Text = "";

                textBox2.ForeColor = Color.Black;
            }
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                textBox2.Text = "Enter Your Password";

                textBox2.ForeColor = Color.Gray;
            }
        }

        private void textBox3_Enter(object sender, EventArgs e)
        {
            if (textBox3.Text == "First Name")
            {
                textBox3.Text = "";

                textBox3.ForeColor = Color.Black;
            }
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            if (textBox3.Text == "")
            {
                textBox3.Text = "First Name";

                textBox3.ForeColor = Color.Gray;
            }

            Regex pattern = new Regex("^[A-Za-z]+$");

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
                label13.Text = "Please Enter First Name In String Only.";
                textBox3.Focus();
                textBox3.BackColor = Color.Pink;
            }
        }

        private void textBox5_Enter(object sender, EventArgs e)
        {
            if (textBox5.Text == "Username")
            {
                textBox5.Text = "";

                textBox5.ForeColor = Color.Black;
            }
        }

        private void textBox5_Leave(object sender, EventArgs e)
        {
            if (textBox5.Text == "")
            {
                textBox5.Text = "Username";

                textBox5.ForeColor = Color.Gray;
            }
        }

        private void textBox4_Enter(object sender, EventArgs e)
        {
            if (textBox4.Text == "Last Name")
            {
                textBox4.Text = "";

                textBox4.ForeColor = Color.Black;
            }
        }

        private void textBox4_Leave(object sender, EventArgs e)
        {
            if (textBox4.Text == "")
            {
                textBox4.Text = "Last Name";

                textBox4.ForeColor = Color.Gray;
            }

            Regex pattern = new Regex("^[A-Za-z]+$");

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
                label13.Text = "Please Enter Last Name In String Only.";
                textBox4.Focus();
                textBox4.BackColor = Color.Pink;
            }


        }

        private void textBox6_Enter(object sender, EventArgs e)
        {
            if (textBox6.Text == "Password")
            {
                textBox6.Text = "";

                textBox6.ForeColor = Color.Black;
            }
        }

        private void textBox6_Leave(object sender, EventArgs e)
        {
            if (textBox6.Text == "")
            {
                textBox6.Text = "Password";

                textBox6.ForeColor = Color.Gray;
            }
        }

        private void comboBox1_Enter(object sender, EventArgs e)
        {
            if (comboBox1.Text == "Select Your Position")
            {
                comboBox1.Text = "";

                comboBox1.ForeColor = Color.Black;
            }
        }

        private void comboBox1_Leave(object sender, EventArgs e)
        {
            if (comboBox1.Text == "")
            {
                comboBox1.Text = "Select Your Position";

                comboBox1.ForeColor = Color.Gray;
            }
        }

        private void textBox7_Enter(object sender, EventArgs e)
        {
            if (textBox7.Text == "Birth Date")
            {
                textBox7.Text = "";

                textBox7.ForeColor = Color.Black;
            }
        }

        private void textBox7_Leave(object sender, EventArgs e)
        {
            if (textBox7.Text == "")
            {
                textBox7.Text = "Birth Date";

                textBox7.ForeColor = Color.Gray;
            }
        }

        private void textBox8_Enter(object sender, EventArgs e)
        {
            if (textBox8.Text == "Commission")
            {
                textBox8.Text = "";

                textBox8.ForeColor = Color.Black;
            }
        }

        private void textBox8_Leave(object sender, EventArgs e)
        {
            if (textBox8.Text == "")
            {
                textBox8.Text = "Commission";

                textBox8.ForeColor = Color.Gray;
            }
        }

        private void textBox8_MouseLeave(object sender, EventArgs e)
        {
            Regex pattern = new Regex("^[0-9]{1,1}$");

            if (pattern.IsMatch(textBox8.Text))
            {


                label13.Visible = false;
                textBox8.BackColor = Color.White;
                errorProvider1.Clear();


            }
            else
            {
                label13.Visible = true;
                label13.Text = "Please Enter Only One Digit Commission.";
                textBox8.Focus();
                textBox8.BackColor = Color.Pink;
            }
        }

       
    }
}
