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
    public partial class Sell : Form
    {
        public Sell()
        {
            InitializeComponent();
        }
        SqlConnection connection = new SqlConnection("Data Source=DESKTOP-NT9V6AB;Initial Catalog=\"Gas station\";Integrated Security=True");

        double new_diesel_stock;
        double new_lpg_stock;
        double new_gasoline_stock;

        double stock_lpg;
        double stock_diesel;
        double stock_gasoline;

        double earning_lpg;
        double earning_gasoline;
        double earning_diesel;

        double lpg_price;
        double diesel_price;
        double gasoline_price;

        string selected_fuel;
        private void Sell_Load(object sender, EventArgs e)
        {
            connection.Open();
            SqlCommand price_set = new SqlCommand("select lpg_price , gasoline_price , diesel_price from Fuel_prices",connection);
            SqlDataReader reader = price_set.ExecuteReader();
            while (reader.Read())
            {
                lpg_price = Convert.ToDouble(reader[0]);
                gasoline_price = Convert.ToDouble(reader[1]);
                diesel_price = Convert.ToDouble(reader[2]);
            }
            connection.Close();

            connection.Open();
            SqlCommand set_stock = new SqlCommand("select lpg_stock , diesel_stock , gasoline_stock from Stocks",connection);
            SqlDataReader reader_stock = set_stock.ExecuteReader();
            while (reader_stock.Read())
            {
                stock_lpg = Convert.ToDouble(reader_stock[0]);
                stock_diesel = Convert.ToDouble(reader_stock[1]);
                stock_gasoline = Convert.ToDouble(reader_stock[2]);
            }
            connection.Close();

            textBox1.Enabled = false;
            button1.Enabled = false;
            textBox2.Enabled = false;
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem.ToString() == "LPG")
            {
                selected_fuel = "Lpg";
                label_information.Text = "Please write how much LPG will be purchased.";
                textBox1.Enabled = Enabled;
                button1.Enabled = Enabled;
                textBox2.Enabled = true;
            }
            else if (comboBox1.SelectedItem.ToString() == "Diesel")
            {
                selected_fuel = "Diesel";
                label_information.Text = "Please write how much Diesel will be purchased.";
                textBox1.Enabled = Enabled;
                button1.Enabled = Enabled;
                textBox2.Enabled = true;
            }
            else if (comboBox1.SelectedItem.ToString() == "Gasoline")
            {
                selected_fuel = "Gasoline";
                label_information.Text = "Please write how much Gasoline will be purchased.";
                textBox1.Enabled = Enabled;
                button1.Enabled = Enabled;
                textBox2.Enabled = true;
            }
            else
            {
                selected_fuel = "Electric";
                label_information.Text = "how many hours it will take to charge the car.";
                textBox1.Enabled = true;
                button1.Enabled = true;
                textBox2.Enabled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (selected_fuel == "Lpg" && textBox2.Text != "")
            {
                double selling_fuel;
                earning_lpg = Convert.ToDouble(textBox1.Text);
                selling_fuel = earning_lpg / lpg_price;
                if (stock_lpg >= selling_fuel)
                {
                    connection.Open();
                    string lpg_gain = "insert into Sales (lpg_gain,car_license_plate) values (@lpg,@plate) ";
                    SqlCommand lpg = new SqlCommand(lpg_gain,connection);
                    lpg.Parameters.AddWithValue("@lpg",earning_lpg);
                    lpg.Parameters.AddWithValue("@plate",textBox2.Text);
                    lpg.ExecuteNonQuery();
                    SqlCommand fuel_update = new SqlCommand("update Stocks set lpg_stock=@lpg",connection);
                    new_lpg_stock = stock_lpg - selling_fuel;
                    stock_lpg = new_lpg_stock;
                    fuel_update.Parameters.AddWithValue("lpg",new_lpg_stock);
                    fuel_update.ExecuteNonQuery();
                    connection.Close();
                    MessageBox.Show("Succesfully");
                }
                else
                {
                    MessageBox.Show("There is not that much LPG in the storage");
                }
                
                
            }
            if (selected_fuel == "Diesel" && textBox2.Text != "")
            {
                double selling_fuel2;
                earning_diesel = Convert.ToDouble(textBox1.Text);
                selling_fuel2 = earning_diesel / diesel_price;
                if (stock_diesel >= selling_fuel2)
                {
                    connection.Open();
                    string diesel_gain = "insert into Sales (diesel_gain,car_license_plate) values (@diesel,@plate) ";
                    SqlCommand diesel = new SqlCommand(diesel_gain, connection);
                    diesel.Parameters.AddWithValue("@diesel", earning_diesel);
                    diesel.Parameters.AddWithValue("@plate",textBox2.Text);
                    diesel.ExecuteNonQuery();
                    SqlCommand fuel_update = new SqlCommand("update Stocks set diesel_stock=@diesel_stock", connection);
                    new_diesel_stock = stock_diesel - selling_fuel2;
                    stock_diesel = new_diesel_stock;
                    fuel_update.Parameters.AddWithValue("@diesel_stock", new_diesel_stock);
                    fuel_update.ExecuteNonQuery();
                    connection.Close();
                    MessageBox.Show("Succesfully");
                }
                else
                {
                    MessageBox.Show("There is not that much Diesel in the storage");
                }
            }
            if (selected_fuel == "Gasoline" && textBox2.Text != "")
            {
                double selling_fuel3;
                earning_gasoline = Convert.ToDouble(textBox1.Text);
                selling_fuel3 = earning_gasoline / gasoline_price;
                if (stock_diesel >= selling_fuel3)
                {
                    connection.Open();
                    string gasoline_gain = "insert into Sales (gasoline_gain,car_license_plate) values (@gasoline,@plate)";
                    SqlCommand gasoline = new SqlCommand(gasoline_gain,connection);
                    gasoline.Parameters.AddWithValue("@gasoline",earning_gasoline);
                    gasoline.Parameters.AddWithValue("@plate",textBox2.Text);
                    gasoline.ExecuteNonQuery();
                    SqlCommand fuel_update = new SqlCommand("update Stocks set gasoline_stock=@gasoline_stock", connection);
                    new_gasoline_stock = stock_gasoline - selling_fuel3;
                    stock_gasoline = new_gasoline_stock;
                    fuel_update.Parameters.AddWithValue("@gasoline_stock", new_gasoline_stock);
                    fuel_update.ExecuteNonQuery();
                    connection.Close();
                    MessageBox.Show("Succesfully");
                }
                else
                {
                    MessageBox.Show("There is not that much Diesel in the storage");
                }
            }
            if (selected_fuel == "Electric" && textBox2.Text != "")
            {
                double connection_pay = 5;
                double kilowatt_hour = 0.75;
                double charge_time = Convert.ToDouble(textBox1.Text);
                double payment = charge_time * kilowatt_hour + connection_pay;
                MessageBox.Show(payment.ToString());
                connection.Open();
                string electric_gain = "insert into Sales (electric_gain,car_license_plate) values (@electric,@plate)";
                SqlCommand electric = new SqlCommand(electric_gain, connection);
                electric.Parameters.AddWithValue("@electric",payment);
                electric.Parameters.AddWithValue("@plate",textBox2.Text);
                electric.ExecuteNonQuery();
                MessageBox.Show("Succesfully");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainWindow frm = new MainWindow();
            frm.Show();
        }
    }
}
