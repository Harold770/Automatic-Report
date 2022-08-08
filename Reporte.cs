using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Automatic_Report.Connection;
using Automatic_Report.Object;
using Automatic_Report.View;

namespace Automatic_Report
{
    public partial class Reporte : Form
    {
        public Reporte()
        {
            InitializeComponent();
        }


        private void btn_OpenAddField_Click(object sender, EventArgs e)
        {
            AddField window = new AddField();
            window.Show();
        }

        private void btn_printExcel_Click(object sender, EventArgs e)
        {
            SaveFileDialog Save1 = new SaveFileDialog();
            Save1.InitialDirectory = @"C:\";
            Save1.Title = "Browse Text Files";
            Save1.FileName = "Reporte.xlsx";
            string Dir;
            if (Save1.ShowDialog() == DialogResult.OK)
            {
                Dir = Save1.FileName;
                ReportExcel data = new ReportExcel();
                string StartDate = dateTimePicker1.Value.ToString("yyyy-MM-dd");
                string EndDate = dateTimePicker2.Value.ToString("yyyy-MM-dd");
                try
                {
                    data.CreateExcel(StartDate, EndDate, Dir);
                    MessageBox.Show("Archivo Excel Generado", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception)
                {
                    MessageBox.Show("El archivo Excel se encuentra abierto o la direccion donde se desea guardar no es accesible", "Error Excel", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }            

        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }
    }
}
