﻿using CommonControl;
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
            GlobalVariable1.DaoLangPosition.KieuLang = donHang.Lang;//chon kiểu lằng
            GlobalVariable1.DaoLangPosition.Xa = donHang.Xa;//xả bao nhiêu tấm, cái này quy định dao
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
            GlobalVariable1.DaoLangPosition.Dao1U = 0;
            GlobalVariable1.DaoLangPosition.Dao2U = 0;
            GlobalVariable1.DaoLangPosition.Dao3U = 0;
            GlobalVariable1.DaoLangPosition.Dao4U = 0;
            GlobalVariable1.DaoLangPosition.Dao5U = 0;

            GlobalVariable1.DaoLangPosition.Lang1U = 0;
            GlobalVariable1.DaoLangPosition.Lang2U = 0;
            GlobalVariable1.DaoLangPosition.Lang3U = 0;
            GlobalVariable1.DaoLangPosition.Lang4U = 0;
            GlobalVariable1.DaoLangPosition.Lang5U = 0;
            GlobalVariable1.DaoLangPosition.Lang6U = 0;
            GlobalVariable1.DaoLangPosition.Lang7U = 0;
            GlobalVariable1.DaoLangPosition.Lang8U = 0;

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
                if (GlobalVariable1.DaoLangPosition.Dao2Max > GlobalVariable1.DaoLangPosition.Dao2Min && GlobalVariable1.DaoLangPosition.Dao1Max > GlobalVariable1.DaoLangPosition.Dao1Min)
                {
                    if (GlobalVariable1.DaoLangPosition.Dao2Max >= rongChia2)
                    {
                        GlobalVariable1.DaoLangPosition.Dao2SV = (int)(rongChia2);
                        GlobalVariable1.DaoLangPosition.Dao2U = 1;//bat dao

                        GlobalVariable1.DaoLangPosition.Dao1SV = GlobalVariable1.DaoLangPosition.Dao1Min + GlobalVariable1.DaoLangPosition.Dao_Dao;//cho dao về vị trí trung tâm của nó
                    }
                    else
                    {
                        GlobalVariable1.DaoLangPosition.Dao1SV = (int)(rongChia2);
                        GlobalVariable1.DaoLangPosition.Dao1U = 1;

                        GlobalVariable1.DaoLangPosition.Dao2SV = GlobalVariable1.DaoLangPosition.Dao2Min + GlobalVariable1.DaoLangPosition.Dao_Dao;//cho dao về vị trí trung tâm của nó
                    }
                }
                // Xả 1 - Dao 1
                else if (GlobalVariable1.DaoLangPosition.Dao1Max > GlobalVariable1.DaoLangPosition.Dao1Min && GlobalVariable1.DaoLangPosition.Dao2Max == GlobalVariable1.DaoLangPosition.Dao2Min)
                {
                    GlobalVariable1.DaoLangPosition.Dao1SV = (int)(rongChia2);
                    GlobalVariable1.DaoLangPosition.Dao1U = 1;

                    GlobalVariable1.DaoLangPosition.Dao2SV = GlobalVariable1.DaoLangPosition.Dao2Min + GlobalVariable1.DaoLangPosition.Dao_Dao;//cho dao về vị trí trung tâm của nó
                }
                // Xả 1 - Dao 2
                else if (GlobalVariable1.DaoLangPosition.Dao2Max > GlobalVariable1.DaoLangPosition.Dao2Min && GlobalVariable1.DaoLangPosition.Dao1Max == GlobalVariable1.DaoLangPosition.Dao1Min)
                {
                    GlobalVariable1.DaoLangPosition.Dao1SV = (int)(rongChia2);
                    GlobalVariable1.DaoLangPosition.Dao1U = 1;

                    GlobalVariable1.DaoLangPosition.Dao2SV = GlobalVariable1.DaoLangPosition.Dao2Min + GlobalVariable1.DaoLangPosition.Dao_Dao;//cho dao về vị trí trung tâm của nó
                }

                // Xả 1 - Dao 4 hoac 5
                if (GlobalVariable1.DaoLangPosition.Dao5Max > GlobalVariable1.DaoLangPosition.Dao5Min && GlobalVariable1.DaoLangPosition.Dao4Max > GlobalVariable1.DaoLangPosition.Dao4Min)
                {
                    if (GlobalVariable1.DaoLangPosition.Dao4Max >= rongChia2)
                    {
                        GlobalVariable1.DaoLangPosition.Dao4SV = (int)(rongChia2);
                        GlobalVariable1.DaoLangPosition.Dao4U = 1;//bat dao

                        GlobalVariable1.DaoLangPosition.Dao5SV = GlobalVariable1.DaoLangPosition.Dao5Min + GlobalVariable1.DaoLangPosition.Dao_Dao;//cho dao về vị trí trung tâm của nó
                    }
                    else
                    {
                        GlobalVariable1.DaoLangPosition.Dao5SV = (int)(rongChia2);
                        GlobalVariable1.DaoLangPosition.Dao5U = 1;

                        GlobalVariable1.DaoLangPosition.Dao4SV = GlobalVariable1.DaoLangPosition.Dao4Min + GlobalVariable1.DaoLangPosition.Dao_Dao;//cho dao về vị trí trung tâm của nó
                    }
                }
                // Xả 1 - Dao 5
                else if (GlobalVariable1.DaoLangPosition.Dao5Max > GlobalVariable1.DaoLangPosition.Dao5Min && GlobalVariable1.DaoLangPosition.Dao4Max == GlobalVariable1.DaoLangPosition.Dao4Min)
                {
                    GlobalVariable1.DaoLangPosition.Dao5SV = (int)(rongChia2);
                    GlobalVariable1.DaoLangPosition.Dao5U = 1;

                    GlobalVariable1.DaoLangPosition.Dao4SV = GlobalVariable1.DaoLangPosition.Dao4Min + GlobalVariable1.DaoLangPosition.Dao_Dao;//cho dao về vị trí trung tâm của nó
                }
                // Xả 1 - Dao 4
                else if (GlobalVariable1.DaoLangPosition.Dao4Max > GlobalVariable1.DaoLangPosition.Dao4Min && GlobalVariable1.DaoLangPosition.Dao5Max == GlobalVariable1.DaoLangPosition.Dao5Min)
                {
                    GlobalVariable1.DaoLangPosition.Dao4SV = (int)(rongChia2);
                    GlobalVariable1.DaoLangPosition.Dao4U = 1;

                    GlobalVariable1.DaoLangPosition.Dao5SV = GlobalVariable1.DaoLangPosition.Dao5Min + GlobalVariable1.DaoLangPosition.Dao_Dao;//cho dao về vị trí trung tâm của nó
                }
                #endregion

                GlobalVariable1.DaoLangPosition.HutSv = (int)rongChia2;

                #region Lang
                if (GlobalVariable1.DaoLangPosition.Lang4Max >= lanPosition)
                {
                    GlobalVariable1.DaoLangPosition.Lang4U = GlobalVariable1.DaoLangPosition.Lang5U = 1;
                    GlobalVariable1.DaoLangPosition.Lang4Sv = GlobalVariable1.DaoLangPosition.Lang5Sv = (int)(lanPosition);

                    GlobalVariable1.DaoLangPosition.Lang3Sv = GlobalVariable1.DaoLangPosition.Lang6Sv = GlobalVariable1.DaoLangPosition.Lang3Min + GlobalVariable1.DaoLangPosition.Lang_Lang;
                    GlobalVariable1.DaoLangPosition.Lang2Sv = GlobalVariable1.DaoLangPosition.Lang7Sv = GlobalVariable1.DaoLangPosition.Lang2Min + GlobalVariable1.DaoLangPosition.Lang_Lang;
                    GlobalVariable1.DaoLangPosition.Lang1Sv = GlobalVariable1.DaoLangPosition.Lang8Sv = GlobalVariable1.DaoLangPosition.Lang1Min + GlobalVariable1.DaoLangPosition.Lang_Lang;
                }
                else if (GlobalVariable1.DaoLangPosition.Lang3Max >= lanPosition)
                {
                    GlobalVariable1.DaoLangPosition.Lang3U = GlobalVariable1.DaoLangPosition.Lang6U = 1;
                    GlobalVariable1.DaoLangPosition.Lang3Sv = GlobalVariable1.DaoLangPosition.Lang6Sv = (int)(lanPosition);

                    GlobalVariable1.DaoLangPosition.Lang4Sv = GlobalVariable1.DaoLangPosition.Lang5Sv = GlobalVariable1.DaoLangPosition.Lang4Min + GlobalVariable1.DaoLangPosition.Lang_Lang;
                    GlobalVariable1.DaoLangPosition.Lang2Sv = GlobalVariable1.DaoLangPosition.Lang7Sv = GlobalVariable1.DaoLangPosition.Lang2Min + GlobalVariable1.DaoLangPosition.Lang_Lang;
                    GlobalVariable1.DaoLangPosition.Lang1Sv = GlobalVariable1.DaoLangPosition.Lang8Sv = GlobalVariable1.DaoLangPosition.Lang1Min + GlobalVariable1.DaoLangPosition.Lang_Lang;
                }
                else if (GlobalVariable1.DaoLangPosition.Lang2Max >= lanPosition)
                {
                    GlobalVariable1.DaoLangPosition.Lang2U = GlobalVariable1.DaoLangPosition.Lang7U = 1;
                    GlobalVariable1.DaoLangPosition.Lang2Sv = GlobalVariable1.DaoLangPosition.Lang7Sv = (int)(lanPosition);

                    GlobalVariable1.DaoLangPosition.Lang4Sv = GlobalVariable1.DaoLangPosition.Lang5Sv = GlobalVariable1.DaoLangPosition.Lang4Min + GlobalVariable1.DaoLangPosition.Lang_Lang;
                    GlobalVariable1.DaoLangPosition.Lang3Sv = GlobalVariable1.DaoLangPosition.Lang6Sv = GlobalVariable1.DaoLangPosition.Lang3Min + GlobalVariable1.DaoLangPosition.Lang_Lang;
                    GlobalVariable1.DaoLangPosition.Lang1Sv = GlobalVariable1.DaoLangPosition.Lang8Sv = GlobalVariable1.DaoLangPosition.Lang1Min + GlobalVariable1.DaoLangPosition.Lang_Lang;
                }
                else if (GlobalVariable1.DaoLangPosition.Lang1Max >= lanPosition)
                {
                    GlobalVariable1.DaoLangPosition.Lang1U = GlobalVariable1.DaoLangPosition.Lang8U = 1;
                    GlobalVariable1.DaoLangPosition.Lang1Sv = GlobalVariable1.DaoLangPosition.Lang8Sv = (int)(lanPosition);

                    GlobalVariable1.DaoLangPosition.Lang4Sv = GlobalVariable1.DaoLangPosition.Lang5Sv = GlobalVariable1.DaoLangPosition.Lang4Min + GlobalVariable1.DaoLangPosition.Lang_Lang;
                    GlobalVariable1.DaoLangPosition.Lang3Sv = GlobalVariable1.DaoLangPosition.Lang6Sv = GlobalVariable1.DaoLangPosition.Lang3Min + GlobalVariable1.DaoLangPosition.Lang_Lang;
                    GlobalVariable1.DaoLangPosition.Lang2Sv = GlobalVariable1.DaoLangPosition.Lang7Sv = GlobalVariable1.DaoLangPosition.Lang2Min + GlobalVariable1.DaoLangPosition.Lang_Lang;
                }
                #endregion
            }

            // Xả = 2 --luôn bật dao 3 tại vị trí 0
            else if (donHang.Xa == 2)
            {
                GlobalVariable1.DaoLangPosition.Dao3U = 1;

                double rongChia2 = rongTotal / 2 * 10;//đang là đơn vị mm, x10 để loại bỏ số lẻ
                double lanPosition1 = canh * 10;//vi trí lằn 1
                double lanPosition2 = canh * 10 + cao * 10;//vi trí lằn2

                #region Dao
                // Dao 1 hoac 2
                if (GlobalVariable1.DaoLangPosition.Dao2Max > GlobalVariable1.DaoLangPosition.Dao2Min && GlobalVariable1.DaoLangPosition.Dao1Max > GlobalVariable1.DaoLangPosition.Dao1Min)
                {
                    if (GlobalVariable1.DaoLangPosition.Dao2Max >= rongChia2)
                    {
                        GlobalVariable1.DaoLangPosition.Dao2SV = (int)(rongChia2);
                        GlobalVariable1.DaoLangPosition.Dao2U = 1;//bat dao

                        GlobalVariable1.DaoLangPosition.Dao1SV = GlobalVariable1.DaoLangPosition.Dao1Min + GlobalVariable1.DaoLangPosition.Dao_Dao;//cho dao về vị trí trung tâm của nó
                    }
                    else
                    {
                        GlobalVariable1.DaoLangPosition.Dao1SV = (int)(rongChia2);
                        GlobalVariable1.DaoLangPosition.Dao1U = 1;

                        GlobalVariable1.DaoLangPosition.Dao2SV = GlobalVariable1.DaoLangPosition.Dao2Min + GlobalVariable1.DaoLangPosition.Dao_Dao;//cho dao về vị trí trung tâm của nó
                    }
                }
                //Dao 1
                else if (GlobalVariable1.DaoLangPosition.Dao1Max > GlobalVariable1.DaoLangPosition.Dao1Min && GlobalVariable1.DaoLangPosition.Dao2Max == GlobalVariable1.DaoLangPosition.Dao2Min)
                {
                    GlobalVariable1.DaoLangPosition.Dao1SV = (int)(rongChia2);
                    GlobalVariable1.DaoLangPosition.Dao1U = 1;

                    GlobalVariable1.DaoLangPosition.Dao2SV = GlobalVariable1.DaoLangPosition.Dao2Min + GlobalVariable1.DaoLangPosition.Dao_Dao;//cho dao về vị trí trung tâm của nó
                }
                //Dao 2
                else if (GlobalVariable1.DaoLangPosition.Dao2Max > GlobalVariable1.DaoLangPosition.Dao2Min && GlobalVariable1.DaoLangPosition.Dao1Max == GlobalVariable1.DaoLangPosition.Dao1Min)
                {
                    GlobalVariable1.DaoLangPosition.Dao1SV = (int)(rongChia2);
                    GlobalVariable1.DaoLangPosition.Dao1U = 1;

                    GlobalVariable1.DaoLangPosition.Dao2SV = GlobalVariable1.DaoLangPosition.Dao2Min + GlobalVariable1.DaoLangPosition.Dao_Dao;//cho dao về vị trí trung tâm của nó
                }

                //Dao 4 hoac 5
                if (GlobalVariable1.DaoLangPosition.Dao5Max > GlobalVariable1.DaoLangPosition.Dao5Min && GlobalVariable1.DaoLangPosition.Dao4Max > GlobalVariable1.DaoLangPosition.Dao4Min)
                {
                    if (GlobalVariable1.DaoLangPosition.Dao4Max >= rongChia2)
                    {
                        GlobalVariable1.DaoLangPosition.Dao4SV = (int)(rongChia2);
                        GlobalVariable1.DaoLangPosition.Dao4U = 1;//bat dao

                        GlobalVariable1.DaoLangPosition.Dao5SV = GlobalVariable1.DaoLangPosition.Dao5Min + GlobalVariable1.DaoLangPosition.Dao_Dao;//cho dao về vị trí trung tâm của nó
                    }
                    else
                    {
                        GlobalVariable1.DaoLangPosition.Dao5SV = (int)(rongChia2);
                        GlobalVariable1.DaoLangPosition.Dao5U = 1;

                        GlobalVariable1.DaoLangPosition.Dao4SV = GlobalVariable1.DaoLangPosition.Dao4Min + GlobalVariable1.DaoLangPosition.Dao_Dao;//cho dao về vị trí trung tâm của nó
                    }
                }
                //Dao 5
                else if (GlobalVariable1.DaoLangPosition.Dao5Max > GlobalVariable1.DaoLangPosition.Dao5Min && GlobalVariable1.DaoLangPosition.Dao4Max == GlobalVariable1.DaoLangPosition.Dao4Min)
                {
                    GlobalVariable1.DaoLangPosition.Dao5SV = (int)(rongChia2);
                    GlobalVariable1.DaoLangPosition.Dao5U = 1;

                    GlobalVariable1.DaoLangPosition.Dao4SV = GlobalVariable1.DaoLangPosition.Dao4Min + GlobalVariable1.DaoLangPosition.Dao_Dao;//cho dao về vị trí trung tâm của nó
                }
                //Dao 4
                else if (GlobalVariable1.DaoLangPosition.Dao4Max > GlobalVariable1.DaoLangPosition.Dao4Min && GlobalVariable1.DaoLangPosition.Dao5Max == GlobalVariable1.DaoLangPosition.Dao5Min)
                {
                    GlobalVariable1.DaoLangPosition.Dao4SV = (int)(rongChia2);
                    GlobalVariable1.DaoLangPosition.Dao4U = 1;

                    GlobalVariable1.DaoLangPosition.Dao5SV = GlobalVariable1.DaoLangPosition.Dao5Min + GlobalVariable1.DaoLangPosition.Dao_Dao;//cho dao về vị trí trung tâm của nó
                }
                #endregion

                GlobalVariable1.DaoLangPosition.HutSv = (int)rongChia2;

                #region Lang
                #region Lan 1
                if (GlobalVariable1.DaoLangPosition.Lang4Max >= lanPosition1)
                {
                    GlobalVariable1.DaoLangPosition.Lang4U = GlobalVariable1.DaoLangPosition.Lang5U = 1;
                    GlobalVariable1.DaoLangPosition.Lang4Sv = GlobalVariable1.DaoLangPosition.Lang5Sv = (int)(lanPosition1);

                    GlobalVariable1.DaoLangPosition.Lang3Sv = GlobalVariable1.DaoLangPosition.Lang6Sv = GlobalVariable1.DaoLangPosition.Lang3Min + GlobalVariable1.DaoLangPosition.Lang_Lang;
                    GlobalVariable1.DaoLangPosition.Lang2Sv = GlobalVariable1.DaoLangPosition.Lang7Sv = GlobalVariable1.DaoLangPosition.Lang2Min + GlobalVariable1.DaoLangPosition.Lang_Lang;
                    GlobalVariable1.DaoLangPosition.Lang1Sv = GlobalVariable1.DaoLangPosition.Lang8Sv = GlobalVariable1.DaoLangPosition.Lang1Min + GlobalVariable1.DaoLangPosition.Lang_Lang;

                    #region Lan 2
                    if (GlobalVariable1.DaoLangPosition.Lang3Max >= lanPosition2)
                    {
                        GlobalVariable1.DaoLangPosition.Lang3U = GlobalVariable1.DaoLangPosition.Lang6U = 1;
                        GlobalVariable1.DaoLangPosition.Lang3Sv = GlobalVariable1.DaoLangPosition.Lang6Sv = (int)(lanPosition2);

                        GlobalVariable1.DaoLangPosition.Lang2Sv = GlobalVariable1.DaoLangPosition.Lang7Sv = GlobalVariable1.DaoLangPosition.Lang2Min + GlobalVariable1.DaoLangPosition.Lang_Lang;
                        GlobalVariable1.DaoLangPosition.Lang1Sv = GlobalVariable1.DaoLangPosition.Lang8Sv = GlobalVariable1.DaoLangPosition.Lang1Min + GlobalVariable1.DaoLangPosition.Lang_Lang;
                    }
                    else if (GlobalVariable1.DaoLangPosition.Lang2Max >= lanPosition2)
                    {
                        GlobalVariable1.DaoLangPosition.Lang2U = GlobalVariable1.DaoLangPosition.Lang7U = 1;
                        GlobalVariable1.DaoLangPosition.Lang2Sv = GlobalVariable1.DaoLangPosition.Lang7Sv = (int)(lanPosition2);

                        GlobalVariable1.DaoLangPosition.Lang3Sv = GlobalVariable1.DaoLangPosition.Lang6Sv = GlobalVariable1.DaoLangPosition.Lang3Min + GlobalVariable1.DaoLangPosition.Lang_Lang;
                        GlobalVariable1.DaoLangPosition.Lang1Sv = GlobalVariable1.DaoLangPosition.Lang8Sv = GlobalVariable1.DaoLangPosition.Lang1Min + GlobalVariable1.DaoLangPosition.Lang_Lang;
                    }
                    else if (GlobalVariable1.DaoLangPosition.Lang1Max >= lanPosition2)
                    {
                        GlobalVariable1.DaoLangPosition.Lang1U = GlobalVariable1.DaoLangPosition.Lang8U = 1;
                        GlobalVariable1.DaoLangPosition.Lang1Sv = GlobalVariable1.DaoLangPosition.Lang8Sv = (int)(lanPosition2);

                        GlobalVariable1.DaoLangPosition.Lang3Sv = GlobalVariable1.DaoLangPosition.Lang6Sv = GlobalVariable1.DaoLangPosition.Lang3Min + GlobalVariable1.DaoLangPosition.Lang_Lang;
                        GlobalVariable1.DaoLangPosition.Lang2Sv = GlobalVariable1.DaoLangPosition.Lang7Sv = GlobalVariable1.DaoLangPosition.Lang2Min + GlobalVariable1.DaoLangPosition.Lang_Lang;
                    }
                    #endregion
                }
                else if (GlobalVariable1.DaoLangPosition.Lang3Max >= lanPosition1)
                {
                    GlobalVariable1.DaoLangPosition.Lang3U = GlobalVariable1.DaoLangPosition.Lang6U = 1;
                    GlobalVariable1.DaoLangPosition.Lang3Sv = GlobalVariable1.DaoLangPosition.Lang6Sv = (int)(lanPosition1);

                    GlobalVariable1.DaoLangPosition.Lang4Sv = GlobalVariable1.DaoLangPosition.Lang5Sv = GlobalVariable1.DaoLangPosition.Lang4Min + GlobalVariable1.DaoLangPosition.Lang_Lang;
                    GlobalVariable1.DaoLangPosition.Lang2Sv = GlobalVariable1.DaoLangPosition.Lang7Sv = GlobalVariable1.DaoLangPosition.Lang2Min + GlobalVariable1.DaoLangPosition.Lang_Lang;
                    GlobalVariable1.DaoLangPosition.Lang1Sv = GlobalVariable1.DaoLangPosition.Lang8Sv = GlobalVariable1.DaoLangPosition.Lang1Min + GlobalVariable1.DaoLangPosition.Lang_Lang;

                    #region Lan 2
                    if (GlobalVariable1.DaoLangPosition.Lang2Max >= lanPosition2)
                    {
                        GlobalVariable1.DaoLangPosition.Lang2U = GlobalVariable1.DaoLangPosition.Lang7U = 1;
                        GlobalVariable1.DaoLangPosition.Lang2Sv = GlobalVariable1.DaoLangPosition.Lang7Sv = (int)(lanPosition2);

                        GlobalVariable1.DaoLangPosition.Lang1Sv = GlobalVariable1.DaoLangPosition.Lang8Sv = GlobalVariable1.DaoLangPosition.Lang1Min + GlobalVariable1.DaoLangPosition.Lang_Lang;
                    }
                    else if (GlobalVariable1.DaoLangPosition.Lang1Max >= lanPosition2)
                    {
                        GlobalVariable1.DaoLangPosition.Lang1U = GlobalVariable1.DaoLangPosition.Lang8U = 1;
                        GlobalVariable1.DaoLangPosition.Lang1Sv = GlobalVariable1.DaoLangPosition.Lang8Sv = (int)(lanPosition2);

                        GlobalVariable1.DaoLangPosition.Lang2Sv = GlobalVariable1.DaoLangPosition.Lang7Sv = GlobalVariable1.DaoLangPosition.Lang2Min + GlobalVariable1.DaoLangPosition.Lang_Lang;
                    }
                    #endregion
                }
                else if (GlobalVariable1.DaoLangPosition.Lang2Max >= lanPosition1)
                {
                    GlobalVariable1.DaoLangPosition.Lang2U = GlobalVariable1.DaoLangPosition.Lang7U = 1;
                    GlobalVariable1.DaoLangPosition.Lang2Sv = GlobalVariable1.DaoLangPosition.Lang7Sv = (int)(lanPosition1);

                    GlobalVariable1.DaoLangPosition.Lang4Sv = GlobalVariable1.DaoLangPosition.Lang5Sv = GlobalVariable1.DaoLangPosition.Lang4Min + GlobalVariable1.DaoLangPosition.Lang_Lang;
                    GlobalVariable1.DaoLangPosition.Lang3Sv = GlobalVariable1.DaoLangPosition.Lang6Sv = GlobalVariable1.DaoLangPosition.Lang3Min + GlobalVariable1.DaoLangPosition.Lang_Lang;
                    GlobalVariable1.DaoLangPosition.Lang1Sv = GlobalVariable1.DaoLangPosition.Lang8Sv = GlobalVariable1.DaoLangPosition.Lang1Min + GlobalVariable1.DaoLangPosition.Lang_Lang;

                    #region Lan 2
                    if (GlobalVariable1.DaoLangPosition.Lang1Max >= lanPosition2)
                    {
                        GlobalVariable1.DaoLangPosition.Lang1U = GlobalVariable1.DaoLangPosition.Lang8U = 1;
                        GlobalVariable1.DaoLangPosition.Lang1Sv = GlobalVariable1.DaoLangPosition.Lang8Sv = (int)(lanPosition2);
                    }
                    #endregion
                }
                else if (GlobalVariable1.DaoLangPosition.Lang1Max >= lanPosition1)
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

                if (GlobalVariable1.DaoLangPosition.Dao2Max > GlobalVariable1.DaoLangPosition.Dao2Min && GlobalVariable1.DaoLangPosition.Dao1Max > GlobalVariable1.DaoLangPosition.Dao1Min)
                {
                    GlobalVariable1.DaoLangPosition.Dao1SV = GlobalVariable1.DaoLangPosition.Dao5SV = (int)rongChia2;
                    GlobalVariable1.DaoLangPosition.Dao1U = GlobalVariable1.DaoLangPosition.Dao5U = 1;

                    GlobalVariable1.DaoLangPosition.Dao2SV = GlobalVariable1.DaoLangPosition.Dao4SV = (int)(rongChia2 - rongA * 10);
                    GlobalVariable1.DaoLangPosition.Dao2U = GlobalVariable1.DaoLangPosition.Dao4U = 1;

                    GlobalVariable1.DaoLangPosition.HutSv = (int)rongChia2;

                    #region Lang
                    #region Lan 1
                    if (GlobalVariable1.DaoLangPosition.Lang4Max >= lanPosition1)
                    {
                        GlobalVariable1.DaoLangPosition.Lang4U = GlobalVariable1.DaoLangPosition.Lang5U = 1;
                        GlobalVariable1.DaoLangPosition.Lang4Sv = GlobalVariable1.DaoLangPosition.Lang5Sv = (int)(lanPosition1);

                        GlobalVariable1.DaoLangPosition.Lang3Sv = GlobalVariable1.DaoLangPosition.Lang6Sv = GlobalVariable1.DaoLangPosition.Lang3Min + GlobalVariable1.DaoLangPosition.Lang_Lang;
                        GlobalVariable1.DaoLangPosition.Lang2Sv = GlobalVariable1.DaoLangPosition.Lang7Sv = GlobalVariable1.DaoLangPosition.Lang2Min + GlobalVariable1.DaoLangPosition.Lang_Lang;
                        GlobalVariable1.DaoLangPosition.Lang1Sv = GlobalVariable1.DaoLangPosition.Lang8Sv = GlobalVariable1.DaoLangPosition.Lang1Min + GlobalVariable1.DaoLangPosition.Lang_Lang;

                        #region Lan 2
                        if (GlobalVariable1.DaoLangPosition.Lang3Max >= lanPosition2)
                        {
                            GlobalVariable1.DaoLangPosition.Lang3U = GlobalVariable1.DaoLangPosition.Lang6U = 1;
                            GlobalVariable1.DaoLangPosition.Lang3Sv = GlobalVariable1.DaoLangPosition.Lang6Sv = (int)(lanPosition2);

                            GlobalVariable1.DaoLangPosition.Lang2Sv = GlobalVariable1.DaoLangPosition.Lang7Sv = GlobalVariable1.DaoLangPosition.Lang2Min + GlobalVariable1.DaoLangPosition.Lang_Lang;
                            GlobalVariable1.DaoLangPosition.Lang1Sv = GlobalVariable1.DaoLangPosition.Lang8Sv = GlobalVariable1.DaoLangPosition.Lang1Min + GlobalVariable1.DaoLangPosition.Lang_Lang;

                            if (GlobalVariable1.DaoLangPosition.Lang2Max >= lanPosition3)
                            {
                                GlobalVariable1.DaoLangPosition.Lang2U = GlobalVariable1.DaoLangPosition.Lang7U = 1;
                                GlobalVariable1.DaoLangPosition.Lang2Sv = GlobalVariable1.DaoLangPosition.Lang7Sv = (int)(lanPosition3);

                                GlobalVariable1.DaoLangPosition.Lang1Sv = GlobalVariable1.DaoLangPosition.Lang8Sv = GlobalVariable1.DaoLangPosition.Lang1Min + GlobalVariable1.DaoLangPosition.Lang_Lang;
                            }
                            else if (GlobalVariable1.DaoLangPosition.Lang1Max >= lanPosition3)
                            {
                                GlobalVariable1.DaoLangPosition.Lang1U = GlobalVariable1.DaoLangPosition.Lang8U = 1;
                                GlobalVariable1.DaoLangPosition.Lang1Sv = GlobalVariable1.DaoLangPosition.Lang8Sv = (int)(lanPosition3);
                            }
                        }
                        else if (GlobalVariable1.DaoLangPosition.Lang2Max >= lanPosition2)
                        {
                            GlobalVariable1.DaoLangPosition.Lang2U = GlobalVariable1.DaoLangPosition.Lang7U = 1;
                            GlobalVariable1.DaoLangPosition.Lang2Sv = GlobalVariable1.DaoLangPosition.Lang7Sv = (int)(lanPosition2);

                            GlobalVariable1.DaoLangPosition.Lang1U = GlobalVariable1.DaoLangPosition.Lang8U = 1;
                            GlobalVariable1.DaoLangPosition.Lang8Sv = GlobalVariable1.DaoLangPosition.Lang8Sv = (int)(lanPosition3);
                        }
                        #endregion
                    }
                    else if (GlobalVariable1.DaoLangPosition.Lang3Max >= lanPosition1)
                    {
                        GlobalVariable1.DaoLangPosition.Lang4Sv = GlobalVariable1.DaoLangPosition.Lang5Sv = GlobalVariable1.DaoLangPosition.Lang4Min + GlobalVariable1.DaoLangPosition.Lang_Lang;

                        GlobalVariable1.DaoLangPosition.Lang3U = GlobalVariable1.DaoLangPosition.Lang6U = 1;
                        GlobalVariable1.DaoLangPosition.Lang3Sv = GlobalVariable1.DaoLangPosition.Lang6Sv = (int)(lanPosition1);

                        GlobalVariable1.DaoLangPosition.Lang2U = GlobalVariable1.DaoLangPosition.Lang7U = 1;
                        GlobalVariable1.DaoLangPosition.Lang2Sv = GlobalVariable1.DaoLangPosition.Lang7Sv = (int)(lanPosition2);

                        GlobalVariable1.DaoLangPosition.Lang1U = GlobalVariable1.DaoLangPosition.Lang8U = 1;
                        GlobalVariable1.DaoLangPosition.Lang1Sv = GlobalVariable1.DaoLangPosition.Lang8Sv = (int)(lanPosition3);
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
                GlobalVariable1.DaoLangPosition.Dao3U = 1;
                double rongChia2 = rongTotal / 2 * 10;//đang là đơn vị mm, x10 để loại bỏ số lẻ
                double lanPosition1 = canh * 10;//vi trí lằn 1
                double lanPosition2 = (cao + canh) * 10;//vi trí lằn 1
                double lanPosition3 = (rongA + canh) * 10;//vi trí lằn2
                double lanPosition4 = (rongA + canh + cao) * 10;//vi trí lằn2

                if (GlobalVariable1.DaoLangPosition.Dao2Max > GlobalVariable1.DaoLangPosition.Dao2Min && GlobalVariable1.DaoLangPosition.Dao1Max > GlobalVariable1.DaoLangPosition.Dao1Min)
                {
                    GlobalVariable1.DaoLangPosition.Dao1SV = GlobalVariable1.DaoLangPosition.Dao5SV = (int)rongChia2;
                    GlobalVariable1.DaoLangPosition.Dao1U = GlobalVariable1.DaoLangPosition.Dao5U = 1;

                    GlobalVariable1.DaoLangPosition.Dao2SV = GlobalVariable1.DaoLangPosition.Dao4SV = (int)(rongChia2 - rongA * 10);
                    GlobalVariable1.DaoLangPosition.Dao2U = GlobalVariable1.DaoLangPosition.Dao4U = 1;

                    GlobalVariable1.DaoLangPosition.HutSv = (int)rongChia2;

                    #region lan
                    GlobalVariable1.DaoLangPosition.Lang4U = GlobalVariable1.DaoLangPosition.Lang5U = 1;
                    GlobalVariable1.DaoLangPosition.Lang4Sv = GlobalVariable1.DaoLangPosition.Lang5Sv = (int)(lanPosition1);

                    GlobalVariable1.DaoLangPosition.Lang3U = GlobalVariable1.DaoLangPosition.Lang6U = 1;
                    GlobalVariable1.DaoLangPosition.Lang3Sv = GlobalVariable1.DaoLangPosition.Lang6Sv = (int)(lanPosition2);

                    GlobalVariable1.DaoLangPosition.Lang2U = GlobalVariable1.DaoLangPosition.Lang7U = 1;
                    GlobalVariable1.DaoLangPosition.Lang2Sv = GlobalVariable1.DaoLangPosition.Lang7Sv = (int)(lanPosition3);

                    GlobalVariable1.DaoLangPosition.Lang1U = GlobalVariable1.DaoLangPosition.Lang8U = 1;
                    GlobalVariable1.DaoLangPosition.Lang1Sv = GlobalVariable1.DaoLangPosition.Lang8Sv = (int)(lanPosition4);
                    #endregion
                }
                else
                {
                    MessageBox.Show($"Xả 3 yêu cầu tất cả các dao phải hoạt động.\rKiểm tra lại các dao.", "CẢNH BÁO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                GlobalVariable1.DaoLangPosition.Dao1SV= GlobalVariable1.DaoLangPosition.Dao5SV = GlobalVariable1.DaoLangPosition.Dao1OffSV;
                GlobalVariable1.DaoLangPosition.Dao2SV= GlobalVariable1.DaoLangPosition.Dao4SV = GlobalVariable1.DaoLangPosition.Dao2OffSV;
                GlobalVariable1.DaoLangPosition.Dao3SV = 0;

                GlobalVariable1.DaoLangPosition.Lang1Sv= GlobalVariable1.DaoLangPosition.Lang8Sv = GlobalVariable1.DaoLangPosition.Lang1OffSV;
                GlobalVariable1.DaoLangPosition.Lang2Sv= GlobalVariable1.DaoLangPosition.Lang7Sv = GlobalVariable1.DaoLangPosition.Lang2OffSV;
                GlobalVariable1.DaoLangPosition.Lang3Sv= GlobalVariable1.DaoLangPosition.Lang6Sv = GlobalVariable1.DaoLangPosition.Lang3OffSV;
                GlobalVariable1.DaoLangPosition.Lang4Sv= GlobalVariable1.DaoLangPosition.Lang5Sv = GlobalVariable1.DaoLangPosition.Lang4OffSV;
            }
        }
    }
}
