using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonControl
{
   public class tblDonHangModel
    {
        [Browsable(false)]
        public Guid Id { get; set; }
        public int? STT { get; set; }
        [DisplayName("Mã")]
        public string Ma { get; set; }
        [DisplayName("Sóng")]
        public string Song { get; set; }
        [DisplayName("Khổ")]
        public string Kho { get; set; }
        [DisplayName("Dài")]
        public string ChieuDai { get; set; }
        [DisplayName("SL")]
        public string SoLuong { get; set; }
        public string Pallet { get; set; }
        [DisplayName("Xả")]
        public string Xa { get; set; }
        [DisplayName("Nắp 1")]
        public string NapC { get; set; }
        public string Cao { get; set; }
        [DisplayName("Nắp 2")]
        public string NapL { get; set; }
        [DisplayName("Lằng")]
        public string Lang { get; set; }
        [DisplayName("Mền")]
        public string GiayMen { get; set; }
        [DisplayName("Sóng 1")]
        public string GiaySong1 { get; set; }
        [DisplayName("Mặt 1")]
        public string GiayMat1 { get; set; }
        [DisplayName("Sóng 2")]
        public string GiaySong2 { get; set; }
        [DisplayName("Mặt 2")]
        public string GiayMat2 { get; set; }
        [DisplayName("Sóng 3")]
        public string GiaySong3 { get; set; }
        [DisplayName("Mặt 3")]
        public string GiayMat3 { get; set; }
        [DisplayName("Máy xả")]
        public string MayXa { get; set; }
        public string Line { get; set; }
        [DisplayName("Ghi chú")]
        public string GhiChu { get; set; }
        [DisplayName("Ngày tạo")]
        public DateTime? CreatedDate { get; set; }
        public StatusDHEnum Status { get; set; }

        [Browsable(false)]
        public string KhachHang { get; set; }
        [Browsable(false)]
        public string DaiKH { get; set; }
        [Browsable(false)]
        public string RongKH { get; set; }
        [Browsable(false)]
        public string CaoKH { get; set; }
        [Browsable(false)]
        public string DonHang { get; set; }
        [Browsable(false)]
        public string PO { get; set; }
        [Browsable(false)]
        public string MayIn { get; set; }
        [Browsable(false)]
        public string ChapBe { get; set; }
        [Browsable(false)]
        public string GhimDan { get; set; }
        [Browsable(false)]
        public int? IsActived { get; set; }
    }
}
