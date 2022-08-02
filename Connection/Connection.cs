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
            return conn;
        }

        public void ConnnectClose()
        {
            conn.Close();
        }

        public DataSet ExecuteCommand(string command)
        {
            var cmd = new MySqlCommand(command, ConnectExecute());
            MySqlDataReader rdr = cmd.ExecuteReader();
            DataSet dataSet = new DataSet();
            DataTable table = new DataTable();
            dataSet.Tables.Add(table);
            dataSet.EnforceConstraints = false;
            table.Load(rdr);
            rdr.Close();
            return dataSet;
        }
    }
}
