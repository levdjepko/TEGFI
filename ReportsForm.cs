using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace TEGFI_3
{
    public partial class ReportsForm : Form
    {
        public string ConnectionString { get; set; }
        
        // this is the main connection string to our database
        public string UserName { get; set; }
        public MySqlConnection cnn { get; set; }

        //SELECT ALL command here

        public ReportsForm(string userName, string connectionString)
        {
            InitializeComponent();
            UserName = userName;
            ConnectionString = connectionString;
            LoadDataToShow();
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            // Return back to the main screen
            this.Hide();
            var form2 = new MainScreen();
            form2.Closed += (s, args) => this.Close();
            form2.Show();
        }

        //This loads all the data from the Mysql database
        private void LoadDataToShow()
        {
            MySqlConnection cnn;
            ConnectionString = "Server=3.227.166.251; database=U053QS; UID=U053QS; password=53688416942";
            cnn = new MySqlConnection(ConnectionString);
            MySqlCommand selectData;

            selectData = cnn.CreateCommand();

            // Declare the sript of sql command
            selectData.CommandText = $"SELECT id AS '#' , expense AS '$ Amount ', date AS 'Date', ten_year AS 'Wealth in 10 years', Description FROM U053QS.expenses WHERE user = \"{UserName}\"";

            using (MySqlConnection conn = new MySqlConnection(ConnectionString))
            {
                using (MySqlDataAdapter adapter = new MySqlDataAdapter(selectData.CommandText, conn))
                {
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);
                    dataGridView1.DataSource = ds.Tables[0];
                }
            }
        }
        //This loads only data from date to date (from date pickers)
        private void LoadDataToShow(string fromDate, string toDate)
        {
            cnn = new MySqlConnection(ConnectionString);
            MySqlCommand selectData;

            selectData = cnn.CreateCommand();

            // Declare the sript of sql command
            selectData.CommandText = $"SELECT id AS '#' , expense AS '$ Amount ', date AS 'Date', ten_year AS 'Wealth in 10 years', Description " +
                $"FROM U053QS.expenses " +
                $"WHERE user = \"{UserName}\" AND Date BETWEEN \"{fromDate}\" AND \"{toDate}\"";

            using (MySqlConnection conn = new MySqlConnection(ConnectionString))
            {
                using (MySqlDataAdapter adapter = new MySqlDataAdapter(selectData.CommandText, conn))
                {
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);
                    dataGridView1.DataSource = ds.Tables[0];
                }
            }
        }

        private void saveReport_Button_Click(object sender, EventArgs e)
        {
          
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            string fromDate = dateTimePicker1.Value.ToString("yyyy-MM-dd");
            string toDate = dateTimePicker2.Value.ToString("yyyy-MM-dd");
            LoadDataToShow(fromDate, toDate);
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            string fromDate = dateTimePicker1.Value.ToString("yyyy-MM-dd");
            string toDate = dateTimePicker2.Value.ToString("yyyy-MM-dd");
            LoadDataToShow(fromDate, toDate);
        }

    }
}
