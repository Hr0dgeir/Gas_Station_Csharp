using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gas_station
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
        }
        SqlConnection connection = new SqlConnection("Data Source=DESKTOP-NT9V6AB;Initial Catalog=\"Gas station\";Integrated Security=True");
        int global_code;
        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox4.Text != "" && textBox5.Text != "" && textBox6.Text != "" && maskedTextBox1.Text != "")
            {
                if (textBox4.Text != textBox5.Text)
                {
                    if (textBox6.Text.Length < 4)
                    {
                        MessageBox.Show("Password cannot less than 4 character");
                    }
                    else
                    {
                        if (maskedTextBox1.Text == global_code.ToString())
                        {
                            connection.Open();

                            string personel_add = ("insert into Psn_Register (psn_name,psn_surname,psn_code,psn_password) values (@name,@surname,@code,@password)");

                            string hashed_password = Sha256convertor.sha256hash_(textBox6.Text);

                            SqlCommand add = new SqlCommand(personel_add, connection);
                            add.Parameters.AddWithValue("@name", textBox4.Text);
                            add.Parameters.AddWithValue("@surname", textBox5.Text);
                            add.Parameters.AddWithValue("@code", maskedTextBox1.Text);
                            add.Parameters.AddWithValue("@password", hashed_password);
                            add.ExecuteNonQuery();
                            MessageBox.Show("Succesfully");
                            connection.Close();
                        }
                        else
                        {
                            MessageBox.Show("Code is incorrect");
                        }                        
                    }
                }
                else
                {
                    MessageBox.Show("Name and Surname cannot be same");
                }
            }
            else
            {
                MessageBox.Show("Please fill all required");
            }
        }

        public class Sha256convertor
        {
            public static string sha256hash_(string rawData)
            {
                using (SHA256 sha256 = SHA256.Create())
                {
                    byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(rawData));
                    StringBuilder builder = new StringBuilder();
                    for (int i = 0; i < bytes.Length; i++)
                    {
                        builder.Append(bytes[i].ToString("x2"));
                    }
                    return builder.ToString();
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            register_hide();
        }

        public void register_hide()
        {
            label3.Hide();
            label7.Hide();
            label6.Hide();
            label5.Hide();
            label4.Hide();
            textBox4.Hide();
            textBox5.Hide();
            textBox6.Hide();
            maskedTextBox1.Hide();
            button2.Hide();
            linkLabel2.Hide();
            label11.Hide();
        }

        public void register_show()
        {
            label3.Show();
            label7.Show();
            label6.Show();
            label5.Show();
            label4.Show();
            textBox4.Show();
            textBox5.Show();
            textBox6.Show();
            maskedTextBox1.Show();
            button2.Show();
            label10.Hide();
            linkLabel1.Hide();
            linkLabel2.Show();
            label11.Show();
            button5.Hide();
            label8.Hide();
            label9.Hide();

            btnlogin.Hide();
            txtcode.Hide();
            txtpassword.Hide();
        }

        public void login_show()
        {
            txtcode.Show();
            txtpassword.Show();
            btnlogin.Show();
            label3.Show();
            linkLabel1.Show();
            label10.Show();
            button5.Show();
            label8.Show();
            label9.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            login_show();
            register_hide();
        }

        private void button5_MouseDown(object sender, MouseEventArgs e)
        {
            txtpassword.UseSystemPasswordChar = false;
        }

        private void button5_MouseUp(object sender, MouseEventArgs e)
        {
            txtpassword.UseSystemPasswordChar = true;
        }

        private void btnlogin_Click(object sender, EventArgs e)
        {
            connection.Open();
            SqlCommand login = new SqlCommand("Select * from Psn_Register where psn_code=@code and psn_password=@password",connection);
            
            string hashed_passwordlogin = Sha256convertor.sha256hash_(txtpassword.Text);
            login.Parameters.AddWithValue("@code",txtcode.Text);
            login.Parameters.AddWithValue("@password",hashed_passwordlogin);
            login.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter(login);

            DataTable dt = new DataTable();

            da.Fill(dt);

            if (dt.Rows.Count > 0 )
            {
                MessageBox.Show("Succesfully");
                MainWindow fmr = new MainWindow();
                fmr.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Name or password incorrect");
            }

            connection.Close();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Random rnd = new Random();
            int code;
            code = rnd.Next(0, 99999);
            label3.Text = ("Your Code is : " + code);
            global_code = code;
            register_show();
            txtcode.Clear();
            txtpassword.Clear();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            login_show();
            register_hide();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            maskedTextBox1.Clear();
        }
    }
}
