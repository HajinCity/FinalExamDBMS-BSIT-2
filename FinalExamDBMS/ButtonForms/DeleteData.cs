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

namespace FinalExamDBMS.ButtonForms
{
    //Carlos David Tabacon
    //BSIT-2
    public partial class DeleteData : Form
    {
        private const string ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\WINDOWS 10\Documents\Carlos David Tabacon\OOP Project\FinalExamDBMS\DatabaseForDBMSExam.accdb";
        public DeleteData()
        {
            InitializeComponent();
        }

        private void DeleteData_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Retrieve the StudentID from the textbox
            string studentIDText = textBox1.Text;

            if (int.TryParse(studentIDText, out int studentID))
            {
                // Call the method to delete data based on StudentID
                DeleteStudentData(studentID);
            }
            else
            {
                MessageBox.Show("Please enter a valid StudentID.");
            }
        }

        private void DeleteStudentData(int studentID)
        {
            try
            {
                // Create a connection to the database
                using (OleDbConnection connection = new OleDbConnection(ConnectionString))
                {
                    connection.Open();

                    // Create the SQL command with parameters
                    string deleteQuery = "DELETE FROM Students WHERE StudentID = @StudentID";

                    using (OleDbCommand command = new OleDbCommand(deleteQuery, connection))
                    {
                        // Add parameter with StudentID value
                        command.Parameters.AddWithValue("@StudentID", studentID);

                        // Execute the query
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Student data deleted successfully!");
                        }
                        else
                        {
                            MessageBox.Show("No records found for the given StudentID.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
}
