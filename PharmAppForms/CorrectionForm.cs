﻿using System;
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
    public partial class CorrectionForm : Form
    {
        DbConnector2 OurConnection = new DbConnector2();
        public static CorrectionForm instance;
        public string rid { get; set; }
        public CorrectionForm(string RID)
        {
            rid = RID;
            InitializeComponent();
            instance = this;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void CorrectionForm_Load(object sender, EventArgs e)
        {

        }
        private void Submit(object sender, EventArgs e)
        {
            if(checkBox1.CheckState == CheckState.Checked && checkBox2.CheckState == CheckState.Checked)
            {
                OurConnection.updateErrorStatus(rid, "Both");
                EntryForm.instance.Show();
                this.Close();
            }
            else if (checkBox1.CheckState == CheckState.Checked && checkBox2.CheckState != CheckState.Checked)
            {
                OurConnection.updateErrorStatus(rid, "QL");
                EntryForm.instance.Show();
                this.Close();
            }
            else if (checkBox1.CheckState != CheckState.Checked && checkBox2.CheckState == CheckState.Checked)
            {
                OurConnection.updateErrorStatus(rid, "RL");
                EntryForm.instance.Show();
                this.Close();

            }
            else
            {
                throw new Exception("No Error Present");
            }
        }
    }
}
