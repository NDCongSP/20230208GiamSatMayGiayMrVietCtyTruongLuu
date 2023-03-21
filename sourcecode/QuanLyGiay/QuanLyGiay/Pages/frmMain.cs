using CommonControl;
using Dapper;
using EasyScada.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace QuanLyGiay
{
    public partial class frmMain : Form
    {
        private Timer _t = new Timer();
        private Task _taskLoadOrder;

        tblDonHangModel _donHangDangChay = new tblDonHangModel();

        public frmMain()
        {
            InitializeComponent();

            Load += Form1_Load;

        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            _t.Interval = 100;
            _t.Enabled = true;
            _t.Tick += (s, o) =>
            {
                Timer t = (Timer)s;
                t.Enabled = false;

                this.Invoke((MethodInvoker)delegate { labDateTime.Text = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"); });

                t.Enabled = true;
            };

            #region Sự kiện nhấn phím tắt
            this.KeyPreview = true;
            this.KeyDown += (s, o) =>
            {
                if (o.KeyCode == Keys.F1)
                {
                    BtnOrder_Click(null, null);
                }
                else if (o.KeyCode == Keys.F2)
                {

                }
            };
            #endregion

            btnOrder.Click += BtnOrder_Click;

            easyDriverConnector1.Started += EasyDriverConnector1_Started;

            //LoadOrder();
            GlobalVariable.MyEvents.RefreshChangedEvent += (s, o) =>
            {
                if (o.NewStatus)
                {
                    LoadOrder();

                    GlobalVariable.MyEvents.Refresh = false;
                }
            };

            GlobalVariable.MyEvents.Refresh = true;
            //_taskLoadOrder = new Task(LoadOrder);
            //_taskLoadOrder.Start();
        }

        private void BtnOrder_Click(object sender, EventArgs e)
        {
            frmAddDonHang nf = new frmAddDonHang();
            nf.ShowDialog();
        }

        private void LoadOrder()
        {
            //while (true)
            {
                Console.WriteLine("vong lap task");

                using (var connection = GlobalVariable.GetDbConnection())
                {
                    var resultData = connection.Query<tblDonHangModel>("sp_tblDonHangGetOnProcess").ToList();

                    if (resultData.Count > 0)
                    {
                        this.Invoke((MethodInvoker)delegate
                        {
                            grvDH.DataSource = resultData;
                            grvDH.Columns["CreatedDate"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm:ss";
                        });
                    }
                }
                //Thread.Sleep(200);
            }
        }

        private void EasyDriverConnector1_Started(object sender, EventArgs e)
        {
            easyDriverConnector1.GetTag("Local Station/ChannelServer/DeviceCutter/LenhChuyenDon").ValueChanged += LenhChuyenDonCutter_ValueChanged;

            LenhChuyenDonCutter_ValueChanged(easyDriverConnector1.GetTag("Local Station/ChannelServer/DeviceCutter/LenhChuyenDon"),
                              new TagValueChangedEventArgs(easyDriverConnector1.GetTag("Local Station/ChannelServer/DeviceCutter/LenhChuyenDon")
                              , "", easyDriverConnector1.GetTag("Local Station/ChannelServer/DeviceCutter/LenhChuyenDon").Value));

            if (easyDriverConnector1.ConnectionStatus == ConnectionStatus.Connected)
            {
                _labDriverStatus.BackColor = Color.Green;
            }
            else
            {
                _labDriverStatus.BackColor = Color.Red;
            }
        }

        //Lenh chuyen don may cat tam. tu PLC truyen ve
        private void LenhChuyenDonCutter_ValueChanged(object sender, EasyScada.Core.TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                Debug.WriteLine("Chuyen don");

                using (var connection = GlobalVariable.GetDbConnection())
                {
                    var resultData = connection.Query<tblDonHangModel>("sp_tblDonHangGetOnProcess").ToList();

                    if (resultData.Count > 0)
                    {
                        _donHangDangChay = resultData.FirstOrDefault(x => x.STT == resultData.Min(u => u.STT));

                        DoiDonCutter(_donHangDangChay);
                    }
                }

                easyDriverConnector1.GetTag("Local Station/ChannelServer/DeviceCutter/LenhChuyenDon").WriteAsync("0", WritePiority.High);
            }
        }

        private void DoiDonCutter(tblDonHangModel donHangDangChay)
        {
            easyDriverConnector1.GetTag("Local Station/ChannelServer/DeviceCutter/ChieuDaiCat1").WriteAsync(donHangDangChay.ChieuDai.ToString(), WritePiority.High);
            easyDriverConnector1.GetTag("Local Station/ChannelServer/DeviceCutter/SoLuongCat1").WriteAsync(donHangDangChay.SoLuong.ToString(), WritePiority.High);

            using (var connection =GlobalVariable.GetDbConnection())
            {
                connection.Execute($"UPDATE tbldonhang set Status = 1 WHERE Id = {donHangDangChay.Id}");
            }
        }
    }
}
