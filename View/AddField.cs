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

namespace Automatic_Report.View
{
    public partial class AddField : Form
    {
        connectDB dB = new connectDB();
        public AddField()
        {
            InitializeComponent();
        }

        private void txt_importe_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void txt_importe_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
        (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            string command = "";
            if (string.IsNullOrEmpty(txt_concepto.Text)== false && string.IsNullOrEmpty(txt_importe.Text) == false)
            {
                command = $"CALL InsertIntoEgresos('{txt_concepto.Text}', '{Convert.ToDouble(txt_importe.Text)}', '{dateTimePicker1.Value.ToString("yyyy-MM-dd")}')";
                dB.ExecuteInsertCommand(command);
                txt_concepto.Clear();
                txt_importe.Clear();
            }
            else
            {
                MessageBox.Show("Rellene todos los campos", "Campos Vacios", MessageBoxButtons.OK);
            }
            
            
        }
    }
}
