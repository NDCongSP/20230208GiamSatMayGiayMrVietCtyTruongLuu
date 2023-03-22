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
        int _countRefesh = 0; //100*100=10,000 =10S
        private Timer _t = new Timer();
        private Task _taskLoadOrder;

        tblDonHangModel _donHangDoiDon = new tblDonHangModel();//chứa thông tin đơn hàng chuẩn bị đổi đơn.
        DonHangModel _donHangDangChay = new DonHangModel();//chứa thông tin đơn hàng đang chạy
        DonHangModel _donHangHoanThanh = new DonHangModel();//chứa thông tin đơn hàng đã hoàn thành, sau khi đổi đơn thì sẽ move thông tin của _donHangDangChay sang đây, để phục vụ cho các tác vụ tiếp theo.

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
                    var resultData = connection.Query<tblDonHangModel>("sp_tblDonHangGetOnProcess").ToList();

                    if (resultData.Count > 0)
                    {
                        this.Invoke((MethodInvoker)delegate
                        {
                            grvDH.DataSource = resultData;
                            grvDH.Columns["CreatedDate"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm:ss";
                        });
                    }
                }
                //Thread.Sleep(200);
            }
        }
        #region Events
        private void EasyDriverConnector1_Started(object sender, EventArgs e)
        {
            //may Cat tam
            easyDriverConnector1.GetTag("Local Station/ChannelServer/DeviceCutter/LenhChuyenDon").ValueChanged += LenhChuyenDonCutter_ValueChanged;
            easyDriverConnector1.GetTag("Local Station/ChannelServer/DeviceCutter/DoiDon").ValueChanged += DoiDonCutter_ValueChanged;
            easyDriverConnector1.GetTag("Local Station/ChannelServer/DeviceCutter/Run").ValueChanged += RunCutter_ValueChanged;

            //may xa
            easyDriverConnector1.GetTag("Local Station/ChannelXa/Device/Run").ValueChanged += RunMayXa_ValueChanged;


            LenhChuyenDonCutter_ValueChanged(easyDriverConnector1.GetTag("Local Station/ChannelServer/DeviceCutter/LenhChuyenDon"),
                              new TagValueChangedEventArgs(easyDriverConnector1.GetTag("Local Station/ChannelServer/DeviceCutter/LenhChuyenDon")
                              , "", easyDriverConnector1.GetTag("Local Station/ChannelServer/DeviceCutter/LenhChuyenDon").Value));

            RunCutter_ValueChanged(easyDriverConnector1.GetTag("Local Station/ChannelServer/DeviceCutter/Run"),
                              new TagValueChangedEventArgs(easyDriverConnector1.GetTag("Local Station/ChannelServer/DeviceCutter/Run")
                              , "", easyDriverConnector1.GetTag("Local Station/ChannelServer/DeviceCutter/Run").Value));

            RunMayXa_ValueChanged(easyDriverConnector1.GetTag("Local Station/ChannelXa/Device/Run"),
                              new TagValueChangedEventArgs(easyDriverConnector1.GetTag("Local Station/ChannelXa/Device/Run")
                              , "", easyDriverConnector1.GetTag("Local Station/ChannelXa/Device/Run").Value));

            if (easyDriverConnector1.ConnectionStatus == ConnectionStatus.Connected)
            {
                _labDriverStatus.BackColor = Color.Green;
            }
            else
            {
                _labDriverStatus.BackColor = Color.Red;
            }
        }

        private void RunMayXa_ValueChanged(object sender, TagValueChangedEventArgs e)
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

        /// <summary>
        /// Tag PLC báo là đổi đơn. PC sẽ ghi lên 1 sau khi truyền đơn mới xuống, PLC đọc 1 thì đổi đơn rồi set về 0.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DoiDonCutter_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "0")
            {
                var para = new DynamicParameters();
                para.Add("_donHangId", _donHangDangChay.DonHangId);
                para.Add("_createdDateDonHang", _donHangDangChay.CreatedDateDonHang);
                para.Add("_ma", _donHangDangChay.Ma);
                para.Add("_song", _donHangDangChay.Song);
                para.Add("_kho", _donHangDangChay.Kho);
                para.Add("_chieuDai", _donHangDangChay.ChieuDai);
                para.Add("_soLuong", _donHangDangChay.SoLuong);
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

                //sau khi nhận tín hiệu PLC báo về đã đổi đơn thành công thì update status đơn hàng thành Processing
                using (var connection = GlobalVariable.GetDbConnection())
                {
                    connection.Execute($"UPDATE tbldonhang set Status = 1 WHERE Id = {_donHangDoiDon.Id}");
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
                    var donhang = new DonHangModel()
                    {
                        DonHangId = _donHangDangChay.DonHangId,
                        STT = _donHangDangChay.STT,
                        Ma = _donHangDangChay.Ma,
                        Song = _donHangDangChay.Song,
                        Kho = _donHangDangChay.Kho,
                        ChieuDai = _donHangDangChay.ChieuDai,
                        SoLuong = _donHangDangChay.SoLuong,
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
                    _donHangHoanThanh = new DonHangModel();
                    _donHangHoanThanh = donhang;
                }

                using (var connection = GlobalVariable.GetDbConnection())
                {
                    var resultData = connection.Query<tblDonHangModel>("sp_tblDonHangGetOnProcess").ToList();

                    if (resultData.Count > 0)
                    {
                        var c = resultData.Where(u => u.Status == 0).ToList();
                        _donHangDoiDon = c.FirstOrDefault(x => x.STT == c.Min(u => u.STT));

                        //add thong tin don hang moi vao donHangDangChay
                        _donHangDangChay = null;
                        _donHangDangChay = new DonHangModel()
                        {
                            DonHangId = _donHangDoiDon.Id,
                            STT = _donHangDoiDon.STT,
                            Ma = _donHangDoiDon.Ma,
                            Song = _donHangDoiDon.Song,
                            Kho = _donHangDoiDon.Kho,
                            ChieuDai = _donHangDoiDon.ChieuDai,
                            SoLuong = _donHangDoiDon.SoLuong,
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
                            KhachHang = _donHangDoiDon.KhachHang,
                            DonHang = _donHangDoiDon.DonHang,
                            PO = _donHangDoiDon.PO,
                            Line = _donHangDoiDon.Line,
                            GhiChu = _donHangDoiDon.GhiChu
                        };

                        DoiDonCutter(_donHangDoiDon);
                    }
                }

                easyDriverConnector1.GetTag("Local Station/ChannelServer/DeviceCutter/LenhChuyenDon").WriteAsync("0", WritePiority.High);
            }
        }
        #endregion

        #region Methods
        private void DoiDonCutter(tblDonHangModel donHangDangChay)
        {
            easyDriverConnector1.GetTag("Local Station/ChannelServer/DeviceCutter/ChieuDaiCat1").WriteAsync(donHangDangChay.ChieuDai.ToString(), WritePiority.High);
            easyDriverConnector1.GetTag("Local Station/ChannelServer/DeviceCutter/SoLuongCat1").WriteAsync(donHangDangChay.SoLuong.ToString(), WritePiority.High);
            easyDriverConnector1.GetTag("Local Station/ChannelServer/DeviceCutter/Pallet1").WriteAsync(donHangDangChay.SoLuong.ToString(), WritePiority.High);
            easyDriverConnector1.GetTag("Local Station/ChannelServer/DeviceCutter/STT1").WriteAsync(donHangDangChay.SoLuong.ToString(), WritePiority.High);

            this.Invoke((MethodInvoker)delegate
            {
                _labDonHang.Text = _donHangDangChay.DonHang;
                _labKhachHang.Text = _donHangDangChay.KhachHang;
                _labPo.Text = _donHangDangChay.PO;
                _labSlConLai.Text = _donHangDangChay.SLConLai.ToString();
            });

            //baos cho PLC biet là đã truyền thông số đơn mới xuống xong.
            easyDriverConnector1.GetTag("Local Station/ChannelServer/DeviceCutter/DoiDon").WriteAsync("1", WritePiority.High);
        }
        #endregion
    }
}
