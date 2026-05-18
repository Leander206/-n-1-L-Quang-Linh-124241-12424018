namespace QLBH
{
    partial class frmTraCuuHoaDon
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle styleHeader1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle styleHeader2 = new System.Windows.Forms.DataGridViewCellStyle();

            // ── Khai báo controls ──────────────────────────────────────────
            this.pnlTop        = new System.Windows.Forms.Panel();
            this.lblTitle      = new System.Windows.Forms.Label();

            this.pnlFilter     = new System.Windows.Forms.Panel();
            this.lblTu         = new System.Windows.Forms.Label();
            this.dtpTu         = new System.Windows.Forms.DateTimePicker();
            this.lblDen        = new System.Windows.Forms.Label();
            this.dtpDen        = new System.Windows.Forms.DateTimePicker();
            this.lblTK         = new System.Windows.Forms.Label();
            this.txtTimKiem    = new System.Windows.Forms.TextBox();
            this.btnTimKiem    = new System.Windows.Forms.Button();
            this.btnLamMoi     = new System.Windows.Forms.Button();
            this.btnDong       = new System.Windows.Forms.Button();

            this.pnlStat       = new System.Windows.Forms.Panel();
            this.lblTongHD     = new System.Windows.Forms.Label();
            this.lblDoanhThu   = new System.Windows.Forms.Label();

            this.lblDsHD       = new System.Windows.Forms.Label();
            this.dgvHoaDon     = new System.Windows.Forms.DataGridView();

            this.lblMaHDB      = new System.Windows.Forms.Label();
            this.dgvChiTiet    = new System.Windows.Forms.DataGridView();

            this.pnlTop.SuspendLayout();
            this.pnlFilter.SuspendLayout();
            this.pnlStat.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHoaDon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTiet)).BeginInit();
            this.SuspendLayout();

            // ── pnlTop (tiêu đề) ──────────────────────────────────────────
            this.pnlTop.BackColor  = System.Drawing.Color.CornflowerBlue;
            this.pnlTop.Dock       = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Height     = 48;
            this.pnlTop.Controls.Add(this.lblTitle);

            this.lblTitle.Text      = "TRA CỨU LỊCH SỬ HÓA ĐƠN BÁN HÀNG";
            this.lblTitle.Font      = new System.Drawing.Font("Tahoma", 14F,
                                          System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.AutoSize  = true;
            this.lblTitle.Location  = new System.Drawing.Point(20, 11);

            // ── pnlFilter ─────────────────────────────────────────────────
            this.pnlFilter.BackColor  = System.Drawing.Color.WhiteSmoke;
            this.pnlFilter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlFilter.Location   = new System.Drawing.Point(10, 58);
            this.pnlFilter.Size       = new System.Drawing.Size(1140, 50);
            this.pnlFilter.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblTu, this.dtpTu, this.lblDen, this.dtpDen,
                this.lblTK, this.txtTimKiem,
                this.btnTimKiem, this.btnLamMoi, this.btnDong });

            // Từ ngày
            this.lblTu.Text      = "Từ ngày:";
            this.lblTu.Font      = new System.Drawing.Font("Tahoma", 9.75F,
                                       System.Drawing.FontStyle.Bold);
            this.lblTu.AutoSize  = true;
            this.lblTu.Location  = new System.Drawing.Point(10, 14);

            this.dtpTu.Format       = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpTu.Location     = new System.Drawing.Point(80, 11);
            this.dtpTu.Size         = new System.Drawing.Size(120, 22);
            this.dtpTu.Font         = new System.Drawing.Font("Tahoma", 9.75F);

            // Đến ngày
            this.lblDen.Text     = "Đến ngày:";
            this.lblDen.Font     = new System.Drawing.Font("Tahoma", 9.75F,
                                       System.Drawing.FontStyle.Bold);
            this.lblDen.AutoSize = true;
            this.lblDen.Location = new System.Drawing.Point(215, 14);

            this.dtpDen.Format      = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDen.Location    = new System.Drawing.Point(292, 11);
            this.dtpDen.Size        = new System.Drawing.Size(120, 22);
            this.dtpDen.Font        = new System.Drawing.Font("Tahoma", 9.75F);

            // Từ khóa
            this.lblTK.Text      = "Tìm:";
            this.lblTK.Font      = new System.Drawing.Font("Tahoma", 9.75F,
                                       System.Drawing.FontStyle.Bold);
            this.lblTK.AutoSize  = true;
            this.lblTK.Location  = new System.Drawing.Point(430, 14);

            this.txtTimKiem.Location    = new System.Drawing.Point(466, 11);
            this.txtTimKiem.Size        = new System.Drawing.Size(220, 22);
            this.txtTimKiem.Font        = new System.Drawing.Font("Tahoma", 9.75F);

            this.txtTimKiem.KeyDown    += new System.Windows.Forms.KeyEventHandler(this.txtTimKiem_KeyDown);

            // Nút Tìm kiếm
            this.btnTimKiem.Text          = "🔍 Tìm kiếm";
            this.btnTimKiem.Font          = new System.Drawing.Font("Tahoma", 9.75F,
                                                System.Drawing.FontStyle.Bold);
            this.btnTimKiem.BackColor     = System.Drawing.Color.RoyalBlue;
            this.btnTimKiem.ForeColor     = System.Drawing.Color.White;
            this.btnTimKiem.FlatStyle     = System.Windows.Forms.FlatStyle.Flat;
            this.btnTimKiem.Location      = new System.Drawing.Point(700, 9);
            this.btnTimKiem.Size          = new System.Drawing.Size(120, 30);
            this.btnTimKiem.Click        += new System.EventHandler(this.btnTimKiem_Click);

            // Nút Làm mới
            this.btnLamMoi.Text           = "↺ Làm mới";
            this.btnLamMoi.Font           = new System.Drawing.Font("Tahoma", 9.75F,
                                                System.Drawing.FontStyle.Bold);
            this.btnLamMoi.BackColor      = System.Drawing.Color.SteelBlue;
            this.btnLamMoi.ForeColor      = System.Drawing.Color.White;
            this.btnLamMoi.FlatStyle      = System.Windows.Forms.FlatStyle.Flat;
            this.btnLamMoi.Location       = new System.Drawing.Point(830, 9);
            this.btnLamMoi.Size           = new System.Drawing.Size(110, 30);
            this.btnLamMoi.Click         += new System.EventHandler(this.btnLamMoi_Click);

            // Nút Đóng
            this.btnDong.Text             = "✖ Đóng";
            this.btnDong.Font             = new System.Drawing.Font("Tahoma", 9.75F,
                                                System.Drawing.FontStyle.Bold);
            this.btnDong.BackColor        = System.Drawing.Color.Crimson;
            this.btnDong.ForeColor        = System.Drawing.Color.White;
            this.btnDong.FlatStyle        = System.Windows.Forms.FlatStyle.Flat;
            this.btnDong.Location         = new System.Drawing.Point(950, 9);
            this.btnDong.Size             = new System.Drawing.Size(100, 30);
            this.btnDong.Click           += new System.EventHandler(this.btnDong_Click);

            // ── pnlStat (thống kê nhanh) ──────────────────────────────────
            this.pnlStat.BackColor   = System.Drawing.Color.AliceBlue;
            this.pnlStat.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlStat.Location    = new System.Drawing.Point(10, 118);
            this.pnlStat.Size        = new System.Drawing.Size(1140, 32);
            this.pnlStat.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblTongHD, this.lblDoanhThu });

            this.lblTongHD.Text      = "Tổng hóa đơn: 0";
            this.lblTongHD.Font      = new System.Drawing.Font("Tahoma", 10F,
                                           System.Drawing.FontStyle.Bold);
            this.lblTongHD.ForeColor = System.Drawing.Color.DarkBlue;
            this.lblTongHD.AutoSize  = true;
            this.lblTongHD.Location  = new System.Drawing.Point(15, 7);

            this.lblDoanhThu.Text      = "Doanh thu: 0 đ";
            this.lblDoanhThu.Font      = new System.Drawing.Font("Tahoma", 10F,
                                             System.Drawing.FontStyle.Bold);
            this.lblDoanhThu.ForeColor = System.Drawing.Color.DarkGreen;
            this.lblDoanhThu.AutoSize  = true;
            this.lblDoanhThu.Location  = new System.Drawing.Point(300, 7);

            // ── Label + DGV Danh sách hóa đơn ─────────────────────────────
            this.lblDsHD.Text      = "DANH SÁCH HÓA ĐƠN BÁN:";
            this.lblDsHD.Font      = new System.Drawing.Font("Tahoma", 9.75F,
                                         System.Drawing.FontStyle.Bold);
            this.lblDsHD.ForeColor = System.Drawing.Color.DarkSlateBlue;
            this.lblDsHD.AutoSize  = true;
            this.lblDsHD.Location  = new System.Drawing.Point(10, 158);

            // Style header DGV 1
            styleHeader1.BackColor          = System.Drawing.Color.CornflowerBlue;
            styleHeader1.ForeColor          = System.Drawing.Color.White;
            styleHeader1.Font               = new System.Drawing.Font("Tahoma", 9.75F,
                                                  System.Drawing.FontStyle.Bold);
            styleHeader1.Alignment          = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            styleHeader1.SelectionBackColor = System.Drawing.Color.CornflowerBlue;
            styleHeader1.SelectionForeColor = System.Drawing.Color.White;

            this.dgvHoaDon.Location                         = new System.Drawing.Point(10, 178);
            this.dgvHoaDon.Size                             = new System.Drawing.Size(1140, 280);
            this.dgvHoaDon.AllowUserToAddRows               = false;
            this.dgvHoaDon.AllowUserToDeleteRows            = false;
            this.dgvHoaDon.ReadOnly                         = true;
            this.dgvHoaDon.SelectionMode                    = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvHoaDon.MultiSelect                      = false;
            this.dgvHoaDon.BackgroundColor                  = System.Drawing.SystemColors.Control;
            this.dgvHoaDon.RowHeadersVisible                = false;
            this.dgvHoaDon.AutoSizeColumnsMode              = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvHoaDon.ColumnHeadersDefaultCellStyle    = styleHeader1;
            this.dgvHoaDon.EnableHeadersVisualStyles        = false;
            this.dgvHoaDon.ColumnHeadersHeightSizeMode      =
                System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvHoaDon.Font                             = new System.Drawing.Font("Tahoma", 9F);
            this.dgvHoaDon.CellClick                       += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvHoaDon_CellClick);
            this.dgvHoaDon.DataBindingComplete             += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvHoaDon_DataBindingComplete);

            // ── Label + DGV Chi tiết hóa đơn ──────────────────────────────
            this.lblMaHDB.Text      = "Chi tiết HD: (chọn một hóa đơn)";
            this.lblMaHDB.Font      = new System.Drawing.Font("Tahoma", 9.75F,
                                          System.Drawing.FontStyle.Bold);
            this.lblMaHDB.ForeColor = System.Drawing.Color.DarkRed;
            this.lblMaHDB.AutoSize  = true;
            this.lblMaHDB.Location  = new System.Drawing.Point(10, 468);

            // Style header DGV 2
            styleHeader2.BackColor          = System.Drawing.Color.SteelBlue;
            styleHeader2.ForeColor          = System.Drawing.Color.White;
            styleHeader2.Font               = new System.Drawing.Font("Tahoma", 9.75F,
                                                  System.Drawing.FontStyle.Bold);
            styleHeader2.Alignment          = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            styleHeader2.SelectionBackColor = System.Drawing.Color.SteelBlue;
            styleHeader2.SelectionForeColor = System.Drawing.Color.White;

            this.dgvChiTiet.Location                         = new System.Drawing.Point(10, 488);
            this.dgvChiTiet.Size                             = new System.Drawing.Size(1140, 200);
            this.dgvChiTiet.AllowUserToAddRows               = false;
            this.dgvChiTiet.AllowUserToDeleteRows            = false;
            this.dgvChiTiet.ReadOnly                         = true;
            this.dgvChiTiet.SelectionMode                    = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvChiTiet.BackgroundColor                  = System.Drawing.SystemColors.Control;
            this.dgvChiTiet.RowHeadersVisible                = false;
            this.dgvChiTiet.AutoSizeColumnsMode              = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvChiTiet.ColumnHeadersDefaultCellStyle    = styleHeader2;
            this.dgvChiTiet.EnableHeadersVisualStyles        = false;
            this.dgvChiTiet.ColumnHeadersHeightSizeMode      =
                System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvChiTiet.Font                             = new System.Drawing.Font("Tahoma", 9F);
            this.dgvChiTiet.DataBindingComplete             += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvChiTiet_DataBindingComplete);

            // ── Form ──────────────────────────────────────────────────────
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode       = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor           = System.Drawing.Color.White;
            this.ClientSize          = new System.Drawing.Size(1160, 700);
            this.FormBorderStyle     = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox         = false;
            this.StartPosition       = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text                = "Tra Cứu Lịch Sử Hóa Đơn Bán Hàng";
            this.Name                = "frmTraCuuHoaDon";
            this.Load               += new System.EventHandler(this.frmTraCuuHoaDon_Load);

            this.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.pnlTop,
                this.pnlFilter,
                this.pnlStat,
                this.lblDsHD,
                this.dgvHoaDon,
                this.lblMaHDB,
                this.dgvChiTiet
            });

            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.pnlFilter.ResumeLayout(false);
            this.pnlFilter.PerformLayout();
            this.pnlStat.ResumeLayout(false);
            this.pnlStat.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHoaDon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTiet)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Panel              pnlTop;
        private System.Windows.Forms.Label              lblTitle;
        private System.Windows.Forms.Panel              pnlFilter;
        private System.Windows.Forms.Label              lblTu;
        private System.Windows.Forms.DateTimePicker     dtpTu;
        private System.Windows.Forms.Label              lblDen;
        private System.Windows.Forms.DateTimePicker     dtpDen;
        private System.Windows.Forms.Label              lblTK;
        private System.Windows.Forms.TextBox            txtTimKiem;
        private System.Windows.Forms.Button             btnTimKiem;
        private System.Windows.Forms.Button             btnLamMoi;
        private System.Windows.Forms.Button             btnDong;
        private System.Windows.Forms.Panel              pnlStat;
        private System.Windows.Forms.Label              lblTongHD;
        private System.Windows.Forms.Label              lblDoanhThu;
        private System.Windows.Forms.Label              lblDsHD;
        private System.Windows.Forms.DataGridView       dgvHoaDon;
        private System.Windows.Forms.Label              lblMaHDB;
        private System.Windows.Forms.DataGridView       dgvChiTiet;
    }
}
