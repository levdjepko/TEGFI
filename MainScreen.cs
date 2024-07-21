using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace TEGFI_3
{
    public partial class MainScreen : Form
    {
        private const string ConnectionConstant = "Server=3.227.166.251; database=U053QS; UID=U053QS; password="; // This server is outdated now

        public MainScreen()
        {
            InitializeComponent();
        }

        private void exit_Button_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private string ConnectionString { get; set; }
        //this is the main connection string to our database
        public string UserName { get; set; }
        //SELECT ALL command here
                
        private void loadExpenses_button_Click(object sender, EventArgs e)
        {
            if (userName.TextLength == 0)
            {
                userName.Text = "Admin";
                UserName = userName.Text;
            }
            else
            {
                UserName = userName.Text;
            }

            MySqlConnection cnn;
            ConnectionString = ConnectionConstant;
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

        //INSERT EXPENSE command here
        //TODO: Move to the separate class
        private void addExpense_Button_Click(object sender, EventArgs e)
        {
            //check the user name associated with this command
            if (userName.TextLength == 0)
            {
                userName.Text = "Admin";
                UserName = userName.Text;
            }
            else
            {
                UserName = userName.Text;
            }

            try
            {
                //This is my connection string i have assigned the database file address path  
                string MyConnection2 = ConnectionConstant;
                //This is my insert query in which i am taking input from the user through windows forms 
                if (Double.TryParse(amountBox.Text, out double result))
                {
                    //get the amount of this expense in ten years
                    double tenYearsAmount = 0.0;
                    if (oneTime_Button.Checked)
                    {
                        tenYearsAmount = WealthEstimator.EstimateTenYearsAmount_OneTime(Double.Parse(amountBox.Text));
                        tenYears_Amount.Text = tenYearsAmount.ToString("C0");
                    }
                    if (monthlyButton.Checked)
                    {
                        tenYearsAmount = WealthEstimator.EstimateTenYearsAmount_Monthly(Double.Parse(amountBox.Text));
                        tenYears_Amount.Text = tenYearsAmount.ToString("C0");
                    }

                    string Query = $"INSERT INTO `U053QS`.`expenses` (`user`, `expense`, `date`, `ten_year`, `Description`) VALUES (\"{UserName}\", '{amountBox.Text}', CURRENT_DATE(), '{tenYearsAmount.ToString("0")}', '{description_Box.Text}');";
                    //This is  MySqlConnection here i have created the object and pass my connection string.  
                    MySqlConnection MyConn2 = new MySqlConnection(MyConnection2);
                    //This is command class which will handle the query and connection object.  
                    MySqlCommand MyCommand2 = new MySqlCommand(Query, MyConn2);
                    MySqlDataReader MyReader2;
                    MyConn2.Open();
                    MyReader2 = MyCommand2.ExecuteReader();     // Here our query will be executed and data saved into the database.  
                    MessageBox.Show("Expense added");

                    MyConn2.Close();
                }
                else
                {
                    MessageBox.Show("The data is in incorrect format, NOT inserted!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private int SelectedID { get; set; }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //do nothing
        }
        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            SelectedID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
            MessageBox.Show(SelectedID.ToString());
            //Used before for testing purposes
        }

        //MODIFY EXISTING Expense
        private void modifyExpense_Button_Click(object sender, EventArgs e)
        {
            Int32 selectedRowCount = dataGridView1.Rows.GetRowCount(DataGridViewElementStates.Selected);
            if (selectedRowCount == 1)
            {
                for (int i = 0; i < selectedRowCount; i++)
                {
                    SelectedID = Convert.ToInt32(dataGridView1.SelectedRows[i].Cells[0].Value.ToString());
                }
            }
            else { MessageBox.Show("Please select only one row for this operation"); }

            try
            {
                string MyConnection2 = ConnectionConstant;
                if (SelectedID != 0)
                {
                    string Query = $"UPDATE `U053QS`.`expenses` SET `Expense` = '{amountBox.Text}' WHERE (`id` = {SelectedID});";
                    MySqlConnection MyConn2 = new MySqlConnection(MyConnection2);
                    MySqlCommand MyCommand2 = new MySqlCommand(Query, MyConn2);
                    MySqlDataReader MyReader2;
                    MyConn2.Open();
                    MyReader2 = MyCommand2.ExecuteReader();     // Here our query will be executed and data saved into the database.  
                    MessageBox.Show("Expense modified");
                    MyConn2.Close();
                }
                else
                {
                    MessageBox.Show("Modification was unsuccessful!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        //DELETE Operation
        private void deleteExpense_Button_Click(object sender, EventArgs e)
        {
            Int32 selectedRowCount =
            dataGridView1.Rows.GetRowCount(DataGridViewElementStates.Selected);
            if (selectedRowCount == 1)
            {
                for (int i = 0; i < selectedRowCount; i++)
                {
                    SelectedID = Convert.ToInt32(dataGridView1.SelectedRows[i].Cells[0].Value.ToString());
                }
            }
            else { MessageBox.Show("Please select only one row for this operation"); }

            try
            {
                string MyConnection2 = ConnectionConstant;
                if (SelectedID != 0)
                {
                    string Query = $"DELETE FROM `U053QS`.`expenses` WHERE (`id` = {SelectedID});";
                    MySqlConnection MyConn2 = new MySqlConnection(MyConnection2);
                    MySqlCommand MyCommand2 = new MySqlCommand(Query, MyConn2);
                    MySqlDataReader MyReader2;
                    MyConn2.Open();
                    MyReader2 = MyCommand2.ExecuteReader();     // Here our query will be executed and data saved into the database.  
                    MessageBox.Show("Expense deleted");

                    MyConn2.Close();
                }
                else
                {
                    MessageBox.Show("Delete was unsuccessful!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void selectedRowsButton_Click(object sender, System.EventArgs e)
        {
            //used this for testing purposes
        }

        private void MainScreen_Load(object sender, EventArgs e)
        {
            //do something on the form load?
        }

        private void monthlyButton_CheckedChanged(object sender, EventArgs e)
        {
            try { tenYears_Amount.Text = WealthEstimator.EstimateTenYearsAmount_Monthly(Double.Parse(amountBox.Text)).ToString("C0"); }
            catch { }
        }

        private void oneTime_Button_CheckedChanged(object sender, EventArgs e)
        {
            try { tenYears_Amount.Text = WealthEstimator.EstimateTenYearsAmount_OneTime(Double.Parse(amountBox.Text)).ToString("C0"); }
            catch { }
        }

        //small comment section to explain what the app does and how it works below
        private void help_Button_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This application helps you to estimate how much your expenses - whether they are one-time or regular ones - " +
                "\n affecting your wealth over 10 years." +
                "\n \t Instructions to use: \n" +
                "1. Type your user name. If it a new name, a new user will be created\n" +
                "2. Start adding your expenses as you go, don't forget to check whether they are one-time or monthly\n" +
                "3. Use 'Load all expenses' Button to load a list of all the expenses under your name\n" +
                "4. Use 'Reports' Button to generate custom reports for different date periods\n" +
                "5. Enjoy! :)");
        }
        //Get to reports form, 
        private void reports_Button_Click(object sender, EventArgs e)
        {
            loadExpenses_button_Click(this, e);
            this.Hide();
            var form3 = new ReportsForm(UserName, ConnectionString);
            form3.Closed += (s, args) => this.Close();
            form3.Show();
        }
    }
}


