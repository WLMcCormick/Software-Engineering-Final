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


            object selected = comboBox1.SelectedItem;
            if (selected != null)
            {
                string selectedRid = selected.ToString();
                // Remove old data
                dataGridView2.Columns.Clear();
                dataGridView2.Rows.Clear();
                DataTable dt = new DataTable();
                dt = OurConnection.getReport(selectedRid);

                dataGridView1.DataSource = dt;

                int w = 0;
                int count = 1;
                double[] dataHPLC = OurConnection.getHPLCValues();
                this.dataGridView2.Columns.Add("1", "HPLC Values COL 1");
                this.dataGridView2.Columns.Add("2", "HPLC Values COL 2");
                this.dataGridView2.Columns.Add("3", "HPLC Values COL 3");
                this.dataGridView2.Columns.Add("4", "HPLC Values COL 4");
                this.dataGridView2.Rows.Add(1);

                //get rl and ql
                float[] rlql = OurConnection.GetRLQL(selectedRid);
                float rl = rlql[0];
                float ql = rlql[1];


                for (int i = 1; i < dataHPLC.Length; i = i + 2)
                {
                    if (count % 5 == 0)
                    {
                        count = 1;
                        w++;
                        this.dataGridView2.Rows.Add(w);
                    }
                    this.dataGridView2.Rows[w].Cells[count.ToString()].Value = dataHPLC[i];

                    if(dataHPLC[i] > rl && dataHPLC[i] < ql)
                    {
                        this.dataGridView2.Rows[w].Cells[count.ToString()].Style.BackColor = Color.Aqua;
                    }
                    else if(dataHPLC[i] > ql)
                    {
                        this.dataGridView2.Rows[w].Cells[count.ToString()].Style.BackColor = Color.Green;
                    }
                    count++;
                }
            }
        }
        private void Main_Click(object sender, EventArgs e)
        {
            EntryForm.instance.Show();
            this.Close();
        }
        private void Pass_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex > -1)
            {
                string selected = comboBox1.SelectedItem.ToString();
                OurConnection.updateReportStatus(selected, "Finalized");
                EntryForm.instance.Show();
                this.Close();
            }

        }
        private void Fail_Click(object sender, EventArgs e)
        {
            if(comboBox1.SelectedIndex > -1)
            {
                string selected = comboBox1.SelectedItem.ToString();
                CorrectionForm correct = new CorrectionForm(selected);
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

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
