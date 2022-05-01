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
    public partial class ViewForm : MetroSet_UI.Forms.MetroSetForm
    {
        DbConnector2 OurConnection = new DbConnector2();
        public static ViewForm instance;
        public ViewForm(string RID)
        {
            InitializeComponent();
            instance = this;
            DataTable dt = new DataTable();
            dt = OurConnection.getReport(RID);
            dataGridView1.DataSource = dt;
            dataGridView1.DefaultCellStyle.ForeColor = Color.Black;
            dataGridView2.DefaultCellStyle.ForeColor = Color.Black;
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.Yellow;
            dataGridView2.DefaultCellStyle.SelectionForeColor = Color.Yellow;
            int w = 0;
            int count = 1;
            double[] dataHPLC = OurConnection.getHPLCValues();
            this.dataGridView2.Columns.Add("1", "HPLC Values COL 1");
            this.dataGridView2.Columns.Add("2", "HPLC Values COL 2");
            this.dataGridView2.Columns.Add("3", "HPLC Values COL 3");
            this.dataGridView2.Columns.Add("4", "HPLC Values COL 4");
            this.dataGridView2.Rows.Add(1);

            //get rl and ql
            float[] rlql = OurConnection.GetRLQL(RID);
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

                if (dataHPLC[i] > rl && dataHPLC[i] < ql)
                {
                    this.dataGridView2.Rows[w].Cells[count.ToString()].Style.BackColor = Color.Teal;
                }
                else if (dataHPLC[i] > ql)
                {
                    this.dataGridView2.Rows[w].Cells[count.ToString()].Style.BackColor = Color.HotPink;
                }
                count++;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Main_Click(object sender, EventArgs e)
        {
            if (EntryForm.instance != null)
            {
                EntryForm.instance.Show();
                this.Hide();
            }
            else
            {
                EntryForm entry = new EntryForm();
                entry.Show();
                this.Hide();
            }
        }

        private void ViewForm_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
