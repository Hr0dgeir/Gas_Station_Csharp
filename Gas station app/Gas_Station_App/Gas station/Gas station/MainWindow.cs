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
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        SqlConnection connection = new SqlConnection("Data Source=DESKTOP-NT9V6AB;Initial Catalog=\"Gas station\";Integrated Security=True");

        private void MainWindow_Load(object sender, EventArgs e)
        {
            groupBox1.Hide();
            button4.Hide();

            connection.Open();
            SqlCommand show_lpg = new SqlCommand("select lpg_price from Fuel_prices", connection);
            SqlDataReader reader_lpg = show_lpg.ExecuteReader();
            while (reader_lpg.Read())
            {
                lbl_lpg.Text = reader_lpg[0].ToString();
            }
            connection.Close();

            connection.Open();
            SqlCommand show_dieseL = new SqlCommand("select diesel_price from Fuel_prices", connection);
            SqlDataReader reader_diesel = show_dieseL.ExecuteReader();
            while (reader_diesel.Read())
            {
                lbl_diesel.Text = reader_diesel[0].ToString();
            }
            connection.Close();
            connection.Open();
            SqlCommand show_electricity = new SqlCommand("select electricity_price from Fuel_prices", connection);
            SqlDataReader reader_electricity = show_electricity.ExecuteReader();
            while (reader_electricity.Read())
            {
                lbl_electric.Text = reader_electricity[0].ToString();
            }
            connection.Close();
            connection.Open();
            SqlCommand show_gasoline = new SqlCommand("select gasoline_price from Fuel_prices", connection);
            SqlDataReader reader_gasoline = show_gasoline.ExecuteReader();
            while (reader_gasoline.Read())
            {
                lbl_gasoline.Text = reader_gasoline[0].ToString();
            }
            connection.Close();

            connection.Open();
            SqlCommand lpg_stock = new SqlCommand("select lpg_stock from Stocks",connection);
            SqlDataReader lpg_read = lpg_stock.ExecuteReader();
            while (lpg_read.Read())
            {
                lbl_lpg_stock.Text = lpg_read[0].ToString();
            }
            connection.Close();

            connection.Open();
            SqlCommand diesel_stock = new SqlCommand("select diesel_stock from Stocks",connection);
            SqlDataReader diesel_read = diesel_stock.ExecuteReader();
            while (diesel_read.Read())
            {
                lbl_diesel_stock.Text = diesel_read[0].ToString();
            }
            connection.Close();

            connection.Open();
            SqlCommand gasoline_stock = new SqlCommand("select gasoline_stock from Stocks", connection);
            SqlDataReader gasloline_read = gasoline_stock.ExecuteReader();
            while (gasloline_read.Read())
            {
                lbl_gasoline_stock.Text = gasloline_read[0].ToString();
            }
            connection.Close();

            double lpg_round = Convert.ToDouble(lbl_lpg_stock.Text);
            lpg_round = Math.Round(lpg_round,MidpointRounding.AwayFromZero);

            double diesel_round = Convert.ToDouble(lbl_diesel_stock.Text);
            diesel_round = Math.Round(diesel_round, MidpointRounding.AwayFromZero);

            double gasoline_round = Convert.ToDouble(lbl_gasoline_stock.Text);
            gasoline_round = Math.Round(gasoline_round, MidpointRounding.AwayFromZero);

            progressBar3.Value = Convert.ToInt32(gasoline_round);
            progressBar2.Value = Convert.ToInt32(diesel_round);
            progressBar1.Value = Convert.ToInt32(lpg_round);

            pictureBox1.Location = new Point(6, 150);
            label10.Location = new Point(6, 85);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            groupBox1.Show();
            groupBox1.Location = new Point(0, 0);
            button4.Show();
            button4.Location = new Point(199, 12);

            pictureBox1.Hide();
            label10.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Prices frm = new Prices();
            frm.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            stocks frm = new stocks();
            frm.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            groupBox1.Hide();
            button4.Hide();
            button1.Show();

            pictureBox1.Show();
            label10.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            Sell frm = new Sell();
            frm.Show();
        }
    }
}
