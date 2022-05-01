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
    public partial class StatusForm : MetroSet_UI.Forms.MetroSetForm
    {
        DbConnector2 OurConnection = new DbConnector2();
        public static StatusForm instance;
        public StatusForm()
        {
            InitializeComponent();
            instance = this;
            dataGridView1.DefaultCellStyle.ForeColor = Color.Black;

            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.Yellow;

            DataTable dt = OurConnection.getReports();
            dataGridView1.DataSource = dt;
            dataGridView1.Refresh();
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
