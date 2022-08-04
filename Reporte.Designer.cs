namespace Automatic_Report
{
    partial class Reporte
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Reporte));
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btn_OpenAddField = new System.Windows.Forms.Button();
            this.btn_printExcel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(48, 54);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(200, 20);
            this.dateTimePicker1.TabIndex = 1;
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Location = new System.Drawing.Point(301, 54);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(200, 20);
            this.dateTimePicker2.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(45, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Fecha Inicial";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(298, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Fecha Final";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(347, 125);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Campos Nuevos";
            // 
            // btn_OpenAddField
            // 
            this.btn_OpenAddField.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_OpenAddField.BackgroundImage")));
            this.btn_OpenAddField.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_OpenAddField.Location = new System.Drawing.Point(453, 117);
            this.btn_OpenAddField.Name = "btn_OpenAddField";
            this.btn_OpenAddField.Size = new System.Drawing.Size(33, 28);
            this.btn_OpenAddField.TabIndex = 6;
            this.btn_OpenAddField.UseVisualStyleBackColor = true;
            this.btn_OpenAddField.Click += new System.EventHandler(this.btn_OpenAddField_Click);
            // 
            // btn_printExcel
            // 
            this.btn_printExcel.Location = new System.Drawing.Point(74, 120);
            this.btn_printExcel.Name = "btn_printExcel";
            this.btn_printExcel.Size = new System.Drawing.Size(75, 23);
            this.btn_printExcel.TabIndex = 7;
            this.btn_printExcel.Text = "Excel";
            this.btn_printExcel.UseVisualStyleBackColor = true;
            this.btn_printExcel.Click += new System.EventHandler(this.btn_printExcel_Click);
            // 
            // Reporte
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(579, 230);
            this.Controls.Add(this.btn_printExcel);
            this.Controls.Add(this.btn_OpenAddField);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dateTimePicker2);
            this.Controls.Add(this.dateTimePicker1);
            this.Name = "Reporte";
            this.Text = "Reporte";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btn_OpenAddField;
        private System.Windows.Forms.Button btn_printExcel;
    }
}

