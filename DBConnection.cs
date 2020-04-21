using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;


namespace TEGFI_3
{
    public static class DBConnection
    {
        public static void CheckConnection(string id, string password, string user)
        {
            string connetionString = null;
            MySqlConnection cnn;
            connetionString = $"Server=3.227.166.251; database=U053QS; UID={id}; password={password}";
            cnn = new MySqlConnection(connetionString);
            try
            {
                cnn.Open();
                MessageBox.Show("Connection Open ! ");
                cnn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Can not open connection ! ");
            }
        }

        public static bool ConnectToDatabase(string id, string password)
        {
            string connetionString = null;
            MySqlConnection cnn;
            connetionString = $"Server=3.227.166.251; database=U053QS; UID={id}; password={password}";
            cnn = new MySqlConnection(connetionString);
            try
            {
                cnn.Open();
                MessageBox.Show("Successful login!");
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Can not open connection !");
                return false;
            }
        }

        public static void GetAllData()
        {
            //used in a different way
        }

    }
}
