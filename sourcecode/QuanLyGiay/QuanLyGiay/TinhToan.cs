using CommonControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyGiay
{
    public static class TinhToan
    {
        /// <summary>
        /// Hàm tính toán giá trị nạp xuống PLC. Đơn vị tính toán là mm.
        /// </summary>
        /// <param name="dao1_sv"></param>
        /// <param name="dao2_sv"></param>
        /// <param name="dao3_sv"></param>
        /// <param name="dao4_sv"></param>
        /// <param name="dao5_sv"></param>
        /// <param name="hut"></param>
        /// <param name="lang1_sv"></param>
        /// <param name="lang2_sv"></param>
        /// <param name="lang3_sv"></param>
        /// <param name="lang4_sv"></param>
        /// <param name="lang5_sv"></param>
        /// <param name="lang6_sv"></param>
        /// <param name="lang7_sv"></param>
        /// <param name="lang8_sv"></param>
        public static void TinhToanGiaTri(DonHangChayModel donHang)
        {
            GlobalVariable.DaoLangPosition.KieuLang = donHang.Lang;//chon kiểu lằng
            GlobalVariable.DaoLangPosition.Xa = donHang.Xa;//xả bao nhiêu tấm, cái này quy định dao
            //GlobalVariable.DaoLangPosition.Song = (int)(donHang.CongThem * 10);//xả bao nhiêu tấm, cái này quy định dao

            double rongTotal = 0;

            /*Cán sóng(lằn cán) : 
            7 lớp: (Rộng / 2 + 0.3) / cao / (Rộng / 2 + 0.3)
            5 lớp: (Rộng / 2 + 0.2) / cao / (Rộng / 2 + 0.2)
            3 lớp: (Rộng / 2 + 0.1) / cao / ((Rộng / 2 + 0.1)
            A3 - Nắp chồm + thùng gói sườn+thùng gói giữa  : (Rộng / cao / Rộng) - 3 loại này cán lằn như nhau/
                 hàng bế: cái này dựa theo thông số bên thiết kế khuôn bế cung cấp
            các số 0.3,0.2 là phần cộng thêm theo sóng là lớp giấy
            tất cả các thông sô khi cài đặt đơn hàng đơn vị là cm, chỉ ngoại trừ tổng số mét cắt đơn vị là met
            chuyeernr đổi về đơn vị mm.
            */
            double rong = donHang.Rong * 10;
            double cao = donHang.Cao * 10;
            double canh = rong / 2;

            double caoA = cao;
            double canhA = canh;
            double rongA = canhA + caoA + canhA + donHang.CongThem * 10;

            double caoB = cao;
            double canhB = canh;
            double rongB = canhB + caoB + canhB + donHang.CongThem * 10;

            double caoC = cao;
            double canhC = canh;
            double rongC = canhC + caoC + canhC + donHang.CongThem * 10;

            double caoD = cao;
            double canhD = canh;
            double rongD = canhD + caoD + canhD + donHang.CongThem * 10;

            //tắt hết tất cả các dao và lằng trước khi vào set position, khi set xong vị trí thì sẽ bật dao và lằng tương ứng.
            GlobalVariable.DaoLangPosition.Dao1U = 0;
            GlobalVariable.DaoLangPosition.Dao2U = 0;
            GlobalVariable.DaoLangPosition.Dao3U = 0;
            GlobalVariable.DaoLangPosition.Dao4U = 0;
            GlobalVariable.DaoLangPosition.Dao5U = 0;

            GlobalVariable.DaoLangPosition.Lang1U = 0;
            GlobalVariable.DaoLangPosition.Lang2U = 0;
            GlobalVariable.DaoLangPosition.Lang3U = 0;
            GlobalVariable.DaoLangPosition.Lang4U = 0;
            GlobalVariable.DaoLangPosition.Lang5U = 0;
            GlobalVariable.DaoLangPosition.Lang6U = 0;
            GlobalVariable.DaoLangPosition.Lang7U = 0;
            GlobalVariable.DaoLangPosition.Lang8U = 0;

            if (donHang.Xa == 1)
                rongTotal = rongA;
            if (donHang.Xa == 2)
                rongTotal = rongA + rongB;
            if (donHang.Xa == 3)
                rongTotal = rongA + rongB + rongC;
            if (donHang.Xa == 4)
                rongTotal = rongA + rongB + rongC + rongD;

            //int Rongchia2 = Rong / 2;
            //int Rongx3chia2 = (Rong * 3) / 2;
            //int Cutline1 = 0, Cutline2 = 0, Cutline3 = 0, Cutline4 = 0, Cutline5 = 0;
            //int[] Cutline = { Cutline1, Cutline2, Cutline3, Cutline4, Cutline5 };
            //int[] D_Max = { Dao1_Max, Dao2_Max, Dao3_Max, Dao4_Max, Dao5_Max };
            //int[] Dao = { dao1_sv, dao2_sv, dao3_sv, dao4_sv, dao5_sv };

            // Xả = 1
            if (donHang.Xa == 1)
            {
                double rongChia2 = rongTotal / 2 * 10;//đang là đơn vị mm, x10 để loại bỏ số lẻ

                #region Dao
                // Xả 1 - Dao 1 hoac 2
                if (GlobalVariable.DaoLangPosition.Dao2Max > GlobalVariable.DaoLangPosition.Dao2Min && GlobalVariable.DaoLangPosition.Dao1Max > GlobalVariable.DaoLangPosition.Dao1Min)
                {
                    if (GlobalVariable.DaoLangPosition.Dao2Max >= rongChia2)
                    {
                        GlobalVariable.DaoLangPosition.Dao2SV = (int)(rongChia2);
                        GlobalVariable.DaoLangPosition.Dao2U = 1;//bat dao

                        GlobalVariable.DaoLangPosition.Dao1SV = GlobalVariable.DaoLangPosition.Dao1Min + GlobalVariable.DaoLangPosition.Dao_Dao;//cho dao về vị trí trung tâm của nó
                    }
                    else
                    {
                        GlobalVariable.DaoLangPosition.Dao1SV = (int)(rongChia2);
                        GlobalVariable.DaoLangPosition.Dao1U = 1;

                        GlobalVariable.DaoLangPosition.Dao2SV = GlobalVariable.DaoLangPosition.Dao2Min + GlobalVariable.DaoLangPosition.Dao_Dao;//cho dao về vị trí trung tâm của nó
                    }
                }
                // Xả 1 - Dao 1
                else if (GlobalVariable.DaoLangPosition.Dao1Max > GlobalVariable.DaoLangPosition.Dao1Min && GlobalVariable.DaoLangPosition.Dao2Max == GlobalVariable.DaoLangPosition.Dao2Min)
                {
                    GlobalVariable.DaoLangPosition.Dao1SV = (int)(rongChia2);
                    GlobalVariable.DaoLangPosition.Dao1U = 1;

                    GlobalVariable.DaoLangPosition.Dao2SV = GlobalVariable.DaoLangPosition.Dao2Min + GlobalVariable.DaoLangPosition.Dao_Dao;//cho dao về vị trí trung tâm của nó
                }
                // Xả 1 - Dao 2
                else if (GlobalVariable.DaoLangPosition.Dao2Max > GlobalVariable.DaoLangPosition.Dao2Min && GlobalVariable.DaoLangPosition.Dao1Max == GlobalVariable.DaoLangPosition.Dao1Min)
                {
                    GlobalVariable.DaoLangPosition.Dao1SV = (int)(rongChia2);
                    GlobalVariable.DaoLangPosition.Dao1U = 1;

                    GlobalVariable.DaoLangPosition.Dao2SV = GlobalVariable.DaoLangPosition.Dao2Min + GlobalVariable.DaoLangPosition.Dao_Dao;//cho dao về vị trí trung tâm của nó
                }

                // Xả 1 - Dao 4 hoac 5
                if (GlobalVariable.DaoLangPosition.Dao5Max > GlobalVariable.DaoLangPosition.Dao5Min && GlobalVariable.DaoLangPosition.Dao4Max > GlobalVariable.DaoLangPosition.Dao4Min)
                {
                    if (GlobalVariable.DaoLangPosition.Dao4Max >= rongChia2)
                    {
                        GlobalVariable.DaoLangPosition.Dao4SV = (int)(rongChia2);
                        GlobalVariable.DaoLangPosition.Dao4U = 1;//bat dao

                        GlobalVariable.DaoLangPosition.Dao5SV = GlobalVariable.DaoLangPosition.Dao5Min + GlobalVariable.DaoLangPosition.Dao_Dao;//cho dao về vị trí trung tâm của nó
                    }
                    else
                    {
                        GlobalVariable.DaoLangPosition.Dao5SV = (int)(rongChia2);
                        GlobalVariable.DaoLangPosition.Dao5U = 1;

                        GlobalVariable.DaoLangPosition.Dao4SV = GlobalVariable.DaoLangPosition.Dao4Min + GlobalVariable.DaoLangPosition.Dao_Dao;//cho dao về vị trí trung tâm của nó
                    }
                }
                // Xả 1 - Dao 5
                else if (GlobalVariable.DaoLangPosition.Dao5Max > GlobalVariable.DaoLangPosition.Dao5Min && GlobalVariable.DaoLangPosition.Dao4Max == GlobalVariable.DaoLangPosition.Dao4Min)
                {
                    GlobalVariable.DaoLangPosition.Dao5SV = (int)(rongChia2);
                    GlobalVariable.DaoLangPosition.Dao5U = 1;

                    GlobalVariable.DaoLangPosition.Dao4SV = GlobalVariable.DaoLangPosition.Dao4Min + GlobalVariable.DaoLangPosition.Dao_Dao;//cho dao về vị trí trung tâm của nó
                }
                // Xả 1 - Dao 4
                else if (GlobalVariable.DaoLangPosition.Dao4Max > GlobalVariable.DaoLangPosition.Dao4Min && GlobalVariable.DaoLangPosition.Dao5Max == GlobalVariable.DaoLangPosition.Dao5Min)
                {
                    GlobalVariable.DaoLangPosition.Dao4SV = (int)(rongChia2);
                    GlobalVariable.DaoLangPosition.Dao4U = 1;

                    GlobalVariable.DaoLangPosition.Dao5SV = GlobalVariable.DaoLangPosition.Dao5Min + GlobalVariable.DaoLangPosition.Dao_Dao;//cho dao về vị trí trung tâm của nó
                }
                #endregion

                GlobalVariable.DaoLangPosition.HutSv = (int)rongChia2;

                #region Lang
                //if (rongChia2 <= GlobalVariable.DaoLangPosition.Lang4Max)
                //{
                //    GlobalVariable.DaoLangPosition.Lang4Sv = GlobalVariable.DaoLangPosition.Lang5Sv = (int)(CaoA / 2 * 10);
                //    GlobalVariable.DaoLangPosition.Lang4U = GlobalVariable.DaoLangPosition.Lang5U = 1;

                //    GlobalVariable.DaoLangPosition.Lang3Sv = GlobalVariable.DaoLangPosition.Lang6Sv = GlobalVariable.DaoLangPosition.Lang3Min + GlobalVariable.DaoLangPosition.Lang_Lang;
                //    GlobalVariable.DaoLangPosition.Lang2Sv = GlobalVariable.DaoLangPosition.Lang7Sv = GlobalVariable.DaoLangPosition.Lang2Min + GlobalVariable.DaoLangPosition.Lang_Lang;
                //    GlobalVariable.DaoLangPosition.Lang1Sv = GlobalVariable.DaoLangPosition.Lang8Sv = GlobalVariable.DaoLangPosition.Lang1Min + GlobalVariable.DaoLangPosition.Lang_Lang;
                //}
                //else if (rongChia2 > GlobalVariable.DaoLangPosition.Lang4Max && rongChia2 <= GlobalVariable.DaoLangPosition.Lang3Max)
                //{
                //    GlobalVariable.DaoLangPosition.Lang3Sv = GlobalVariable.DaoLangPosition.Lang6Sv = (int)(CaoA / 2 * 10);
                //    GlobalVariable.DaoLangPosition.Lang3U = GlobalVariable.DaoLangPosition.Lang6U = 1;

                //    GlobalVariable.DaoLangPosition.Lang4Sv = GlobalVariable.DaoLangPosition.Lang5Sv = GlobalVariable.DaoLangPosition.Lang4Min + GlobalVariable.DaoLangPosition.Lang_Lang;
                //    GlobalVariable.DaoLangPosition.Lang2Sv = GlobalVariable.DaoLangPosition.Lang7Sv = GlobalVariable.DaoLangPosition.Lang2Min + GlobalVariable.DaoLangPosition.Lang_Lang;
                //    GlobalVariable.DaoLangPosition.Lang1Sv = GlobalVariable.DaoLangPosition.Lang8Sv = GlobalVariable.DaoLangPosition.Lang1Min + GlobalVariable.DaoLangPosition.Lang_Lang;
                //}
                //else if (rongChia2 > GlobalVariable.DaoLangPosition.Lang3Max && CaoA / 2 <= GlobalVariable.DaoLangPosition.Lang2Max)
                //{
                //    GlobalVariable.DaoLangPosition.Lang2Sv = GlobalVariable.DaoLangPosition.Lang7Sv = (int)(CaoA / 2 * 10);
                //    GlobalVariable.DaoLangPosition.Lang2U = GlobalVariable.DaoLangPosition.Lang7U = 1;

                //    GlobalVariable.DaoLangPosition.Lang4Sv = GlobalVariable.DaoLangPosition.Lang5Sv = GlobalVariable.DaoLangPosition.Lang4Min + GlobalVariable.DaoLangPosition.Lang_Lang;
                //    GlobalVariable.DaoLangPosition.Lang3Sv = GlobalVariable.DaoLangPosition.Lang6Sv = GlobalVariable.DaoLangPosition.Lang3Min + GlobalVariable.DaoLangPosition.Lang_Lang;
                //    GlobalVariable.DaoLangPosition.Lang1Sv = GlobalVariable.DaoLangPosition.Lang8Sv = GlobalVariable.DaoLangPosition.Lang1Min + GlobalVariable.DaoLangPosition.Lang_Lang;
                //}
                //else if (CaoA / 2 > GlobalVariable.DaoLangPosition.Lang2Max && CaoA / 2 <= GlobalVariable.DaoLangPosition.Lang1Max)
                //{
                //    GlobalVariable.DaoLangPosition.Lang1Sv = GlobalVariable.DaoLangPosition.Lang8Sv = (int)(CaoA / 2 * 10);
                //    GlobalVariable.DaoLangPosition.Lang1U = GlobalVariable.DaoLangPosition.Lang8U = 1;

                //    GlobalVariable.DaoLangPosition.Lang4Sv = GlobalVariable.DaoLangPosition.Lang5Sv = GlobalVariable.DaoLangPosition.Lang4Min + GlobalVariable.DaoLangPosition.Lang_Lang;
                //    GlobalVariable.DaoLangPosition.Lang2Sv = GlobalVariable.DaoLangPosition.Lang7Sv = GlobalVariable.DaoLangPosition.Lang2Min + GlobalVariable.DaoLangPosition.Lang_Lang;
                //    GlobalVariable.DaoLangPosition.Lang3Sv = GlobalVariable.DaoLangPosition.Lang6Sv = GlobalVariable.DaoLangPosition.Lang3Min + GlobalVariable.DaoLangPosition.Lang_Lang;
                //}

                #endregion
            }

            // Xả = 2 --luôn bật dao 3 tại vị trí 0
            else if (donHang.Xa == 2)
            {
                GlobalVariable.DaoLangPosition.Dao3U = 1;

                double rongChia2 = rongTotal / 2 * 10;//đang là đơn vị mm, x10 để loại bỏ số lẻ

                #region Dao
                // Dao 1 hoac 2
                if (GlobalVariable.DaoLangPosition.Dao2Max > GlobalVariable.DaoLangPosition.Dao2Min && GlobalVariable.DaoLangPosition.Dao1Max > GlobalVariable.DaoLangPosition.Dao1Min)
                {
                    if (GlobalVariable.DaoLangPosition.Dao2Max >= rongChia2)
                    {
                        GlobalVariable.DaoLangPosition.Dao2SV = (int)(rongChia2);
                        GlobalVariable.DaoLangPosition.Dao2U = 1;//bat dao

                        GlobalVariable.DaoLangPosition.Dao1SV = GlobalVariable.DaoLangPosition.Dao1Min + GlobalVariable.DaoLangPosition.Dao_Dao;//cho dao về vị trí trung tâm của nó
                    }
                    else
                    {
                        GlobalVariable.DaoLangPosition.Dao1SV = (int)(rongChia2);
                        GlobalVariable.DaoLangPosition.Dao1U = 1;

                        GlobalVariable.DaoLangPosition.Dao2SV = GlobalVariable.DaoLangPosition.Dao2Min + GlobalVariable.DaoLangPosition.Dao_Dao;//cho dao về vị trí trung tâm của nó
                    }
                }
                //Dao 1
                else if (GlobalVariable.DaoLangPosition.Dao1Max > GlobalVariable.DaoLangPosition.Dao1Min && GlobalVariable.DaoLangPosition.Dao2Max == GlobalVariable.DaoLangPosition.Dao2Min)
                {
                    GlobalVariable.DaoLangPosition.Dao1SV = (int)(rongChia2);
                    GlobalVariable.DaoLangPosition.Dao1U = 1;

                    GlobalVariable.DaoLangPosition.Dao2SV = GlobalVariable.DaoLangPosition.Dao2Min + GlobalVariable.DaoLangPosition.Dao_Dao;//cho dao về vị trí trung tâm của nó
                }
                //Dao 2
                else if (GlobalVariable.DaoLangPosition.Dao2Max > GlobalVariable.DaoLangPosition.Dao2Min && GlobalVariable.DaoLangPosition.Dao1Max == GlobalVariable.DaoLangPosition.Dao1Min)
                {
                    GlobalVariable.DaoLangPosition.Dao1SV = (int)(rongChia2);
                    GlobalVariable.DaoLangPosition.Dao1U = 1;

                    GlobalVariable.DaoLangPosition.Dao2SV = GlobalVariable.DaoLangPosition.Dao2Min + GlobalVariable.DaoLangPosition.Dao_Dao;//cho dao về vị trí trung tâm của nó
                }

                //Dao 4 hoac 5
                if (GlobalVariable.DaoLangPosition.Dao5Max > GlobalVariable.DaoLangPosition.Dao5Min && GlobalVariable.DaoLangPosition.Dao4Max > GlobalVariable.DaoLangPosition.Dao4Min)
                {
                    if (GlobalVariable.DaoLangPosition.Dao4Max >= rongChia2)
                    {
                        GlobalVariable.DaoLangPosition.Dao4SV = (int)(rongChia2);
                        GlobalVariable.DaoLangPosition.Dao4U = 1;//bat dao

                        GlobalVariable.DaoLangPosition.Dao5SV = GlobalVariable.DaoLangPosition.Dao5Min + GlobalVariable.DaoLangPosition.Dao_Dao;//cho dao về vị trí trung tâm của nó
                    }
                    else
                    {
                        GlobalVariable.DaoLangPosition.Dao5SV = (int)(rongChia2);
                        GlobalVariable.DaoLangPosition.Dao5U = 1;

                        GlobalVariable.DaoLangPosition.Dao4SV = GlobalVariable.DaoLangPosition.Dao4Min + GlobalVariable.DaoLangPosition.Dao_Dao;//cho dao về vị trí trung tâm của nó
                    }
                }
                //Dao 5
                else if (GlobalVariable.DaoLangPosition.Dao5Max > GlobalVariable.DaoLangPosition.Dao5Min && GlobalVariable.DaoLangPosition.Dao4Max == GlobalVariable.DaoLangPosition.Dao4Min)
                {
                    GlobalVariable.DaoLangPosition.Dao5SV = (int)(rongChia2);
                    GlobalVariable.DaoLangPosition.Dao5U = 1;

                    GlobalVariable.DaoLangPosition.Dao4SV = GlobalVariable.DaoLangPosition.Dao4Min + GlobalVariable.DaoLangPosition.Dao_Dao;//cho dao về vị trí trung tâm của nó
                }
                //Dao 4
                else if (GlobalVariable.DaoLangPosition.Dao4Max > GlobalVariable.DaoLangPosition.Dao4Min && GlobalVariable.DaoLangPosition.Dao5Max == GlobalVariable.DaoLangPosition.Dao5Min)
                {
                    GlobalVariable.DaoLangPosition.Dao4SV = (int)(rongChia2);
                    GlobalVariable.DaoLangPosition.Dao4U = 1;

                    GlobalVariable.DaoLangPosition.Dao5SV = GlobalVariable.DaoLangPosition.Dao5Min + GlobalVariable.DaoLangPosition.Dao_Dao;//cho dao về vị trí trung tâm của nó
                }
                #endregion

                GlobalVariable.DaoLangPosition.HutSv = (int)rongChia2;

                #region Lang

                #endregion
            }
            // Xả = 3
            else if (donHang.Xa == 3)
            {
                double rongChia2 = rongTotal / 2 * 10;//đang là đơn vị mm, x10 để loại bỏ số lẻ

                if (GlobalVariable.DaoLangPosition.Dao2Max > GlobalVariable.DaoLangPosition.Dao2Min && GlobalVariable.DaoLangPosition.Dao1Max > GlobalVariable.DaoLangPosition.Dao1Min)
                {
                    GlobalVariable.DaoLangPosition.Dao1SV = GlobalVariable.DaoLangPosition.Dao5SV = (int)rongChia2;
                    GlobalVariable.DaoLangPosition.Dao1U = GlobalVariable.DaoLangPosition.Dao5U = 1;

                    GlobalVariable.DaoLangPosition.Dao2SV = GlobalVariable.DaoLangPosition.Dao4SV = (int)(rongChia2 - rongA * 10);
                    GlobalVariable.DaoLangPosition.Dao2U = GlobalVariable.DaoLangPosition.Dao4U = 1;

                    GlobalVariable.DaoLangPosition.HutSv = (int)rongChia2;
                }
                else
                {
                    MessageBox.Show($"Xả 3 yêu cầu tất cả các dao phải hoạt động.\rKiểm tra lại các dao.", "CẢNH BÁO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }


            }

            else if (donHang.Xa == 4)
            {
                GlobalVariable.DaoLangPosition.Dao3U = 1;
                double rongChia2 = rongTotal / 2 * 10;//đang là đơn vị mm, x10 để loại bỏ số lẻ

                if (GlobalVariable.DaoLangPosition.Dao2Max > GlobalVariable.DaoLangPosition.Dao2Min && GlobalVariable.DaoLangPosition.Dao1Max > GlobalVariable.DaoLangPosition.Dao1Min)
                {
                    GlobalVariable.DaoLangPosition.Dao1SV = GlobalVariable.DaoLangPosition.Dao5SV = (int)rongChia2;
                    GlobalVariable.DaoLangPosition.Dao1U = GlobalVariable.DaoLangPosition.Dao5U = 1;

                    GlobalVariable.DaoLangPosition.Dao2SV = GlobalVariable.DaoLangPosition.Dao4SV = (int)(rongChia2 - rongA * 10);
                    GlobalVariable.DaoLangPosition.Dao2U = GlobalVariable.DaoLangPosition.Dao4U = 1;

                    GlobalVariable.DaoLangPosition.HutSv = (int)rongChia2;
                }
                else
                {
                    MessageBox.Show($"Xả 3 yêu cầu tất cả các dao phải hoạt động.\rKiểm tra lại các dao.", "CẢNH BÁO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
    }
}
