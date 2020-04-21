using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TEGFI_3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void connect_Button_Click(object sender, EventArgs e)
        {
            if(DBConnection.ConnectToDatabase(loginTextBox.Text, passwordTextBox.Text))
            {
                this.Hide();
                var form2 = new MainScreen();
                form2.Closed += (s, args) => this.Close();
                form2.Show();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //temporarily have a password in the box for debugging reasons
            loginTextBox.Text = "U053QS";
            passwordTextBox.Text = "53688416942";
        }
    }
}
