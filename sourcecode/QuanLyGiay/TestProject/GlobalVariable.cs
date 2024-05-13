using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Windows.Forms;
using CommonControl;
using MySql.Data.MySqlClient;

namespace TestProject

{
    public static class GlobalVariable
    {
        public static string ConnectionString { get; set; } = "Data Source=localhost;Port=3306;Database=quanlygiay;UID=root;Password=100100; Min Pool Size=0;Max Pool Size=1000;Pooling=true; Connect Timeout=65535;";

        public static MySqlConnection GetDbConnection()
        {
            return new MySqlConnection(ConnectionString);
        }

      

        public static DaoLangPositionModel DaoLangPosition = new DaoLangPositionModel();


        public static void InvokeIfRequired(Control control,Action action)
        {
            if (control.InvokeRequired)
            {
                control.BeginInvoke(action);
            }
            else
            {
                action();
            }
        }

        /// <summary>
        /// dùng cho app máy sóng, để quy định đang mở app cho máy sóng nào để lấy thông số hiển thị lên giao diện cho đúng.
        /// có 3 máy sóng, thứ tự lần lượt là 1,2,3.
        /// </summary>
        public static string MaySOng = "1";
        public static string TenMaySong1 { get; set; }
        public static string TenMaySong2 { get; set; }
        public static string TenMaySong3 { get; set; }
    }
}
