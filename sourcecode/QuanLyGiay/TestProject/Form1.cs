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
        int _countRefesh = 0; //100*100=10,000 =10S        
        //private Task _taskLoadOrder;
        List<DonHangModel> _resultData = new List<DonHangModel>();
        DonHangModel _donHangDoiDon = new DonHangModel();//chứa thông tin đơn hàng chuẩn bị đổi đơn.
        DonHangChayModel _donHangDangChay = new DonHangChayModel();//chứa thông tin đơn hàng đang chạy

        bool _firstLoad = true;

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

        private void LoadOrder()
        {
            using (var connection = GlobalVariable.GetDbConnection())
            {
                _resultData = connection.Query<DonHangModel>("sp_tblDonHangGetOnProcess").ToList();

                if (_resultData.Count > 0)
                {
                    var c = _resultData.Where(u => u.Status == StatusDHEnum.Chay).FirstOrDefault();
                    var cc = _resultData.Where(u => u.Status == StatusDHEnum.Moi).OrderBy(x => x.STT).FirstOrDefault();

                    if (cc != null)
                    {
                        _donHangDoiDon = cc;
                    }

                    if (c != null)
                    {
                        //add thong tin don hang danng chay vao donHangDangChay
                        _donHangDangChay = null;
                        _donHangDangChay = new DonHangChayModel()
                        {
                            IdDonHang = c.Id,
                            STT = c.STT,
                            Ma = c.Ma,
                            Song = c.Song,
                            Kho = c.Kho,
                            DaiCat = c.DaiCat,
                            SLCatTam = c.SLCatTam,
                            Pallet = c.Pallet,
                            Xa = c.Xa,
                            Rong = c.Rong,
                            Canh = c.Canh,
                            Cao = c.Cao,
                            Lang = c.Lang,
                            GiayMen = c.GiayMen,
                            GiaySongE = c.GiaySongE,
                            GiayMatE = c.GiayMatE,
                            GiaySongB = c.GiaySongB,
                            GiayMatB = c.GiayMatB,
                            GiaySongC = c.GiaySongC,
                            GiayMatC = c.GiayMatC,
                            Line = c.Line,
                            GhiChu = c.GhiChu,
                            KhachHang = c.KhachHang,
                            DonHang = c.DonHang,
                            PO = c.PO
                        };
                    }

                    GlobalVariable.InvokeIfRequired(this, () =>
                    {
                        grvDH.DataSource = _resultData;
                        //grvDH.Columns["CreatedDate"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm:ss";
                    });

                    if (!_firstLoad)
                    {
                        //grvDH.Columns["CustomerID"].Visible = false;
                        grvDH.Columns["Status"].DisplayIndex = 0;
                        grvDH.Columns["STT"].DisplayIndex = 1;
                        grvDH.Columns["Ma"].DisplayIndex = 2;
                        grvDH.Columns["Song"].DisplayIndex = 3;
                        grvDH.Columns["GiayMen"].DisplayIndex = 4;
                        grvDH.Columns["Xa"].DisplayIndex = 5;
                        grvDH.Columns["Rong"].DisplayIndex = 6;
                        grvDH.Columns["Cao"].DisplayIndex = 7;
                        grvDH.Columns["Lang"].DisplayIndex = 8;
                        grvDH.Columns["Kho"].DisplayIndex = 9;
                        grvDH.Columns["DaiCat"].DisplayIndex = 10;
                        grvDH.Columns["SLCatTam"].DisplayIndex = 11;
                        grvDH.Columns["SoMetCaiDat"].DisplayIndex = 12;
                        grvDH.Columns["TongSoThung"].DisplayIndex = 13;
                        grvDH.Columns["DoSauLan"].DisplayIndex = 14;
                        grvDH.Columns["SoLop"].DisplayIndex = 15;
                        grvDH.Columns["CongThem"].DisplayIndex = 16;

                        _firstLoad = true;
                    }
                }
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
