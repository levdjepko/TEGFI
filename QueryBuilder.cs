using MySql.Data.MySqlClient;
using System.Data;

namespace TEGFI_3
{
    public static class QueryBuilder
    {

        //this class will be a base class to create all the queries that manipulate data in a database
        //in one way or another

        public static DataSet PopulateTheForm(string userName, MySqlConnection connection)
        {

            MySqlConnection cnn = connection;

            MySqlCommand selectData = cnn.CreateCommand();

            // Declare the sript of sql command
            selectData.CommandText = $"SELECT id AS '#' , expense AS '$ Amount ', date AS 'Date', ten_year AS 'Wealth in 10 years', Description FROM U053QS.expenses WHERE user = \"{userName}\"";

            using (MySqlConnection conn = connection)
            {
                using (MySqlDataAdapter adapter = new MySqlDataAdapter(selectData.CommandText, conn))
                {
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);
                    return ds;
                }
            }
        }
    };
}
