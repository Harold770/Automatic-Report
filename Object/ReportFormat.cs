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


        public void CreateExcel(string StartDate, string EndDate, string OutputDir)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            FileInfo newFile = new FileInfo(OutputDir);
            if (newFile.Exists)
            {
                newFile.Delete();
                newFile = new FileInfo(OutputDir);
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
            int CantidadesNum = 8;
            for (int i = 0; i < Datos.Tables.Count; i++)
            {
                try
                {
                    if (string.IsNullOrEmpty(Datos.Tables[i].Rows[0]["Cantidad"].ToString()) == false)
                    {
                        worksheet.Cells["E" + CantidadesNum.ToString()].Value = Datos.Tables[i].Rows[0]["Cantidad"];
                    }
                    else
                    {
                        worksheet.Cells["E" + CantidadesNum.ToString()].Value = 0;
                    }
                    CantidadesNum++;
                }
                catch (Exception)
                {
                    worksheet.Cells["E" + CantidadesNum.ToString()].Value = 0;
                    CantidadesNum++;
                }


            }
            worksheet.Cells["E7"].Value = 0;
            worksheet.Cells["E12"].Value = 0;
            worksheet.Cells["E13"].Value = 0;
            using (var range = worksheet.Cells["E7:E13"])
            {
                range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                range.Style.Numberformat.Format = "0";
                range.AutoFitColumns();
            }


            //Precio
            using (var range = worksheet.Cells["F7:F13"])
            {
                range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                range.Style.Numberformat.Format = "$0.00";
                range.AutoFitColumns();
            }
            int PrecioNum = 8;
            for (int i = 0; i < Datos.Tables.Count; i++)
            {
                try
                {
                    if (string.IsNullOrEmpty(Datos.Tables[i].Rows[0]["Precio"].ToString()) == false)
                    {
                        worksheet.Cells["F" + PrecioNum.ToString()].Value = Datos.Tables[i].Rows[0]["Precio"];
                    }
                    else
                    {
                        worksheet.Cells["F" + PrecioNum.ToString()].Value = 0;
                    }
                    PrecioNum++;
                }
                catch (Exception)
                {
                    worksheet.Cells["F" + PrecioNum.ToString()].Value = 0;
                    PrecioNum++;
                }

            }
            worksheet.Cells["F7"].Value = 0;
            worksheet.Cells["F12"].Value = 0;
            worksheet.Cells["F13"].Value = 0;


            //Importe
            worksheet.Cells["G7"].Formula = "=E7*F7";
            worksheet.Cells["G8"].Formula = "=E8*F8";
            worksheet.Cells["G9"].Value = Datos.Tables[1].Rows[0]["importe"];
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



            //TOTAL VENTAS-EGRESOS
            worksheet.Cells["E17"].Value = "TOTAL";
            worksheet.Cells["G17"].Style.Numberformat.Format = "$0.00";
            worksheet.Cells["G17"].Formula = "=G15-G16";

            //Segunda Parte
            //Egresos
            ExcelWorksheet worksheetegresos = excelReport.Workbook.Worksheets.Add("Egresos");
            DataSet DatosEgresos = ExtractDataEgresos(StartDate, EndDate);
            int NumEgresos = 2;
            for (int i = 0; i < DatosEgresos.Tables[0].Rows.Count; i++)
            {
                try
                {
                    worksheetegresos.Cells["B" + NumEgresos.ToString()].Value = DatosEgresos.Tables[0].Rows[i]["concepto"];
                    worksheetegresos.Cells["C" + NumEgresos.ToString()].Value = DatosEgresos.Tables[0].Rows[i]["importe"];
                    worksheetegresos.Cells["C" + NumEgresos.ToString()].Style.Numberformat.Format = "$0.00";
                    if (NumEgresos>2)
                    {
                        worksheetegresos.Cells["G2"].Formula = "=SUM(C2:C" + NumEgresos.ToString() +")";

                    }
                    else
                    {
                        worksheetegresos.Cells["G2"].Value = DatosEgresos.Tables[0].Rows[0]["importe"];
                    }
                    NumEgresos++;
                }
                catch (Exception)
                {
                    worksheet.Cells["B2"].Value = "NO HAY EGRESOS REGISTRADOS";
                    
                }
            }
            worksheetegresos.Cells["F2"].Value = "Total Egresos";
            worksheetegresos.Cells["G2"].Style.Numberformat.Format = "$0.00";
            //TOTAL EGRESOS
            worksheet.Cells["E16"].Value = "TOTAL EGRESOS";
            worksheet.Cells["G16"].Style.Numberformat.Format = "$0.00";
            worksheet.Cells["G16"].Formula = "=Egresos!G2";


            //INGRESOS LA PARTE DE INGRESOS NO ESTA LISTA NO SE DONDE SE ENCUENTRAN LOS DATOS QUE
            //ME ESTAN SOLICITANDO

            excelReport.Save();
        }

        public DataSet ExtractData(string StartDate, string EndDate)
        {
            connectDB dB = new connectDB();
            string SelectBurritos = "CALL SelectBurritos('" + StartDate + "', '" + EndDate + "'); ";
            string SelectSumMensualidades = "CALL SelectSumMensualidades('" + StartDate + "', '" + EndDate + "'); ";
            string SelectVentasSPArtistico = "CALL SelectVentasSPArtistico('" + StartDate + "', '" + EndDate + "'); ";
            string SelectVentasFreeArtistico = "CALL SelectVentasFreeArtistico('" + StartDate + "', '" + EndDate + "'); ";
            string FinalCommand = SelectBurritos + SelectSumMensualidades + SelectVentasSPArtistico + SelectVentasFreeArtistico;
            MySqlCommand mySqlCommand = new MySqlCommand(FinalCommand, dB.ConnectExecute());
            MySqlDataAdapter dataAdapter = new MySqlDataAdapter(mySqlCommand);
            DataSet datos = new DataSet();
            dataAdapter.Fill(datos);
            string command = "CALL SelectCountPrecio('" + StartDate + "', '" + EndDate + "');";



            return datos;
        }

        public DataSet ExtractDataEgresos(string StartDate, string EndDate)
        {
            connectDB dB = new connectDB();
            string SelectEgresos = "CALL SelectEgresos('" + StartDate + "', '" + EndDate + "'); ";
            string FinalCommand = SelectEgresos;
            MySqlCommand mySqlCommand = new MySqlCommand(FinalCommand, dB.ConnectExecute());
            MySqlDataAdapter dataAdapter = new MySqlDataAdapter(mySqlCommand);
            DataSet datos = new DataSet();
            dataAdapter.Fill(datos);

            return datos;
        }
    }
}
