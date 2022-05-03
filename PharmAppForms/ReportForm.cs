using System;
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
    public partial class ReportForm : MetroSet_UI.Forms.MetroSetForm
    {
        DbConnector2 OurConnection = new DbConnector2();
        public static ReportForm instance;
        public ReportForm()
        {

            InitializeComponent();
            instance = this;
            this.FormClosed += new FormClosedEventHandler(OurConnection.Form_FormClosed);
            string[] dropDownElements = new string[OurConnection.determineDropArray()];
            dropDownElements = OurConnection.getReview();

            for (int i = 0; i <= dropDownElements.Length; i++)
            {
                if (i == dropDownElements.Length)
                {
                    this.comboBox1.Items.Add("New");
                }
                else
                {
                    if (dropDownElements[i] != null)
                    {
                        this.comboBox1.Items.Add(dropDownElements[i]);
                    }
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
            if (comboBox1.SelectedItem == null) { return; }
            if (textBox1.Text == "" || textBox1.Text == "Quantification Limit")
            {
                MessageBox.Show("Please enter a qualification limit.");
                return;
            }
            if (textBox2.Text == null || textBox1.Text == "Reporting Limit")
            {
                MessageBox.Show("Please enter a Reporting limit.");
                return;
            }

            string QL = textBox1.Text;
            string RL = textBox2.Text;
            string selected = this.comboBox1.SelectedItem.ToString();
            if (selected == "New")
            {
                string newRID = OurConnection.newReport(RL, QL);
                ViewForm viewer = new ViewForm(newRID);
                viewer.Show();
                this.Close();
                EntryForm.instance.Hide();
            }
            else
            {
                OurConnection.updateRLQL(RL, QL, selected);
                ViewForm viewer = new ViewForm(selected);
                viewer.Show();
                this.Close();
                EntryForm.instance.Hide();
            }
        }

        private void ReportForm_Load(object sender, EventArgs e)
        {

        }

        private void textBox2_Click(object sender, EventArgs e)
        {
            if (textBox2.Text.Contains("Limit"))
            {
                textBox2.Text = "";
            }
        }

        private void textBox1_Click(object sender, EventArgs e)
        {

            if (textBox1.Text.Contains("Limit"))
            {
                textBox1.Text = "";
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
