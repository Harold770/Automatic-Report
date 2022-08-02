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
            //ReportExcel excel = new ReportExcel();
            //excel.outputDir = "C:\\Users\\Harold\\Desktop";
            //excel.CreateExcel();
            DataReport data = new DataReport();
            string StartDate = dateTimePicker1.Value.ToString("yyyy-MM-dd");
            string EndDate = dateTimePicker2.Value.ToString("yyyy-MM-dd");
            DataSet datos = data.ExtractData(StartDate, EndDate);
            dataGridView1.DataSource = datos.Tables[1];
            dataGridView1.Refresh();
        }
    }
}
