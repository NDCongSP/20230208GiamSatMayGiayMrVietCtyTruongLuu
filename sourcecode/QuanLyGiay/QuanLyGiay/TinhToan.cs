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
            tất cả các thông sô khi cài đặt đơn hàng đơn vị là mm, chỉ ngoại trừ tổng số mét cắt đơn vị là met
            chuyển đổi về đơn vị mm.
            */
            double rong = donHang.Rong;//chuyen sang đơn vị mm
            double cao = donHang.Cao;//chuyen sang đơn vị mm
            double canh = rong / 2;

            double caoA = cao;
            double canhA = canh;
            double rongA = canhA + caoA + canhA + donHang.CongThem;

            double caoB = cao;
            double canhB = canh;
            double rongB = canhB + caoB + canhB + donHang.CongThem;

            double caoC = cao;
            double canhC = canh;
            double rongC = canhC + caoC + canhC + donHang.CongThem;

            double caoD = cao;
            double canhD = canh;
            double rongD = canhD + caoD + canhD + donHang.CongThem;

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
            else if (donHang.Xa == 2)
                rongTotal = rongA + rongB;
            else if (donHang.Xa == 3)
                rongTotal = rongA + rongB + rongC;
            else if (donHang.Xa == 4)
                rongTotal = rongA + rongB + rongC + rongD;
            else//Xa=0--không dùng dáo xả
            {

            }

            // Xả = 1
            if (donHang.Xa == 1)
            {
                double rongChia2 = rongTotal / 2 * 10;//đang là đơn vị mm, x10 để loại bỏ số lẻ
                double lanPosition = rongChia2 - canh * 10;//vi trí lằn

                #region Dao
                //Dao 1 hoac 2
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
                if (GlobalVariable.DaoLangPosition.Lang4Max >= lanPosition)
                {
                    GlobalVariable.DaoLangPosition.Lang4U = GlobalVariable.DaoLangPosition.Lang5U = 1;
                    GlobalVariable.DaoLangPosition.Lang4Sv = GlobalVariable.DaoLangPosition.Lang5Sv = (int)(lanPosition);

                    GlobalVariable.DaoLangPosition.Lang3Sv = GlobalVariable.DaoLangPosition.Lang6Sv = GlobalVariable.DaoLangPosition.Lang3Min + GlobalVariable.DaoLangPosition.Lang_Lang;
                    GlobalVariable.DaoLangPosition.Lang2Sv = GlobalVariable.DaoLangPosition.Lang7Sv = GlobalVariable.DaoLangPosition.Lang2Min + GlobalVariable.DaoLangPosition.Lang_Lang;
                    GlobalVariable.DaoLangPosition.Lang1Sv = GlobalVariable.DaoLangPosition.Lang8Sv = GlobalVariable.DaoLangPosition.Lang1Min + GlobalVariable.DaoLangPosition.Lang_Lang;
                }
                else if (GlobalVariable.DaoLangPosition.Lang3Max >= lanPosition)
                {
                    GlobalVariable.DaoLangPosition.Lang3U = GlobalVariable.DaoLangPosition.Lang6U = 1;
                    GlobalVariable.DaoLangPosition.Lang3Sv = GlobalVariable.DaoLangPosition.Lang6Sv = (int)(lanPosition);

                    GlobalVariable.DaoLangPosition.Lang4Sv = GlobalVariable.DaoLangPosition.Lang5Sv = GlobalVariable.DaoLangPosition.Lang4Min + GlobalVariable.DaoLangPosition.Lang_Lang;
                    GlobalVariable.DaoLangPosition.Lang2Sv = GlobalVariable.DaoLangPosition.Lang7Sv = GlobalVariable.DaoLangPosition.Lang2Min + GlobalVariable.DaoLangPosition.Lang_Lang;
                    GlobalVariable.DaoLangPosition.Lang1Sv = GlobalVariable.DaoLangPosition.Lang8Sv = GlobalVariable.DaoLangPosition.Lang1Min + GlobalVariable.DaoLangPosition.Lang_Lang;
                }
                else if (GlobalVariable.DaoLangPosition.Lang2Max >= lanPosition)
                {
                    GlobalVariable.DaoLangPosition.Lang2U = GlobalVariable.DaoLangPosition.Lang7U = 1;
                    GlobalVariable.DaoLangPosition.Lang2Sv = GlobalVariable.DaoLangPosition.Lang7Sv = (int)(lanPosition);

                    GlobalVariable.DaoLangPosition.Lang4Sv = GlobalVariable.DaoLangPosition.Lang5Sv = GlobalVariable.DaoLangPosition.Lang4Min + GlobalVariable.DaoLangPosition.Lang_Lang;
                    GlobalVariable.DaoLangPosition.Lang3Sv = GlobalVariable.DaoLangPosition.Lang6Sv = GlobalVariable.DaoLangPosition.Lang3Min + GlobalVariable.DaoLangPosition.Lang_Lang;
                    GlobalVariable.DaoLangPosition.Lang1Sv = GlobalVariable.DaoLangPosition.Lang8Sv = GlobalVariable.DaoLangPosition.Lang1Min + GlobalVariable.DaoLangPosition.Lang_Lang;
                }
                else if (GlobalVariable.DaoLangPosition.Lang1Max >= lanPosition)
                {
                    GlobalVariable.DaoLangPosition.Lang1U = GlobalVariable.DaoLangPosition.Lang8U = 1;
                    GlobalVariable.DaoLangPosition.Lang1Sv = GlobalVariable.DaoLangPosition.Lang8Sv = (int)(lanPosition);

                    GlobalVariable.DaoLangPosition.Lang4Sv = GlobalVariable.DaoLangPosition.Lang5Sv = GlobalVariable.DaoLangPosition.Lang4Min + GlobalVariable.DaoLangPosition.Lang_Lang;
                    GlobalVariable.DaoLangPosition.Lang3Sv = GlobalVariable.DaoLangPosition.Lang6Sv = GlobalVariable.DaoLangPosition.Lang3Min + GlobalVariable.DaoLangPosition.Lang_Lang;
                    GlobalVariable.DaoLangPosition.Lang2Sv = GlobalVariable.DaoLangPosition.Lang7Sv = GlobalVariable.DaoLangPosition.Lang2Min + GlobalVariable.DaoLangPosition.Lang_Lang;
                }
                #endregion
            }

            // Xả = 2 --luôn bật dao 3 tại vị trí 0
            else if (donHang.Xa == 2)
            {
                GlobalVariable.DaoLangPosition.Dao3U = 1;

                double rongChia2 = rongTotal / 2 * 10;//đang là đơn vị mm, x10 để loại bỏ số lẻ
                double lanPosition1 = canh * 10;//vi trí lằn 1
                double lanPosition2 = canh * 10 + cao * 10;//vi trí lằn2

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
                #region Lan 1
                if (GlobalVariable.DaoLangPosition.Lang4Max >= lanPosition1)
                {
                    GlobalVariable.DaoLangPosition.Lang4U = GlobalVariable.DaoLangPosition.Lang5U = 1;
                    GlobalVariable.DaoLangPosition.Lang4Sv = GlobalVariable.DaoLangPosition.Lang5Sv = (int)(lanPosition1);

                    GlobalVariable.DaoLangPosition.Lang3Sv = GlobalVariable.DaoLangPosition.Lang6Sv = GlobalVariable.DaoLangPosition.Lang3Min + GlobalVariable.DaoLangPosition.Lang_Lang;
                    GlobalVariable.DaoLangPosition.Lang2Sv = GlobalVariable.DaoLangPosition.Lang7Sv = GlobalVariable.DaoLangPosition.Lang2Min + GlobalVariable.DaoLangPosition.Lang_Lang;
                    GlobalVariable.DaoLangPosition.Lang1Sv = GlobalVariable.DaoLangPosition.Lang8Sv = GlobalVariable.DaoLangPosition.Lang1Min + GlobalVariable.DaoLangPosition.Lang_Lang;

                    #region Lan 2
                    if (GlobalVariable.DaoLangPosition.Lang3Max >= lanPosition2)
                    {
                        GlobalVariable.DaoLangPosition.Lang3U = GlobalVariable.DaoLangPosition.Lang6U = 1;
                        GlobalVariable.DaoLangPosition.Lang3Sv = GlobalVariable.DaoLangPosition.Lang6Sv = (int)(lanPosition2);

                        GlobalVariable.DaoLangPosition.Lang2Sv = GlobalVariable.DaoLangPosition.Lang7Sv = GlobalVariable.DaoLangPosition.Lang2Min + GlobalVariable.DaoLangPosition.Lang_Lang;
                        GlobalVariable.DaoLangPosition.Lang1Sv = GlobalVariable.DaoLangPosition.Lang8Sv = GlobalVariable.DaoLangPosition.Lang1Min + GlobalVariable.DaoLangPosition.Lang_Lang;
                    }
                    else if (GlobalVariable.DaoLangPosition.Lang2Max >= lanPosition2)
                    {
                        GlobalVariable.DaoLangPosition.Lang2U = GlobalVariable.DaoLangPosition.Lang7U = 1;
                        GlobalVariable.DaoLangPosition.Lang2Sv = GlobalVariable.DaoLangPosition.Lang7Sv = (int)(lanPosition2);

                        GlobalVariable.DaoLangPosition.Lang3Sv = GlobalVariable.DaoLangPosition.Lang6Sv = GlobalVariable.DaoLangPosition.Lang3Min + GlobalVariable.DaoLangPosition.Lang_Lang;
                        GlobalVariable.DaoLangPosition.Lang1Sv = GlobalVariable.DaoLangPosition.Lang8Sv = GlobalVariable.DaoLangPosition.Lang1Min + GlobalVariable.DaoLangPosition.Lang_Lang;
                    }
                    else if (GlobalVariable.DaoLangPosition.Lang1Max >= lanPosition2)
                    {
                        GlobalVariable.DaoLangPosition.Lang1U = GlobalVariable.DaoLangPosition.Lang8U = 1;
                        GlobalVariable.DaoLangPosition.Lang1Sv = GlobalVariable.DaoLangPosition.Lang8Sv = (int)(lanPosition2);

                        GlobalVariable.DaoLangPosition.Lang3Sv = GlobalVariable.DaoLangPosition.Lang6Sv = GlobalVariable.DaoLangPosition.Lang3Min + GlobalVariable.DaoLangPosition.Lang_Lang;
                        GlobalVariable.DaoLangPosition.Lang2Sv = GlobalVariable.DaoLangPosition.Lang7Sv = GlobalVariable.DaoLangPosition.Lang2Min + GlobalVariable.DaoLangPosition.Lang_Lang;
                    }
                    #endregion
                }
                else if (GlobalVariable.DaoLangPosition.Lang3Max >= lanPosition1)
                {
                    GlobalVariable.DaoLangPosition.Lang3U = GlobalVariable.DaoLangPosition.Lang6U = 1;
                    GlobalVariable.DaoLangPosition.Lang3Sv = GlobalVariable.DaoLangPosition.Lang6Sv = (int)(lanPosition1);

                    GlobalVariable.DaoLangPosition.Lang4Sv = GlobalVariable.DaoLangPosition.Lang5Sv = GlobalVariable.DaoLangPosition.Lang4Min + GlobalVariable.DaoLangPosition.Lang_Lang;
                    GlobalVariable.DaoLangPosition.Lang2Sv = GlobalVariable.DaoLangPosition.Lang7Sv = GlobalVariable.DaoLangPosition.Lang2Min + GlobalVariable.DaoLangPosition.Lang_Lang;
                    GlobalVariable.DaoLangPosition.Lang1Sv = GlobalVariable.DaoLangPosition.Lang8Sv = GlobalVariable.DaoLangPosition.Lang1Min + GlobalVariable.DaoLangPosition.Lang_Lang;

                    #region Lan 2
                    if (GlobalVariable.DaoLangPosition.Lang2Max >= lanPosition2)
                    {
                        GlobalVariable.DaoLangPosition.Lang2U = GlobalVariable.DaoLangPosition.Lang7U = 1;
                        GlobalVariable.DaoLangPosition.Lang2Sv = GlobalVariable.DaoLangPosition.Lang7Sv = (int)(lanPosition2);

                        GlobalVariable.DaoLangPosition.Lang1Sv = GlobalVariable.DaoLangPosition.Lang8Sv = GlobalVariable.DaoLangPosition.Lang1Min + GlobalVariable.DaoLangPosition.Lang_Lang;
                    }
                    else if (GlobalVariable.DaoLangPosition.Lang1Max >= lanPosition2)
                    {
                        GlobalVariable.DaoLangPosition.Lang1U = GlobalVariable.DaoLangPosition.Lang8U = 1;
                        GlobalVariable.DaoLangPosition.Lang1Sv = GlobalVariable.DaoLangPosition.Lang8Sv = (int)(lanPosition2);

                        GlobalVariable.DaoLangPosition.Lang2Sv = GlobalVariable.DaoLangPosition.Lang7Sv = GlobalVariable.DaoLangPosition.Lang2Min + GlobalVariable.DaoLangPosition.Lang_Lang;
                    }
                    #endregion
                }
                else if (GlobalVariable.DaoLangPosition.Lang2Max >= lanPosition1)
                {
                    GlobalVariable.DaoLangPosition.Lang2U = GlobalVariable.DaoLangPosition.Lang7U = 1;
                    GlobalVariable.DaoLangPosition.Lang2Sv = GlobalVariable.DaoLangPosition.Lang7Sv = (int)(lanPosition1);

                    GlobalVariable.DaoLangPosition.Lang4Sv = GlobalVariable.DaoLangPosition.Lang5Sv = GlobalVariable.DaoLangPosition.Lang4Min + GlobalVariable.DaoLangPosition.Lang_Lang;
                    GlobalVariable.DaoLangPosition.Lang3Sv = GlobalVariable.DaoLangPosition.Lang6Sv = GlobalVariable.DaoLangPosition.Lang3Min + GlobalVariable.DaoLangPosition.Lang_Lang;
                    GlobalVariable.DaoLangPosition.Lang1Sv = GlobalVariable.DaoLangPosition.Lang8Sv = GlobalVariable.DaoLangPosition.Lang1Min + GlobalVariable.DaoLangPosition.Lang_Lang;

                    #region Lan 2
                    if (GlobalVariable.DaoLangPosition.Lang1Max >= lanPosition2)
                    {
                        GlobalVariable.DaoLangPosition.Lang1U = GlobalVariable.DaoLangPosition.Lang8U = 1;
                        GlobalVariable.DaoLangPosition.Lang1Sv = GlobalVariable.DaoLangPosition.Lang8Sv = (int)(lanPosition2);
                    }
                    #endregion
                }
                else if (GlobalVariable.DaoLangPosition.Lang1Max >= lanPosition1)
                {
                    MessageBox.Show($"Xả lằn vượt ngoài giới hạn cho phép, cần kiểm tra lại cài đặt đơn hàng.", "CẢNH BÁO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                #endregion
                #endregion
            }
            // Xả = 3
            else if (donHang.Xa == 3)
            {
                double rongChia2 = rongTotal / 2 * 10;//đang là đơn vị mm, x10 để loại bỏ số lẻ
                double lanPosition1 = cao / 2 * 10;//vi trí lằn 1
                double lanPosition2 = (cao / 2 + canh * 2) * 10;//vi trí lằn 1
                double lanPosition3 = rongChia2 - canh * 10;//vi trí lằn2

                if (GlobalVariable.DaoLangPosition.Dao2Max > GlobalVariable.DaoLangPosition.Dao2Min && GlobalVariable.DaoLangPosition.Dao1Max > GlobalVariable.DaoLangPosition.Dao1Min)
                {
                    GlobalVariable.DaoLangPosition.Dao1SV = GlobalVariable.DaoLangPosition.Dao5SV = (int)rongChia2;
                    GlobalVariable.DaoLangPosition.Dao1U = GlobalVariable.DaoLangPosition.Dao5U = 1;

                    GlobalVariable.DaoLangPosition.Dao2SV = GlobalVariable.DaoLangPosition.Dao4SV = (int)(rongChia2 - rongA * 10);
                    GlobalVariable.DaoLangPosition.Dao2U = GlobalVariable.DaoLangPosition.Dao4U = 1;

                    GlobalVariable.DaoLangPosition.HutSv = (int)rongChia2;

                    #region Lang
                    #region Lan 1
                    if (GlobalVariable.DaoLangPosition.Lang4Max >= lanPosition1)
                    {
                        GlobalVariable.DaoLangPosition.Lang4U = GlobalVariable.DaoLangPosition.Lang5U = 1;
                        GlobalVariable.DaoLangPosition.Lang4Sv = GlobalVariable.DaoLangPosition.Lang5Sv = (int)(lanPosition1);

                        GlobalVariable.DaoLangPosition.Lang3Sv = GlobalVariable.DaoLangPosition.Lang6Sv = GlobalVariable.DaoLangPosition.Lang3Min + GlobalVariable.DaoLangPosition.Lang_Lang;
                        GlobalVariable.DaoLangPosition.Lang2Sv = GlobalVariable.DaoLangPosition.Lang7Sv = GlobalVariable.DaoLangPosition.Lang2Min + GlobalVariable.DaoLangPosition.Lang_Lang;
                        GlobalVariable.DaoLangPosition.Lang1Sv = GlobalVariable.DaoLangPosition.Lang8Sv = GlobalVariable.DaoLangPosition.Lang1Min + GlobalVariable.DaoLangPosition.Lang_Lang;

                        #region Lan 2
                        if (GlobalVariable.DaoLangPosition.Lang3Max >= lanPosition2)
                        {
                            GlobalVariable.DaoLangPosition.Lang3U = GlobalVariable.DaoLangPosition.Lang6U = 1;
                            GlobalVariable.DaoLangPosition.Lang3Sv = GlobalVariable.DaoLangPosition.Lang6Sv = (int)(lanPosition2);

                            GlobalVariable.DaoLangPosition.Lang2Sv = GlobalVariable.DaoLangPosition.Lang7Sv = GlobalVariable.DaoLangPosition.Lang2Min + GlobalVariable.DaoLangPosition.Lang_Lang;
                            GlobalVariable.DaoLangPosition.Lang1Sv = GlobalVariable.DaoLangPosition.Lang8Sv = GlobalVariable.DaoLangPosition.Lang1Min + GlobalVariable.DaoLangPosition.Lang_Lang;

                            if (GlobalVariable.DaoLangPosition.Lang2Max >= lanPosition3)
                            {
                                GlobalVariable.DaoLangPosition.Lang2U = GlobalVariable.DaoLangPosition.Lang7U = 1;
                                GlobalVariable.DaoLangPosition.Lang2Sv = GlobalVariable.DaoLangPosition.Lang7Sv = (int)(lanPosition3);

                                GlobalVariable.DaoLangPosition.Lang1Sv = GlobalVariable.DaoLangPosition.Lang8Sv = GlobalVariable.DaoLangPosition.Lang1Min + GlobalVariable.DaoLangPosition.Lang_Lang;
                            }
                            else if (GlobalVariable.DaoLangPosition.Lang1Max >= lanPosition3)
                            {
                                GlobalVariable.DaoLangPosition.Lang1U = GlobalVariable.DaoLangPosition.Lang8U = 1;
                                GlobalVariable.DaoLangPosition.Lang1Sv = GlobalVariable.DaoLangPosition.Lang8Sv = (int)(lanPosition3);
                            }
                        }
                        else if (GlobalVariable.DaoLangPosition.Lang2Max >= lanPosition2)
                        {
                            GlobalVariable.DaoLangPosition.Lang2U = GlobalVariable.DaoLangPosition.Lang7U = 1;
                            GlobalVariable.DaoLangPosition.Lang2Sv = GlobalVariable.DaoLangPosition.Lang7Sv = (int)(lanPosition2);

                            GlobalVariable.DaoLangPosition.Lang1U = GlobalVariable.DaoLangPosition.Lang8U = 1;
                            GlobalVariable.DaoLangPosition.Lang8Sv = GlobalVariable.DaoLangPosition.Lang8Sv = (int)(lanPosition3);
                        }
                        #endregion
                    }
                    else if (GlobalVariable.DaoLangPosition.Lang3Max >= lanPosition1)
                    {
                        GlobalVariable.DaoLangPosition.Lang4Sv = GlobalVariable.DaoLangPosition.Lang5Sv = GlobalVariable.DaoLangPosition.Lang4Min + GlobalVariable.DaoLangPosition.Lang_Lang;

                        GlobalVariable.DaoLangPosition.Lang3U = GlobalVariable.DaoLangPosition.Lang6U = 1;
                        GlobalVariable.DaoLangPosition.Lang3Sv = GlobalVariable.DaoLangPosition.Lang6Sv = (int)(lanPosition1);

                        GlobalVariable.DaoLangPosition.Lang2U = GlobalVariable.DaoLangPosition.Lang7U = 1;
                        GlobalVariable.DaoLangPosition.Lang2Sv = GlobalVariable.DaoLangPosition.Lang7Sv = (int)(lanPosition2);

                        GlobalVariable.DaoLangPosition.Lang1U = GlobalVariable.DaoLangPosition.Lang8U = 1;
                        GlobalVariable.DaoLangPosition.Lang1Sv = GlobalVariable.DaoLangPosition.Lang8Sv = (int)(lanPosition3);
                    }
                    else
                    {
                        MessageBox.Show($"Xả lằn vượt ngoài giới hạn cho phép, cần kiểm tra lại cài đặt đơn hàng.", "CẢNH BÁO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    #endregion
                    #endregion
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
                double lanPosition1 = canh * 10;//vi trí lằn 1
                double lanPosition2 = (cao + canh) * 10;//vi trí lằn 1
                double lanPosition3 = (rongA + canh) * 10;//vi trí lằn2
                double lanPosition4 = (rongA + canh + cao) * 10;//vi trí lằn2

                if (GlobalVariable.DaoLangPosition.Dao2Max > GlobalVariable.DaoLangPosition.Dao2Min && GlobalVariable.DaoLangPosition.Dao1Max > GlobalVariable.DaoLangPosition.Dao1Min)
                {
                    GlobalVariable.DaoLangPosition.Dao1SV = GlobalVariable.DaoLangPosition.Dao5SV = (int)rongChia2;
                    GlobalVariable.DaoLangPosition.Dao1U = GlobalVariable.DaoLangPosition.Dao5U = 1;

                    GlobalVariable.DaoLangPosition.Dao2SV = GlobalVariable.DaoLangPosition.Dao4SV = (int)(rongChia2 - rongA * 10);
                    GlobalVariable.DaoLangPosition.Dao2U = GlobalVariable.DaoLangPosition.Dao4U = 1;

                    GlobalVariable.DaoLangPosition.HutSv = (int)rongChia2;

                    #region lan
                    GlobalVariable.DaoLangPosition.Lang4U = GlobalVariable.DaoLangPosition.Lang5U = 1;
                    GlobalVariable.DaoLangPosition.Lang4Sv = GlobalVariable.DaoLangPosition.Lang5Sv = (int)(lanPosition1);

                    GlobalVariable.DaoLangPosition.Lang3U = GlobalVariable.DaoLangPosition.Lang6U = 1;
                    GlobalVariable.DaoLangPosition.Lang3Sv = GlobalVariable.DaoLangPosition.Lang6Sv = (int)(lanPosition2);

                    GlobalVariable.DaoLangPosition.Lang2U = GlobalVariable.DaoLangPosition.Lang7U = 1;
                    GlobalVariable.DaoLangPosition.Lang2Sv = GlobalVariable.DaoLangPosition.Lang7Sv = (int)(lanPosition3);

                    GlobalVariable.DaoLangPosition.Lang1U = GlobalVariable.DaoLangPosition.Lang8U = 1;
                    GlobalVariable.DaoLangPosition.Lang1Sv = GlobalVariable.DaoLangPosition.Lang8Sv = (int)(lanPosition4);
                    #endregion
                }
                else
                {
                    MessageBox.Show($"Xả 3 yêu cầu tất cả các dao phải hoạt động.\rKiểm tra lại các dao.", "CẢNH BÁO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                GlobalVariable.DaoLangPosition.Dao1SV= GlobalVariable.DaoLangPosition.Dao5SV = GlobalVariable.DaoLangPosition.Dao1OffSV;
                GlobalVariable.DaoLangPosition.Dao2SV= GlobalVariable.DaoLangPosition.Dao4SV = GlobalVariable.DaoLangPosition.Dao2OffSV;
                GlobalVariable.DaoLangPosition.Dao3SV = 0;

                GlobalVariable.DaoLangPosition.Lang1Sv= GlobalVariable.DaoLangPosition.Lang8Sv = GlobalVariable.DaoLangPosition.Lang1OffSV;
                GlobalVariable.DaoLangPosition.Lang2Sv= GlobalVariable.DaoLangPosition.Lang7Sv = GlobalVariable.DaoLangPosition.Lang2OffSV;
                GlobalVariable.DaoLangPosition.Lang3Sv= GlobalVariable.DaoLangPosition.Lang6Sv = GlobalVariable.DaoLangPosition.Lang3OffSV;
                GlobalVariable.DaoLangPosition.Lang4Sv= GlobalVariable.DaoLangPosition.Lang5Sv = GlobalVariable.DaoLangPosition.Lang4OffSV;
            }
        }
    }
}
