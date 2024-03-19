using CommonControl;
using Dapper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MaySong
{
    public partial class frmMainMaySong : Form
    {
        int _countRefesh = 0; //100*100=10,000 =10S
        private Timer _t = new Timer();
        //private Task _taskLoadOrder;
        List<DonHangModel> _resultData = new List<DonHangModel>();
        DonHangModel _donHangDoiDon = new DonHangModel();//chứa thông tin đơn hàng chuẩn bị đổi đơn.
        DonHangChayModel _donHangDangChay = new DonHangChayModel();//chứa thông tin đơn hàng đang chạy

        bool _firstLoad = true;

        public frmMainMaySong()
        {
            InitializeComponent();

            Load += FrmMainMaySong_Load;
        }

        private void FrmMainMaySong_Load(object sender, EventArgs e)
        {
            #region Hiển thị tên của máy sóng
            if (GlobalVariable.MaySOng == "1")
            {
                this.Text = GlobalVariable.TenMaySong1;
            }
            else if (GlobalVariable.MaySOng == "2")
            {
                this.Text = GlobalVariable.TenMaySong2;
            }
            else
            {
                this.Text = GlobalVariable.TenMaySong3;
            }
            #endregion

            _t.Interval = 100;
            _t.Enabled = true;
            _t.Tick += (s, o) =>
            {
                Timer t = (Timer)s;
                t.Enabled = false;

                GlobalVariable.InvokeIfRequired(this, () =>
                {
                    labDateTime.Text = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");

                    //_labKhachHang.Text = _donHangDangChay.KhachHang;
                    //_labDonHang.Text = _donHangDangChay.DonHang;
                    //_labPO.Text = _donHangDangChay.PO;
                    _labDaiCat.Text = (_donHangDangChay.DaiCat).ToString();
                    _labSLCat.Text = _donHangDangChay.SLCatTam.ToString();
                    _labTongSoMetCD.Text = _donHangDangChay.SoMetCaiDat.ToString();
                    _labSLDat.Text = _donHangDangChay.SLDat.ToString();
                    _labSLLoi.Text = _donHangDangChay.SLLoi.ToString();
                    _labSLConlai.Text = _donHangDangChay.SLConLai.ToString();
                    _labTocDo.Text = _donHangDangChay.TocDo.ToString();
                    _labSoMetCL.Text = _donHangDangChay.ConLai.ToString();

                    _labGiaySongHienTai.Text = GlobalVariable.MaySOng=="1"?_donHangDangChay.GiaySongE:GlobalVariable.MaySOng=="2"?_donHangDangChay.GiaySongB:_donHangDangChay.GiaySongC;
                    _labGiayMatHienTai.Text = GlobalVariable.MaySOng == "1" ? _donHangDangChay.GiayMatE : GlobalVariable.MaySOng == "2" ? _donHangDangChay.GiayMatB : _donHangDangChay.GiayMatC;

                    _labGiaySongKeTiep.Text = GlobalVariable.MaySOng == "1" ? _donHangDoiDon.GiaySongE : GlobalVariable.MaySOng == "2" ? _donHangDoiDon.GiaySongB : _donHangDoiDon.GiaySongC;
                    _labGiayMatKeTiep.Text = GlobalVariable.MaySOng == "1" ? _donHangDoiDon.GiayMatE : GlobalVariable.MaySOng == "2" ? _donHangDoiDon.GiayMatB : _donHangDoiDon.GiayMatC;
                });

                _countRefesh += 1;

                //đếm đúng 10s thì get Order 1 lần
                if (_countRefesh >= 100)
                {
                    GlobalVariable.MyEvents.Refresh = true;

                    _countRefesh = 0;
                }

                t.Enabled = true;
            };

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
    }
}
