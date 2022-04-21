using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static PharmApp.DbConnector2;


namespace PharmApp
{
    public partial class CorrectionForm : Form
    {
        public static CorrectionForm instance;

        //DbConnector2 OurConnection2 = new DbConnector2();
        public CorrectionForm()
        {
            InitializeComponent();
            instance = this;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void CorrectionForm_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //OurConnection2.getHPLCValues();
        }
    }
}
