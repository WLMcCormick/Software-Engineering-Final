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

            int w = 1;
            string[] dataHPLC = OurConnection.getFinal();
            this.dataGridView1.Columns.Add("1", "Finalized");
            this.dataGridView1.Columns.Add("2", "Need Review");
            this.dataGridView1.Columns.Add("3", "Corrections Needed");
            this.dataGridView1.Rows.Add(1);
            for (int i = 0; i < dataHPLC.Length; i++)
            {
                w++;
                this.dataGridView1.Rows.Add(w);
                this.dataGridView1.Rows[w].Cells["1"].Value = dataHPLC[i];
            }
            w = 0;
            dataHPLC = OurConnection.getReview();
            for (int i = 0; i < dataHPLC.Length; i++)
            {
                w++;
                this.dataGridView1.Rows.Add(w);
                this.dataGridView1.Rows[w].Cells["2"].Value = dataHPLC[i];
            }
            w = 0;
            dataHPLC = OurConnection.getCorrections();
            for (int i = 0; i < dataHPLC.Length; i++)
            {
                w++;
                this.dataGridView1.Rows.Add(w);
                this.dataGridView1.Rows[w].Cells["3"].Value = dataHPLC[i];
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Back_Click(object sender, EventArgs e)
        {
            EntryForm.instance.Show();
            this.Close();
        }

        private void FinForm_Load(object sender, EventArgs e)
        {

        }
    }
}
