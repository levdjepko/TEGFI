using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace TEGFI_3
{
    public partial class ReportsForm : Form
    {
        public string ConnectionString { get; set; }
        
        //this is the main connection string to our database
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
            //Return back to main screen
            this.Hide();
            var form2 = new MainScreen();
            form2.Closed += (s, args) => this.Close();
            form2.Show();
        }

        
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
            //TODO: Add reporting capabilities
            //Here we will have to open a file, then add a date to the file, and add a report with all the expenses for the period
            FileAccesser.WriteNewFile();
            FileAccesser.WriteASingleValue ("From date:  " + dateTimePicker1.Value.ToString("yyyy-MM-dd"));
            FileAccesser.NewLine();
            FileAccesser.WriteASingleValue ("To date:  " + dateTimePicker2.Value.ToString("yyyy-MM-dd"));
            FileAccesser.NewLine();

            
            for (int i = 0; i < dataGridView1.Columns.Count; i++)
                {
                    string header = dataGridView1.Columns[i].HeaderText;
                    FileAccesser.WriteASingleValue(header);
                    FileAccesser.WriteASingleValue("\t\t");
                }
            FileAccesser.NewLine();

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {

                for (int i = 0; i < dataGridView1.Columns.Count; i++)
                {
                    string cellText = row.Cells[i].Value.ToString();
                    FileAccesser.WriteASingleValue(cellText);
                    FileAccesser.WriteASingleValue("\t\t");
                }
                FileAccesser.NewLine();
            }
            MessageBox.Show("Report saved in file REPORT.txt in current folder. Thank you!");
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
