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
    public partial class EntryForm : Form
    {
        public static EntryForm instance;
        bool sciSel = false;

        public EntryForm()
        {
            InitializeComponent();
            instance = this;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (sciSel == true)
            {
                ValidationForm form = new ValidationForm();
                form.Show();
            }
            else
            {
                ReportForm form = new ReportForm();
                form.Show();
            }
            
        }
        private void button2_Click(object sender, EventArgs e)
        {
            FinForm form = new FinForm();
            form.Show();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBox1.Text == "QC Scientist")
            {
                sciSel = true;
            }
            else
            {
                sciSel = false;
            }
        }

        private void EntryForm_Load(object sender, EventArgs e)
        {

        }
    }
}
