using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonControl
{
    /// <summary>
    /// Đơn vị là mm.
    /// </summary>
    public class DaoLangPositionModel
    {
        //Bật tắt dao
        public int Dao1U { get; set; } = 0;
        public int Dao2U { get; set; } = 0;
        public int Dao3U { get; set; } = 0;
        public int Dao4U { get; set; } = 0;
        public int Dao5U { get; set; } = 0;
        public int Lang1U { get; set; } = 0;
        public int Lang2U { get; set; } = 0;
        public int Lang3U { get; set; } = 0;
        public int Lang4U { get; set; } = 0;
        public int Lang5U { get; set; } = 0;
        public int Lang6U { get; set; } = 0;
        public int Lang7U { get; set; } = 0;
        public int Lang8U { get; set; } = 0;

        //vị trí cài đặt
        public int Dao1SV { get; set; } = 0;//mm
        public int Dao2SV { get; set; } = 0;
        public int Dao3SV { get; set; } = 0;
        public int Dao4SV { get; set; } = 0;
        public int Dao5SV { get; set; } = 0;

        public int Lang1Sv { get; set; } = 0;
        public int Lang2Sv { get; set; } = 0;
        public int Lang3Sv { get; set; } = 0;
        public int Lang4Sv { get; set; } = 0;
        public int Lang5Sv { get; set; } = 0;
        public int Lang6Sv { get; set; } = 0;
        public int Lang7Sv { get; set; } = 0;
        public int Lang8Sv { get; set; } = 0;

        public int HutSv { get; set; } = 0;

        //vị trí hiện tại
        public int Dao1PV { get; set; } = 0;
        public int Dao2PV { get; set; } = 0;
        public int Dao3PV { get; set; } = 0;
        public int Dao4PV { get; set; } = 0;
        public int Dao5PV { get; set; } = 0;

        public int Lang1Pv { get; set; } = 0;
        public int Lang2Pv { get; set; } = 0;
        public int Lang3Pv { get; set; } = 0;
        public int Lang4Pv { get; set; } = 0;
        public int Lang5Pv { get; set; } = 0;
        public int Lang6Pv { get; set; } = 0;
        public int Lang7Pv { get; set; } = 0;
        public int Lang8Pv { get; set; } = 0;

        public int HutPv { get; set; } = 0;

        //các giá trị cài đặt
        public int Dao_Dao { get; set; } = 0;//khoảng cách tối thiểu để 2 dao không đụng nhau
        public int Dao1Max { get; set; } = 0;//vị trí xa nhất dao 1 có thể di chuyển. Dao 5 đối xứng
        public int Dao1Min { get; set; } = 0;//vị trí xa nhất dao 1 có thể di chuyển. Dao 5 đối xứng
        public int Dao2Max { get; set; } = 0;//vị trí xa nhất dao 2 có thể di chuyển. Dao 4 đối xứng
        public int Dao2Min { get; set; } = 0;//vị trí xa nhất dao 2 có thể di chuyển. Dao 4 đối xứng
        public int Dao3Max { get; set; } = 0;//vị trí xa nhất dao 2 có thể di chuyển. Dao 4 đối xứng      
        public int Dao4Max { get; set; } = 0;//vị trí xa nhất dao 2 có thể di chuyển. Dao 4 đối xứng
        public int Dao4Min { get; set; } = 0;//vị trí xa nhất dao 2 có thể di chuyển. Dao 4 đối xứng
        public int Dao5Max { get; set; } = 0;//vị trí xa nhất dao 2 có thể di chuyển. Dao 4 đối xứng
        public int Dao5Min { get; set; } = 0;//vị trí xa nhất dao 2 có thể di chuyển. Dao 4 đối xứng

        public int Lang1Max { get; set; }//vị trí xa nhất lằng có thể di chuyển. đối xứng lằng 8
        public int Lang1Min { get; set; }//vị trí xa nhất lằng có thể di chuyển. đối xứng lằng 8
        public int Lang2Max { get; set; }//đối xứng lằng 7
        public int Lang2Min { get; set; }//vị trí xa nhất lằng có thể di chuyển. đối xứng lằng 8
        public int Lang3Max { get; set; }//đối xứng lằng 6
        public int Lang3Min { get; set; }//vị trí xa nhất lằng có thể di chuyển. đối xứng lằng 8
        public int Lang4Max { get; set; }//đối xứng lằng 5
        public int Lang4Min { get; set; }//vị trí xa nhất lằng có thể di chuyển. đối xứng lằng 8
        public int Lang_Lang { get; set; } = 0;//Khoảng cách tối thiểu để 2 lằng không đụng nhau
        public int KieuLang { get; set; }
        public int Xa { get; set; }//so lượng tấm cho 1 lan xa
        //public int Song { get; set; }// chính là phần cộng thêm theo kiểu sóng và số lớp
        public int KhoMay { get; set; }//chiều rộng của máy xả
        public int MinSize //chiều rộng tối thiểu mà máy xả có thể cắt được
        {
            set { }
            get { return KhoMay / 5; }
        }

        //cac vi tri cua dao lang khi khong su dung, set tren phan mem
        public int Dao1OffSV { get; set; }
        public int Dao2OffSV { get; set; }
        public int Lang1OffSV { get; set; }
        public int Lang2OffSV { get; set; }
        public int Lang3OffSV { get; set; }
        public int Lang4OffSV { get; set; }
    }
}
