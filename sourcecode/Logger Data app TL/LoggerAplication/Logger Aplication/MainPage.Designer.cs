
namespace Logger_Aplication
{
    partial class MainPage
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
            this.components = new System.ComponentModel.Container();
            this.DriverConnect = new EasyScada.Winforms.Controls.EasyDriverConnector(this.components);
            this.easyLabel1 = new EasyScada.Winforms.Controls.EasyLabel();
            this.easyLabel2 = new EasyScada.Winforms.Controls.EasyLabel();
            this.easyLabel3 = new EasyScada.Winforms.Controls.EasyLabel();
            this.easyLabel4 = new EasyScada.Winforms.Controls.EasyLabel();
            this.easyLabel5 = new EasyScada.Winforms.Controls.EasyLabel();
            this.easyLabel6 = new EasyScada.Winforms.Controls.EasyLabel();
            this.easyLabel7 = new EasyScada.Winforms.Controls.EasyLabel();
            this.easyLabel8 = new EasyScada.Winforms.Controls.EasyLabel();
            this.easyLabel9 = new EasyScada.Winforms.Controls.EasyLabel();
            this.easyLabel10 = new EasyScada.Winforms.Controls.EasyLabel();
            this.easyLabel11 = new EasyScada.Winforms.Controls.EasyLabel();
            this.easyLabel12 = new EasyScada.Winforms.Controls.EasyLabel();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.richTextBox3 = new System.Windows.Forms.RichTextBox();
            this.richTextBox4 = new System.Windows.Forms.RichTextBox();
            this.richTextBox5 = new System.Windows.Forms.RichTextBox();
            this.richTextBox6 = new System.Windows.Forms.RichTextBox();
            this.richTextBox7 = new System.Windows.Forms.RichTextBox();
            this.richTextBox8 = new System.Windows.Forms.RichTextBox();
            this.richTextBox9 = new System.Windows.Forms.RichTextBox();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.DriverConnect)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.easyLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.easyLabel2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.easyLabel3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.easyLabel4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.easyLabel5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.easyLabel6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.easyLabel7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.easyLabel8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.easyLabel9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.easyLabel10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.easyLabel11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.easyLabel12)).BeginInit();
            this.SuspendLayout();
            // 
            // DriverConnect
            // 
            this.DriverConnect.CollectionName = null;
            this.DriverConnect.CommunicationMode = EasyScada.Core.CommunicationMode.ReceiveFromServer;
            this.DriverConnect.DatabaseName = null;
            this.DriverConnect.MongoDb_ConnectionString = null;
            this.DriverConnect.Port = ((ushort)(8800));
            this.DriverConnect.RefreshRate = 1000;
            this.DriverConnect.ServerAddress = "127.0.0.1";
            this.DriverConnect.StationName = null;
            this.DriverConnect.Timeout = 30;
            this.DriverConnect.UseMongoDb = false;
            // 
            // easyLabel1
            // 
            this.easyLabel1.DisplayMode = EasyScada.Winforms.Controls.DisplayMode.Value;
            this.easyLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.84906F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.easyLabel1.ForeColor = System.Drawing.Color.Red;
            this.easyLabel1.Location = new System.Drawing.Point(184, 212);
            this.easyLabel1.Name = "easyLabel1";
            this.easyLabel1.Size = new System.Drawing.Size(274, 50);
            this.easyLabel1.StringFormat = null;
            this.easyLabel1.TabIndex = 0;
            this.easyLabel1.TagPath = "Local Station/StationC/StC_HMI/StC_FaceIn";
            this.easyLabel1.Text = "123456789.0";
            // 
            // easyLabel2
            // 
            this.easyLabel2.DisplayMode = EasyScada.Winforms.Controls.DisplayMode.Value;
            this.easyLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.84906F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.easyLabel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.easyLabel2.Location = new System.Drawing.Point(467, 212);
            this.easyLabel2.Name = "easyLabel2";
            this.easyLabel2.Size = new System.Drawing.Size(274, 50);
            this.easyLabel2.StringFormat = null;
            this.easyLabel2.TabIndex = 1;
            this.easyLabel2.TagPath = "Local Station/StationC/StC_HMI/StC_WaveIn";
            this.easyLabel2.Text = "123456789.0";
            // 
            // easyLabel3
            // 
            this.easyLabel3.DisplayMode = EasyScada.Winforms.Controls.DisplayMode.Value;
            this.easyLabel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.84906F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.easyLabel3.ForeColor = System.Drawing.Color.Blue;
            this.easyLabel3.Location = new System.Drawing.Point(755, 212);
            this.easyLabel3.Name = "easyLabel3";
            this.easyLabel3.Size = new System.Drawing.Size(274, 50);
            this.easyLabel3.StringFormat = null;
            this.easyLabel3.TabIndex = 2;
            this.easyLabel3.TagPath = "Local Station/StationC/StC_HMI/StC_PaperOut";
            this.easyLabel3.Text = "123456789.0";
            // 
            // easyLabel4
            // 
            this.easyLabel4.BackColor = System.Drawing.Color.White;
            this.easyLabel4.DisplayMode = EasyScada.Winforms.Controls.DisplayMode.Value;
            this.easyLabel4.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.84906F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.easyLabel4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.easyLabel4.Location = new System.Drawing.Point(1045, 212);
            this.easyLabel4.Name = "easyLabel4";
            this.easyLabel4.Size = new System.Drawing.Size(274, 50);
            this.easyLabel4.StringFormat = null;
            this.easyLabel4.TabIndex = 3;
            this.easyLabel4.TagPath = "Local Station/StationC/StC_HMI/StC_ConRemain";
            this.easyLabel4.Text = "123456789.0";
            // 
            // easyLabel5
            // 
            this.easyLabel5.BackColor = System.Drawing.Color.White;
            this.easyLabel5.DisplayMode = EasyScada.Winforms.Controls.DisplayMode.Value;
            this.easyLabel5.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.84906F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.easyLabel5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.easyLabel5.Location = new System.Drawing.Point(1045, 302);
            this.easyLabel5.Name = "easyLabel5";
            this.easyLabel5.Size = new System.Drawing.Size(274, 50);
            this.easyLabel5.StringFormat = null;
            this.easyLabel5.TabIndex = 7;
            this.easyLabel5.TagPath = "Local Station/StationB/StB_HMI/StB_ConRemain";
            this.easyLabel5.Text = "123456789.0";
            // 
            // easyLabel6
            // 
            this.easyLabel6.DisplayMode = EasyScada.Winforms.Controls.DisplayMode.Value;
            this.easyLabel6.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.84906F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.easyLabel6.ForeColor = System.Drawing.Color.Blue;
            this.easyLabel6.Location = new System.Drawing.Point(755, 302);
            this.easyLabel6.Name = "easyLabel6";
            this.easyLabel6.Size = new System.Drawing.Size(274, 50);
            this.easyLabel6.StringFormat = null;
            this.easyLabel6.TabIndex = 6;
            this.easyLabel6.TagPath = "Local Station/StationB/StB_HMI/StB_PaperOut";
            this.easyLabel6.Text = "123456789.0";
            // 
            // easyLabel7
            // 
            this.easyLabel7.DisplayMode = EasyScada.Winforms.Controls.DisplayMode.Value;
            this.easyLabel7.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.84906F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.easyLabel7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.easyLabel7.Location = new System.Drawing.Point(467, 302);
            this.easyLabel7.Name = "easyLabel7";
            this.easyLabel7.Size = new System.Drawing.Size(274, 50);
            this.easyLabel7.StringFormat = null;
            this.easyLabel7.TabIndex = 5;
            this.easyLabel7.TagPath = "Local Station/StationB/StB_HMI/StB_WaveIn";
            this.easyLabel7.Text = "123456789.0";
            // 
            // easyLabel8
            // 
            this.easyLabel8.DisplayMode = EasyScada.Winforms.Controls.DisplayMode.Value;
            this.easyLabel8.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.84906F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.easyLabel8.ForeColor = System.Drawing.Color.Red;
            this.easyLabel8.Location = new System.Drawing.Point(184, 302);
            this.easyLabel8.Name = "easyLabel8";
            this.easyLabel8.Size = new System.Drawing.Size(274, 50);
            this.easyLabel8.StringFormat = null;
            this.easyLabel8.TabIndex = 4;
            this.easyLabel8.TagPath = "Local Station/StationB/StB_HMI/StB_FaceIn";
            this.easyLabel8.Text = "123456789.0";
            // 
            // easyLabel9
            // 
            this.easyLabel9.BackColor = System.Drawing.Color.White;
            this.easyLabel9.DisplayMode = EasyScada.Winforms.Controls.DisplayMode.Value;
            this.easyLabel9.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.84906F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.easyLabel9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.easyLabel9.Location = new System.Drawing.Point(1045, 393);
            this.easyLabel9.Name = "easyLabel9";
            this.easyLabel9.Size = new System.Drawing.Size(274, 50);
            this.easyLabel9.StringFormat = null;
            this.easyLabel9.TabIndex = 11;
            this.easyLabel9.TagPath = "Local Station/StationE/Ste_HMI/StE_ConRemain";
            this.easyLabel9.Text = "123456789.0";
            // 
            // easyLabel10
            // 
            this.easyLabel10.DisplayMode = EasyScada.Winforms.Controls.DisplayMode.Value;
            this.easyLabel10.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.84906F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.easyLabel10.ForeColor = System.Drawing.Color.Blue;
            this.easyLabel10.Location = new System.Drawing.Point(755, 393);
            this.easyLabel10.Name = "easyLabel10";
            this.easyLabel10.Size = new System.Drawing.Size(274, 50);
            this.easyLabel10.StringFormat = null;
            this.easyLabel10.TabIndex = 10;
            this.easyLabel10.TagPath = "Local Station/StationE/Ste_HMI/StE_PaperOut";
            this.easyLabel10.Text = "123456789.0";
            // 
            // easyLabel11
            // 
            this.easyLabel11.DisplayMode = EasyScada.Winforms.Controls.DisplayMode.Value;
            this.easyLabel11.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.84906F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.easyLabel11.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.easyLabel11.Location = new System.Drawing.Point(467, 393);
            this.easyLabel11.Name = "easyLabel11";
            this.easyLabel11.Size = new System.Drawing.Size(274, 50);
            this.easyLabel11.StringFormat = null;
            this.easyLabel11.TabIndex = 9;
            this.easyLabel11.TagPath = "Local Station/StationE/Ste_HMI/StE_WaveIn";
            this.easyLabel11.Text = "123456789.0";
            // 
            // easyLabel12
            // 
            this.easyLabel12.DisplayMode = EasyScada.Winforms.Controls.DisplayMode.Value;
            this.easyLabel12.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.84906F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.easyLabel12.ForeColor = System.Drawing.Color.Red;
            this.easyLabel12.Location = new System.Drawing.Point(184, 393);
            this.easyLabel12.Name = "easyLabel12";
            this.easyLabel12.Size = new System.Drawing.Size(274, 50);
            this.easyLabel12.StringFormat = null;
            this.easyLabel12.TabIndex = 8;
            this.easyLabel12.TagPath = "Local Station/StationE/Ste_HMI/StE_FaceIn";
            this.easyLabel12.Text = "123456789.0";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Font = new System.Drawing.Font("Cambria", 27.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.richTextBox1.Location = new System.Drawing.Point(25, 139);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(153, 54);
            this.richTextBox1.TabIndex = 12;
            this.richTextBox1.Text = "SÓNG";
            // 
            // richTextBox2
            // 
            this.richTextBox2.Font = new System.Drawing.Font("Cambria", 25.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.richTextBox2.Location = new System.Drawing.Point(25, 212);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.Size = new System.Drawing.Size(153, 54);
            this.richTextBox2.TabIndex = 13;
            this.richTextBox2.Text = "SÓNG C";
            // 
            // richTextBox3
            // 
            this.richTextBox3.Font = new System.Drawing.Font("Cambria", 25.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.richTextBox3.Location = new System.Drawing.Point(25, 298);
            this.richTextBox3.Name = "richTextBox3";
            this.richTextBox3.Size = new System.Drawing.Size(153, 54);
            this.richTextBox3.TabIndex = 14;
            this.richTextBox3.Text = "SÓNG B";
            // 
            // richTextBox4
            // 
            this.richTextBox4.Font = new System.Drawing.Font("Cambria", 25.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.richTextBox4.Location = new System.Drawing.Point(25, 389);
            this.richTextBox4.Name = "richTextBox4";
            this.richTextBox4.Size = new System.Drawing.Size(153, 54);
            this.richTextBox4.TabIndex = 15;
            this.richTextBox4.Text = "SÓNG E";
            // 
            // richTextBox5
            // 
            this.richTextBox5.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox5.Font = new System.Drawing.Font("Cambria", 25.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.richTextBox5.ForeColor = System.Drawing.Color.Red;
            this.richTextBox5.Location = new System.Drawing.Point(200, 139);
            this.richTextBox5.Name = "richTextBox5";
            this.richTextBox5.Size = new System.Drawing.Size(219, 54);
            this.richTextBox5.TabIndex = 16;
            this.richTextBox5.Text = "GIẤY MẶT";
            // 
            // richTextBox6
            // 
            this.richTextBox6.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox6.Font = new System.Drawing.Font("Cambria", 25.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.richTextBox6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.richTextBox6.Location = new System.Drawing.Point(484, 139);
            this.richTextBox6.Name = "richTextBox6";
            this.richTextBox6.Size = new System.Drawing.Size(219, 54);
            this.richTextBox6.TabIndex = 17;
            this.richTextBox6.Text = "GIẤY SÓNG";
            // 
            // richTextBox7
            // 
            this.richTextBox7.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox7.Font = new System.Drawing.Font("Cambria", 25.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.richTextBox7.ForeColor = System.Drawing.Color.Blue;
            this.richTextBox7.Location = new System.Drawing.Point(769, 139);
            this.richTextBox7.Name = "richTextBox7";
            this.richTextBox7.Size = new System.Drawing.Size(219, 54);
            this.richTextBox7.TabIndex = 18;
            this.richTextBox7.Text = "GHÉP LỚP";
            // 
            // richTextBox8
            // 
            this.richTextBox8.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox8.BackColor = System.Drawing.Color.White;
            this.richTextBox8.Font = new System.Drawing.Font("Cambria", 25.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.richTextBox8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.richTextBox8.Location = new System.Drawing.Point(1054, 139);
            this.richTextBox8.Name = "richTextBox8";
            this.richTextBox8.Size = new System.Drawing.Size(219, 54);
            this.richTextBox8.TabIndex = 19;
            this.richTextBox8.Text = "TRÊN DÀN";
            // 
            // richTextBox9
            // 
            this.richTextBox9.Font = new System.Drawing.Font("Cambria", 27.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.richTextBox9.Location = new System.Drawing.Point(269, 46);
            this.richTextBox9.Name = "richTextBox9";
            this.richTextBox9.Size = new System.Drawing.Size(810, 54);
            this.richTextBox9.TabIndex = 20;
            this.richTextBox9.Text = "CHƯƠNG TRÌNH QUẢN LÝ GIẤY MÁY SÓNG";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(1138, 12);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(200, 20);
            this.dateTimePicker1.TabIndex = 21;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Cambria", 16.30189F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.button1.Location = new System.Drawing.Point(1116, 664);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(207, 51);
            this.button1.TabIndex = 22;
            this.button1.Text = "XUẤT BÁO CÁO";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // MainPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1350, 727);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.richTextBox9);
            this.Controls.Add(this.richTextBox8);
            this.Controls.Add(this.richTextBox7);
            this.Controls.Add(this.richTextBox6);
            this.Controls.Add(this.richTextBox5);
            this.Controls.Add(this.richTextBox4);
            this.Controls.Add(this.richTextBox3);
            this.Controls.Add(this.richTextBox2);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.easyLabel9);
            this.Controls.Add(this.easyLabel10);
            this.Controls.Add(this.easyLabel11);
            this.Controls.Add(this.easyLabel12);
            this.Controls.Add(this.easyLabel5);
            this.Controls.Add(this.easyLabel6);
            this.Controls.Add(this.easyLabel7);
            this.Controls.Add(this.easyLabel8);
            this.Controls.Add(this.easyLabel4);
            this.Controls.Add(this.easyLabel3);
            this.Controls.Add(this.easyLabel2);
            this.Controls.Add(this.easyLabel1);
            this.Name = "MainPage";
            this.Text = "Truong Luu Automation Logger Data App";
            ((System.ComponentModel.ISupportInitialize)(this.DriverConnect)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.easyLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.easyLabel2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.easyLabel3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.easyLabel4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.easyLabel5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.easyLabel6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.easyLabel7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.easyLabel8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.easyLabel9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.easyLabel10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.easyLabel11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.easyLabel12)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private EasyScada.Winforms.Controls.EasyDriverConnector DriverConnect;
        private EasyScada.Winforms.Controls.EasyLabel easyLabel1;
        private EasyScada.Winforms.Controls.EasyLabel easyLabel2;
        private EasyScada.Winforms.Controls.EasyLabel easyLabel3;
        private EasyScada.Winforms.Controls.EasyLabel easyLabel4;
        private EasyScada.Winforms.Controls.EasyLabel easyLabel5;
        private EasyScada.Winforms.Controls.EasyLabel easyLabel6;
        private EasyScada.Winforms.Controls.EasyLabel easyLabel7;
        private EasyScada.Winforms.Controls.EasyLabel easyLabel8;
        private EasyScada.Winforms.Controls.EasyLabel easyLabel9;
        private EasyScada.Winforms.Controls.EasyLabel easyLabel10;
        private EasyScada.Winforms.Controls.EasyLabel easyLabel11;
        private EasyScada.Winforms.Controls.EasyLabel easyLabel12;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.RichTextBox richTextBox2;
        private System.Windows.Forms.RichTextBox richTextBox3;
        private System.Windows.Forms.RichTextBox richTextBox4;
        private System.Windows.Forms.RichTextBox richTextBox5;
        private System.Windows.Forms.RichTextBox richTextBox6;
        private System.Windows.Forms.RichTextBox richTextBox7;
        private System.Windows.Forms.RichTextBox richTextBox8;
        private System.Windows.Forms.RichTextBox richTextBox9;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Button button1;
    }
}

