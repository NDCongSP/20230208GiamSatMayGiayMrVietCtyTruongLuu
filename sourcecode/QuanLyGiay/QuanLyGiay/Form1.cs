using CommonControl;
using Dapper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
            //_taskLoadOrder = new Task(LoadOrder);
            //_taskLoadOrder.Start();
        }

        private void BtnOrder_Click(object sender, EventArgs e)
        {
            GlobalVariable.MyEvents.Refresh = true;
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

        }

        private void RefresjOrder()
        {
            using (var connection = GlobalVariable.GetDbConnection())
            {
                var para = new DynamicParameters();
                para.Add("", "");

                var resultData = connection.Query<tblDonHangModel>("", para, commandType: CommandType.StoredProcedure).ToList();
            }
        }
    }
}
