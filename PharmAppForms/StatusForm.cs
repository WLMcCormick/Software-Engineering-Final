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
    public partial class StatusForm : Form
    {
        DbConnector2 OurConnection = new DbConnector2();
        public static StatusForm instance;
        public StatusForm()
        {
            InitializeComponent();
            instance = this;
            DataTable dt = new DataTable();
            dt = OurConnection.getReport(RID);
            dataGridView1.DataSource = dt;


            double[] dataHPLC = OurConnection.getHPLCValues();
            this.dataGridView1.Columns.Add("1", "Finalized");
            this.dataGridView1.Columns.Add("2", "Need Review");
            this.dataGridView1.Columns.Add("3", "Corrections Needed");
            this.dataGridView1.Rows.Add(1);
            for (int i = 1; i < dataHPLC.Length; i = i + 2)
            {

                    count = 1;
                    w++;
                    this.dataGridView2.Rows.Add(w);
                }
                this.dataGridView2.Rows[w].Cells[count.ToString()].Value = dataHPLC[i];
                count++;



            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Back_Click(object sender, EventArgs e)
        {
            EntryForm.instance.Show();
            this.Hide();
        }

        private void FinForm_Load(object sender, EventArgs e)
        {

        }
    }
}
