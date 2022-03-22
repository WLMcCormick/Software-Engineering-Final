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
        private void Submit_Click(object sender, EventArgs e)
        {
            if (sciSel == true)
            {
                if(ValidationForm.instance != null)
                {
                    ValidationForm.instance.Show();
                    this.Hide();
                }
                else
                {
                    ValidationForm form = new ValidationForm();
                    form.Show();
                    this.Hide();
                }
            }
            else
            {
                ReportForm reports = new ReportForm();
                reports.Show();
                this.Hide();
            }
            
        }
        private void Final_Click(object sender, EventArgs e)
        {
            if (FinForm.instance != null)
            {
                FinForm.instance.Show();
                this.Hide();
            }
            else
            {
                FinForm finalized = new FinForm();
                finalized.Show();
                this.Hide();
            }
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
