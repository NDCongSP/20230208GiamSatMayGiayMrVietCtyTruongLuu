using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonControl;
using MySql.Data.MySqlClient;

namespace QuanLyGiay
{
    public static class GlobalVariable1
    {
        public static string ConnectionString { get; set; }

        public static MySqlConnection GetDbConnection()
        {
            return new MySqlConnection(ConnectionString);
        }

        public static CustomeEvents MyEvents { get; set; } = new CustomeEvents();

        public static DaoLangPositionModel DaoLangPosition = new DaoLangPositionModel();
    }
}
