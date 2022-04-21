﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PharmApp
{
    public partial class ValidationForm : Form
    {
        DbConnector2 OurConnection = new DbConnector2();
        public static ValidationForm instance;
        public ValidationForm()
        {
            InitializeComponent();
            instance = this;
            string[] dropDownElements = new string[OurConnection.determineDropArrayQC()];
            dropDownElements = OurConnection.getCorrections();

            for (int i = 0; i < dropDownElements.Length; i++)
            {
                if (dropDownElements[i] != null)
                {
                    this.comboBox1.Items.Add(dropDownElements[i]);
                }

            }
        }

        private void Pull_Click(object sender, EventArgs e)
        {
            string selected = comboBox1.SelectedItem.ToString();

            DataTable dt = new DataTable();

            dt = OurConnection.getReport(selected);

            dataGridView1.DataSource = dt;

        }
        private void Main_Click(object sender, EventArgs e)
        {
            EntryForm.instance.Show();
            this.Hide();
        }
        private void Pass_Click(object sender, EventArgs e)
        {


        }
        private void Fail_Click(object sender, EventArgs e)
        { 
            if (CorrectionForm.instance != null)
            {
                CorrectionForm.instance.Show();
                this.Hide();
            }
            else
            {
                CorrectionForm correct = new CorrectionForm();
                correct.Show();
                this.Hide();
            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void ValidationForm_Load(object sender, EventArgs e)
        {

        }
    }
}
