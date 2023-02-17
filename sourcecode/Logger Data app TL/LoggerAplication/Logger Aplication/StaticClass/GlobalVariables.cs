using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Logger_Aplication
{
    public static class GlobalVariables
    {
        public static string ConnectionString { get; set; }

        public static IDbConnection GetDbConnectionSql()
        {
            return new SqlConnection(ConnectionString);
        }

        public static MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }
    }
}
