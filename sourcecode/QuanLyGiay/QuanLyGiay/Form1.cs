using Dapper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyGiay
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            Load += Form1_Load;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            using (var connection = GlobalVariable.GetDbConnection())
            {
                var res = connection.Query<tblDanhSachDonHangModel>("sp_tblDanhSachDonHangGets").ToList();

                this.Invoke((MethodInvoker)delegate
                {
                    grvDonHang.DataSource = res;
                });
            }
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
