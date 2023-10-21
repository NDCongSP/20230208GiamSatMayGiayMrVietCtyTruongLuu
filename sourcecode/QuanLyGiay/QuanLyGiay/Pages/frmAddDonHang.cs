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

namespace QuanLyGiay
{
    public partial class frmAddDonHang : Form
    {
        private List<DonHangModel> _listDonHang = new List<DonHangModel>();
        private List<DonHangModel> _listDonHangNew = new List<DonHangModel>();
        private DonHangModel _donHang = new DonHangModel();

        public frmAddDonHang()
        {
            InitializeComponent();

            Load += FrmAddDonHang_Load;
        }

        private void FrmAddDonHang_Load(object sender, EventArgs e)
        {
            _txtCanh.ReadOnly = true;

            #region Đăng ký sự kiện
            _btnThem.Click += _btnThem_Click;
            _btnLuu.Click += _btnLuu_Click;
            _btnXoa.Click += _btnXoa_Click;
            _btnSua.Click += _btnSua_Click;
            _grvDH.Click += _grvDH_Click;
            _grvDH.CellClick += _grvDH_CellClick;


            _txtSTT.TextChanged += (s, o) =>
            {
                TextBox t = (TextBox)s;
                if (!string.IsNullOrEmpty(t.Text))
                {
                    _donHang.STT = int.TryParse(t.Text, out int value) ? value : 0;
                }
                else
                {
                    MessageBox.Show($"Không được để trống STT.", $"CẢNH BÁO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            };

            _txtKho.TextChanged += (s, o) =>
            {
                TextBox t = (TextBox)s;
                if (!string.IsNullOrEmpty(t.Text))
                {
                    _donHang.Kho = double.TryParse(t.Text, out double value) ? value : 0;
                }
                else
                {
                    MessageBox.Show($"Không được để trống 'Khổ'.", $"CẢNH BÁO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            };
            _txtChieuDai.TextChanged += (s, o) =>
            {
                TextBox t = (TextBox)s;
                if (!string.IsNullOrEmpty(t.Text))
                {
                    _donHang.DaiCat = double.TryParse(t.Text, out double value) ? value : 0;
                }
                else
                {
                    MessageBox.Show($"Không được để trống 'Chiều Dài'.", $"CẢNH BÁO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            };
            _txtSoLuong.TextChanged += (s, o) =>
            {
                TextBox t = (TextBox)s;
                if (!string.IsNullOrEmpty(t.Text))
                {
                    _donHang.SLCatTam = int.TryParse(t.Text, out int value) ? value : 0;
                }
                else
                {
                    MessageBox.Show($"Không được để trống 'Số Lượng'.", $"CẢNH BÁO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            };
            _txtPallet.TextChanged += (s, o) =>
            {
                TextBox t = (TextBox)s;
                if (!string.IsNullOrEmpty(t.Text))
                {
                    _donHang.Pallet = int.TryParse(t.Text, out int value) ? value : 0;
                }
                else
                {
                    MessageBox.Show($"Không được để trống 'Pallet'.", $"CẢNH BÁO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            };
            _txtXa.TextChanged += (s, o) =>
            {
                TextBox t = (TextBox)s;
                if (!string.IsNullOrEmpty(t.Text))
                {
                    _donHang.Xa = int.TryParse(t.Text, out int value) ? value : 0;
                }
                else
                {
                    MessageBox.Show($"Không được để trống 'Xả'.", $"CẢNH BÁO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            };
            _txtRong.TextChanged += (s, o) =>
            {
                TextBox t = (TextBox)s;
                if (!string.IsNullOrEmpty(t.Text))
                {
                    _donHang.Rong = double.TryParse(t.Text, out double value) ? value : 0;
                    _donHang.Canh = Math.Round((double)_donHang.Rong / 2, 2);

                    if (this.InvokeRequired)
                    {
                        this.Invoke(new Action(()=> {
                            _txtCanh.Text = _donHang.Canh.ToString();
                        }));
                    }
                    else
                    {
                        _txtCanh.Text = _donHang.Canh.ToString();
                    }
                }
                else
                {
                    MessageBox.Show($"Không được để trống 'Rộng'.", $"CẢNH BÁO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            };
            _txtDoSauLang.TextChanged += (s, o) =>
            {
                TextBox t = (TextBox)s;
                if (!string.IsNullOrEmpty(t.Text))
                {
                    _donHang.DoSauLan = double.TryParse(t.Text, out double value) ? value : 0;
                }
                else
                {
                    MessageBox.Show($"Không được để trống 'Cánh'.", $"CẢNH BÁO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            };
            _txtCao.TextChanged += (s, o) =>
            {
                TextBox t = (TextBox)s;
                if (!string.IsNullOrEmpty(t.Text))
                {
                    _donHang.Cao = double.TryParse(t.Text, out double value) ? value : 0;
                }
                else
                {
                    MessageBox.Show($"Không được để trống 'Cao'.", $"CẢNH BÁO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            };
            _txtLang.TextChanged += (s, o) =>
            {
                TextBox t = (TextBox)s;
                if (!string.IsNullOrEmpty(t.Text))
                {
                    _donHang.Lang = int.TryParse(t.Text, out int value) ? value : 0;
                }
                else
                {
                    MessageBox.Show($"Không được để trống 'Lằng'.", $"CẢNH BÁO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            };
            _txtGhiChu.TextChanged += (s, o) =>
            {
                TextBox t = (TextBox)s;
                //if (!string.IsNullOrEmpty(t.Text))
                {
                    _donHang.GhiChu = t.Text;
                }
            };
            _txtKhachHang.TextChanged += (s, o) =>
            {
                TextBox t = (TextBox)s;
                //if (!string.IsNullOrEmpty(t.Text))
                {
                    _donHang.KhachHang = t.Text;
                }
            };
            _txtDonHang.TextChanged += (s, o) =>
            {
                TextBox t = (TextBox)s;
                //if (!string.IsNullOrEmpty(t.Text))
                {
                    _donHang.DonHang = t.Text;
                }
            };
            _txtPO.TextChanged += (s, o) =>
            {
                TextBox t = (TextBox)s;
                //if (!string.IsNullOrEmpty(t.Text))
                {
                    _donHang.PO = t.Text;
                }
            };

            _cbMa.SelectedValueChanged += (s, o) =>
            {
                ComboBox c = (ComboBox)s;
                _donHang.Ma = c.Text;
            };
            _cbSong.SelectedValueChanged += (s, o) =>
            {
                ComboBox c = (ComboBox)s;
                _donHang.Song = c.Text;
            };
            _cbSoLop.SelectedValueChanged += (s, o) =>
            {
                ComboBox c = (ComboBox)s;
                _donHang.SoLop = c.Text;
            };
            _cbGiayMen.SelectedValueChanged += (s, o) =>
            {
                ComboBox c = (ComboBox)s;
                _donHang.GiayMen = c.Text;
            };
            _cbGiaySongE.SelectedValueChanged += (s, o) =>
            {
                ComboBox c = (ComboBox)s;
                _donHang.GiaySongE = c.Text;
            };
            _cbGiayMatE.SelectedValueChanged += (s, o) =>
            {
                ComboBox c = (ComboBox)s;
                _donHang.GiayMatE = c.Text;
            };
            _cbGiaySongB.SelectedValueChanged += (s, o) =>
            {
                ComboBox c = (ComboBox)s;
                _donHang.GiaySongB = c.Text;
            };
            _cbGiayMatB.SelectedValueChanged += (s, o) =>
            {
                ComboBox c = (ComboBox)s;
                _donHang.GiayMatB = c.Text;
            };
            _cbGiaySongC.SelectedValueChanged += (s, o) =>
            {
                ComboBox c = (ComboBox)s;
                _donHang.GiaySongC = c.Text;
            };
            _cbGiayMatC.SelectedValueChanged += (s, o) =>
            {
                ComboBox c = (ComboBox)s;
                _donHang.GiayMatC = c.Text;
            };
            #endregion

            using (var connection = GlobalVariable.GetDbConnection())
            {
                var ma = connection.Query<MaModel>("Select * from tblmasettings").ToList();

                foreach (var item in ma)
                {
                    _cbMa.Items.Add(item.Name);
                }

                var song = connection.Query<SongModel>("Select * from tblsongsettings").ToList();

                foreach (var item in song)
                {
                    _cbSong.Items.Add(item.Name);
                }

                var soLop = connection.Query<SoLopModel>("Select * from tblSoLop").ToList();

                foreach (var item in soLop)
                {
                    _cbSoLop.Items.Add(item.TenLop);
                }

                var giayMen = connection.Query<GiayMenModel>("Select * from tblgiaymensettings").ToList();

                foreach (var item in giayMen)
                {
                    _cbGiayMen.Items.Add(item.Name);
                }

                var giaySong = connection.Query<GiaySongModel>("Select * from tblsongesettings").ToList();
                foreach (var item in giaySong)
                {
                    _cbGiaySongE.Items.Add(item.GiaySong);
                    _cbGiayMatE.Items.Add(item.GiayMat);
                }
                giaySong = connection.Query<GiaySongModel>("Select * from tblsongbsettings").ToList();
                foreach (var item in giaySong)
                {
                    _cbGiaySongB.Items.Add(item.GiaySong);
                    _cbGiayMatB.Items.Add(item.GiayMat);
                }
                giaySong = connection.Query<GiaySongModel>("Select * from tblsongcsettings").ToList();
                foreach (var item in giaySong)
                {
                    _cbGiaySongC.Items.Add(item.GiaySong);
                    _cbGiayMatC.Items.Add(item.GiayMat);
                }

                _cbMa.SelectedIndex = 0;
                _cbSong.SelectedIndex = 0;
                _cbSoLop.SelectedIndex = 0;
                _cbGiayMen.SelectedIndex = 0;
                _cbGiaySongE.SelectedIndex = 0;
                _cbGiayMatE.SelectedIndex = 0;
                _cbGiaySongB.SelectedIndex = 0;
                _cbGiayMatB.SelectedIndex = 0;
                _cbGiaySongC.SelectedIndex = 0;
                _cbGiayMatC.SelectedIndex = 0;

                //get don hang
                _listDonHang = connection.Query<DonHangModel>("sp_tblDonHangGetOnProcess").ToList();

                _grvDH.DataSource = _listDonHang;
                _grvDH.Columns["CreatedDate"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm:ss";

                //lay ra STT đơn hàng lớn nhất
                var maxSTT = _listDonHang.Max(x => x.STT);
                maxSTT += 1;
                _txtSTT.Text = maxSTT.ToString();
            }
        }

        private void _grvDH_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView sen = (DataGridView)sender;

            DataGridViewRow s = sen.Rows[e.RowIndex];

            this.Invoke((MethodInvoker)delegate
            {
                //_txtSTT.Text=
            });
        }

        private void _grvDH_Click(object sender, EventArgs e)
        {

        }

        private void _btnSua_Click(object sender, EventArgs e)
        {

        }

        private void _btnXoa_Click(object sender, EventArgs e)
        {

        }

        private void _btnLuu_Click(object sender, EventArgs e)
        {
            var dialogResult = MessageBox.Show($"Bạn chắc chắn muốn lưu danh sách đơn hàng mới tạo chứ?", "THÔNG BÁO", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                using (var connection = GlobalVariable.GetDbConnection())
                {
                    var result = connection.Execute($"INSERT INTO tbldonhang (Stt,Status,Ma,Song,SoLop,CongThem,Kho,DaiCat,SLCatTam,SoMetCaiDat,Xa,Rong,Canh,Cao,Lang,DoSauLan,GiaySongE,GiayMatE," +
                         $"GiaySongB,GiayMatB,GiaySongC,GiayMatC,GiayMen,GhiChu,MayXa,Line,KhachHang,DaiKH,RongKH,CaoKH,DonHang,PO,MayIn,ChapBe,GhimDan) " +
                         $"VALUES (@Stt,@Status,@Ma,@Song,@SoLop,@CongThem,@Kho,@DaiCat,@SLCatTam,@SoMetCaiDat,@Xa,@Rong,@Canh,@Cao,@Lang,@DoSauLan,@GiaySongE,@GiayMatE," +
                         $"@GiaySongB,@GiayMatB,@GiaySongC,@GiayMatC,@GiayMen,@GhiChu,@MayXa,@Line,@KhachHang,@DaiKH,@RongKH,@CaoKH,@DonHang,@PO,@MayIn,@ChapBe,@GhimDan)", _listDonHangNew);

                    GlobalVariable.MyEvents.Refresh = true;
                }
            }
        }

        private void _btnThem_Click(object sender, EventArgs e)
        {
            if (_donHang.STT != 0)
            {
                var donHang = new DonHangModel()
                {
                    STT = _donHang.STT,
                    Ma = _donHang.Ma,
                    Song = _donHang.Song,
                    SoLop=_donHang.SoLop,
                    Kho = _donHang.Kho,
                    DaiCat = _donHang.DaiCat,
                    SLCatTam = _donHang.SLCatTam,
                    Pallet = _donHang.Pallet,
                    Xa = _donHang.Xa,
                    Rong = _donHang.Rong,
                    Canh = _donHang.Canh,
                    Cao = _donHang.Cao,
                    Lang = _donHang.Lang,
                    GiayMen = _donHang.GiayMen,
                    GiaySongE = _donHang.GiaySongE,
                    GiayMatE = _donHang.GiayMatE,
                    GiaySongB = _donHang.GiaySongB,
                    GiayMatB = _donHang.GiayMatB,
                    GiaySongC = _donHang.GiaySongC,
                    GiayMatC = _donHang.GiayMatC,
                    KhachHang = _donHang.KhachHang,
                    DonHang = _donHang.DonHang,
                    PO = _donHang.PO,
                    DoSauLan = _donHang.DoSauLan
                };

                _listDonHangNew.Add(donHang);

                _listDonHang.Add(donHang);

                this.Invoke((MethodInvoker)delegate
                {
                    _grvDH.DataSource = null;
                    _grvDH.DataSource = _listDonHang;
                    _grvDH.Columns["CreatedDate"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm:ss";
                });

                ResetControl();
            }
            else
            {
                MessageBox.Show($"CẢNH BÁO", $"Nhập thiếu thông tin đơn hàng, kiểm tra lại.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        #region Methods
        void ResetControl()
        {
            this.Invoke((MethodInvoker)delegate
            {
                _txtSTT.Text = _txtChieuDai.Text = _txtSoLuong.Text = _txtPallet.Text = _txtXa.Text = _txtRong.Text = _txtCanh.Text = _txtCao.Text = _txtLang.Text = "0";
                _txtKho.Text = _txtKhachHang.Text = _txtDonHang.Text = _txtPO.Text = string.Empty;

                _cbMa.SelectedIndex = _cbSong.SelectedIndex = _cbSoLop.SelectedIndex = _cbGiayMen.SelectedIndex = _cbGiaySongE.SelectedIndex = _cbGiaySongB.SelectedIndex = _cbGiaySongC.SelectedIndex
                = _cbGiayMatE.SelectedIndex = _cbGiayMatB.SelectedIndex = _cbGiayMatC.SelectedIndex = 0;
            });
        }
        #endregion
    }
}
