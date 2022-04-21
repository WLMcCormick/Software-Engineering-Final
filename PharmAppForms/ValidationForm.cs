using System;
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

            int w = 1;
            int count = 1;
            double[] dataHPLC = OurConnection.getHPLCValues();
            this.dataGridView2.Columns.Add("1", "HPLC Values");
            this.dataGridView2.Columns.Add("2", "HPLC Values");
            this.dataGridView2.Columns.Add("3", "HPLC Values");
            this.dataGridView2.Columns.Add("4", "HPLC Values");
            this.dataGridView2.Rows.Add(1);
            for (int i = 1; i < dataHPLC.Length; i = i + 2)
            {
                if (count % 5 == 0)
                {
                    count = 1;
                    w++;
                    this.dataGridView2.Rows.Add(w);
                }
                this.dataGridView2.Rows[w].Cells[count.ToString()].Value = dataHPLC[i];
                count++;



            }

        }
        private void Main_Click(object sender, EventArgs e)
        {
            EntryForm.instance.Show();
            this.Hide();
        }
        private void Pass_Click(object sender, EventArgs e)
        {
            string selected = comboBox1.SelectedItem.ToString();
            OurConnection.updateReportStatus(selected, "Finalized");
            EntryForm.instance.Show();
            this.Close();


        }
        private void Fail_Click(object sender, EventArgs e)
        {
            string selected = comboBox1.SelectedItem.ToString();
            CorrectionForm correct = new CorrectionForm(selected);
            correct.Show();
            this.Hide();


        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void ValidationForm_Load(object sender, EventArgs e)
        {

        }
    }
}
