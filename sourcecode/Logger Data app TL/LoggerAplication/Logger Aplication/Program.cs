using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Logger_Aplication
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            #region Get các giá trị cài đặt ban đầu
            GlobalVariables.ConnectionString = Properties.Settings.Default.ConString;
            #endregion

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainPage());
        }
    }
}
