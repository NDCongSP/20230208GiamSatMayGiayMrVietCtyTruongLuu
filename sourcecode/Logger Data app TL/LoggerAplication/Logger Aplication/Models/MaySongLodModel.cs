using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logger_Aplication
{
    public class MaySongLodModel
    {
        [DisplayName("Thời Gian")]
        public DateTime CreatedDate { get; set; }
        [DisplayName("Sóng")]
        public string MaySong { get; set; }
        [DisplayName("Giấy Mặt")]
        public string GiayMat { get; set; }
        [DisplayName("Giấy Sóng")]
        public string GiaySong { get; set; }
        [DisplayName("Ghép Lớp")]
        public string GhepLop { get; set; }
        [DisplayName("Trên Dàn Còn Lại")]
        public string TrenDanConLai { get; set; }
        [DisplayName("Độ Dài Cài Đặt")]
        public string DoDaiCaiDat { get; set; }
        [DisplayName("Số Mét Còn Lại")]
        public string SoMetConLai { get; set; }
        [DisplayName("Thao Tác Thực Hiện")]
        public ThaoTacEnum ThaoTacThucHien { get; set; }
    }
}
