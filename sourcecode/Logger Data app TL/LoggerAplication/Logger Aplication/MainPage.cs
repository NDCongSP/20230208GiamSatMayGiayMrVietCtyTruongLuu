using Dapper;
using EasyScada.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Logger_Aplication
{
    public partial class MainPage : Form
    {
        private MaySongLodModel _maySongBInfo = new MaySongLodModel() { MaySong = "B" };
        private MaySongLodModel _maySongCInfo = new MaySongLodModel() { MaySong = "C" };
        private MaySongLodModel _maySongEInfo = new MaySongLodModel() { MaySong = "E" };

        Timer _t1 = new Timer();

        public MainPage()
        {
            InitializeComponent();
            Load += MainPage_Load;
        }

        private void MainPage_Load(object sender, EventArgs e)
        {
            easyDriverConnector1.Started += EasyDriverConnector1_Started;


            _t1.Interval = 100;
            _t1.Enabled = true;
            _t1.Tick += (s, o) =>
            {
                Timer t = (Timer)s;
                t.Enabled = false;
                this.Invoke((MethodInvoker)delegate { labTime.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"); });
                t.Enabled = true;
            };
        }

        private void EasyDriverConnector1_Started(object sender, EventArgs e)
        {
            easyDriverConnector1.GetTag("Local Station/StationC/StC_HMI/StC_Command").ValueChanged += StC_Command_ValueChanged;//Su kien log file
            easyDriverConnector1.GetTag("Local Station/StationB/StB_HMI/StB_Command").ValueChanged += StB_Command_ValueChanged;//Su kien log file
            easyDriverConnector1.GetTag("Local Station/StationE/StE_HMI/StE_Command").ValueChanged += StE_Command_ValueChanged;//Su kien log file

            #region STC
            easyDriverConnector1.GetTag("Local Station/StationC/StC_HMI/StC_FaceIn").ValueChanged += StC_FaceIn_ValueChanged; ;
            easyDriverConnector1.GetTag("Local Station/StationC/StC_HMI/StC_WaveIn").ValueChanged += StC_WaveIn_ValueChanged; ;
            easyDriverConnector1.GetTag("Local Station/StationC/StC_HMI/StC_PaperOut").ValueChanged += StC_PaperOut_ValueChanged; ;
            easyDriverConnector1.GetTag("Local Station/StationC/StC_HMI/StC_ConRemain").ValueChanged += StC_ConRemain_ValueChanged; ;
            easyDriverConnector1.GetTag("Local Station/StationC/StC_HMI/StC_SetLength").ValueChanged += StC_SetLength_ValueChanged; ;
            easyDriverConnector1.GetTag("Local Station/StationC/StC_HMI/StC_RemainRun").ValueChanged += StC_RemainRun_ValueChanged; ;
            easyDriverConnector1.GetTag("Local Station/StationC/StC_HMI/StC_TypeRst").ValueChanged += StC_TypeRst_ValueChanged; ;
            #endregion

            #region STB
            easyDriverConnector1.GetTag("Local Station/StationB/StB_HMI/StB_FaceIn").ValueChanged += StB_FaceIn_ValueChanged; ;
            easyDriverConnector1.GetTag("Local Station/StationB/StB_HMI/StB_WaveIn").ValueChanged += StB_WaveIn_ValueChanged; ;
            easyDriverConnector1.GetTag("Local Station/StationB/StB_HMI/StB_PaperOut").ValueChanged += StB_PaperOut_ValueChanged; ;
            easyDriverConnector1.GetTag("Local Station/StationB/StB_HMI/StB_ConRemain").ValueChanged += StB_ConRemain_ValueChanged; ;
            easyDriverConnector1.GetTag("Local Station/StationB/StB_HMI/StB_SetLength").ValueChanged += StB_SetLength_ValueChanged; ;
            easyDriverConnector1.GetTag("Local Station/StationB/StB_HMI/StB_RemainRun").ValueChanged += StB_RemainRun_ValueChanged; ;
            easyDriverConnector1.GetTag("Local Station/StationB/StB_HMI/StB_TypeRst").ValueChanged += StB_TypeRst_ValueChanged; ;
            #endregion

            #region STE
            easyDriverConnector1.GetTag("Local Station/StationE/StE_HMI/StE_FaceIn").ValueChanged += StE_FaceIn_ValueChanged; ;
            easyDriverConnector1.GetTag("Local Station/StationE/StE_HMI/StE_WaveIn").ValueChanged += StE_WaveIn_ValueChanged; ;
            easyDriverConnector1.GetTag("Local Station/StationE/StE_HMI/StE_PaperOut").ValueChanged += StE_PaperOut_ValueChanged; ;
            easyDriverConnector1.GetTag("Local Station/StationE/StE_HMI/StE_ConRemain").ValueChanged += StE_ConRemain_ValueChanged; ;
            easyDriverConnector1.GetTag("Local Station/StationE/StE_HMI/StE_SetLength").ValueChanged += StE_SetLength_ValueChanged; ;
            easyDriverConnector1.GetTag("Local Station/StationE/StE_HMI/StE_RemainRun").ValueChanged += StE_RemainRun_ValueChanged; ;
            easyDriverConnector1.GetTag("Local Station/StationE/StE_HMI/StE_TypeRst").ValueChanged += StE_TypeRst_ValueChanged; ;
            #endregion

            #region get giá trị ban đầu tag
            StC_FaceIn_ValueChanged(easyDriverConnector1.GetTag("Local Station/StationC/StC_HMI/StC_FaceIn"),
                             new TagValueChangedEventArgs(easyDriverConnector1.GetTag("Local Station/StationC/StC_HMI/StC_FaceIn")
                             , "", easyDriverConnector1.GetTag("Local Station/StationC/StC_HMI/StC_FaceIn").Value));
            StC_WaveIn_ValueChanged(easyDriverConnector1.GetTag("Local Station/StationC/StC_HMI/StC_WaveIn"),
                             new TagValueChangedEventArgs(easyDriverConnector1.GetTag("Local Station/StationC/StC_HMI/StC_WaveIn")
                             , "", easyDriverConnector1.GetTag("Local Station/StationC/StC_HMI/StC_WaveIn").Value));
            StC_PaperOut_ValueChanged(easyDriverConnector1.GetTag("Local Station/StationC/StC_HMI/StC_PaperOut"),
                             new TagValueChangedEventArgs(easyDriverConnector1.GetTag("Local Station/StationC/StC_HMI/StC_PaperOut")
                             , "", easyDriverConnector1.GetTag("Local Station/StationC/StC_HMI/StC_PaperOut").Value));
            StC_ConRemain_ValueChanged(easyDriverConnector1.GetTag("Local Station/StationC/StC_HMI/StC_ConRemain"),
                             new TagValueChangedEventArgs(easyDriverConnector1.GetTag("Local Station/StationC/StC_HMI/StC_ConRemain")
                             , "", easyDriverConnector1.GetTag("Local Station/StationC/StC_HMI/StC_ConRemain").Value));
            StC_SetLength_ValueChanged(easyDriverConnector1.GetTag("Local Station/StationC/StC_HMI/StC_SetLength"),
                             new TagValueChangedEventArgs(easyDriverConnector1.GetTag("Local Station/StationC/StC_HMI/StC_SetLength")
                             , "", easyDriverConnector1.GetTag("Local Station/StationC/StC_HMI/StC_SetLength").Value));
            StC_RemainRun_ValueChanged(easyDriverConnector1.GetTag("Local Station/StationC/StC_HMI/StC_RemainRun"),
                             new TagValueChangedEventArgs(easyDriverConnector1.GetTag("Local Station/StationC/StC_HMI/StC_RemainRun")
                             , "", easyDriverConnector1.GetTag("Local Station/StationC/StC_HMI/StC_RemainRun").Value));
            StC_TypeRst_ValueChanged(easyDriverConnector1.GetTag("Local Station/StationC/StC_HMI/StC_TypeRst"),
                             new TagValueChangedEventArgs(easyDriverConnector1.GetTag("Local Station/StationC/StC_HMI/StC_TypeRst")
                             , "", easyDriverConnector1.GetTag("Local Station/StationC/StC_HMI/StC_TypeRst").Value));

            StB_FaceIn_ValueChanged(easyDriverConnector1.GetTag("Local Station/StationB/StB_HMI/StB_FaceIn"),
                            new TagValueChangedEventArgs(easyDriverConnector1.GetTag("Local Station/StationB/StB_HMI/StB_FaceIn")
                            , "", easyDriverConnector1.GetTag("Local Station/StationB/StB_HMI/StB_FaceIn").Value));
            StB_WaveIn_ValueChanged(easyDriverConnector1.GetTag("Local Station/StationB/StB_HMI/StB_WaveIn"),
                            new TagValueChangedEventArgs(easyDriverConnector1.GetTag("Local Station/StationB/StB_HMI/StB_WaveIn")
                            , "", easyDriverConnector1.GetTag("Local Station/StationB/StB_HMI/StB_WaveIn").Value));
            StB_PaperOut_ValueChanged(easyDriverConnector1.GetTag("Local Station/StationB/StB_HMI/StB_PaperOut"),
                            new TagValueChangedEventArgs(easyDriverConnector1.GetTag("Local Station/StationB/StB_HMI/StB_PaperOut")
                            , "", easyDriverConnector1.GetTag("Local Station/StationB/StB_HMI/StB_PaperOut").Value));
            StB_ConRemain_ValueChanged(easyDriverConnector1.GetTag("Local Station/StationB/StB_HMI/StB_ConRemain"),
                            new TagValueChangedEventArgs(easyDriverConnector1.GetTag("Local Station/StationB/StB_HMI/StB_ConRemain")
                            , "", easyDriverConnector1.GetTag("Local Station/StationB/StB_HMI/StB_ConRemain").Value));
            StB_SetLength_ValueChanged(easyDriverConnector1.GetTag("Local Station/StationB/StB_HMI/StB_SetLength"),
                            new TagValueChangedEventArgs(easyDriverConnector1.GetTag("Local Station/StationB/StB_HMI/StB_SetLength")
                            , "", easyDriverConnector1.GetTag("Local Station/StationB/StB_HMI/StB_SetLength").Value));
            StB_RemainRun_ValueChanged(easyDriverConnector1.GetTag("Local Station/StationB/StB_HMI/StB_RemainRun"),
                            new TagValueChangedEventArgs(easyDriverConnector1.GetTag("Local Station/StationB/StB_HMI/StB_RemainRun")
                            , "", easyDriverConnector1.GetTag("Local Station/StationB/StB_HMI/StB_RemainRun").Value));
            StB_TypeRst_ValueChanged(easyDriverConnector1.GetTag("Local Station/StationB/StB_HMI/StB_TypeRst"),
                             new TagValueChangedEventArgs(easyDriverConnector1.GetTag("Local Station/StationB/StB_HMI/StB_TypeRst")
                             , "", easyDriverConnector1.GetTag("Local Station/StationB/StB_HMI/StB_TypeRst").Value));

            StE_FaceIn_ValueChanged(easyDriverConnector1.GetTag("Local Station/StationE/StE_HMI/StE_FaceIn"),
                             new TagValueChangedEventArgs(easyDriverConnector1.GetTag("Local Station/StationE/StE_HMI/StE_FaceIn")
                             , "", easyDriverConnector1.GetTag("Local Station/StationE/StE_HMI/StE_FaceIn").Value));
            StE_WaveIn_ValueChanged(easyDriverConnector1.GetTag("Local Station/StationE/StE_HMI/StE_WaveIn"),
                             new TagValueChangedEventArgs(easyDriverConnector1.GetTag("Local Station/StationE/StE_HMI/StE_WaveIn")
                             , "", easyDriverConnector1.GetTag("Local Station/StationE/StE_HMI/StE_WaveIn").Value));
            StE_PaperOut_ValueChanged(easyDriverConnector1.GetTag("Local Station/StationE/StE_HMI/StE_PaperOut"),
                             new TagValueChangedEventArgs(easyDriverConnector1.GetTag("Local Station/StationE/StE_HMI/StE_PaperOut")
                             , "", easyDriverConnector1.GetTag("Local Station/StationE/StE_HMI/StE_PaperOut").Value));
            StE_ConRemain_ValueChanged(easyDriverConnector1.GetTag("Local Station/StationE/StE_HMI/StE_ConRemain"),
                             new TagValueChangedEventArgs(easyDriverConnector1.GetTag("Local Station/StationE/StE_HMI/StE_ConRemain")
                             , "", easyDriverConnector1.GetTag("Local Station/StationE/StE_HMI/StE_ConRemain").Value));
            StE_SetLength_ValueChanged(easyDriverConnector1.GetTag("Local Station/StationE/StE_HMI/StE_SetLength"),
                             new TagValueChangedEventArgs(easyDriverConnector1.GetTag("Local Station/StationE/StE_HMI/StE_SetLength")
                             , "", easyDriverConnector1.GetTag("Local Station/StationE/StE_HMI/StE_SetLength").Value));
            StE_RemainRun_ValueChanged(easyDriverConnector1.GetTag("Local Station/StationE/StE_HMI/StE_RemainRun"),
                             new TagValueChangedEventArgs(easyDriverConnector1.GetTag("Local Station/StationE/StE_HMI/StE_RemainRun")
                             , "", easyDriverConnector1.GetTag("Local Station/StationE/StE_HMI/StE_RemainRun").Value));
            StE_TypeRst_ValueChanged(easyDriverConnector1.GetTag("Local Station/StationE/StE_HMI/StE_TypeRst"),
                             new TagValueChangedEventArgs(easyDriverConnector1.GetTag("Local Station/StationE/StE_HMI/StE_TypeRst")
                             , "", easyDriverConnector1.GetTag("Local Station/StationE/StE_HMI/StE_TypeRst").Value));
            #endregion

            if (easyDriverConnector1.ConnectionStatus == ConnectionStatus.Connected)
            {
                labDriverStatus.BackColor = Color.Green;
            }
            else
            {
                labDriverStatus.BackColor = Color.Red;
            }
        }

        #region tag value change events
        private void StC_TypeRst_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            _maySongCInfo.ThaoTacThucHien = e.NewValue == "0" ? ThaoTacEnum.XoaTuDau : e.NewValue == "1" ? ThaoTacEnum.ChuyenDon : ThaoTacEnum.ChuyeKho;
        }

        private void StC_RemainRun_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            _maySongCInfo.SoMetConLai = e.NewValue;
        }

        private void StC_SetLength_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            _maySongCInfo.DoDaiCaiDat = e.NewValue;
        }

        private void StC_ConRemain_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            _maySongCInfo.TrenDanConLai = e.NewValue;
        }

        private void StC_PaperOut_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            _maySongCInfo.GhepLop = e.NewValue;
        }

        private void StC_WaveIn_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            _maySongCInfo.GiaySong = e.NewValue;
        }

        private void StC_FaceIn_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            _maySongCInfo.GiayMat = e.NewValue;
        }

        private void StB_TypeRst_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            _maySongBInfo.ThaoTacThucHien = e.NewValue == "0" ? ThaoTacEnum.XoaTuDau : e.NewValue == "1" ? ThaoTacEnum.ChuyenDon : ThaoTacEnum.ChuyeKho;
        }

        private void StB_RemainRun_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            _maySongBInfo.SoMetConLai = e.NewValue;
        }

        private void StB_SetLength_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            _maySongBInfo.DoDaiCaiDat = e.NewValue;
        }

        private void StB_ConRemain_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            _maySongBInfo.TrenDanConLai = e.NewValue;
        }

        private void StB_PaperOut_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            _maySongBInfo.GhepLop = e.NewValue;
        }

        private void StB_WaveIn_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            _maySongBInfo.GiaySong = e.NewValue;
        }

        private void StB_FaceIn_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            _maySongBInfo.GiayMat = e.NewValue;
        }

        private void StE_TypeRst_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            _maySongEInfo.ThaoTacThucHien = e.NewValue == "0" ? ThaoTacEnum.XoaTuDau : e.NewValue == "1" ? ThaoTacEnum.ChuyenDon : ThaoTacEnum.ChuyeKho;
        }

        private void StE_RemainRun_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            _maySongEInfo.SoMetConLai = e.NewValue;
        }

        private void StE_SetLength_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            _maySongEInfo.DoDaiCaiDat = e.NewValue;
        }

        private void StE_ConRemain_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            _maySongEInfo.TrenDanConLai = e.NewValue;
        }

        private void StE_PaperOut_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            _maySongEInfo.GhepLop = e.NewValue;
        }

        private void StE_WaveIn_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            _maySongEInfo.GiaySong = e.NewValue;
        }

        private void StE_FaceIn_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            _maySongEInfo.GiayMat = e.NewValue;
        }


        private void StE_Command_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue != "0")
            {
                LogData(_maySongEInfo);
            }
        }

        private void StB_Command_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue != "0")
            {
                LogData(_maySongBInfo);
            }
        }

        private void StC_Command_ValueChanged(object sender, EasyScada.Core.TagValueChangedEventArgs e)
        {
            if (e.NewValue != "0")
            {
                LogData(_maySongCInfo);
            }
        }
        #endregion


        private void button1_Click(object sender, EventArgs e)
        {
            Report form2 = new Report();
            form2.ShowDialog();
        }

        private void LogData(MaySongLodModel data)
        {
            try
            {
                using (var connection = GlobalVariables.GetConnection())
                {
                    var para = new DynamicParameters();
                    para.Add("_MaySong", data.MaySong);
                    para.Add("_GiayMat", data.GiayMat);
                    para.Add("_GiaySong", data.GiaySong);
                    para.Add("_GhepLop", data.GhepLop);
                    para.Add("_TrenDanConLai", data.TrenDanConLai);
                    para.Add("_DoDaiCaiDat", data.DoDaiCaiDat);
                    para.Add("_SoMetConLai", data.SoMetConLai);
                    para.Add("_ThaoTacThucHien", data.ThaoTacThucHien);

                    var res = connection.Execute("sp_dataMaySongInsert", para, commandType: CommandType.StoredProcedure);
                }
            }
            catch { }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmSetLength nF = new frmSetLength();
            nF.ShowDialog();
        }
    }
}