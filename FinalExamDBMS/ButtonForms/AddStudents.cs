using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace FinalExamDBMS.ButtonForms
{
    //Carlos David Tabacon
    //BSIT-2
    
    public partial class AddStudents : Form
    {
        private const string ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\WINDOWS 10\Documents\Carlos David Tabacon\OOP Project\FinalExamDBMS\DatabaseForDBMSExam.accdb";
        public AddStudents()
        {
            InitializeComponent();
        }
        private int calculatedAge;
        private void AddStudents_Load(object sender, EventArgs e)
        {

        }


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

       

        private byte[] GetImageData()
        {
            if (pictureBox1.Image != null)
            {
                // Convert the image to a byte array for storage in the database
                using (MemoryStream ms = new MemoryStream())
                {
                    pictureBox1.Image.Save(ms, ImageFormat.Jpeg); // You can change the format as needed
                    return ms.ToArray();
                }
            }
            return null;
        }

        private void ClearInputData()
        {
            // Clear the input fields after insertion
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            dateTimePicker1.Value = DateTime.Now;
            pictureBox1.Image = null;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Get values from textboxes and other controls
                string lastName = textBox1.Text;
                string firstName = textBox2.Text;
                string middleName = textBox3.Text;
                string studentCourse = textBox4.Text;
                string address = textBox5.Text;
                DateTime birthdate = dateTimePicker1.Value;

                // Calculate age from birthdate
                int age = DateTime.Now.Year - birthdate.Year;

                // Set the calculated age to the label
                calculatedAge = DateTime.Now.Year - birthdate.Year;

                // Get image data
                byte[] imageData = GetImageData();

                // Create a connection to the database
                using (OleDbConnection connection = new OleDbConnection(ConnectionString))
                {
                    connection.Open();

                    // Create the SQL command with parameters
                    string insertQuery = "INSERT INTO Students (LastName, FirstName, MiddleName, StudentCourse, Address, Birthdate, Age, ImageData) " +
                                         "VALUES (@LastName, @FirstName, @MiddleName, @StudentCourse, @Address, @Birthdate, @Age, @ImageData)";

                    using (OleDbCommand command = new OleDbCommand(insertQuery, connection))
                    {
                        // Add parameters with their values
                        command.Parameters.AddWithValue("@LastName", lastName);
                        command.Parameters.AddWithValue("@FirstName", firstName);
                        command.Parameters.AddWithValue("@MiddleName", middleName);
                        command.Parameters.AddWithValue("@StudentCourse", studentCourse);
                        command.Parameters.AddWithValue("@Address", address);
                        command.Parameters.AddWithValue("@Birthdate", birthdate);
                        command.Parameters.AddWithValue("@Age", age);

                        if (imageData != null)
                        {
                            command.Parameters.Add("@ImageData", OleDbType.LongVarBinary).Value = imageData;
                        }
                        else
                        {
                            // Handle case where no image is provided
                            command.Parameters.Add("@ImageData", OleDbType.LongVarBinary).Value = DBNull.Value;
                        }

                        // Execute the query
                        command.ExecuteNonQuery();
                    }

                    MessageBox.Show("Student data added successfully!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

            ClearInputData();
        }


        private void button2_Click(object sender, EventArgs e)
        {
            // Create OpenFileDialog to allow the user to select an image
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files (*.png;*.jpeg;*.jpg;*.gif;*.bmp)|*.png;*.jpeg;*.jpg;*.gif;*.bmp";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Display the selected image in the PictureBox
                    pictureBox1.Image = new Bitmap(openFileDialog.FileName);
                }
            }
        }


        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

       
    }
}
