using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinalExamDBMS
{
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
        }
        private Form activeForm;
        //i am creating a method class establishing a referrence in activeform
        //
        public void OpenChildForm(Form childForm)
        {
            if (activeForm != null)
                activeForm.Close();

            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;

            panel2.Controls.Add(childForm);
            panel2.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //parameters      folder name   form name
            OpenChildForm(new ButtonForms.AddStudents());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenChildForm(new ButtonForms.StudentsData());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenChildForm(new ButtonForms.EditData());
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OpenChildForm(new ButtonForms.DeleteData());
        }

        private void button5_Click(object sender, EventArgs e)
        {
            OpenChildForm(new ButtonForms.LogOut());
        }
    }
}
