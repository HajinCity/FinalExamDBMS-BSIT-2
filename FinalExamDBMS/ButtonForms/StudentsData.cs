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
    public partial class StudentsData : Form
    {
        private const string ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\WINDOWS 10\Documents\Carlos David Tabacon\OOP Project\FinalExamDBMS\DatabaseForDBMSExam.accdb";
        public StudentsData()
        {
            InitializeComponent();
        }

        private void StudentsData_Load(object sender, EventArgs e)
        {
            // Load data into the DataGridView when the form is loaded
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                using (OleDbConnection connection = new OleDbConnection(ConnectionString))
                {
                    connection.Open();

                    // Query to select all data from the Students table
                    string selectQuery = "SELECT * FROM Students";

                    using (OleDbDataAdapter adapter = new OleDbDataAdapter(selectQuery, connection))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        // Bind the DataTable to the DataGridView
                        dataGridView1.DataSource = dataTable;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
