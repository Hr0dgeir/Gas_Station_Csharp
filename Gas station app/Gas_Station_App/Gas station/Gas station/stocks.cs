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
    public partial class stocks : Form
    {
        public stocks()
        {
            InitializeComponent();
        }

        double new_stock;
        double lpg_stock;
        double gasoline_stock;
        double diesel_stock;
        double electricity_stock;
        SqlConnection connection = new SqlConnection("Data Source=DESKTOP-NT9V6AB;Initial Catalog=\"Gas station\";Integrated Security=True");

        private void button1_Click(object sender, EventArgs e)
        {
            lpg_stock = Convert.ToDouble(lbl_lpg_stock.Text);
            if (textBox1.Text.StartsWith("+"))
            {
                new_stock = Convert.ToDouble(textBox1.Text);
                lpg_stock = lpg_stock + new_stock;
                lbl_lpg_stock.Text = lpg_stock.ToString();
            }
            if (textBox1.Text.StartsWith("-"))
            {
                new_stock = Convert.ToDouble(textBox1.Text);
                lpg_stock = lpg_stock + new_stock;
                lbl_lpg_stock.Text = lpg_stock.ToString();
            }
            
            connection.Open();
            SqlCommand lpg_stock_update = new SqlCommand("update Stocks set lpg_stock=@lpg",connection);
            lpg_stock_update.Parameters.AddWithValue("@lpg", lpg_stock);
            lpg_stock_update.ExecuteNonQuery();
            connection.Close();
            
            double lpg_round = Convert.ToDouble(lbl_lpg_stock.Text);
            lpg_round = Math.Round(lpg_round, MidpointRounding.AwayFromZero);

            progressBar1.Value = Convert.ToInt32(lpg_round);

            textBox1.Clear();          
        }

        private void button2_Click(object sender, EventArgs e)
        {
            gasoline_stock = Convert.ToDouble(lbl_gasoline_stock.Text);
            if (textBox2.Text.StartsWith("+"))
            {
                new_stock = Convert.ToDouble(textBox2.Text);
                gasoline_stock = gasoline_stock + new_stock;
                lbl_gasoline_stock.Text = gasoline_stock.ToString(); 
            }
            if (textBox2.Text.StartsWith("-"))
            {
                new_stock = Convert.ToDouble(textBox2.Text);
                gasoline_stock = gasoline_stock + new_stock;
                lbl_gasoline_stock.Text = gasoline_stock.ToString();
            }
            connection.Open();
            SqlCommand gasoline_stock_update = new SqlCommand("update Stocks set gasoline_stock=@gasoline",connection);
            gasoline_stock_update.Parameters.AddWithValue("@gasoline",gasoline_stock);
            gasoline_stock_update.ExecuteNonQuery();
            connection.Close();
            double gasoline_round = Convert.ToDouble(lbl_gasoline_stock.Text);
            gasoline_round = Math.Round(gasoline_round, MidpointRounding.AwayFromZero);
            progressBar2.Value = Convert.ToInt32(gasoline_round);
            textBox2.Clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            diesel_stock = Convert.ToDouble(lbl_dieseL_stock.Text);
            if (textBox3.Text.StartsWith("+"))
            {
                new_stock = Convert.ToDouble(textBox3.Text);
                diesel_stock = diesel_stock + new_stock;
                lbl_dieseL_stock.Text = diesel_stock.ToString();
            }
            if (textBox3.Text.StartsWith("-"))
            {
                new_stock = Convert.ToDouble(textBox3.Text);
                diesel_stock = diesel_stock + new_stock;
                lbl_dieseL_stock.Text = diesel_stock.ToString();
            }
            connection.Open();
            SqlCommand diesel_stock_update = new SqlCommand("update Stocks set diesel_stock=@diesel", connection);
            diesel_stock_update.Parameters.AddWithValue("@diesel", diesel_stock);
            diesel_stock_update.ExecuteNonQuery();
            connection.Close();
            double diesel_round = Convert.ToDouble(lbl_dieseL_stock.Text);
            diesel_round = Math.Round(diesel_round, MidpointRounding.AwayFromZero);
            progressBar3.Value = Convert.ToInt32(diesel_round);
            textBox3.Clear();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            /*if (textBox4.Text.StartsWith("+"))
            {
                new_stock = Convert.ToDouble(textBox4.Text);
                electricity_stock = electricity_stock + new_stock;
                lbl_electric_stock.Text = electricity_stock.ToString();
            }
            if (textBox4.Text.StartsWith("-"))
            {
                new_stock = Convert.ToDouble(textBox4.Text);
                electricity_stock = electricity_stock + new_stock;
                lbl_electric_stock.Text = electricity_stock.ToString();
            }
            progressBar4.Value = Convert.ToInt32(electricity_stock);
            textBox4.Clear();
            */
        }

        private void stocks_Load(object sender, EventArgs e)
        {
            connection.Open();
            SqlCommand lpg_set = new SqlCommand("select lpg_stock from Stocks", connection);
            SqlDataReader lpg_read = lpg_set.ExecuteReader();
            while (lpg_read.Read())
            {
                lbl_lpg_stock.Text = lpg_read[0].ToString();
            }
            connection.Close();

            connection.Open();
            SqlCommand gasoline_set = new SqlCommand("select gasoline_stock from Stocks", connection);
            SqlDataReader gasoline_read = gasoline_set.ExecuteReader();
            while (gasoline_read.Read())
            {
                lbl_gasoline_stock.Text = gasoline_read[0].ToString();
            }
            connection.Close ();

            connection.Open();
            SqlCommand diesel_set = new SqlCommand("select diesel_stock from Stocks",connection);
            SqlDataReader diesel_read = diesel_set.ExecuteReader();
            while (diesel_read.Read())
            {
                lbl_dieseL_stock.Text = diesel_read[0].ToString();
            }
            connection.Close();

            double lpg_round = Convert.ToDouble(lbl_lpg_stock.Text);
            lpg_round = Math.Round(lpg_round, MidpointRounding.AwayFromZero);

            double diesel_round = Convert.ToDouble(lbl_dieseL_stock.Text);
            diesel_round = Math.Round(diesel_round, MidpointRounding.AwayFromZero);

            double gasoline_round = Convert.ToDouble(lbl_gasoline_stock.Text);
            gasoline_round = Math.Round(gasoline_round, MidpointRounding.AwayFromZero);

            progressBar1.Value = Convert.ToInt32(lpg_round);
            progressBar3.Value = Convert.ToInt32(diesel_round);
            progressBar2.Value = Convert.ToInt32(gasoline_round);
            progressBar4.Value = 1000;
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            textBox3.Enabled = false;

            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;

        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainWindow frm = new MainWindow();
            frm.Show();
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem.ToString() == "Lpg")
            {
                button1.Enabled = true;
                button2.Enabled = false;
                button3.Enabled = false;

                textBox1.Enabled = true;
                textBox2.Enabled = false;
                textBox3.Enabled = false;
            }
            else if (comboBox1.SelectedItem.ToString() == "Diesel")
            {
                button3.Enabled = true;
                button2.Enabled = false;
                button1.Enabled = false;

                textBox3.Enabled = true;
                textBox2.Enabled = false;
                textBox1.Enabled = false;
            }
            else if (comboBox1.SelectedItem.ToString() == "Gasoline")
            {
                button2.Enabled = true;
                button1.Enabled = false;
                button3.Enabled = false;

                textBox2.Enabled = true;
                textBox1.Enabled = false;
                textBox3.Enabled = false;
            }
        }
    }
}
