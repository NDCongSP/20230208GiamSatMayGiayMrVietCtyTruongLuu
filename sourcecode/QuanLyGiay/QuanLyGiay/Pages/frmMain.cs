﻿using CommonControl;
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
        //bool _isLoaded = false;
        int _countRefesh = 0; //100*100=10,000 =10S
        private Timer _t = new Timer();
        //private Task _taskLoadOrder;
        List<DonHangModel> _resultData = new List<DonHangModel>();
        DonHangModel _donHangDoiDon = new DonHangModel();//chứa thông tin đơn hàng chuẩn bị đổi đơn.
        DonHangChayModel _donHangDangChay = new DonHangChayModel();//chứa thông tin đơn hàng đang chạy
        DonHangChayModel _donHangHoanThanh = new DonHangChayModel();//chứa thông tin đơn hàng đã hoàn thành, sau khi đổi đơn thì sẽ move thông tin của _donHangDangChay sang đây, để phục vụ cho các tác vụ tiếp theo.

        int _donHangMoiCount = 0;//dem don hang moi

        public frmMain()
        {
            InitializeComponent();

            Load += Form1_Load;

        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            using (var connection = GlobalVariable.GetDbConnection())
            {
                var resultData = connection.Query<DonHangModel>("sp_tblDonHangGetOnProcess").ToList();

                if (resultData.Count > 0)
                {
                    var c = resultData.Where(u => u.Status == StatusDHEnum.Chay).FirstOrDefault();

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
                }
            }

            _t.Interval = 100;
            _t.Enabled = true;
            _t.Tick += (s, o) =>
            {
                Timer t = (Timer)s;
                t.Enabled = false;

                //Debug.WriteLine($"Trạng thái kết nối driver server {easyDriverConnector1.ConnectionStatus}");
                if (_donHangDangChay != null)
                {
                    _donHangDangChay.TGKetThuc = DateTime.Now;
                }
                //if (_donHangDangChay.STT != 0)
                {
                    if (this.InvokeRequired)
                    {
                        this?.Invoke(new Action(() =>
                        {
                            labDateTime.Text = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");

                            _labKhachHang.Text = _donHangDangChay.KhachHang;
                            _labDonHang.Text = _donHangDangChay.DonHang;
                            _labPO.Text = _donHangDangChay.PO;
                            _labDaiCat.Text = (_donHangDangChay.DaiCat).ToString();
                            _labSLCat.Text = _donHangDangChay.SLCatTam.ToString();
                            _labTongSoMetCD.Text = _donHangDangChay.SoMetCaiDat.ToString();
                            _labSLDat.Text = _donHangDangChay.SLDat.ToString();
                            _labSLLoi.Text = _donHangDangChay.SLLoi.ToString();
                            _labSLConlai.Text = _donHangDangChay.SLConLai.ToString();
                            _labTocDo.Text = _donHangDangChay.TocDo.ToString();
                            _labSoMetCL.Text = _donHangDangChay.ConLai.ToString();
                        }));
                    }
                    else
                    {
                        labDateTime.Text = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");

                        _labKhachHang.Text = _donHangDangChay.KhachHang;
                        _labDonHang.Text = _donHangDangChay.DonHang;
                        _labPO.Text = _donHangDangChay.PO;
                        _labDaiCat.Text = (_donHangDangChay.DaiCat).ToString();
                        _labSLCat.Text = _donHangDangChay.SLCatTam.ToString();
                        _labTongSoMetCD.Text = _donHangDangChay.SoMetCaiDat.ToString();
                        _labSLDat.Text = _donHangDangChay.SLDat.ToString();
                        _labSLLoi.Text = _donHangDangChay.SLLoi.ToString();
                        _labSLConlai.Text = _donHangDangChay.SLConLai.ToString();
                        _labTocDo.Text = _donHangDangChay.TocDo.ToString();
                        _labSoMetCL.Text = _donHangDangChay.ConLai.ToString();
                    }
                }

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
                else if (o.KeyCode == Keys.F5)
                {
                    if (MessageBox.Show($"Bạn có chắc chắn muốn chuyển đơn?", "XÁC NHẬN", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        BtnNapMayXa_Click(null, null);
                    }
                }
                else if (o.KeyCode == Keys.F2)
                {
                    _btnSettings_Click(null, null);
                }
                else if (o.KeyCode == Keys.F3)
                {

                }
                else if (o.KeyCode == Keys.F6)
                {
                    _btnTatMay_Click(null, null);
                }
            };
            #endregion

            btnOrder.Click += BtnOrder_Click;
            btnNapMayXa.Click += BtnNapMayXa_Click;
            _btnSettings.Click += _btnSettings_Click;
            _btnTatMay.Click += _btnTatMay_Click;

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

        private void _btnTatMay_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn tắt máy tính?", "Cảnh Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Process.Start("shutdown.exe", "-s -t 00");
            }
        }

        private void _btnSettings_Click(object sender, EventArgs e)
        {
            using (var nf = new frmSettings())
            {
                nf.ShowDialog();
            }
        }

        private void BtnNapMayXa_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("Chuyen don");

            //baos cho PLC biet là đã truyền thông số đơn mới xuống xong.
            easyDriverConnector1.GetTag("Local Station/ChannelServer/DeviceCutter/DoiDon").WriteAsync("0", WritePiority.High);
            //reset lenhChuyenDon
            easyDriverConnector1.GetTag("Local Station/ChannelServer/DeviceCutter/LenhChuyenDon").WriteAsync("1", WritePiority.High);

            //LuuDonHangChayXong();
            //ChuyenDon();
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
                //Console.WriteLine("vong lap task");

                using (var connection = GlobalVariable.GetDbConnection())
                {
                    _resultData = connection.Query<DonHangModel>("sp_tblDonHangGetOnProcess").ToList();

                    if (_resultData.Count > 0)
                    {
                        _donHangMoiCount = _resultData.Where(u => u.Status == StatusDHEnum.Moi).ToList().Count;
                        Console.WriteLine($"So luong don hang moi = {_donHangMoiCount}");
                        if (grvDH.InvokeRequired)
                        {
                            grvDH?.Invoke(new Action(() =>
                            {
                                grvDH.DataSource = _resultData;
                                grvDH.Columns["CreatedDate"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm:ss";
                            }));
                        }
                        else
                        {
                            grvDH.DataSource = _resultData;

                            grvDH.Columns["CreatedDate"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm:ss";
                        }

                        //grvDH.Columns["CustomerID"].Visible = false;
                        grvDH.Columns["Status"].DisplayIndex = 0;
                        grvDH.Columns["STT"].DisplayIndex = 1;
                        grvDH.Columns["Ma"].DisplayIndex = 2;
                        grvDH.Columns["Song"].DisplayIndex = 3;
                        grvDH.Columns["Xa"].DisplayIndex = 4;
                        grvDH.Columns["Rong"].DisplayIndex = 5;
                        grvDH.Columns["Cao"].DisplayIndex = 6;
                        grvDH.Columns["Lang"].DisplayIndex = 7;
                        grvDH.Columns["Kho"].DisplayIndex = 8;
                        grvDH.Columns["DaiCat"].DisplayIndex = 9;
                        grvDH.Columns["SLCatTam"].DisplayIndex = 10;
                        grvDH.Columns["SoMetCaiDat"].DisplayIndex = 11;
                        grvDH.Columns["TongSoThung"].DisplayIndex = 12;
                        grvDH.Columns["DoSauLan"].DisplayIndex = 13;
                        grvDH.Columns["SoLop"].DisplayIndex = 14;
                        grvDH.Columns["CongThem"].DisplayIndex = 15;
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
            easyDriverConnector1.GetTag("Local Station/ChannelServer/DeviceCutter/SoLuongDat_FB").ValueChanged += SoLuongDat_ValueChanged;
            easyDriverConnector1.GetTag("Local Station/ChannelServer/DeviceCutter/SoLuongLoi_FB").ValueChanged += SoLuongLoi_ValueChanged;
            easyDriverConnector1.GetTag("Local Station/ChannelServer/DeviceCutter/TocDo_FB").ValueChanged += TocDo_ValueChanged;
            easyDriverConnector1.GetTag("Local Station/ChannelServer/DeviceCutter/KhoangCachMayXaDenMayCat").ValueChanged += KhoangCachMayXaDenMayCat_ValueChanged;

            LenhChuyenDonCutter_ValueChanged(easyDriverConnector1.GetTag("Local Station/ChannelServer/DeviceCutter/LenhChuyenDon"),
                              new TagValueChangedEventArgs(easyDriverConnector1.GetTag("Local Station/ChannelServer/DeviceCutter/LenhChuyenDon")
                              , "", easyDriverConnector1.GetTag("Local Station/ChannelServer/DeviceCutter/LenhChuyenDon").Value));

            RunCutter_ValueChanged(easyDriverConnector1.GetTag("Local Station/ChannelServer/DeviceCutter/Run"),
                              new TagValueChangedEventArgs(easyDriverConnector1.GetTag("Local Station/ChannelServer/DeviceCutter/Run")
                              , "", easyDriverConnector1.GetTag("Local Station/ChannelServer/DeviceCutter/Run").Value));

            //DoiDonCutter_ValueChanged(easyDriverConnector1.GetTag("Local Station/ChannelServer/DeviceCutter/DoiDon"),
            //                 new TagValueChangedEventArgs(easyDriverConnector1.GetTag("Local Station/ChannelServer/DeviceCutter/DoiDon")
            //                 , "", easyDriverConnector1.GetTag("Local Station/ChannelServer/DeviceCutter/DoiDon").Value));

            SoLuongDat_ValueChanged(easyDriverConnector1.GetTag("Local Station/ChannelServer/DeviceCutter/SoLuongDat_FB"),
                              new TagValueChangedEventArgs(easyDriverConnector1.GetTag("Local Station/ChannelServer/DeviceCutter/SoLuongDat_FB")
                              , "", easyDriverConnector1.GetTag("Local Station/ChannelServer/DeviceCutter/SoLuongDat_FB").Value));

            SoLuongLoi_ValueChanged(easyDriverConnector1.GetTag("Local Station/ChannelServer/DeviceCutter/SoLuongLoi_FB"),
                              new TagValueChangedEventArgs(easyDriverConnector1.GetTag("Local Station/ChannelServer/DeviceCutter/SoLuongLoi_FB")
                              , "", easyDriverConnector1.GetTag("Local Station/ChannelServer/DeviceCutter/SoLuongLoi_FB").Value));

            TocDo_ValueChanged(easyDriverConnector1.GetTag("Local Station/ChannelServer/DeviceCutter/TocDo_FB"),
                              new TagValueChangedEventArgs(easyDriverConnector1.GetTag("Local Station/ChannelServer/DeviceCutter/TocDo_FB")
                              , "", easyDriverConnector1.GetTag("Local Station/ChannelServer/DeviceCutter/TocDo_FB").Value));
            #endregion

            #region may xa
            easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Run").ValueChanged += RunMayXa1_ValueChanged;

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

            easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Lan_Lan").ValueChanged += Lang_Lang_MayXa1_ValueChanged;
            easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Lan1_Min").ValueChanged += Lang1_MinMayXa1_ValueChanged;
            easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Lan2_Min").ValueChanged += Lang2_MinMayXa1_ValueChanged;
            easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Lan3_Min").ValueChanged += Lang3_MinMayXa1_ValueChanged;
            easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Lan4_Min").ValueChanged += Lang4_MinMayXa1_ValueChanged;
            easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Lan1_Max").ValueChanged += Lang1_MaxMayXa1_ValueChanged;
            easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Lan2_Max").ValueChanged += Lang2_MaxMayXa1_ValueChanged;
            easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Lan3_Max").ValueChanged += Lang3_MaxMayXa1_ValueChanged;
            easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Lan4_Max").ValueChanged += Lang4_MaxMayXa1_ValueChanged;

            easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/KhoMay").ValueChanged += KhoMayMayXa1_ValueChanged;

            easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Dao1OffSV").ValueChanged += Dao1OffSV_ValueChanged;
            easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Dao2OffSV").ValueChanged += Dao2OffSV_ValueChanged;
            easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Lang1OffSV").ValueChanged += Lang1OffSV_ValueChanged;
            easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Lang2OffSV").ValueChanged += Lang2OffSV_ValueChanged;
            easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Lang3OffSV").ValueChanged += Lang3OffSV_ValueChanged;
            easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Lang4OffSV").ValueChanged += Lang4OffSV_ValueChanged;

            easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/TrangThaiMayXa").ValueChanged += TrangThaiMayXa_ValueChanged;

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


            Lang_Lang_MayXa1_ValueChanged(easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Lan_Lan"), new TagValueChangedEventArgs(easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Lan_Lan"), "", easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Lan_Lan").Value));
            Lang1_MinMayXa1_ValueChanged(easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Lan1_Min"), new TagValueChangedEventArgs(easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Lan1_Min"), "", easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Lan1_Min").Value));
            Lang2_MinMayXa1_ValueChanged(easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Lan2_Min"), new TagValueChangedEventArgs(easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Lan2_Min"), "", easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Lan2_Min").Value));
            Lang3_MinMayXa1_ValueChanged(easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Lan3_Min"), new TagValueChangedEventArgs(easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Lan3_Min"), "", easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Lan3_Min").Value));
            Lang4_MinMayXa1_ValueChanged(easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Lan4_Min"), new TagValueChangedEventArgs(easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Lan4_Min"), "", easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Lan4_Min").Value));

            Lang1_MaxMayXa1_ValueChanged(easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Lan1_Max"), new TagValueChangedEventArgs(easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Lan1_Max"), "", easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Lan1_Max").Value));
            Lang2_MaxMayXa1_ValueChanged(easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Lan2_Max"), new TagValueChangedEventArgs(easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Lan2_Max"), "", easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Lan2_Max").Value));
            Lang3_MaxMayXa1_ValueChanged(easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Lan3_Max"), new TagValueChangedEventArgs(easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Lan3_Max"), "", easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Lan3_Max").Value));
            Lang4_MaxMayXa1_ValueChanged(easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Lan4_Max"), new TagValueChangedEventArgs(easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Lan4_Max"), "", easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Lan4_Max").Value));
            KhoMayMayXa1_ValueChanged(easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/KhoMay"), new TagValueChangedEventArgs(easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/KhoMay"), "", easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/KhoMay").Value));

            Dao1OffSV_ValueChanged(easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Dao1OffSV"), new TagValueChangedEventArgs(easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Dao1OffSV"), "", easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Dao1OffSV").Value));
            Dao2OffSV_ValueChanged(easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Dao2OffSV"), new TagValueChangedEventArgs(easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Dao2OffSV"), "", easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Dao2OffSV").Value));
            Lang1OffSV_ValueChanged(easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Lang1OffSV"), new TagValueChangedEventArgs(easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Lang1OffSV"), "", easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Lang1OffSV").Value));
            Lang2OffSV_ValueChanged(easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Lang2OffSV"), new TagValueChangedEventArgs(easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Lang2OffSV"), "", easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Lang2OffSV").Value));
            Lang3OffSV_ValueChanged(easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Lang3OffSV"), new TagValueChangedEventArgs(easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Lang3OffSV"), "", easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Lang3OffSV").Value));
            Lang4OffSV_ValueChanged(easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Lang4OffSV"), new TagValueChangedEventArgs(easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Lang4OffSV"), "", easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Lang4OffSV").Value));
            #endregion

            #endregion

            if (easyDriverConnector1.ConnectionStatus == ConnectionStatus.Connected)
            {
                _labDriverStatus.BackColor = Color.Green;
                _labDriverStatus.ForeColor = Color.White;
            }
            else
            {
                _labDriverStatus.BackColor = Color.Red;
                _labDriverStatus.ForeColor = Color.White;
            }
        }

        private void KhoangCachMayXaDenMayCat_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            
        }

        /// <summary>
        /// ghi trạng thái chuyển đơn của máy xe sang cho máy cắt biết.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TrangThaiMayXa_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            easyDriverConnector1.GetTag("Local Station/ChannelServer/DeviceCutter/TrangThaiMayXa").WriteAsync(e.NewValue, WritePiority.High);
        }

        #region may xa 1
        private void Lang4OffSV_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            GlobalVariable.DaoLangPosition.Lang4OffSV = int.TryParse(e.NewValue, out int value) ? value : 0;
        }

        private void Lang3OffSV_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            GlobalVariable.DaoLangPosition.Lang3OffSV = int.TryParse(e.NewValue, out int value) ? value : 0;
        }

        private void Lang2OffSV_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            GlobalVariable.DaoLangPosition.Lang2OffSV = int.TryParse(e.NewValue, out int value) ? value : 0;
        }

        private void Lang1OffSV_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            GlobalVariable.DaoLangPosition.Lang1OffSV = int.TryParse(e.NewValue, out int value) ? value : 0;
        }

        private void Dao2OffSV_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            GlobalVariable.DaoLangPosition.Dao2OffSV = int.TryParse(e.NewValue, out int value) ? value : 0;
        }

        private void Dao1OffSV_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            GlobalVariable.DaoLangPosition.Dao1OffSV = int.TryParse(e.NewValue, out int value) ? value : 0;
        }

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

        private void Lang4_MinMayXa1_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            GlobalVariable.DaoLangPosition.Lang4Min = int.TryParse(e.NewValue, out int value) ? value : 0;
        }

        private void Lang3_MinMayXa1_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            GlobalVariable.DaoLangPosition.Lang3Min = int.TryParse(e.NewValue, out int value) ? value : 0;
        }

        private void Lang2_MinMayXa1_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            GlobalVariable.DaoLangPosition.Lang2Min = int.TryParse(e.NewValue, out int value) ? value : 0;
        }

        private void Lang1_MinMayXa1_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            GlobalVariable.DaoLangPosition.Lang1Min = int.TryParse(e.NewValue, out int value) ? value : 0;
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
                this?.Invoke((MethodInvoker)delegate { _panelCutterStatus.BackColor = Color.Green; });
            }
            else
            {
                this?.Invoke((MethodInvoker)delegate { _panelCutterStatus.BackColor = Color.Gray; });
            }
        }

        private void TocDo_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            _donHangDangChay.TocDo = Convert.ToDouble(e.NewValue) / 10;
            Debug.WriteLine($"Speed: {_donHangDangChay.TocDo}");
        }

        private void SoLuongLoi_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            _donHangDangChay.SLLoi = int.TryParse(e.NewValue, out int value) ? value : 0;
        }

        private void SoLuongDat_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            _donHangDangChay.SLDat = int.TryParse(e.NewValue, out int value) ? value : 0;
        }

        /// <summary>
        /// Tag PLC báo là đổi đơn. PC sẽ ghi lên 1 sau khi truyền đơn mới xuống, PLC đọc 1 thì đổi đơn rồi set về 0.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DoiDonCutter_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            Debug.WriteLine($"Đổi đơn cutter {e.NewValue}");
            if (e.NewValue == "0")
            {

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
                if (_donHangMoiCount > 0)
                {
                    LuuDonHangChayXong();
                    ChuyenDon();
                }
                else
                {
                    //reset lenhChuyenDon
                    easyDriverConnector1.GetTag("Local Station/ChannelServer/DeviceCutter/LenhChuyenDon").WriteAsync("0", WritePiority.High);

                    MessageBox.Show("Hết đơn hàng. Mời bạn nhập thêm đơn hàng mới.", "CẢNH BÁO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
        #endregion

        #endregion

        #region Methods
        private void DoiDonMayXa(DonHangModel donHangDangChay)
        {
            var s = ((donHangDangChay.DoSauLan * 10)).ToString("#");
            easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Xa").WriteAsync(donHangDangChay.Xa.ToString(), WritePiority.High);
            //Nhan 10 hai lần là vì, 1 lần chuyển từ cm->mm, lần 2 là để bỏ số lẻ ghi xuống driver.
            easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Rong").WriteAsync((donHangDangChay.Rong * 10).ToString("#"), WritePiority.High);
            easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Cao").WriteAsync((donHangDangChay.Cao * 10).ToString("#"), WritePiority.High);
            easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Canh").WriteAsync((donHangDangChay.Canh * 10).ToString("#"), WritePiority.High);
            easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Lan").WriteAsync(donHangDangChay.Lang.ToString(), WritePiority.High);
            easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Song").WriteAsync((donHangDangChay.CongThem * 10).ToString("#"), WritePiority.High);
            easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/DoSauLan").WriteAsync((donHangDangChay.DoSauLan * 10).ToString("#"), WritePiority.High);
            easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/MetToiKeHoach").WriteAsync((donHangDangChay.SoMetCaiDat * 10).ToString("#"), WritePiority.High);

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

            easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Lan1_SV").WriteAsync(GlobalVariable.DaoLangPosition.Lang1Sv.ToString(), WritePiority.High);
            easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Lan2_SV").WriteAsync(GlobalVariable.DaoLangPosition.Lang2Sv.ToString(), WritePiority.High);
            easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Lan3_SV").WriteAsync(GlobalVariable.DaoLangPosition.Lang3Sv.ToString(), WritePiority.High);
            easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Lan4_SV").WriteAsync(GlobalVariable.DaoLangPosition.Lang4Sv.ToString(), WritePiority.High);
            easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Lan5_SV").WriteAsync(GlobalVariable.DaoLangPosition.Lang5Sv.ToString(), WritePiority.High);
            easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Lan6_SV").WriteAsync(GlobalVariable.DaoLangPosition.Lang6Sv.ToString(), WritePiority.High);
            easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Lan7_SV").WriteAsync(GlobalVariable.DaoLangPosition.Lang7Sv.ToString(), WritePiority.High);
            easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Lan8_SV").WriteAsync(GlobalVariable.DaoLangPosition.Lang8Sv.ToString(), WritePiority.High);

            easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Lan1_U").WriteAsync(GlobalVariable.DaoLangPosition.Lang1U.ToString(), WritePiority.High);
            easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Lan2_U").WriteAsync(GlobalVariable.DaoLangPosition.Lang2U.ToString(), WritePiority.High);
            easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Lan3_U").WriteAsync(GlobalVariable.DaoLangPosition.Lang3U.ToString(), WritePiority.High);
            easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Lan4_U").WriteAsync(GlobalVariable.DaoLangPosition.Lang4U.ToString(), WritePiority.High);
            easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Lan5_U").WriteAsync(GlobalVariable.DaoLangPosition.Lang5U.ToString(), WritePiority.High);
            easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Lan6_U").WriteAsync(GlobalVariable.DaoLangPosition.Lang6U.ToString(), WritePiority.High);
            easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Lan7_U").WriteAsync(GlobalVariable.DaoLangPosition.Lang7U.ToString(), WritePiority.High);
            easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Lan8_U").WriteAsync(GlobalVariable.DaoLangPosition.Lang8U.ToString(), WritePiority.High);

            easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Hut_SV").WriteAsync(GlobalVariable.DaoLangPosition.HutSv.ToString(), WritePiority.High);

            //baos cho PLC biet là đã truyền thông số đơn mới xuống xong.
            easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/DoiDon").WriteAsync("1", WritePiority.High);
        }

        private void DoiDonCutter(DonHangModel donHangDangChay)
        {
            easyDriverConnector1.GetTag("Local Station/ChannelServer/DeviceCutter/ChieuDaiCat").WriteAsync(((int)(donHangDangChay.DaiCat * 10)).ToString(), WritePiority.High);
            easyDriverConnector1.GetTag("Local Station/ChannelServer/DeviceCutter/SoLuongCat").WriteAsync(donHangDangChay.SLCatTam.ToString(), WritePiority.High);
            easyDriverConnector1.GetTag("Local Station/ChannelServer/DeviceCutter/Pallet").WriteAsync(donHangDangChay.Pallet.ToString(), WritePiority.High);

            //baos cho PLC biet là đã truyền thông số đơn mới xuống xong.
            easyDriverConnector1.GetTag("Local Station/ChannelServer/DeviceCutter/DoiDon").WriteAsync("1", WritePiority.High);
            //reset lenhChuyenDon
            easyDriverConnector1.GetTag("Local Station/ChannelServer/DeviceCutter/LenhChuyenDon").WriteAsync("0", WritePiority.High);
        }

        private void LuuDonHangChayXong()
        {
            //move _donHangDangChay sang _donHangHoanThanh
            if (_donHangDangChay.STT != 0)
            {
                _donHangHoanThanh = (DonHangChayModel)_donHangDangChay.Clone();

                using (var connection = GlobalVariable.GetDbConnection())
                {
                    var para = new DynamicParameters();
                    //update trạng thái hoàn thành cho đơn hàng vừa chạy xong
                    connection.Execute($"UPDATE tbldonhang set Status = 2 WHERE Id = {_donHangHoanThanh.IdDonHang}");

                    para = new DynamicParameters();
                    para.Add("_donHangId", _donHangHoanThanh.IdDonHang);
                    para.Add("_ma", _donHangHoanThanh.Ma);
                    para.Add("_song", _donHangHoanThanh.Song);
                    para.Add("_kho", _donHangHoanThanh.Kho);
                    para.Add("_chieuDai", _donHangHoanThanh.DaiCat);
                    para.Add("_soLuong", _donHangHoanThanh.SLCatTam);
                    para.Add("_slDat", _donHangHoanThanh.SLDat);
                    para.Add("_slLoi", _donHangHoanThanh.SLLoi);
                    para.Add("_slConLai", _donHangHoanThanh.SLConLai);
                    para.Add("_pallet", _donHangHoanThanh.Pallet);
                    para.Add("_xa", _donHangHoanThanh.Xa);
                    para.Add("_thoiGianBatDau", _donHangHoanThanh.TGBatDau);
                    para.Add("_thoiGianKetThuc", _donHangHoanThanh.TGKetThuc);
                    para.Add("_thoiGianChay", _donHangHoanThanh.Chay);
                    para.Add("_thoiGianDung", _donHangHoanThanh.Dung);
                    para.Add("_hoanTatCutter", _donHangHoanThanh.HoanTatCutter);
                    para.Add("_hoanTatSpliter", _donHangHoanThanh.HoanTatSpliter);
                    para.Add("_hoanTatMayMen", _donHangHoanThanh.HoanTatMayMen);
                    para.Add("_hoanTatSongE", _donHangHoanThanh.HoanTatSongE);
                    para.Add("_hoanTatGiayMatE", _donHangHoanThanh.HoanTatGiayMatE);
                    para.Add("_hoanTatGiaySongE", _donHangHoanThanh.HoanTatGiaySongE);
                    para.Add("_hoanTatSongB", _donHangHoanThanh.HoanTatSongB);
                    para.Add("_hoanTatGiayMatB", _donHangHoanThanh.HoanTatGiayMatB);
                    para.Add("_hoanTatGiaySongB", _donHangHoanThanh.HoanTatGiaySongB);
                    para.Add("_hoanTatSongC", _donHangHoanThanh.HoanTatSongC);
                    para.Add("_hoanTatGiayMatC", _donHangHoanThanh.HoanTatGiayMatC);
                    para.Add("_hoanTatGiaySongC", _donHangHoanThanh.HoanTatGiaySongC);
                    //connection.Execute($"sp_tblDonHangDangChayInsert", para, commandType: CommandType.StoredProcedure);
                }
            }
        }
        private void ChuyenDon()
        {
            using (var connection = GlobalVariable.GetDbConnection())
            {
                var para = new DynamicParameters();

                #region lấy đơn hàng mới để chuyển đơn
                var resultData = connection.Query<DonHangModel>("sp_tblDonHangGetOnProcess").ToList();

                if (resultData.Count > 0)
                {
                    var c = resultData.Where(u => u.Status == StatusDHEnum.Moi).ToList();
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
                        GhiChu = _donHangDoiDon.GhiChu,
                        KhachHang = _donHangDoiDon.KhachHang,
                        DonHang = _donHangDoiDon.DonHang,
                        PO = _donHangDoiDon.PO,
                        TGBatDau = DateTime.Now,
                    };

                    DoiDonCutter(_donHangDoiDon);

                    //chuyen don may xa
                    TinhToan.TinhToanGiaTri(_donHangDangChay);

                    DoiDonMayXa(_donHangDoiDon);

                    //update trang thái OnProcess cho đơn hàng mới được ghi xuống chạy
                    connection.Execute($"UPDATE tbldonhang set Status = 1 WHERE Id = {_donHangDangChay.IdDonHang}");
                }
                #endregion
            }
        }

        private void ResetCacGiaTriCaiDat()
        {
            easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Xa").WriteAsync("0", WritePiority.High);
            //Nhan 10 hai lần là vì, 1 lần chuyển từ cm->mm, lần 2 là để bỏ số lẻ ghi xuống driver.
            easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Rong").WriteAsync("0", WritePiority.High);
            easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Cao").WriteAsync("0", WritePiority.High);
            easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Canh").WriteAsync("0", WritePiority.High);
            easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Lan").WriteAsync("0", WritePiority.High);
            easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Song").WriteAsync("0", WritePiority.High);
            easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/DoSauLan").WriteAsync("0", WritePiority.High);
            easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/MetToiKeHoach").WriteAsync("0", WritePiority.High);

            easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Dao1_SV").WriteAsync("0", WritePiority.High);
            easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Dao2_SV").WriteAsync("0", WritePiority.High);
            easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Dao3_SV").WriteAsync("0", WritePiority.High);
            easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Dao4_SV").WriteAsync("0", WritePiority.High);
            easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Dao5_SV").WriteAsync("0", WritePiority.High);

            easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Dao1_U").WriteAsync("0", WritePiority.High);
            easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Dao2_U").WriteAsync("0", WritePiority.High);
            easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Dao3_U").WriteAsync("0", WritePiority.High);
            easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Dao4_U").WriteAsync("0", WritePiority.High);
            easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Dao5_U").WriteAsync("0", WritePiority.High);

            easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Lan1_SV").WriteAsync("0", WritePiority.High);
            easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Lan2_SV").WriteAsync("0", WritePiority.High);
            easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Lan3_SV").WriteAsync("0", WritePiority.High);
            easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Lan4_SV").WriteAsync("0", WritePiority.High);
            easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Lan5_SV").WriteAsync("0", WritePiority.High);
            easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Lan6_SV").WriteAsync("0", WritePiority.High);
            easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Lan7_SV").WriteAsync("0", WritePiority.High);
            easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Lan8_SV").WriteAsync("0", WritePiority.High);

            easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Lan1_U").WriteAsync("0", WritePiority.High);
            easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Lan2_U").WriteAsync("0", WritePiority.High);
            easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Lan3_U").WriteAsync("0", WritePiority.High);
            easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Lan4_U").WriteAsync("0", WritePiority.High);
            easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Lan5_U").WriteAsync("0", WritePiority.High);
            easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Lan6_U").WriteAsync("0", WritePiority.High);
            easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Lan7_U").WriteAsync("0", WritePiority.High);
            easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Lan8_U").WriteAsync("0", WritePiority.High);

            easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/Hut_SV").WriteAsync("0", WritePiority.High);

            //baos cho PLC biet là đã truyền thông số đơn mới xuống xong.
            easyDriverConnector1.GetTag("Local Station/ChannelMayXa/May1/DoiDon").WriteAsync("0", WritePiority.High);

            easyDriverConnector1.GetTag("Local Station/ChannelServer/DeviceCutter/ChieuDaiCat").WriteAsync("0", WritePiority.High);
            easyDriverConnector1.GetTag("Local Station/ChannelServer/DeviceCutter/SoLuongCat").WriteAsync("0", WritePiority.High);
            easyDriverConnector1.GetTag("Local Station/ChannelServer/DeviceCutter/Pallet").WriteAsync("0", WritePiority.High);

            //baos cho PLC biet là đã truyền thông số đơn mới xuống xong.
            easyDriverConnector1.GetTag("Local Station/ChannelServer/DeviceCutter/DoiDon").WriteAsync("0", WritePiority.High);
            //reset lenhChuyenDon
            easyDriverConnector1.GetTag("Local Station/ChannelServer/DeviceCutter/LenhChuyenDon").WriteAsync("0", WritePiority.High);
        }
        #endregion
    }
}
