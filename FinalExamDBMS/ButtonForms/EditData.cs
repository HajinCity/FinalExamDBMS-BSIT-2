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
    public partial class EditData : Form
    {
        private const string ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\WINDOWS 10\Documents\Carlos David Tabacon\OOP Project\FinalExamDBMS\DatabaseForDBMSExam.accdb";
        public EditData()
        {
            InitializeComponent();
        }

        private void EditData_Load(object sender, EventArgs e)
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

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
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

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string studentIdToSearch = textBox1.Text;

            if (!string.IsNullOrEmpty(studentIdToSearch))
            {
                using (OleDbConnection connection = new OleDbConnection(ConnectionString))
                {
                    connection.Open();
                    string query = "SELECT * FROM Students WHERE studentID = @StudentID";

                    using (OleDbCommand command = new OleDbCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@StudentID", studentIdToSearch);

                        using (OleDbDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                textBox2.Text = reader["lastname"].ToString();
                                textBox3.Text = reader["firstname"].ToString();
                                textBox4.Text = reader["middlename"].ToString();
                                textBox5.Text = reader["studentcourse"].ToString();
                                textBox6.Text = reader["address"].ToString();

                                // Convert birthdate to DateTime
                                if (DateTime.TryParse(reader["birthdate"].ToString(), out DateTime birthdate))
                                {
                                    dateTimePicker1.Value = birthdate; // Set the value to DateTimePicker
                                }

                                // Set age to label10
                                label10.Text = reader["age"].ToString();

                                // Check if the "photo" column exists in the reader
                                if (reader["ImageData"] != DBNull.Value)
                                {
                                    // Assuming "pictureBox1" is a PictureBox control on your form
                                    pictureBox1.Image = ByteArrayToImage((byte[])reader["ImageData"]);
                                }
                                else
                                {
                                    // Handle the case where no photo is available
                                    pictureBox1.Image = null;
                                }
                            }
                            else
                            {
                                MessageBox.Show("Student not found.");
                                ClearTextBoxes(); // Implement this method to clear textboxes
                                                  // Clear the image if no student is found
                                pictureBox1.Image = null;
                            }
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Please enter a student ID.");
            }
        }
        private void ClearTextBoxes()
        {
            // Assuming these are the TextBoxes on your form
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";

            // Reset DateTimePicker to default mode
            dateTimePicker1.Value = DateTime.Now;

            // Assuming "pictureBox1" is a PictureBox control on your form
            pictureBox1.Image = null;

            // Clear the label
            label10.Text = "";
        }





        private Image ByteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string studentIdToUpdate = textBox1.Text;

            if (!string.IsNullOrEmpty(studentIdToUpdate))
            {
                try
                {
                    using (OleDbConnection connection = new OleDbConnection(ConnectionString))
                    {
                        connection.Open();
                        string query = "UPDATE Students SET lastname = @LastName, firstname = @FirstName, " +
                                       "middlename = @MiddleName, studentcourse = @StudentCourse, " +
                                       "address = @Address, birthdate = @Birthdate, age = @Age " +
                                       "WHERE studentID = @StudentID";

                        using (OleDbCommand command = new OleDbCommand(query, connection))
                        {
                            // Get values from the form controls
                            string lastName = textBox2.Text;
                            string firstName = textBox3.Text;
                            string middleName = textBox4.Text;
                            string studentCourse = textBox5.Text;
                            string address = textBox6.Text;
                            DateTime birthdate = dateTimePicker1.Value;

                            // Calculate age from birthdate
                            int age = DateTime.Now.Year - birthdate.Year;

                            // Make sure the PictureBox has the selected image
                            if (pictureBox1.Image != null)
                            {
                                // Get image data
                                byte[] imageData = GetImageData();

                                // Add parameters with their values
                                command.Parameters.AddWithValue("@LastName", lastName);
                                command.Parameters.AddWithValue("@FirstName", firstName);
                                command.Parameters.AddWithValue("@MiddleName", middleName);
                                command.Parameters.AddWithValue("@StudentCourse", studentCourse);
                                command.Parameters.AddWithValue("@Address", address);
                                command.Parameters.AddWithValue("@Birthdate", birthdate);
                                command.Parameters.AddWithValue("@Age", age);
                                command.Parameters.AddWithValue("@StudentID", studentIdToUpdate);

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
                                int rowsAffected = command.ExecuteNonQuery();

                                if (rowsAffected > 0)
                                {
                                    MessageBox.Show("Student data updated successfully!");
                                }
                                else
                                {
                                    MessageBox.Show("No changes made or student not found.");
                                }
                            }
                            else
                            {
                                MessageBox.Show("Please select an image.");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Please enter a student ID.");
            }
            ClearInputData();
        }
        private void ClearInputData()
        {
            // Clear the input fields after insertion
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            dateTimePicker1.Value = DateTime.Now;
            pictureBox1.Image = null;
            label10.Text = "";
        }
        private byte[] GetImageData()
        {
            if (pictureBox1.Image != null)
            {
                // Convert the image to a byte array for storage in the database
                using (MemoryStream ms = new MemoryStream())
                {
                    // Save the image in its original format
                    pictureBox1.Image.Save(ms, pictureBox1.Image.RawFormat);
                    return ms.ToArray();
                }
            }
            return null;
        }




        private void button3_Click(object sender, EventArgs e)
        {
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

        private void button4_Click(object sender, EventArgs e)
        {
            string studentIdToUpdate = textBox1.Text;

            if (!string.IsNullOrEmpty(studentIdToUpdate))
            {
                try
                {
                    using (OleDbConnection connection = new OleDbConnection(ConnectionString))
                    {
                        connection.Open();
                        string query = "UPDATE Students SET ImageData = @ImageData WHERE studentID = @StudentID";

                        using (OleDbCommand command = new OleDbCommand(query, connection))
                        {
                            // Get image data
                            byte[] imageData = GetImageData();

                            // Add parameters with their values
                            command.Parameters.AddWithValue("@ImageData", OleDbType.LongVarBinary).Value = imageData;
                            command.Parameters.AddWithValue("@StudentID", studentIdToUpdate);

                            // Execute the query
                            int rowsAffected = command.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Image updated successfully!");
                            }
                            else
                            {
                                MessageBox.Show("No changes made or student not found.");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Please enter a student ID.");
            }
            ClearInputData();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                FetchStudentData();
            }
        }

        private void FetchStudentData()
        {
            string studentIdToSearch = textBox1.Text;

            if (!string.IsNullOrEmpty(studentIdToSearch))
            {
                using (OleDbConnection connection = new OleDbConnection(ConnectionString))
                {
                    connection.Open();
                    string query = "SELECT * FROM Students WHERE studentID = @StudentID";

                    using (OleDbCommand command = new OleDbCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@StudentID", studentIdToSearch);

                        using (OleDbDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                textBox2.Text = reader["lastname"].ToString();
                                textBox3.Text = reader["firstname"].ToString();
                                textBox4.Text = reader["middlename"].ToString();
                                textBox5.Text = reader["studentcourse"].ToString();
                                textBox6.Text = reader["address"].ToString();

                                // Convert birthdate to DateTime
                                if (DateTime.TryParse(reader["birthdate"].ToString(), out DateTime birthdate))
                                {
                                    dateTimePicker1.Value = birthdate; // Set the value to DateTimePicker
                                }

                                // Set age to label10
                                label10.Text = reader["age"].ToString();

                                // Check if the "photo" column exists in the reader
                                if (reader["ImageData"] != DBNull.Value)
                                {
                                    // Assuming "pictureBox1" is a PictureBox control on your form
                                    pictureBox1.Image = ByteArrayToImage((byte[])reader["ImageData"]);
                                }
                                else
                                {
                                    // Handle the case where no photo is available
                                    pictureBox1.Image = null;
                                }
                            }
                            else
                            {
                                MessageBox.Show("Student not found.");
                                ClearTextBoxes(); // Implement this method to clear textboxes
                                                  // Clear the image if no student is found
                                pictureBox1.Image = null;
                            }
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Please enter a student ID.");
            }
        }

    }
}
