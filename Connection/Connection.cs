using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;

namespace Automatic_Report.Connection
{
    class connectDB
    {
        MySql.Data.MySqlClient.MySqlConnection conn;
        string myconnectionStr; 
        
        public void ConnectExecute()
        {
            myconnectionStr = System.IO.File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "Config.txt"));
            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection(myconnectionStr);
                conn.Open();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
     }
}
