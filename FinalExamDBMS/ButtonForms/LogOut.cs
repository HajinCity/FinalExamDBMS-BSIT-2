﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinalExamDBMS.ButtonForms
{
    //Carlos David Tabacon
    //BSIT-2
    public partial class LogOut : Form
    {
        public LogOut()
        {
            InitializeComponent();
        }

        private void LogOut_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

      

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
