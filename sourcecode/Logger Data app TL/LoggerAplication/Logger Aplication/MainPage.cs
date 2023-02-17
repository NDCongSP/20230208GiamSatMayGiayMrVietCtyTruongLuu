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

namespace Logger_Aplication
{
    public partial class MainPage : Form
    {
        public MainPage()
        {
            InitializeComponent();
            Load += MainPage_Load;
        }

        private void MainPage_Load(object sender, EventArgs e)
        {
            using (var connection=GlobalVariables.GetConnection())
            {
                connection.Query<>
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Report form2 = new Report();
            form2.Show();

        }
    }
}