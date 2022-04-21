﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static PharmApp.DbConnector2;
using MySqlConnector;

namespace PharmApp
{
    public partial class ReportForm : Form
    {
        DbConnector2 OurConnection = new DbConnector2();
        public static ReportForm instance;
        public ReportForm()
        {
            InitializeComponent();
            instance = this;       
                    string[] dropDownElements = new string[OurConnection.determineDropArray()];
                    dropDownElements = OurConnection.getReview();

                    for(int i = 0; i <= dropDownElements.Length; i++)
                    {
                        if (i == dropDownElements.Length)
                        {
                            this.comboBox1.Items.Add("New");
                        }
                        else {
                            this.comboBox1.Items.Add(dropDownElements[i]);
                        }  
                    }
        }
        private void Main_Click(object sender, EventArgs e)
        {
            EntryForm.instance.Show();
            this.Hide();
        }
        private void Generate_Click(object sender, EventArgs e)
        {
            string QL = textBox1.Text;
            string RL = textBox2.Text;
            string selected = this.comboBox1.SelectedItem.ToString();
            if ( selected == "New")
            {
                string newRID = OurConnection.newReport(RL, QL);
                ViewForm viewer = new ViewForm(newRID);
                viewer.Show();
                this.Hide();
            }
            else
            {
                OurConnection.updateRLQL(RL, QL, selected);
                ViewForm viewer = new ViewForm(selected);
                viewer.Show();
                this.Hide();

            }
        }

        private void ReportForm_Load(object sender, EventArgs e)
        {

        }
    }
}
