using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonControl
{
    public class DonHangModel
    {
        [Browsable(false)]
        public int Id { get; set; }
        public StatusDHEnum Status { get; set; } = StatusDHEnum.MOI;
        public int STT { get; set; }

        [DisplayName("Xả")]
        public int Xa { get; set; }//quy định 1 lần bao nhiêu con --> 1 khổ xả ra 1 lần bao nhiêu tấm

        /// <summary>
        /// Nap1
        /// </summary>
        [DisplayName("Rộng")]
        public double Rong { get; set; }
        /// <summary>
        /// Rong/2.
        /// </summary>
        [DisplayName("Cánh")]
        public double Canh
        {
            set { }
            get
            {
                return Math.Round((double)Rong / 2, 2);
            }
        }
        [DisplayName("Cao")]
        public double Cao { get; set; }
        [DisplayName("Lằn")]
        public int Lang { get; set; }//quy định kiểu lần có giá trị 1 đến 4

        [DisplayName("Khổ")]
        public double Kho { get; set; }
        [DisplayName("Dài Cắt")]
        public double DaiCat { get; set; }//don vi cm
        [DisplayName("SL Cắt Tấm")]
        public int SLCatTam { get; set; }//dung cho máy cắt
        public double SoMetCaiDat//don vi met
        {
            set { }
            get
            {
                return Math.Round(SLCatTam * DaiCat / 100, 2);
            }
        }

        public int TongSoThung
        {
            set { }
            get
            {
                return SLCatTam * Xa;
            }
        }

        [DisplayName("Sóng")]
        public string Song { get; set; }
        public string SoLop { get; set; }//quy định số lớp giấy

        #region phần cài đặt cho máy xẩ splitter
        //chiều rộng của khổ xả = Rộng/2 + Cao + Rong/2

       
        [DisplayName("Mã")]
        public string Ma { get; set; }
        [DisplayName("Độ Sau Lằn")]
        public double DoSauLan { get; set; }
        public double CongThem //tuy vao số lớp mà cọng thêm số tương ứng.
        {
            get; set;
        }
        #endregion

        [DisplayName("Giấy Sóng E")]
        public string GiaySongE { get; set; }
        [DisplayName("Giấy Mặt E")]
        public string GiayMatE { get; set; }
        [DisplayName("Giấy Sóng B")]
        public string GiaySongB { get; set; }
        [DisplayName("Giấy Mặt B")]
        public string GiayMatB { get; set; }
        [DisplayName("GiấySóng C")]
        public string GiaySongC { get; set; }
        [DisplayName("Giấy Mặt C")]
        public string GiayMatC { get; set; }
        [DisplayName("Giấy Mền")]
        public string GiayMen { get; set; }
        [DisplayName("Ghi chú")]
        public string GhiChu { get; set; }

        [DisplayName("Máy xả")]
        public string MayXa { get; set; }
        public string Line { get; set; }
        [DisplayName("Ngày tạo")]
        public DateTime? CreatedDate { get; set; }

        public string KhachHang { get; set; }
        [Browsable(false)]
        public int DaiKH { get; set; } = 0;
        [Browsable(false)]
        public int RongKH { get; set; } = 0;
        [Browsable(false)]
        public int CaoKH { get; set; } = 0;

        public string DonHang { get; set; }
        public string PO { get; set; }

        [Browsable(false)]
        public string MayIn { get; set; }
        [Browsable(false)]
        public string ChapBe { get; set; }
        [Browsable(false)]
        public string GhimDan { get; set; }
        [Browsable(false)]
        public int? IsActived { get; set; }

        public int Pallet { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
