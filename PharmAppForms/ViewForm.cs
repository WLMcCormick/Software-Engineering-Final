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
    public partial class ViewForm : Form
    {
        public static ViewForm instance;
        public ViewForm()
        {
            InitializeComponent();
            instance = this;
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
    }
}
