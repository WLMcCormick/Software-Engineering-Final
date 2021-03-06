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
    public partial class ReportForm : Form
    {
        public static ReportForm instance;
        public ReportForm()
        {
            InitializeComponent();
            instance = this;
        }
        private void Main_Click(object sender, EventArgs e)
        {
            EntryForm.instance.Show();
            this.Hide();
        }
        private void Generate_Click(object sender, EventArgs e)
        {
            ViewForm viewer = new ViewForm();
            viewer.Show();
            this.Hide();
        }

        private void ReportForm_Load(object sender, EventArgs e)
        {

        }
    }
}
