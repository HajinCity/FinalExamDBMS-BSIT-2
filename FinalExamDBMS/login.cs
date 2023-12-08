using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinalExamDBMS
{
    //Carlos David Tabacon
    //BSIT-2
    //DBMS Project
    public partial class login : Form
    {
        private const string ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\WINDOWS 10\Documents\Carlos David Tabacon\OOP Project\FinalExamDBMS\DatabaseForDBMSExam.accdb";
        public login()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        //Clicking The Button Login function
        private void button1_Click(object sender, EventArgs e)
        {
            //Establishing the connection of your Database 
            using (OleDbConnection con = new OleDbConnection(ConnectionString))
            {
                //try and catch handling
                try
                {
                    //opening of connection
                    con.Open();

                    using (OleDbCommand command = new OleDbCommand())
                    {
                        //Queries
                        command.Connection = con;
                        command.CommandText = "select * from [usersLogin] where Username=@username and Password=@password";
                        command.Parameters.AddWithValue("@username", textBox1.Text);
                        command.Parameters.AddWithValue("@password", textBox2.Text);

                        //reading of the data on the database
                        using (OleDbDataReader reader = command.ExecuteReader())
                        {
                            int accountExist = 0;

                            //infinite loop for logging in
                            while (reader.Read())
                            {
                                accountExist = accountExist + 1;
                            }
                            //if data exist, then go to dashboard form
                            if (accountExist == 1)
                            {
                                Dashboard dash = new Dashboard();
                                this.Hide();
                                dash.ShowDialog();
                            }
                            else
                            {
                                MessageBox.Show("Incorrect Username or Password");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    //Exception Handling, Any error on the program will be catch on this code
                    //mostly general errors of the program
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
            }
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //calling our method class to perform
                PerformLogin();
                //if we press enter then we can automatically go to our dashboard
            }
        }
        //method program
        private void PerformLogin()
        {
            //reference on the KeyDown events located in the properties the lightning bolt
            using (OleDbConnection con = new OleDbConnection(ConnectionString))
            {
                try
                {
                    con.Open();

                    using (OleDbCommand command = new OleDbCommand())
                    {
                        command.Connection = con;
                        command.CommandText = "select * from [usersLogin] where Username=@username and Password=@password";
                        command.Parameters.AddWithValue("@username", textBox1.Text);
                        command.Parameters.AddWithValue("@password", textBox2.Text);

                        using (OleDbDataReader reader = command.ExecuteReader())
                        {
                            int accountExist = 0;
                            //reading of data in database is infinite
                            while (reader.Read())
                            {
                                accountExist = accountExist + 1;
                            }

                            if (accountExist == 1)
                            {
                                //if account exist then go to the dashboard
                                Dashboard dash = new Dashboard();
                                this.Hide();
                                dash.ShowDialog();
                            }
                            else
                            {
                                MessageBox.Show("Incorrect Username or Password");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    //Exception Handling, Any error on the program will be catch on this code
                    //mostly general errors of the program
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
            }
        }

    }
}
