using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Management;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Threading;

namespace TestProject
{
    public partial class Form1 : Form
    {
        ConnectMySQL csdl = new ConnectMySQL();
        DataTable bang1 = new DataTable();

        public Form1()
        {
            InitializeComponent();
            Load += Form1_Load;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            bang1 = csdl.TableWhere("tbldonhang", "*", $"IsActived = 1 and Status in (0,1)");
            label4.Text = bang1.Rows.Count.ToString();
            if (bang1 != null)
            {
                grvDH.DataSource = bang1;
            }
        }


        private void _btnTest_Click(object sender, EventArgs e)
        {
            Process.Start("shutdown.exe","-s -t 00");
        }

        void Shutdown()
        {
            ManagementBaseObject mboShutdown = null;
            ManagementClass mcWin32 = new ManagementClass("Win32_OperatingSystem");
            mcWin32.Get();

            // You can't shutdown without security privileges
            mcWin32.Scope.Options.EnablePrivileges = true;
            ManagementBaseObject mboShutdownParams =
                     mcWin32.GetMethodParameters("Win32Shutdown");

            // Flag 1 means we want to shut down the system. Use "2" to reboot.
            mboShutdownParams["Flags"] = "1";
            mboShutdownParams["Reserved"] = "0";
            foreach (ManagementObject manObj in mcWin32.GetInstances())
            {
                mboShutdown = manObj.InvokeMethod("Win32Shutdown",
                                               mboShutdownParams, null);
            }
        }
    }
}
