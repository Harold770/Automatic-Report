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
using OfficeOpenXml.Style;
using System.Drawing;

namespace Automatic_Report.Object
{
    public class ReportExcel
    {
        public string outputDir = @"C:\Users\Harold\Desktop";


        public void CreateExcel(string StartDate, string EndDate)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            FileInfo newFile = new FileInfo(outputDir + @"\Reportes.xlsx");
            if (newFile.Exists)
            {
                newFile.Delete();
                newFile = new FileInfo(outputDir + @"\Reportes.xlsx");
            }
            ExcelPackage excelReport = new ExcelPackage(newFile);
            excelReport.Workbook.Properties.Author = "Empresa";
            excelReport.Workbook.Properties.Title = "Reporte Mensual";
            excelReport.Workbook.Properties.Subject = "Datos de venta";
            excelReport.Workbook.Properties.Created = DateTime.Now;

            ExcelWorksheet worksheet = excelReport.Workbook.Worksheets.Add("Reporte");
            //worksheet.Cells[5, 2].Value = datos.Tables[0].Rows[0]["sum(dPrecio)"];
            worksheet.Cells["C3"].Style.Font.Bold = true;
            worksheet.Cells["C3"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            worksheet.Cells["C3"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            worksheet.Cells["C3"].Value = "REPORTE DE VENTAS";
            worksheet.Cells["C3:E3"].Merge = true;
            worksheet.Cells["G3"].Value = DateTime.Now.ToString("dd/MM/yyyy");

            worksheet.Cells[5, 2].Value = "VENTAS";
            worksheet.Cells[6, 2].Value = "PRODUCTO"; 
            worksheet.Cells[6, 5].Value = "CANTIDAD"; 
            worksheet.Cells[6, 6].Value = "PRECIO"; 
            worksheet.Cells[6, 7].Value = "IMPORTE";
            using (var range = worksheet.Cells[6, 2, 6, 7])
            {
                range.Style.Font.Bold = true;
                range.Style.Font.Italic = true;
                range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                range.Style.Fill.BackgroundColor.SetColor(Color.DarkBlue);
                range.Style.Font.Color.SetColor(Color.White);
                range.AutoFitColumns();
            }

            worksheet.Cells["B7"].Value = "ENTRADAS";
            worksheet.Cells["B8"].Value = "BURRITOS";
            worksheet.Cells["B9"].Value = "MENSUALIDADES";
            worksheet.Cells["B10"].Value = "SP ARTISTICO";
            worksheet.Cells["B11"].Value = "FREE ARTISTICO";
            worksheet.Cells["B12"].Value = "HORAS ARTISTICO";
            worksheet.Cells["B13"].Value = "PUBLICIDAD";
            using (var range = worksheet.Cells[7, 2, 13, 2])
            {
                range.Style.Font.Bold = true;
                range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                range.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                range.Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
                range.Style.Font.Color.SetColor(Color.White);
                range.AutoFitColumns();
            }

            //Metodo para extraer los datos
            DataSet Datos = ExtractData(StartDate, EndDate);

            //CANTIDADES

            worksheet.Cells["E7"].Value = "0";
            worksheet.Cells["E8"].Value = Datos.Tables[0].Rows[0]["Cantidad"];
            //worksheet.Cells["E9"].Value = Datos.Tables[1].Rows[0]["sum(dPrecio)"];
            worksheet.Cells["E10"].Value = Datos.Tables[2].Rows[0]["Cantidad"];
            worksheet.Cells["E11"].Value = Datos.Tables[3].Rows[0]["Cantidad"];
            worksheet.Cells["E12"].Value = "HORAS ARTISTICO";
            worksheet.Cells["E13"].Value = "PUBLICIDAD";
            using (var range = worksheet.Cells["E7:E13"])
            {
                range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                range.Style.Numberformat.Format = "0";
                range.AutoFitColumns();
            }
            

            //Precio
            worksheet.Cells["F7"].Value = "0";
            worksheet.Cells["F8"].Value = Datos.Tables[0].Rows[0]["Precio"];
            worksheet.Cells["F10"].Value = Datos.Tables[2].Rows[0]["Precio"];
            worksheet.Cells["F11"].Value = Datos.Tables[3].Rows[0]["Precio"];
            worksheet.Cells["F12"].Value = "HORAS ARTISTICO";
            worksheet.Cells["F13"].Value = "PUBLICIDAD";
            using (var range = worksheet.Cells["F7:F13"])
            {
                range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                range.Style.Numberformat.Format = "$0.00";
                range.AutoFitColumns();
            }

            //Importe
            worksheet.Cells["G7"].Formula = "=E7*F7";
            worksheet.Cells["G8"].Formula = "=E8*F8";
            worksheet.Cells["G10"].Formula = "=E10*F10";
            worksheet.Cells["G11"].Formula = "=E11*F11";
            worksheet.Cells["G12"].Formula = "=E12*F12";
            worksheet.Cells["G13"].Formula = "=E13*F13";
            using (var range = worksheet.Cells["G7:G13"])
            {
                range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                range.Style.Numberformat.Format = "$0.00";
                range.AutoFitColumns();
            }

            //Total de ventas
            worksheet.Cells["E15"].Value = "TOTAL VENTAS";
            worksheet.Cells["G15"].Style.Numberformat.Format = "$0.00";
            worksheet.Cells["G15"].Formula = "=SUM(G7:G13)";

            //Segunda Parte
            //Egresos
            ExcelWorksheet worksheetegresos = excelReport.Workbook.Worksheets.Add("Egresos");

            excelReport.Save();
        }

        public DataSet ExtractData(string StartDate, string EndDate)
        {
            connectDB dB = new connectDB();
            string SelectBurritos = "CALL SelectBurritos('" + StartDate + "', '" + EndDate + "'); ";
            string SelectSumMensualidades = "CALL SelectSumMensualidades('" + StartDate + "', '" + EndDate + "'); ";
            string SelectVentasSPArtistico = "CALL SelectVentasSPArtistico('" + StartDate + "', '" + EndDate + "'); ";
            string SelectVentasFreeArtistico = "CALL SelectVentasFreeArtistico('" + StartDate + "', '" + EndDate + "'); ";
            string FinalCommand = SelectBurritos + SelectSumMensualidades +  SelectVentasSPArtistico + SelectVentasFreeArtistico;
            MySqlCommand mySqlCommand = new MySqlCommand(FinalCommand, dB.ConnectExecute());
            MySqlDataAdapter dataAdapter = new MySqlDataAdapter(mySqlCommand);
            DataSet datos = new DataSet();
            dataAdapter.Fill(datos);
            string command = "CALL SelectCountPrecio('" + StartDate + "', '" + EndDate + "');";



            return datos;
        }
    }
}
