namespace PharmApp
{
    partial class EntryForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.btn_1 = new System.Windows.Forms.Button();
            this.btn_2 = new System.Windows.Forms.Button();
            this.lbl_1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "QC Scientist",
            "QA Scientist"});
            this.comboBox1.Location = new System.Drawing.Point(12, 51);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(311, 21);
            this.comboBox1.TabIndex = 0;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // btn_1
            // 
            this.btn_1.Location = new System.Drawing.Point(12, 95);
            this.btn_1.Name = "btn_1";
            this.btn_1.Size = new System.Drawing.Size(311, 23);
            this.btn_1.TabIndex = 1;
            this.btn_1.Text = "Submit";
            this.btn_1.UseVisualStyleBackColor = true;
            // 
            // btn_2
            // 
            this.btn_2.Location = new System.Drawing.Point(93, 124);
            this.btn_2.Name = "btn_2";
            this.btn_2.Size = new System.Drawing.Size(144, 23);
            this.btn_2.TabIndex = 2;
            this.btn_2.Text = "Finalized Reports";
            this.btn_2.UseVisualStyleBackColor = true;
            this.btn_2.Click += new System.EventHandler(this.button2_Click);
            // 
            // lbl_1
            // 
            this.lbl_1.AutoSize = true;
            this.lbl_1.Location = new System.Drawing.Point(125, 35);
            this.lbl_1.Name = "lbl_1";
            this.lbl_1.Size = new System.Drawing.Size(79, 13);
            this.lbl_1.TabIndex = 3;
            this.lbl_1.Text = "Report Handler";
            this.lbl_1.Click += new System.EventHandler(this.label1_Click);
            // 
            // EntryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(333, 177);
            this.Controls.Add(this.lbl_1);
            this.Controls.Add(this.btn_2);
            this.Controls.Add(this.btn_1);
            this.Controls.Add(this.comboBox1);
            this.Name = "EntryForm";
            this.Text = "PharmApp";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button btn_1;
        private System.Windows.Forms.Button btn_2;
        private System.Windows.Forms.Label lbl_1;
    }
}

