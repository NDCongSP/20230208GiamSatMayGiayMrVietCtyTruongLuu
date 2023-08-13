using CommonControl;
using Dapper;
using EasyScada.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace QuanLyGiay
{
    public partial class frmMain : Form
    {
        bool _isLoaded = false;
        int _countRefesh = 0; //100*100=10,000 =10S
        private Timer _t = new Timer();
        private Task _taskLoadOrder;

        DonHangModel _donHangDoiDon = new DonHangModel();//chứa thông tin đơn hàng chuẩn bị đổi đơn.
        DonHangChayModel _donHangDangChay = new DonHangChayModel();//chứa thông tin đơn hàng đang chạy
        DonHangChayModel _donHangHoanThanh = new DonHangChayModel();//chứa thông tin đơn hàng đã hoàn thành, sau khi đổi đơn thì sẽ move thông tin của _donHangDangChay sang đây, để phục vụ cho các tác vụ tiếp theo.

        public frmMain()
        {
            InitializeComponent();

            Load += Form1_Load;

        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            _t.Interval = 100;
            _t.Enabled = true;
            _t.Tick += (s, o) =>
            {
                Timer t = (Timer)s;
                t.Enabled = false;

                this.Invoke((MethodInvoker)delegate { labDateTime.Text = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"); });

                _countRefesh += 1;

                //đếm đúng 10s thì get Order 1 lần
                if (_countRefesh >= 100)
                {
                    GlobalVariable.MyEvents.Refresh = true;

                    _countRefesh = 0;
                }

                t.Enabled = true;
            };

            #region Sự kiện nhấn phím tắt
            this.KeyPreview = true;
            this.KeyDown += (s, o) =>
            {
                if (o.KeyCode == Keys.F1)
                {
                    BtnOrder_Click(null, null);
                }
                else if (o.KeyCode == Keys.F2)
                {

                }
            };
            #endregion

            btnOrder.Click += BtnOrder_Click;

            easyDriverConnector1.Started += EasyDriverConnector1_Started;

            //LoadOrder();
            GlobalVariable.MyEvents.RefreshChangedEvent += (s, o) =>
            {
                if (o.NewStatus)
                {
                    LoadOrder();

                    GlobalVariable.MyEvents.Refresh = false;
                }
            };

            GlobalVariable.MyEvents.Refresh = true;
            //_taskLoadOrder = new Task(LoadOrder);
            //_taskLoadOrder.Start();
        }

        private void BtnOrder_Click(object sender, EventArgs e)
        {
            frmAddDonHang nf = new frmAddDonHang();
            nf.ShowDialog();
        }

        private void LoadOrder()
        {
            //while (true)
            {
                Console.WriteLine("vong lap task");

                using (var connection = GlobalVariable.GetDbConnection())
                {
                    var resultData = connection.Query<DonHangModel>("sp_tblDonHangGetOnProcess").ToList();

                    if (resultData.Count > 0)
                    {
                        this.Invoke((MethodInvoker)delegate
                        {
                            grvDH.DataSource = resultData;
                            grvDH.Columns["CreatedDate"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm:ss";
                        });
                    }
                }
                //Thread.Sleep(2000);
            }
        }

        #region Events
        private void EasyDriverConnector1_Started(object sender, EventArgs e)
        {
            #region Su kiên tag value changed
            #region may Cat tam
            easyDriverConnector1.GetTag("Local Station/ChannelServer/DeviceCutter/LenhChuyenDon").ValueChanged += LenhChuyenDonCutter_ValueChanged;
            easyDriverConnector1.GetTag("Local Station/ChannelServer/DeviceCutter/DoiDon").ValueChanged += DoiDonCutter_ValueChanged;
            easyDriverConnector1.GetTag("Local Station/ChannelServer/DeviceCutter/Run").ValueChanged += RunCutter_ValueChanged;
            easyDriverConnector1.GetTag("Local Station/ChannelServer/DeviceCutter/SoLuongDat").ValueChanged += SoLuongDat_ValueChanged;
            easyDriverConnector1.GetTag("Local Station/ChannelServer/DeviceCutter/SoLuongLoi").ValueChanged += SoLuongLoi_ValueChanged;
            easyDriverConnector1.GetTag("Local Station/ChannelServer/DeviceCutter/TocDo").ValueChanged += TocDo_ValueChanged;
            easyDriverConnector1.GetTag("Local Station/ChannelServer/DeviceCutter/STT1").ValueChanged += STT1_ValueChanged;

            LenhChuyenDonCutter_ValueChanged(easyDriverConnector1.GetTag("Local Station/ChannelServer/DeviceCutter/LenhChuyenDon"),
                              new TagValueChangedEventArgs(easyDriverConnector1.GetTag("Local Station/ChannelServer/DeviceCutter/LenhChuyenDon")
                              , "", easyDriverConnector1.GetTag("Local Station/ChannelServer/DeviceCutter/LenhChuyenDon").Value));

            RunCutter_ValueChanged(easyDriverConnector1.GetTag("Local Station/ChannelServer/DeviceCutter/Run"),
                              new TagValueChangedEventArgs(easyDriverConnector1.GetTag("Local Station/ChannelServer/DeviceCutter/Run")
                              , "", easyDriverConnector1.GetTag("Local Station/ChannelServer/DeviceCutter/Run").Value));

            //DoiDonCutter_ValueChanged(easyDriverConnector1.GetTag("Local Station/ChannelServer/DeviceCutter/DoiDon"),
            //                 new TagValueChangedEventArgs(easyDriverConnector1.GetTag("Local Station/ChannelServer/DeviceCutter/DoiDon")
            //                 , "", easyDriverConnector1.GetTag("Local Station/ChannelServer/DeviceCutter/DoiDon").Value));

            SoLuongDat_ValueChanged(easyDriverConnector1.GetTag("Local Station/ChannelServer/DeviceCutter/SoLuongDat"),
                              new TagValueChangedEventArgs(easyDriverConnector1.GetTag("Local Station/ChannelServer/DeviceCutter/SoLuongDat")
                              , "", easyDriverConnector1.GetTag("Local Station/ChannelServer/DeviceCutter/SoLuongDat").Value));

            SoLuongLoi_ValueChanged(easyDriverConnector1.GetTag("Local Station/ChannelServer/DeviceCutter/SoLuongLoi"),
                              new TagValueChangedEventArgs(easyDriverConnector1.GetTag("Local Station/ChannelServer/DeviceCutter/SoLuongLoi")
                              , "", easyDriverConnector1.GetTag("Local Station/ChannelServer/DeviceCutter/SoLuongLoi").Value));

            TocDo_ValueChanged(easyDriverConnector1.GetTag("Local Station/ChannelServer/DeviceCutter/TocDo"),
                              new TagValueChangedEventArgs(easyDriverConnector1.GetTag("Local Station/ChannelServer/DeviceCutter/TocDo")
                              , "", easyDriverConnector1.GetTag("Local Station/ChannelServer/DeviceCutter/TocDo").Value));

            STT1_ValueChanged(easyDriverConnector1.GetTag("Local Station/ChannelServer/DeviceCutter/STT1"),
                              new TagValueChangedEventArgs(easyDriverConnector1.GetTag("Local Station/ChannelServer/DeviceCutter/STT1")
                              , "", easyDriverConnector1.GetTag("Local Station/ChannelServer/DeviceCutter/STT1").Value));
            #endregion

            #region may xa
            easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Run").ValueChanged += RunMayXa1_ValueChanged;
            //easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/LenhChuyenDon").ValueChanged += LenhChuyenDonMayXa1_ValueChanged;

            easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Dao1_PV").ValueChanged += Dao1_PVMayXa1_ValueChanged;
            easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Dao2_PV").ValueChanged += Dao2_PVMayXa1_ValueChanged;
            easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Dao3_PV").ValueChanged += Dao3_PVMayXa1_ValueChanged;
            easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Dao4_PV").ValueChanged += Dao4_PVMayXa1_ValueChanged;
            easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Dao5_PV").ValueChanged += Dao5_PVMayXa1_ValueChanged;

            easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Lang1_PV").ValueChanged += Lang1_PVMayXa1_ValueChanged;
            easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Lang2_PV").ValueChanged += Lang2_PVMayXa1_ValueChanged;
            easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Lang3_PV").ValueChanged += Lang3_PVMayXa1_ValueChanged;
            easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Lang4_PV").ValueChanged += Lang4_PVMayXa1_ValueChanged;
            easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Lang5_PV").ValueChanged += Lang5_PVMayXa1_ValueChanged;
            easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Lang6_PV").ValueChanged += Lang6_PVMayXa1_ValueChanged;
            easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Lang7_PV").ValueChanged += Lang7_PVMayXa1_ValueChanged;
            easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Lang8_PV").ValueChanged += Lang8_PVMayXa1_ValueChanged;

            easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Hut_PV").ValueChanged += Hut_PVMayXa1_ValueChanged;

            easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Dao_Dao").ValueChanged += Dao_Dao_MayXa1_ValueChanged;
            easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Dao1_Max").ValueChanged += Dao1_Max_MayXa1_ValueChanged;
            easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Dao1_Min").ValueChanged += Dao1_Min_MayXa1_ValueChanged;
            easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Dao2_Max").ValueChanged += Dao2_Max_MayXa1_ValueChanged;
            easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Dao2_Min").ValueChanged += Dao2_Min_MayXa1_ValueChanged;
            easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Dao3_Max").ValueChanged += Dao3_Max_MayXa1_ValueChanged;
            easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Dao4_Max").ValueChanged += Dao4_Max_MayXa1_ValueChanged;
            easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Dao4_Min").ValueChanged += Dao4_Min_MayXa1_ValueChanged;
            easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Dao5_Max").ValueChanged += Dao5_Max_MayXa1_ValueChanged;
            easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Dao5_Min").ValueChanged += Dao5_Min_MayXa1_ValueChanged;

            easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Lang_Lang").ValueChanged += Lang_Lang_MayXa1_ValueChanged;
            easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Lang1_Max").ValueChanged += Lang1_MaxMayXa1_ValueChanged;
            easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Lang2_Max").ValueChanged += Lang2_MaxMayXa1_ValueChanged;
            easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Lang3_Max").ValueChanged += Lang3_MaxMayXa1_ValueChanged;
            easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Lang4_Max").ValueChanged += Lang4_MaxMayXa1_ValueChanged;

            easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/KhoMay").ValueChanged += KhoMayMayXa1_ValueChanged;

            RunMayXa1_ValueChanged(easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Run"),
                              new TagValueChangedEventArgs(easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Run")
                              , "", easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Run").Value));

            Dao1_PVMayXa1_ValueChanged(easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Dao1_PV"), new TagValueChangedEventArgs(easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Dao1_PV"), "", easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Dao1_PV").Value));
            Dao2_PVMayXa1_ValueChanged(easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Dao2_PV"), new TagValueChangedEventArgs(easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Dao2_PV"), "", easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Dao2_PV").Value));
            Dao3_PVMayXa1_ValueChanged(easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Dao3_PV"), new TagValueChangedEventArgs(easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Dao3_PV"), "", easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Dao3_PV").Value));
            Dao4_PVMayXa1_ValueChanged(easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Dao4_PV"), new TagValueChangedEventArgs(easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Dao4_PV"), "", easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Dao4_PV").Value));
            Dao5_PVMayXa1_ValueChanged(easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Dao5_PV"), new TagValueChangedEventArgs(easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Dao5_PV"), "", easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Dao5_PV").Value));

            Lang1_PVMayXa1_ValueChanged(easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Lang1_PV"), new TagValueChangedEventArgs(easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Lang1_PV"), "", easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Lang1_PV").Value));
            Lang2_PVMayXa1_ValueChanged(easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Lang2_PV"), new TagValueChangedEventArgs(easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Lang2_PV"), "", easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Lang2_PV").Value));
            Lang3_PVMayXa1_ValueChanged(easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Lang3_PV"), new TagValueChangedEventArgs(easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Lang3_PV"), "", easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Lang3_PV").Value));
            Lang4_PVMayXa1_ValueChanged(easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Lang4_PV"), new TagValueChangedEventArgs(easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Lang4_PV"), "", easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Lang4_PV").Value));
            Lang5_PVMayXa1_ValueChanged(easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Lang5_PV"), new TagValueChangedEventArgs(easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Lang5_PV"), "", easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Lang5_PV").Value));
            Lang6_PVMayXa1_ValueChanged(easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Lang6_PV"), new TagValueChangedEventArgs(easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Lang6_PV"), "", easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Lang6_PV").Value));
            Lang7_PVMayXa1_ValueChanged(easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Lang7_PV"), new TagValueChangedEventArgs(easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Lang7_PV"), "", easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Lang7_PV").Value));
            Lang8_PVMayXa1_ValueChanged(easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Lang8_PV"), new TagValueChangedEventArgs(easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Lang8_PV"), "", easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Lang8_PV").Value));

            Hut_PVMayXa1_ValueChanged(easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Hut_PV"), new TagValueChangedEventArgs(easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Hut_PV"), "", easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Hut_PV").Value));

            Dao_Dao_MayXa1_ValueChanged(easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Dao_Dao"), new TagValueChangedEventArgs(easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Dao_Dao"), "", easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Dao_Dao").Value));
            Dao1_Max_MayXa1_ValueChanged(easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Dao1_Max"), new TagValueChangedEventArgs(easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Dao1_Max"), "", easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Dao1_Max").Value));
            Dao1_Min_MayXa1_ValueChanged(easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Dao1_Min"), new TagValueChangedEventArgs(easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Dao1_Min"), "", easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Dao1_Min").Value));
            Dao2_Max_MayXa1_ValueChanged(easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Dao2_Max"), new TagValueChangedEventArgs(easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Dao2_Max"), "", easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Dao2_Max").Value));
            Dao2_Min_MayXa1_ValueChanged(easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Dao2_Min"), new TagValueChangedEventArgs(easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Dao2_Min"), "", easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Dao2_Min").Value));
            Dao3_Max_MayXa1_ValueChanged(easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Dao3_Max"), new TagValueChangedEventArgs(easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Dao3_Max"), "", easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Dao3_Max").Value));
            Dao4_Max_MayXa1_ValueChanged(easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Dao4_Max"), new TagValueChangedEventArgs(easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Dao4_Max"), "", easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Dao4_Max").Value));
            Dao4_Min_MayXa1_ValueChanged(easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Dao4_Min"), new TagValueChangedEventArgs(easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Dao4_Min"), "", easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Dao4_Min").Value));
            Dao5_Max_MayXa1_ValueChanged(easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Dao5_Max"), new TagValueChangedEventArgs(easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Dao5_Max"), "", easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Dao5_Max").Value));
            Dao5_Min_MayXa1_ValueChanged(easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Dao5_Min"), new TagValueChangedEventArgs(easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Dao5_Min"), "", easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Dao5_Min").Value));


            Lang_Lang_MayXa1_ValueChanged(easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Lang_Lang"), new TagValueChangedEventArgs(easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Lang_Lang"), "", easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Lang_Lang").Value));
            Lang1_MaxMayXa1_ValueChanged(easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Lang1_Max"), new TagValueChangedEventArgs(easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Lang1_Max"), "", easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Lang1_Max").Value));
            Lang2_MaxMayXa1_ValueChanged(easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Lang2_Max"), new TagValueChangedEventArgs(easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Lang2_Max"), "", easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Lang2_Max").Value));
            Lang3_MaxMayXa1_ValueChanged(easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Lang3_Max"), new TagValueChangedEventArgs(easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Lang3_Max"), "", easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Lang3_Max").Value));
            Lang4_MaxMayXa1_ValueChanged(easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Lang4_Max"), new TagValueChangedEventArgs(easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Lang4_Max"), "", easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Lang4_Max").Value));
            KhoMayMayXa1_ValueChanged(easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/KhoMay"), new TagValueChangedEventArgs(easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/KhoMay"), "", easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/KhoMay").Value));
            #endregion

            #endregion

            if (easyDriverConnector1.ConnectionStatus == ConnectionStatus.Connected)
            {
                _labDriverStatus.BackColor = Color.Green;
            }
            else
            {
                _labDriverStatus.BackColor = Color.Red;
            }
        }

        #region may xa 1
        private void Lang8_PVMayXa1_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            GlobalVariable.DaoLangPosition.Lang8Pv = int.TryParse(e.NewValue, out int value) ? value : 0;
        }

        private void Lang7_PVMayXa1_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            GlobalVariable.DaoLangPosition.Lang7Pv = int.TryParse(e.NewValue, out int value) ? value : 0;
        }

        private void Lang6_PVMayXa1_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            GlobalVariable.DaoLangPosition.Lang6Pv = int.TryParse(e.NewValue, out int value) ? value : 0;
        }

        private void Lang5_PVMayXa1_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            GlobalVariable.DaoLangPosition.Lang5Pv = int.TryParse(e.NewValue, out int value) ? value : 0;
        }

        private void Lang4_PVMayXa1_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            GlobalVariable.DaoLangPosition.Lang4Pv = int.TryParse(e.NewValue, out int value) ? value : 0;
        }

        private void Lang3_PVMayXa1_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            GlobalVariable.DaoLangPosition.Lang3Pv = int.TryParse(e.NewValue, out int value) ? value : 0;
        }

        private void Lang2_PVMayXa1_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            GlobalVariable.DaoLangPosition.Lang2Pv = int.TryParse(e.NewValue, out int value) ? value : 0;
        }

        private void Lang1_PVMayXa1_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            GlobalVariable.DaoLangPosition.Lang1Pv = int.TryParse(e.NewValue, out int value) ? value : 0;
        }

        private void Dao5_PVMayXa1_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            GlobalVariable.DaoLangPosition.Dao5PV = int.TryParse(e.NewValue, out int value) ? value : 0;
        }

        private void Dao4_PVMayXa1_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            GlobalVariable.DaoLangPosition.Dao4PV = int.TryParse(e.NewValue, out int value) ? value : 0;
        }

        private void Dao3_PVMayXa1_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            GlobalVariable.DaoLangPosition.Dao3PV = int.TryParse(e.NewValue, out int value) ? value : 0;
        }

        private void Dao2_PVMayXa1_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            GlobalVariable.DaoLangPosition.Dao2PV = int.TryParse(e.NewValue, out int value) ? value : 0;
        }

        private void Dao1_PVMayXa1_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            GlobalVariable.DaoLangPosition.Dao1PV = int.TryParse(e.NewValue, out int value) ? value : 0;
        }

        private void Dao1_Max_MayXa1_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            GlobalVariable.DaoLangPosition.Dao1Max = int.TryParse(e.NewValue, out int value) ? value : 0;
        }
        private void Dao2_Max_MayXa1_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            GlobalVariable.DaoLangPosition.Dao2Max = int.TryParse(e.NewValue, out int value) ? value : 0;
        }

        private void Lang_Lang_MayXa1_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            GlobalVariable.DaoLangPosition.Lang_Lang = int.TryParse(e.NewValue, out int value) ? value : 0;
        }

        private void Dao_Dao_MayXa1_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            GlobalVariable.DaoLangPosition.Dao_Dao = int.TryParse(e.NewValue, out int value) ? value : 0;
        }

        private void Lang4_MaxMayXa1_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            GlobalVariable.DaoLangPosition.Lang4Max = int.TryParse(e.NewValue, out int value) ? value : 0;
        }

        private void Lang3_MaxMayXa1_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            GlobalVariable.DaoLangPosition.Lang3Max = int.TryParse(e.NewValue, out int value) ? value : 0;
        }

        private void Lang2_MaxMayXa1_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            GlobalVariable.DaoLangPosition.Lang2Max = int.TryParse(e.NewValue, out int value) ? value : 0;
        }

        private void Lang1_MaxMayXa1_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            GlobalVariable.DaoLangPosition.Lang1Max = int.TryParse(e.NewValue, out int value) ? value : 0;
        }

        private void KhoMayMayXa1_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            GlobalVariable.DaoLangPosition.KhoMay = int.TryParse(e.NewValue, out int value) ? value : 0;
        }

        private void Dao5_Min_MayXa1_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            GlobalVariable.DaoLangPosition.Dao5Min = int.TryParse(e.NewValue, out int value) ? value : 0;
        }

        private void Dao5_Max_MayXa1_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            GlobalVariable.DaoLangPosition.Dao5Max = int.TryParse(e.NewValue, out int value) ? value : 0;
        }

        private void Dao4_Min_MayXa1_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            GlobalVariable.DaoLangPosition.Dao4Min = int.TryParse(e.NewValue, out int value) ? value : 0;
        }

        private void Dao4_Max_MayXa1_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            GlobalVariable.DaoLangPosition.Dao4Max = int.TryParse(e.NewValue, out int value) ? value : 0;
        }

        private void Dao3_Max_MayXa1_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            GlobalVariable.DaoLangPosition.Dao3Max = int.TryParse(e.NewValue, out int value) ? value : 0;
        }

        private void Dao2_Min_MayXa1_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            GlobalVariable.DaoLangPosition.Dao2Min = int.TryParse(e.NewValue, out int value) ? value : 0;
        }

        private void Dao1_Min_MayXa1_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            GlobalVariable.DaoLangPosition.Dao1Min = int.TryParse(e.NewValue, out int value) ? value : 0;
        }

        private void Hut_PVMayXa1_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            GlobalVariable.DaoLangPosition.HutPv = int.TryParse(e.NewValue, out int value) ? value : 0;
        }

        /// <summary>
        /// khi tag này bật lên 1 thì PC biết là PLC đã chạy xong đơn cũ và bắt đầu đổi đơn.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LenhChuyenDonMayXa1_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                Debug.WriteLine("Chuyen don");

                //move _donHangDangChay sang _donHangHoanThanh
                if (_donHangDangChay.STT != 0)
                {
                    var donhang = new DonHangChayModel()
                    {
                        IdDonHang = _donHangDangChay.IdDonHang,
                        STT = _donHangDangChay.STT,
                        Ma = _donHangDangChay.Ma,
                        Song = _donHangDangChay.Song,
                        Kho = _donHangDangChay.Kho,
                        DaiCat = _donHangDangChay.DaiCat,
                        SLCatTam = _donHangDangChay.SLCatTam,
                        Pallet = _donHangDangChay.Pallet,
                        Xa = _donHangDangChay.Xa,
                        Rong = _donHangDangChay.Rong,
                        Canh = _donHangDangChay.Canh,
                        Cao = _donHangDangChay.Cao,
                        Lang = _donHangDangChay.Lang,
                        GiayMen = _donHangDangChay.GiayMen,
                        GiaySongE = _donHangDangChay.GiaySongE,
                        GiayMatE = _donHangDangChay.GiayMatE,
                        GiaySongB = _donHangDangChay.GiaySongB,
                        GiayMatB = _donHangDangChay.GiayMatB,
                        GiaySongC = _donHangDangChay.GiaySongC,
                        GiayMatC = _donHangDangChay.GiayMatC,
                        KhachHang = _donHangDangChay.KhachHang,
                        DonHang = _donHangDangChay.DonHang,
                        PO = _donHangDangChay.PO,
                        Line = _donHangDangChay.Line,
                        GhiChu = _donHangDangChay.GhiChu
                    };

                    _donHangHoanThanh = null;
                    _donHangHoanThanh = new DonHangChayModel();
                    _donHangHoanThanh = donhang;
                }

                using (var connection = GlobalVariable.GetDbConnection())
                {
                    var resultData = connection.Query<DonHangModel>("sp_tblDonHangGetOnProcess").ToList();

                    if (resultData.Count > 0)
                    {
                        var c = resultData.Where(u => u.Status == 0).ToList();
                        _donHangDoiDon = c.FirstOrDefault(x => x.STT == c.Min(u => u.STT));

                        //add thong tin don hang moi vao donHangDangChay
                        _donHangDangChay = null;
                        _donHangDangChay = new DonHangChayModel()
                        {
                            IdDonHang = _donHangDoiDon.Id,
                            STT = _donHangDoiDon.STT,
                            Ma = _donHangDoiDon.Ma,
                            Song = _donHangDoiDon.Song,
                            Kho = _donHangDoiDon.Kho,
                            DaiCat = _donHangDoiDon.DaiCat,
                            SLCatTam = _donHangDoiDon.SLCatTam,
                            Pallet = _donHangDoiDon.Pallet,
                            Xa = _donHangDoiDon.Xa,
                            Rong = _donHangDoiDon.Rong,
                            Canh = _donHangDoiDon.Canh,
                            Cao = _donHangDoiDon.Cao,
                            Lang = _donHangDoiDon.Lang,
                            GiayMen = _donHangDoiDon.GiayMen,
                            GiaySongE = _donHangDoiDon.GiaySongE,
                            GiayMatE = _donHangDoiDon.GiayMatE,
                            GiaySongB = _donHangDoiDon.GiaySongB,
                            GiayMatB = _donHangDoiDon.GiayMatB,
                            GiaySongC = _donHangDoiDon.GiaySongC,
                            GiayMatC = _donHangDoiDon.GiayMatC,
                            Line = _donHangDoiDon.Line,
                            GhiChu = _donHangDoiDon.GhiChu
                        };

                        TinhToan.TinhToanGiaTri(_donHangDangChay);

                        DoiDonMayXa(_donHangDoiDon);
                    }
                }

                easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/LenhChuyenDon").WriteAsync("0", WritePiority.High);
            }
        }

        private void RunMayXa1_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { _panelMayXaStatus.BackColor = Color.Green; });
            }
            else
            {
                this.Invoke((MethodInvoker)delegate { _panelMayXaStatus.BackColor = Color.Gray; });
            }
        }
        #endregion

        #region may cat
        private void RunCutter_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { _panelCutterStatus.BackColor = Color.Green; });
            }
            else
            {
                this.Invoke((MethodInvoker)delegate { _panelCutterStatus.BackColor = Color.Gray; });
            }
        }

        private void TocDo_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            //_donHangDangChay.TocDoTB = double.TryParse(e.NewValue, out double value) ? value : 0;
        }

        private void SoLuongLoi_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            _donHangDangChay.SLLoi = int.TryParse(e.NewValue, out int value) ? value : 0;
        }

        private void SoLuongDat_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            _donHangDangChay.SLDat = int.TryParse(e.NewValue, out int value) ? value : 0;
        }

        private void STT1_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            _donHangDangChay.IdDonHang = int.TryParse(e.NewValue, out int value) ? value : 0;

            if (!_isLoaded)
            {
                _isLoaded = true;

                using (var connection = GlobalVariable.GetDbConnection())
                {
                    var para = new DynamicParameters();
                    para.Add("_stt", e.NewValue);

                    var donHangDangChay = connection.Query<DonHangModel>("sp_tblDonHangGetOnProcessBySTT", param: para, commandType: CommandType.StoredProcedure).FirstOrDefault();

                    if (donHangDangChay!=null)
                    {
                        _donHangDangChay = new DonHangChayModel()
                        {
                            IdDonHang = donHangDangChay.Id,
                            STT = donHangDangChay.STT,
                            Ma = donHangDangChay.Ma,
                            Song = donHangDangChay.Song,
                            Kho = donHangDangChay.Kho,
                            DaiCat = donHangDangChay.DaiCat,
                            SLCatTam = donHangDangChay.SLCatTam,
                            Pallet = donHangDangChay.Pallet,
                            Xa = donHangDangChay.Xa,
                            Rong = donHangDangChay.Rong,
                            Canh = donHangDangChay.Canh,
                            Cao = donHangDangChay.Cao,
                            Lang = donHangDangChay.Lang,
                            GiayMen = donHangDangChay.GiayMen,
                            GiaySongE = donHangDangChay.GiaySongE,
                            GiayMatE = donHangDangChay.GiayMatE,
                            GiaySongB = donHangDangChay.GiaySongB,
                            GiayMatB = donHangDangChay.GiayMatB,
                            GiaySongC = donHangDangChay.GiaySongC,
                            GiayMatC = donHangDangChay.GiayMatC,
                            Line = donHangDangChay.Line,
                            GhiChu = donHangDangChay.GhiChu
                        };
                    }
                }
            }
        }

        /// <summary>
        /// Tag PLC báo là đổi đơn. PC sẽ ghi lên 1 sau khi truyền đơn mới xuống, PLC đọc 1 thì đổi đơn rồi set về 0.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DoiDonCutter_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "0")
            {
                //sau khi nhận tín hiệu PLC báo về đã đổi đơn thành công thì update status đơn hàng thành Processing
                using (var connection = GlobalVariable.GetDbConnection())
                {
                    var para = new DynamicParameters();
                    //update trạng thái hoàn thành cho đơn hàng vừa chạy xong

                    connection.Execute($"UPDATE tbldonhang set Status = 2 WHERE Id = {_donHangHoanThanh.IdDonHang}");
                    //update
                    connection.Execute($"UPDATE tbldonhang set Status = 1 WHERE Id = {_donHangDangChay.IdDonHang}");

                    para = new DynamicParameters();
                    para.Add("_donHangId", _donHangDangChay.IdDonHang);
                    para.Add("_ma", _donHangDangChay.Ma);
                    para.Add("_song", _donHangDangChay.Song);
                    para.Add("_kho", _donHangDangChay.Kho);
                    para.Add("_chieuDai", _donHangDangChay.DaiCat);
                    para.Add("_soLuong", _donHangDangChay.SLCatTam);
                    para.Add("_slDat", _donHangDangChay.SLDat);
                    para.Add("_slLoi", _donHangDangChay.SLLoi);
                    para.Add("_slConLai", _donHangDangChay.SLConLai);
                    para.Add("_pallet", _donHangDangChay.Pallet);
                    para.Add("_xa", _donHangDangChay.Xa);
                    para.Add("_thoiGianBatDau", _donHangDangChay.TGBatDau);
                    para.Add("_thoiGianKetThuc", _donHangDangChay.TGKetThuc);
                    para.Add("_thoiGianChay", _donHangDangChay.Chay);
                    para.Add("_thoiGianDung", _donHangDangChay.Dung);
                    para.Add("_hoanTatCutter", _donHangDangChay.HoanTatCutter);
                    para.Add("_hoanTatSpliter", _donHangDangChay.HoanTatSpliter);
                    para.Add("_hoanTatMayMen", _donHangDangChay.HoanTatMayMen);
                    para.Add("_hoanTatSongE", _donHangDangChay.HoanTatSongE);
                    para.Add("_hoanTatGiayMatE", _donHangDangChay.HoanTatGiayMatE);
                    para.Add("_hoanTatGiaySongE", _donHangDangChay.HoanTatGiaySongE);
                    para.Add("_hoanTatSongB", _donHangDangChay.HoanTatSongB);
                    para.Add("_hoanTatGiayMatB", _donHangDangChay.HoanTatGiayMatB);
                    para.Add("_hoanTatGiaySongB", _donHangDangChay.HoanTatGiaySongB);
                    para.Add("_hoanTatSongC", _donHangDangChay.HoanTatSongC);
                    para.Add("_hoanTatGiayMatC", _donHangDangChay.HoanTatGiayMatC);
                    para.Add("_hoanTatGiaySongC", _donHangDangChay.HoanTatGiaySongC);
                    connection.Execute($"sp_tblDonHangDangChayInsert", para, commandType: CommandType.StoredProcedure);
                }
            }
        }

        /// <summary>
        /// Tag yêu cầu đổi đơn gửi từ PLC về. Khi PC nhận đc tín hiệu này thì lấy đơn có trạng thái 'NewOrder' có STT nhỏ nhất, truyền xuống PLC.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LenhChuyenDonCutter_ValueChanged(object sender, EasyScada.Core.TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                Debug.WriteLine("Chuyen don");

                //move _donHangDangChay sang _donHangHoanThanh
                if (_donHangDangChay.STT != 0)
                {
                    var donhang = new DonHangChayModel()
                    {
                        IdDonHang = _donHangDangChay.IdDonHang,
                        STT = _donHangDangChay.STT,
                        Ma = _donHangDangChay.Ma,
                        Song = _donHangDangChay.Song,
                        Kho = _donHangDangChay.Kho,
                        DaiCat = _donHangDangChay.DaiCat,
                        SLCatTam = _donHangDangChay.SLCatTam,
                        Pallet = _donHangDangChay.Pallet,
                        Xa = _donHangDangChay.Xa,
                        Rong = _donHangDangChay.Rong,
                        Canh = _donHangDangChay.Canh,
                        Cao = _donHangDangChay.Cao,
                        Lang = _donHangDangChay.Lang,
                        GiayMen = _donHangDangChay.GiayMen,
                        GiaySongE = _donHangDangChay.GiaySongE,
                        GiayMatE = _donHangDangChay.GiayMatE,
                        GiaySongB = _donHangDangChay.GiaySongB,
                        GiayMatB = _donHangDangChay.GiayMatB,
                        GiaySongC = _donHangDangChay.GiaySongC,
                        GiayMatC = _donHangDangChay.GiayMatC,
                        KhachHang = _donHangDangChay.KhachHang,
                        DonHang = _donHangDangChay.DonHang,
                        PO = _donHangDangChay.PO,
                        Line = _donHangDangChay.Line,
                        GhiChu = _donHangDangChay.GhiChu
                    };

                    _donHangHoanThanh = null;
                    _donHangHoanThanh = new DonHangChayModel();
                    _donHangHoanThanh = donhang;
                }

                using (var connection = GlobalVariable.GetDbConnection())
                {
                    var resultData = connection.Query<DonHangModel>("sp_tblDonHangGetOnProcess").ToList();

                    if (resultData.Count > 0)
                    {
                        var c = resultData.Where(u => u.Status == 0).ToList();
                        _donHangDoiDon = c.FirstOrDefault(x => x.STT == c.Min(u => u.STT));

                        //add thong tin don hang moi vao donHangDangChay
                        _donHangDangChay = null;
                        _donHangDangChay = new DonHangChayModel()
                        {
                            IdDonHang = _donHangDoiDon.Id,
                            STT = _donHangDoiDon.STT,
                            Ma = _donHangDoiDon.Ma,
                            Song = _donHangDoiDon.Song,
                            Kho = _donHangDoiDon.Kho,
                            DaiCat = _donHangDoiDon.DaiCat,
                            SLCatTam = _donHangDoiDon.SLCatTam,
                            Pallet = _donHangDoiDon.Pallet,
                            Xa = _donHangDoiDon.Xa,
                            Rong = _donHangDoiDon.Rong,
                            Canh = _donHangDoiDon.Canh,
                            Cao = _donHangDoiDon.Cao,
                            Lang = _donHangDoiDon.Lang,
                            GiayMen = _donHangDoiDon.GiayMen,
                            GiaySongE = _donHangDoiDon.GiaySongE,
                            GiayMatE = _donHangDoiDon.GiayMatE,
                            GiaySongB = _donHangDoiDon.GiaySongB,
                            GiayMatB = _donHangDoiDon.GiayMatB,
                            GiaySongC = _donHangDoiDon.GiaySongC,
                            GiayMatC = _donHangDoiDon.GiayMatC,
                            Line = _donHangDoiDon.Line,
                            GhiChu = _donHangDoiDon.GhiChu
                        };

                        DoiDonCutter(_donHangDoiDon);

                        TinhToan.TinhToanGiaTri(_donHangDangChay);

                        DoiDonMayXa(_donHangDoiDon);
                    }
                }

                easyDriverConnector1.GetTag("Local Station/ChannelServer/DeviceCutter/LenhChuyenDon").WriteAsync("0", WritePiority.High);
            }
        }
        #endregion

        #endregion

        #region Methods
        private void DoiDonMayXa(DonHangModel donHangDangChay)
        {
            easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/STT1").WriteAsync(donHangDangChay.STT.ToString(), WritePiority.High);
            easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Xa1").WriteAsync(donHangDangChay.Xa.ToString(), WritePiority.High);
            easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Nap11").WriteAsync(donHangDangChay.Canh.ToString(), WritePiority.High);
            easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Cao1").WriteAsync(donHangDangChay.Cao.ToString(), WritePiority.High);
            easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Nap12").WriteAsync(donHangDangChay.Canh.ToString(), WritePiority.High);
            easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Lang1").WriteAsync(donHangDangChay.Lang.ToString(), WritePiority.High);
            easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Song1").WriteAsync(GlobalVariable.DaoLangPosition.Song.ToString(), WritePiority.High);
            easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/DoSauLan_SV").WriteAsync(((int)(donHangDangChay.DoSauLan * 10)).ToString(), WritePiority.High);
            easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/MetToiKeHoach1").WriteAsync((donHangDangChay.SoMetCaiDat * 10).ToString(), WritePiority.High);
            //easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Song1").WriteAsync(donHangDangChay.Song.ToString(), WritePiority.High);

            easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Dao1_SV").WriteAsync(GlobalVariable.DaoLangPosition.Dao1SV.ToString(), WritePiority.High);
            easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Dao2_SV").WriteAsync(GlobalVariable.DaoLangPosition.Dao2SV.ToString(), WritePiority.High);
            easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Dao3_SV").WriteAsync(GlobalVariable.DaoLangPosition.Dao3SV.ToString(), WritePiority.High);
            easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Dao4_SV").WriteAsync(GlobalVariable.DaoLangPosition.Dao4SV.ToString(), WritePiority.High);
            easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Dao5_SV").WriteAsync(GlobalVariable.DaoLangPosition.Dao5SV.ToString(), WritePiority.High);

            easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Dao1_U").WriteAsync(GlobalVariable.DaoLangPosition.Dao1U.ToString(), WritePiority.High);
            easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Dao2_U").WriteAsync(GlobalVariable.DaoLangPosition.Dao2U.ToString(), WritePiority.High);
            easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Dao3_U").WriteAsync(GlobalVariable.DaoLangPosition.Dao3U.ToString(), WritePiority.High);
            easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Dao4_U").WriteAsync(GlobalVariable.DaoLangPosition.Dao4U.ToString(), WritePiority.High);
            easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Dao5_U").WriteAsync(GlobalVariable.DaoLangPosition.Dao5U.ToString(), WritePiority.High);

            easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Lang1_SV").WriteAsync(GlobalVariable.DaoLangPosition.Lang1Sv.ToString(), WritePiority.High);
            easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Lang2_SV").WriteAsync(GlobalVariable.DaoLangPosition.Lang2Sv.ToString(), WritePiority.High);
            easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Lang3_SV").WriteAsync(GlobalVariable.DaoLangPosition.Lang3Sv.ToString(), WritePiority.High);
            easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Lang4_SV").WriteAsync(GlobalVariable.DaoLangPosition.Lang4Sv.ToString(), WritePiority.High);
            easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Lang5_SV").WriteAsync(GlobalVariable.DaoLangPosition.Lang5Sv.ToString(), WritePiority.High);
            easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Lang6_SV").WriteAsync(GlobalVariable.DaoLangPosition.Lang6Sv.ToString(), WritePiority.High);
            easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Lang7_SV").WriteAsync(GlobalVariable.DaoLangPosition.Lang7Sv.ToString(), WritePiority.High);
            easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Lang8_SV").WriteAsync(GlobalVariable.DaoLangPosition.Lang8Sv.ToString(), WritePiority.High);

            easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Lang1_U").WriteAsync(GlobalVariable.DaoLangPosition.Lang1U.ToString(), WritePiority.High);
            easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Lang2_U").WriteAsync(GlobalVariable.DaoLangPosition.Lang2U.ToString(), WritePiority.High);
            easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Lang3_U").WriteAsync(GlobalVariable.DaoLangPosition.Lang3U.ToString(), WritePiority.High);
            easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Lang4_U").WriteAsync(GlobalVariable.DaoLangPosition.Lang4U.ToString(), WritePiority.High);
            easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Lang5_U").WriteAsync(GlobalVariable.DaoLangPosition.Lang5U.ToString(), WritePiority.High);
            easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Lang6_U").WriteAsync(GlobalVariable.DaoLangPosition.Lang6U.ToString(), WritePiority.High);
            easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Lang7_U").WriteAsync(GlobalVariable.DaoLangPosition.Lang7U.ToString(), WritePiority.High);
            easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Lang8_U").WriteAsync(GlobalVariable.DaoLangPosition.Lang8U.ToString(), WritePiority.High);

            easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Hut_SV").WriteAsync(GlobalVariable.DaoLangPosition.HutSv.ToString(), WritePiority.High);


            this.Invoke((MethodInvoker)delegate
            {
                //_labDonHang.Text = _donHangDangChay.DonHang;
                //_labKhachHang.Text = _donHangDangChay.KhachHang;
                //_labPo.Text = _donHangDangChay.PO;
                //_labSlConLai.Text = _donHangDangChay.SLConLai.ToString();
            });

            //baos cho PLC biet là đã truyền thông số đơn mới xuống xong.
            easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/DoiDon").WriteAsync("1", WritePiority.High);
        }

        private void DoiDonCutter(DonHangModel donHangDangChay)
        {
            easyDriverConnector1.GetTag("Local Station/ChannelServer/DeviceCutter/ChieuDaiCat1").WriteAsync(donHangDangChay.DaiCat.ToString(), WritePiority.High);
            easyDriverConnector1.GetTag("Local Station/ChannelServer/DeviceCutter/SoLuongCat1").WriteAsync(donHangDangChay.SLCatTam.ToString(), WritePiority.High);
            easyDriverConnector1.GetTag("Local Station/ChannelServer/DeviceCutter/Pallet1").WriteAsync(donHangDangChay.Pallet.ToString(), WritePiority.High);
            easyDriverConnector1.GetTag("Local Station/ChannelServer/DeviceCutter/STT1").WriteAsync(donHangDangChay.STT.ToString(), WritePiority.High);

            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() =>
                {
                    _labDonHang.Text = _donHangDangChay.DonHang;
                    _labKhachHang.Text = _donHangDangChay.KhachHang;
                    _labPo.Text = _donHangDangChay.PO;
                    _labSlConLai.Text = _donHangDangChay.SLConLai.ToString();
                }));
            }
            else
            {
                _labDonHang.Text = _donHangDangChay.DonHang;
                _labKhachHang.Text = _donHangDangChay.KhachHang;
                _labPo.Text = _donHangDangChay.PO;
                _labSlConLai.Text = _donHangDangChay.SLConLai.ToString();
            }

            //baos cho PLC biet là đã truyền thông số đơn mới xuống xong.
            easyDriverConnector1.GetTag("Local Station/ChannelServer/DeviceCutter/DoiDon").WriteAsync("1", WritePiority.High);
        }
        #endregion
    }
}
