using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace Automatic_Report.Connection
{
    class connectDB
    {
        MySql.Data.MySqlClient.MySqlConnection conn;
        string myconnectionStr; 
        
        public MySqlConnection ConnectExecute()
        {
            myconnectionStr = System.IO.File.ReadAllText("Config.txt");
            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection(myconnectionStr);
                conn.Open();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            return conn;
        }

        public void ConnnectClose()
        {
            conn.Close();
        }

        public bool ExecuteInsertCommand(string command)
        {
            var cmd = new MySqlCommand(command, ConnectExecute());
            try
            {
                MySqlDataReader rdr = cmd.ExecuteReader();
                return true;
            }
            catch (Exception)
            {

                return false;
            }

            
        }
    }
}
