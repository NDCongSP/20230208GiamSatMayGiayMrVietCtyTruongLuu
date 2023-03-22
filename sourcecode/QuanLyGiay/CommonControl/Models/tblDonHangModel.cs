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
        public int Id { get; set; }
        public StatusDHEnum Status { get; set; } = StatusDHEnum.NewOrder;
        public int STT { get; set; }
        [DisplayName("Mã")]
        public string Ma { get; set; }
        [DisplayName("Sóng")]
        public string Song { get; set; }
        [DisplayName("Khổ")]
        public int Kho { get; set; }
        [DisplayName("Dài")]
        public double ChieuDai { get; set; }
        [DisplayName("SL")]
        public int SoLuong { get; set; }
        public int Pallet { get; set; }
        [DisplayName("Xả")]
        public int Xa { get; set; }
        [DisplayName("Rộng")]
        public int Rong { get; set; }
        [DisplayName("Cánh")]
        public int Canh { get; set; }
        [DisplayName("Cao")]
        public int Cao { get; set; }
        [DisplayName("Lằng")]
        public int Lang { get; set; }
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

        [Browsable(false)]
        public string KhachHang { get; set; }
        [Browsable(false)]
        public int DaiKH { get; set; } = 0;
        [Browsable(false)]
        public int RongKH { get; set; } = 0;
        [Browsable(false)]
        public int CaoKH { get; set; } = 0;
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
