using ClosedXML.Excel;
using Dapper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Logger_Aplication
{
    public partial class Report : Form
    {
        public Report()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Report_Load(object sender, EventArgs e)
        {
            dtFrom.Value = DateTime.Now;
            dtTo.Value = DateTime.Now;
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            using (var connection = GlobalVariables.GetConnection())
            {
                List<MaySongLodModel> resultData = new List<MaySongLodModel>();
                var para = new DynamicParameters();

                if (cbSong.Text=="Tất cả")
                {
                    para.Add("_From", dtFrom.Text);
                    para.Add("_To", dtTo.Text);

                     resultData = connection.Query<MaySongLodModel>("sp_dataMaySongGetsFromTo", para, commandType: CommandType.StoredProcedure).ToList();
                }
                else
                {
                    para.Add("_Song", cbSong.Text);
                    para.Add("_From", dtFrom.Text);
                    para.Add("_To", dtTo.Text);

                    resultData = connection.Query<MaySongLodModel>("sp_dataMaySongGetsFromToSong", para, commandType: CommandType.StoredProcedure).ToList();
                }

                this.Invoke((MethodInvoker)delegate { grvData.DataSource = resultData; });
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            using (var connection = GlobalVariables.GetConnection())
            {
                List<MaySongLodModel> resultData = new List<MaySongLodModel>();
                var para = new DynamicParameters();

                if (cbSong.Text == "Tất cả")
                {
                    para.Add("_From", dtFrom.Text);
                    para.Add("_To", dtTo.Text);

                    resultData = connection.Query<MaySongLodModel>("sp_dataMaySongGetsFromTo", para, commandType: CommandType.StoredProcedure).ToList();
                }
                else
                {
                    para.Add("_Song", cbSong.Text);
                    para.Add("_From", dtFrom.Text);
                    para.Add("_To", dtTo.Text);

                    resultData = connection.Query<MaySongLodModel>("sp_dataMaySongGetsFromToSong", para, commandType: CommandType.StoredProcedure).ToList();
                }

                if (resultData.Count > 0)
                {
                    XuatExcel(dtFrom.Value, dtTo.Value, resultData);
                }
            }
        }

        public void XuatExcel(DateTime fromTime, DateTime toTime, List<MaySongLodModel> dataExport)
        {
            try
            {
                //SaveFileDialog sfd = new SaveFileDialog();
                //sfd.Filter = "Excel File |*.xlsx";
                //sfd.FileName = "BaoCao";

                //if (sfd.ShowDialog() == DialogResult.OK)
                {
                    // var dsThuMua = GetPurchaseModels(fromTime, toTime, customerId, kieu, payNow, dataExport);

                    using (var wb = new XLWorkbook())
                    {
                        var wsThuMua = wb.Worksheets.Add("MaySong");

                        DataTable dtThuMua = new DataTable();
                        dtThuMua.Columns.Add("Thời Gian", typeof(DateTime));
                        dtThuMua.Columns.Add("Sóng", typeof(string));
                        dtThuMua.Columns.Add("Giấy Mặt", typeof(string));
                        dtThuMua.Columns.Add("Giấy Sóng", typeof(string));
                        dtThuMua.Columns.Add("Ghép Lớp", typeof(double));
                        dtThuMua.Columns.Add("Trên Dàn Còn Lại", typeof(double));
                        dtThuMua.Columns.Add("Độ Dài Cài Đặt", typeof(double));
                        dtThuMua.Columns.Add("Số Mét Còn Lại", typeof(double));
                        dtThuMua.Columns.Add("Thao Tác Thực Hiện", typeof(string));

                        foreach (var item in dataExport)
                        {
                            dtThuMua.Rows.Add(item.CreatedDate, item.MaySong, item.GiayMat, item.GiaySong, item.GhepLop, item.TrenDanConLai,
                                item.DoDaiCaiDat, item.SoMetConLai
                                , item.ThaoTacThucHien == ThaoTacEnum.XoaTuDau ? "Xóa từ đầu" : item.ThaoTacThucHien == ThaoTacEnum.ChuyenDon ? "Chuyển đơn" : "Chuyển khổ");
                        }
                        wsThuMua.Cell("A1").Value = "BÁO CÁO";
                        wsThuMua.Range(1, 1, 1, dtThuMua.Columns.Count).Merge().AddToNamed("Titles");

                        wsThuMua.Range(2, 1, 2, dtThuMua.Columns.Count).Merge().Value = $"Từ ngày: {fromTime} - Đến ngày: {toTime}";
                        wsThuMua.Range(2, 1, 2, dtThuMua.Columns.Count).Style
                           .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Right)
                           .Alignment.SetVertical(XLAlignmentVerticalValues.Center)
                           .Font.FontSize = 12;

                        wsThuMua.Cell("A3").InsertTable(dtThuMua.AsEnumerable());
                        wsThuMua.Range("A4", $"A{dtThuMua.Rows.Count}").CellsUsed().SetDataType(XLDataType.DateTime);

                        wsThuMua.Columns().AdjustToContents();
                        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////

                        // Prepare the style for the titles
                        var titlesStyle = wb.Style;
                        titlesStyle.Font.Bold = true;
                        titlesStyle.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        titlesStyle.Fill.BackgroundColor = XLColor.BlueGreen;

                        // Format all titles in one shot
                        wb.NamedRanges.NamedRange("Titles").Ranges.Style = titlesStyle;

                        string _pathOpen = Path.Combine(GlobalVariables.PathApp, $"BaoCaoMaySong");
                        if (Directory.Exists($"{_pathOpen}"))
                        {
                            _pathOpen = Path.Combine(_pathOpen, $"{DateTime.Now.ToString("yyyyMMdd_HHmmss")}DataMaySong.xlsx");
                            wb.SaveAs(_pathOpen);
                        }
                        else
                        {
                            Directory.CreateDirectory(_pathOpen);
                            _pathOpen = Path.Combine(_pathOpen, $"{DateTime.Now.ToString("yyyyMMdd_HHmmss")}DataMaySong.xlsx");
                            wb.SaveAs(_pathOpen);
                        }

                        //wb.SaveAs(sfd.FileName);
                        if (MessageBox.Show($"Xuất báo cáo thành công! Bạn có muốn mở file không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                        {
                            OpenFile(_pathOpen);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Có lỗi: {ex}");
            }
        }

        public static void OpenFile(string fileName)
        {
            ProcessStartInfo info = new ProcessStartInfo();
            info.FileName = fileName;
            info.CreateNoWindow = true;
            info.WindowStyle = ProcessWindowStyle.Normal;
            Process p = new Process();
            p.StartInfo = info;
            p.Start();
        }
    }
}
