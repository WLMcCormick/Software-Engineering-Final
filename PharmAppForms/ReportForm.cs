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
    public partial class ReportForm : Form
    {
        DbConnector2 OurConnection = new DbConnector2();
        public static ReportForm instance;
        ComboBox comboBox1;
        public ReportForm()
        {
            InitializeComponent();
            instance = this;
            List<string> reportIds = OurConnection.getReportIds();
            comboBox1 = new ComboBox();
            comboBox1.Dock = DockStyle.Fill;
            foreach (string d in reportIds)
            {
                if (d != null)
                {
                    comboBox1.Items.Add(d);
                }
            }
            comboBox1.Items.Add("new");
            this.panel1.Controls.Add(comboBox1);

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
            if (selected == "New")
            {
                string newRID = OurConnection.newReport(RL, QL);
                ViewForm viewer = new ViewForm(newRID);
                viewer.Show();
                this.Close();
            }
            else
            {
                OurConnection.updateRLQL(RL, QL, selected);
                ViewForm viewer = new ViewForm(selected);
                viewer.Show();
                this.Close();

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
    }
}
