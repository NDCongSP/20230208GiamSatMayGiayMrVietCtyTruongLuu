
namespace Logger_Aplication
{
    partial class Report
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.grvData = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dtFrom = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.dtTo = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.btnQuery = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.cbSong = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.grvData)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grvData
            // 
            this.grvData.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.grvData.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.DarkOrange;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grvData.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.grvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grvData.DefaultCellStyle = dataGridViewCellStyle5;
            this.grvData.Location = new System.Drawing.Point(12, 149);
            this.grvData.Name = "grvData";
            this.grvData.ReadOnly = true;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grvData.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.grvData.Size = new System.Drawing.Size(1326, 566);
            this.grvData.TabIndex = 25;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.cbSong);
            this.panel1.Controls.Add(this.btnExport);
            this.panel1.Controls.Add(this.btnQuery);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.dtTo);
            this.panel1.Controls.Add(this.dtFrom);
            this.panel1.Location = new System.Drawing.Point(12, 72);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1326, 71);
            this.panel1.TabIndex = 26;
            // 
            // dtFrom
            // 
            this.dtFrom.CustomFormat = "yyyy-MM-dd 00:00:00";
            this.dtFrom.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.dtFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtFrom.Location = new System.Drawing.Point(105, 19);
            this.dtFrom.Name = "dtFrom";
            this.dtFrom.Size = new System.Drawing.Size(249, 30);
            this.dtFrom.TabIndex = 1;
            this.dtFrom.Value = new System.DateTime(2023, 2, 17, 21, 45, 7, 0);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.label1.Location = new System.Drawing.Point(9, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "Từ ngày:";
            // 
            // dtTo
            // 
            this.dtTo.CustomFormat = "yyyy-MM-dd 23:59:59";
            this.dtTo.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.dtTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtTo.Location = new System.Drawing.Point(467, 19);
            this.dtTo.Name = "dtTo";
            this.dtTo.Size = new System.Drawing.Size(249, 30);
            this.dtTo.TabIndex = 2;
            this.dtTo.Value = new System.DateTime(2023, 2, 17, 21, 45, 7, 0);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.label3.Location = new System.Drawing.Point(361, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(102, 25);
            this.label3.TabIndex = 1;
            this.label3.Text = "Đến ngày:";
            // 
            // btnQuery
            // 
            this.btnQuery.BackColor = System.Drawing.Color.Lime;
            this.btnQuery.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold);
            this.btnQuery.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnQuery.Location = new System.Drawing.Point(1020, 12);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(136, 45);
            this.btnQuery.TabIndex = 4;
            this.btnQuery.Text = "Truy vấn";
            this.btnQuery.UseVisualStyleBackColor = false;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // btnExport
            // 
            this.btnExport.BackColor = System.Drawing.Color.Aqua;
            this.btnExport.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold);
            this.btnExport.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnExport.Location = new System.Drawing.Point(1177, 12);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(136, 45);
            this.btnExport.TabIndex = 5;
            this.btnExport.Text = "Xuất Excel";
            this.btnExport.UseVisualStyleBackColor = false;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(527, 14);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(285, 39);
            this.label4.TabIndex = 1;
            this.label4.Text = "XUẤT BÁO CÁO";
            // 
            // cbSong
            // 
            this.cbSong.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.cbSong.FormattingEnabled = true;
            this.cbSong.Items.AddRange(new object[] {
            "Tất cả",
            "B",
            "C",
            "E"});
            this.cbSong.Location = new System.Drawing.Point(820, 18);
            this.cbSong.Name = "cbSong";
            this.cbSong.Size = new System.Drawing.Size(121, 33);
            this.cbSong.TabIndex = 3;
            this.cbSong.Text = "Tất cả";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.label2.Location = new System.Drawing.Point(743, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 25);
            this.label2.TabIndex = 4;
            this.label2.Text = "Sóng:";
            // 
            // Report
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1350, 727);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.grvData);
            this.Controls.Add(this.label4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Report";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Report";
            this.Load += new System.EventHandler(this.Report_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grvData)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView grvData;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtTo;
        private System.Windows.Forms.DateTimePicker dtFrom;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbSong;
    }
}