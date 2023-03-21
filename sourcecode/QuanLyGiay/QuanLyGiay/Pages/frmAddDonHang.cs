﻿using CommonControl;
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
        private List<tblDonHangModel> _listDonHang = new List<tblDonHangModel>();
        private List<tblDonHangModel> _listDonHangNew = new List<tblDonHangModel>();
        private tblDonHangModel _donHang = new tblDonHangModel();

        public frmAddDonHang()
        {
            InitializeComponent();

            Load += FrmAddDonHang_Load;
        }

        private void FrmAddDonHang_Load(object sender, EventArgs e)
        {
            #region Đăng ký sự kiện
            _btnThem.Click += _btnThem_Click;
            _btnLuu.Click += _btnLuu_Click;
            _btnXoa.Click += _btnXoa_Click;

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
                    _donHang.Kho = t.Text;
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
                    _donHang.ChieuDai = t.Text;
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
                    _donHang.SoLuong = t.Text;
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
                    _donHang.Pallet = t.Text;
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
                    _donHang.Xa = t.Text;
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
                    _donHang.Rong = t.Text;
                }
                else
                {
                    MessageBox.Show($"Không được để trống 'Rộng'.", $"CẢNH BÁO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            };
            _txtCanh.TextChanged += (s, o) =>
            {
                TextBox t = (TextBox)s;
                if (!string.IsNullOrEmpty(t.Text))
                {
                    _donHang.Canh = t.Text;
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
                    _donHang.Cao = t.Text;
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
                    _donHang.Lang = t.Text;
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
                _cbGiayMen.SelectedIndex = 0;
                _cbGiaySongE.SelectedIndex = 0;
                _cbGiayMatE.SelectedIndex = 0;
                _cbGiaySongB.SelectedIndex = 0;
                _cbGiayMatB.SelectedIndex = 0;
                _cbGiaySongC.SelectedIndex = 0;
                _cbGiayMatC.SelectedIndex = 0;

                //get don hang
                _listDonHang = connection.Query<tblDonHangModel>("sp_tblDonHangGetAll").ToList();

                grvDH.DataSource = _listDonHang;
                grvDH.Columns["CreatedDate"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm:ss";

                //lay ra STT đơn hàng lớn nhất
                var maxSTT = _listDonHang.Max(x => x.STT);
                maxSTT += 1;
                _txtSTT.Text = maxSTT.ToString();
            }


        }

        private void _btnXoa_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void _btnLuu_Click(object sender, EventArgs e)
        {
            var dialogResult = MessageBox.Show($"Bạn chắc chắn muốn lưu danh sách đơn hàng mới tạo chứ?", "THÔNG BÁO", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                using (var connection = GlobalVariable.GetDbConnection())
                {
                    var result = connection.Execute($"INSERT INTO tbldonhang (Stt,Status,Ma,Song,Kho,ChieuDai,SoLuong,Xa,Rong,Canh,Cao,Lang,GiaySongE,GiayMatE," +
                         $"GiaySongB,GiayMatB,GiaySongC,GiayMatC,GiayMen,GhiChu,MayXa,Line,KhachHang,DaiKH,RongKH,CaoKH,DonHang,PO,MayIn,ChapBe,GhimDan) " +
                         $"VALUES (@Stt,@Status,@Ma,@Song,@Kho,@ChieuDai,@SoLuong,@Xa,@Rong,@Canh,@Cao,@Lang,@GiaySongE,@GiayMatE," +
                         $"@GiaySongB,@GiayMatB,@GiaySongC,@GiayMatC,@GiayMen,@GhiChu,@MayXa,@Line,@KhachHang,@DaiKH,@RongKH,@CaoKH,@DonHang,@PO,@MayIn,@ChapBe,@GhimDan)", _listDonHangNew);
                }
            }
        }

        private void _btnThem_Click(object sender, EventArgs e)
        {
            if (_donHang.STT != 0)
            {
                var donHang = new tblDonHangModel()
                {
                    STT = _donHang.STT,
                    Ma = _donHang.Ma,
                    Song = _donHang.Song,
                    Kho = _donHang.Kho,
                    ChieuDai = _donHang.ChieuDai,
                    SoLuong = _donHang.SoLuong,
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
                    PO = _donHang.PO
                };

                _listDonHangNew.Add(donHang);

                _listDonHang.Add(donHang);

                this.Invoke((MethodInvoker)delegate
                {
                    grvDH.DataSource = null;
                    grvDH.DataSource = _listDonHang;
                    grvDH.Columns["CreatedDate"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm:ss";
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

                _cbMa.SelectedIndex = _cbSong.SelectedIndex = _cbGiayMen.SelectedIndex = _cbGiaySongE.SelectedIndex = _cbGiaySongB.SelectedIndex = _cbGiaySongC.SelectedIndex
                = _cbGiayMatE.SelectedIndex = _cbGiayMatB.SelectedIndex = _cbGiayMatC.SelectedIndex = 0;
            });
        }
        #endregion
    }
}
