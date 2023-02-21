using CommonControl;
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
        private Timer _t = new Timer();
        public Form1()
        {
            InitializeComponent();

            Load += Form1_Load;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _t.Interval = 100;
            _t.Enabled = true;
            _t.Tick += (s, o) =>
            {
                Timer sen = (Timer)s;
                sen.Enabled = false;

                this.Invoke((MethodInvoker)delegate { labDateTime.Text = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"); });

                sen.Enabled = true;
            };
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
