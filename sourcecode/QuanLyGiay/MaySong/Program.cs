using CommonControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MaySong
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            #region đọc các giá trị khởi tạo ban đầu
            GlobalVariable.ConnectionString = Properties.Settings.Default.ConnectionString;
            GlobalVariable.MaySOng = Properties.Settings.Default.Song;
            GlobalVariable.TenMaySong1 = Properties.Settings.Default.TenMaySong1;
            GlobalVariable.TenMaySong2 = Properties.Settings.Default.TenMaySong2;
            GlobalVariable.TenMaySong3 = Properties.Settings.Default.TenMaySong3;
            #endregion

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmMainMaySong());
        }
    }
}
