using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gas_station
{
    public partial class Prices : Form
    {
        public Prices()
        {
            InitializeComponent();
        }
        SqlConnection connection = new SqlConnection("Data Source=DESKTOP-NT9V6AB;Initial Catalog=\"Gas station\";Integrated Security=True");

        double lpg_price;
        double gasoline;
        double change;
        double diesel;
        double electricity;

        private void button1_Click(object sender, EventArgs e)
        {
            lpg_price = Convert.ToDouble(lbl_lpg_price.Text);
            if (textBox1.Text.StartsWith("+"))
            {
                change = Convert.ToDouble(textBox1.Text);
                lpg_price = lpg_price + change;
                lbl_lpg_price.Text = lpg_price.ToString();
            }
            if (textBox1.Text.StartsWith("-"))
            {
                change = Convert.ToDouble(textBox1.Text);
                lpg_price = lpg_price + change;
                lbl_lpg_price.Text = lpg_price.ToString();
            }
            textBox1.Clear();
            
            connection.Open();
            SqlCommand lpg_update = new SqlCommand("update Fuel_prices set lpg_price=@lpg",connection);
            lpg_update.Parameters.AddWithValue("@lpg", lbl_lpg_price.Text);
            lpg_update.ExecuteNonQuery();
            connection.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            gasoline = Convert.ToDouble(lbl_gasoline_price.Text);
            if (textBox2.Text.StartsWith("+"))
            {
                change = Convert.ToDouble(textBox2.Text);
                gasoline = gasoline + change;
                lbl_gasoline_price.Text = gasoline.ToString();
            }
            if (textBox2.Text.StartsWith("-"))
            {
                change = Convert.ToDouble(textBox2.Text);
                gasoline = gasoline + change;
                lbl_gasoline_price.Text = gasoline.ToString();
            }
            textBox2.Clear();
            
            connection.Open();
            SqlCommand gasoline_update = new SqlCommand("update Fuel_prices set gasoline_price=@gasoline", connection);
            gasoline_update.Parameters.AddWithValue("@gasoline", lbl_gasoline_price.Text);
            gasoline_update.ExecuteNonQuery();
            connection.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            diesel = Convert.ToDouble(lbl_dieseL_price.Text);
            if (textBox3.Text.StartsWith("+"))
            {
                change = Convert.ToDouble(textBox3.Text);
                diesel = diesel + change;
                lbl_dieseL_price.Text = diesel.ToString();
            }
            if (textBox3.Text.StartsWith("-"))
            {
                change = Convert.ToDouble(textBox3.Text);
                diesel = diesel + change;
                lbl_dieseL_price.Text = diesel.ToString();
            }
            textBox3.Clear();

            connection.Open();
            SqlCommand diesel_update = new SqlCommand("update Fuel_prices set diesel_price=@diesel", connection);
            diesel_update.Parameters.AddWithValue("@diesel", lbl_dieseL_price.Text);
            diesel_update.ExecuteNonQuery();
            connection.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            electricity = Convert.ToDouble(lbl_electric_price.Text);
            if (textBox4.Text.StartsWith("+"))
            {
                change = Convert.ToDouble(textBox4.Text);
                electricity = electricity + change;
                lbl_electric_price.Text = electricity.ToString();
            }
            if (textBox4.Text.StartsWith("-"))
            {
                change = Convert.ToDouble(textBox4.Text);
                electricity = electricity + change;
                lbl_electric_price.Text = electricity.ToString();
            }
            textBox4.Clear();

            connection.Open();
            SqlCommand electiricty_update = new SqlCommand("update Fuel_prices set electricity_price=@electiricity", connection);
            electiricty_update.Parameters.AddWithValue("@electiricity", lbl_electric_price.Text);
            electiricty_update.ExecuteNonQuery();
            connection.Close();
        }

        private void Prices_Load(object sender, EventArgs e)
        {
            connection.Open();
            SqlCommand show_lpg = new SqlCommand("select lpg_price from Fuel_prices", connection);
            SqlDataReader reader_lpg = show_lpg.ExecuteReader();
            while (reader_lpg.Read())
            {
                lbl_lpg_price.Text = reader_lpg[0].ToString();
            }
            connection.Close();
            
            connection.Open();
            SqlCommand show_dieseL = new SqlCommand("select diesel_price from Fuel_prices",connection);
            SqlDataReader reader_diesel = show_dieseL.ExecuteReader();
            while (reader_diesel.Read())
            {
                lbl_dieseL_price.Text = reader_diesel[0].ToString();
            }
            connection.Close();
            connection.Open();
            SqlCommand show_electricity = new SqlCommand("select electricity_price from Fuel_prices", connection);
            SqlDataReader reader_electricity = show_electricity.ExecuteReader();
            while (reader_electricity.Read())
            {
                lbl_electric_price.Text = reader_electricity[0].ToString();
            }
            connection.Close();
            connection.Open();
            SqlCommand show_gasoline = new SqlCommand("select gasoline_price from Fuel_prices",connection);
            SqlDataReader reader_gasoline = show_gasoline.ExecuteReader();
            while (reader_gasoline.Read())
            {
                lbl_gasoline_price.Text = reader_gasoline[0].ToString();
            }
            connection.Close();

            textBox1.Enabled = false;
            textBox2.Enabled = false;
            textBox3.Enabled = false;
            textBox4.Enabled = false;

            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainWindow frm = new MainWindow();
            frm.Show();
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem.ToString() == "LPG")
            {
                textBox2.Enabled = false;
                textBox3.Enabled = false;
                textBox4.Enabled = false;
                button2.Enabled = false;
                button3.Enabled = false;
                button4.Enabled = false;
                
                button1.Enabled = true;
                textBox1.Enabled = true;
                MessageBox.Show("When updating the price, please put \"+\" in front of it to increase it and \"-\" in front of it to decrease it.");

            }
            else if (comboBox1.SelectedItem.ToString() == "Gasoline")
            {
                textBox1.Enabled = false;
                textBox3.Enabled = false;
                textBox4.Enabled = false;
                button1.Enabled = false;
                button3.Enabled = false;
                button4.Enabled = false;

                button2.Enabled = true;
                textBox2.Enabled = true;
                MessageBox.Show("When updating the price, please put \"+\" in front of it to increase it and \"-\" in front of it to decrease it.");
            }
            else if (comboBox1.SelectedItem.ToString() == "Diesel")
            {
                textBox1.Enabled = false;
                textBox2.Enabled = false;
                textBox4.Enabled = false;
                button1.Enabled = false;
                button2.Enabled = false;
                button4.Enabled = false;

                button3.Enabled = true;
                textBox3.Enabled = true;
                MessageBox.Show("When updating the price, please put \"+\" in front of it to increase it and \"-\" in front of it to decrease it.");

            }
            else
            {
                textBox1.Enabled = false;
                textBox2.Enabled = false;
                textBox3.Enabled = false;
                button1.Enabled = false;
                button2.Enabled = false;
                button3.Enabled = false;

                button4.Enabled = true;
                textBox4.Enabled = true;
                MessageBox.Show("When updating the price, please put \"+\" in front of it to increase it and \"-\" in front of it to decrease it.");

            }
        }
    }
}
