using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Automatic_Report.Connection;
using System.Data;
using MySql.Data.MySqlClient;

namespace Automatic_Report.Object
{
    class ReportExcel
    {
        public string outputDir;
        

        public void CreateExcel()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            FileInfo newFileExcel = new FileInfo(outputDir + @"\Reporte.xls");
            ExcelPackage excelReport = new ExcelPackage(newFileExcel);
            excelReport.Workbook.Properties.Author = "";
            excelReport.Workbook.Properties.Title = "Reporte";
            excelReport.Workbook.Properties.Subject = "Datos de venta";
            excelReport.Workbook.Properties.Created = DateTime.Now;

            ExcelWorksheet worksheet = excelReport.Workbook.Worksheets.Add("Reporte");
            worksheet.Cells[1, 1].Value = "1";
            worksheet.Cells[1, 2].Value = "2";
            worksheet.Cells[1, 3].Value = "3";
            worksheet.Cells[1, 4].Value = "4";
            worksheet.Cells[1, 5].Value = "5";

            worksheet.Cells["A2"].Value = "A2";
            worksheet.Cells["B2"].Value = "B2";
            worksheet.Cells["C2"].Value = "C2";
            worksheet.Cells["D2"].Value = "D2";

            excelReport.Save();
        }
    }

    class DataReport
    {
        public DataSet ExtractData(string StartDate, string EndDate)
        {
            connectDB dB = new connectDB();
            string SelectMensualidades = "CALL SelectBurritosByDate('" + StartDate + "', '" + EndDate + "'); " + "CALL SelectCountPrecio('" + StartDate + "', '" + EndDate + "');";
            MySqlCommand mySqlCommand = new MySqlCommand(SelectMensualidades, dB.ConnectExecute());
            MySqlDataAdapter dataAdapter = new MySqlDataAdapter(mySqlCommand);
            DataSet datos = new DataSet();
            dataAdapter.Fill(datos);
            string command = "CALL SelectCountPrecio('" + StartDate + "', '" + EndDate + "');";



            return datos;
        }

        
    }
}
