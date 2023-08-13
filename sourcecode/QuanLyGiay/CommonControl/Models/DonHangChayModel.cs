using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonControl
{
    public class DonHangChayModel
    {
        [Browsable(false)]
        public int Id { get; set; }
        public DonHangChayModel()
        {
            Chay = TimeSpan.FromMilliseconds(0);
            Dung = TimeSpan.FromMilliseconds(0);
        }
        public string KhachHang { get; set; }
        public string DonHang { get; set; }
        public string PO { get; set; }

        public int IdDonHang { get; set; }
        public StatusDHEnum Status { get; set; } = StatusDHEnum.Processing;
        public int STT { get; set; }
        [DisplayName("Mã")]
        public string Ma { get; set; }
        [DisplayName("Sóng")]
        public string Song { get; set; }
        public string SoLop { get; set; }//quy định số lớp giấy
        public double CongThem { get; set; }//tuy vao số lớp mà cọng thêm số tương ứng.

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

        public int Pallet { get; set; }

        #region phần cài đặt cho máy xẩ splitter
        //chiều rộng của khổ xả = Rộng/2 + Cao + Rong/2
        [DisplayName("Xả")]
        public int Xa { get; set; }//quy định 1 lần bao nhiêu con --> 1 khổ xả ra 1 lần bao nhiêu tấm

        /// <summary>
        /// Rộng, cm.
        /// </summary>
        [DisplayName("Rộng")]
        public double Rong { get; set; }
        /// <summary>
        /// Rong/2.
        /// </summary>
        [DisplayName("Cánh")]
        public double Canh { get; set; }
        [DisplayName("Cao")]
        public double Cao { get; set; }
        [DisplayName("Lằng")]
        public int Lang { get; set; }//quy định kiểu lần có giá trị 1 đến 4
        public double DoSauLan { get; set; }
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

        public int SLDat { get; set; }
        public int SLLoi { get; set; }
        public int SLConLai
        {
            get { return SLCatTam - SLDat; }
        }
        public double SoMetDat
        {
            get { return (double)(DaiCat * SLDat) / 1000; }
        }
        public double SoMetDatSF { get; set; }

        public double SoMetLoi
        {
            get { return (double)(DaiCat * SLLoi) / 1000; }
        }

        public double ConLai
        {
            get { return SoMetCaiDat - SoMetDat; }
        }

        public string PhanTramLoi
        {
            get
            {
                try
                {
                    if (SoMetDat == 0.0 && SoMetLoi == 0.0)
                        return "0";
                    else
                    {
                        return (SoMetLoi * 100 / (SoMetLoi + SoMetDat)).ToString("f1");
                    }
                }
                catch { return ""; }
            }
        }
        public double TocDo { get; set; }
        public double TocDoTB
        {
            get
            {
                if (Chay.TotalSeconds == 0)
                    return 0;
                return (int)(SoMetDat / Chay.TotalSeconds * 60);
            }
        }
        public TimeSpan Chay { get; set; }
        public DateTime TGBatDau { get; set; }
        public DateTime TGKetThuc { get; set; }
        public TimeSpan Dung { get; set; }
        public int SoDung { get; set; }
        public double M2Dat
        {
            get { return Kho * SoMetDat / 1000; }
        }
        public double M2Loi
        {
            get { return Kho * SoMetLoi / 1000; }
        }
        public string TrangThaiTruocDo { get; set; }

        public int HoanTatCutter { get; set; }
        public int HoanTatSongE { get; set; }
        public int HoanTatGiaySongE { get; set; }
        public int HoanTatGiayMatE { get; set; }
        public int HoanTatSongB { get; set; }
        public int HoanTatGiaySongB { get; set; }
        public int HoanTatGiayMatB { get; set; }
        public int HoanTatSongC { get; set; }
        public int HoanTatGiaySongC { get; set; }
        public int HoanTatGiayMatC { get; set; }
        public int HoanTatSpliter { get; set; }
        public int HoanTatMayMen { get; set; }
        public int FirstRow { get; set; }

        public double Le { get => Kho - (Xa * Rong); }
        public double PhanTramLe { get => (Le / Kho) * 100.0f; }
        public DateTime CreatedDate { get; set; }
        public int IsActived { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
