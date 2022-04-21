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
            dt = OurConnection.getReport("2");
            dataGridView1.DataSource = dt;


            DataTable dataTable = OurConnection.getHPLCValues();
            dataGridView1.DataSource = dataTable;
            dataGridView1.Refresh();

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
