namespace QLBH
{
    partial class frmNhapHang
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.labelMaHDN = new System.Windows.Forms.Label();
            this.txtMaHDN = new System.Windows.Forms.TextBox();
            this.labelNgayNhap = new System.Windows.Forms.Label();
            this.dtNgayNhap = new System.Windows.Forms.DateTimePicker();
            this.labelMaNCC = new System.Windows.Forms.Label();
            this.labelMaSP = new System.Windows.Forms.Label();
            this.txtMaSP = new System.Windows.Forms.TextBox();
            this.btnTimSP = new System.Windows.Forms.Button();
            this.lblTenSP = new System.Windows.Forms.Label();
            this.labelTonKho = new System.Windows.Forms.Label();
            this.lblTonKho = new System.Windows.Forms.Label();
            this.labelSoLuong = new System.Windows.Forms.Label();
            this.txtSoLuong = new System.Windows.Forms.TextBox();
            this.labelGiaNhap = new System.Windows.Forms.Label();
            this.txtGiaNhap = new System.Windows.Forms.TextBox();
            this.btnThemVaoHD = new System.Windows.Forms.Button();
            this.dgvChiTiet = new System.Windows.Forms.DataGridView();
            this.btnLuuHoaDon = new System.Windows.Forms.Button();
            this.btnLamMoi = new System.Windows.Forms.Button();
            this.btnDong = new System.Windows.Forms.Button();
            this.labelGhiChu = new System.Windows.Forms.Label();
            this.txtGhiChu = new System.Windows.Forms.TextBox();
            this.labelTongTien = new System.Windows.Forms.Label();
            this.txtTongTien = new System.Windows.Forms.TextBox();
            this.cboNCC = new System.Windows.Forms.ComboBox();
            this.txtMaNCC = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTiet)).BeginInit();
            this.SuspendLayout();
            // 
            // labelMaHDN
            // 
            this.labelMaHDN.AutoSize = true;
            this.labelMaHDN.Location = new System.Drawing.Point(30, 30);
            this.labelMaHDN.Name = "labelMaHDN";
            this.labelMaHDN.Size = new System.Drawing.Size(62, 16);
            this.labelMaHDN.TabIndex = 22;
            this.labelMaHDN.Text = "Mã HDN:";
            // 
            // txtMaHDN
            // 
            this.txtMaHDN.Location = new System.Drawing.Point(120, 28);
            this.txtMaHDN.Name = "txtMaHDN";
            this.txtMaHDN.ReadOnly = true;
            this.txtMaHDN.Size = new System.Drawing.Size(180, 22);
            this.txtMaHDN.TabIndex = 1;
            // 
            // labelNgayNhap
            // 
            this.labelNgayNhap.AutoSize = true;
            this.labelNgayNhap.Location = new System.Drawing.Point(320, 30);
            this.labelNgayNhap.Name = "labelNgayNhap";
            this.labelNgayNhap.Size = new System.Drawing.Size(76, 16);
            this.labelNgayNhap.TabIndex = 21;
            this.labelNgayNhap.Text = "Ngày nhập:";
            // 
            // dtNgayNhap
            // 
            this.dtNgayNhap.Location = new System.Drawing.Point(400, 28);
            this.dtNgayNhap.Name = "dtNgayNhap";
            this.dtNgayNhap.Size = new System.Drawing.Size(180, 22);
            this.dtNgayNhap.TabIndex = 2;
            // 
            // labelMaNCC
            // 
            this.labelMaNCC.AutoSize = true;
            this.labelMaNCC.Location = new System.Drawing.Point(30, 70);
            this.labelMaNCC.Name = "labelMaNCC";
            this.labelMaNCC.Size = new System.Drawing.Size(60, 16);
            this.labelMaNCC.TabIndex = 20;
            this.labelMaNCC.Text = "Mã NCC:";
            // 
            // labelMaSP
            // 
            this.labelMaSP.AutoSize = true;
            this.labelMaSP.Location = new System.Drawing.Point(30, 110);
            this.labelMaSP.Name = "labelMaSP";
            this.labelMaSP.Size = new System.Drawing.Size(50, 16);
            this.labelMaSP.TabIndex = 19;
            this.labelMaSP.Text = "Mã SP:";
            // 
            // txtMaSP
            // 
            this.txtMaSP.Location = new System.Drawing.Point(120, 108);
            this.txtMaSP.Name = "txtMaSP";
            this.txtMaSP.Size = new System.Drawing.Size(150, 22);
            this.txtMaSP.TabIndex = 4;
            // 
            // btnTimSP
            // 
            this.btnTimSP.Location = new System.Drawing.Point(280, 107);
            this.btnTimSP.Name = "btnTimSP";
            this.btnTimSP.Size = new System.Drawing.Size(80, 28);
            this.btnTimSP.TabIndex = 18;
            this.btnTimSP.Text = "Tìm";
            this.btnTimSP.Click += new System.EventHandler(this.btnTimSP_Click);
            // 
            // lblTenSP
            // 
            this.lblTenSP.AutoSize = true;
            this.lblTenSP.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.lblTenSP.ForeColor = System.Drawing.Color.Blue;
            this.lblTenSP.Location = new System.Drawing.Point(120, 140);
            this.lblTenSP.Name = "lblTenSP";
            this.lblTenSP.Size = new System.Drawing.Size(0, 20);
            this.lblTenSP.TabIndex = 17;
            // 
            // labelTonKho
            // 
            this.labelTonKho.AutoSize = true;
            this.labelTonKho.Location = new System.Drawing.Point(30, 170);
            this.labelTonKho.Name = "labelTonKho";
            this.labelTonKho.Size = new System.Drawing.Size(59, 16);
            this.labelTonKho.TabIndex = 16;
            this.labelTonKho.Text = "Tồn kho:";
            // 
            // lblTonKho
            // 
            this.lblTonKho.AutoSize = true;
            this.lblTonKho.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.lblTonKho.ForeColor = System.Drawing.Color.Red;
            this.lblTonKho.Location = new System.Drawing.Point(120, 170);
            this.lblTonKho.Name = "lblTonKho";
            this.lblTonKho.Size = new System.Drawing.Size(19, 20);
            this.lblTonKho.TabIndex = 15;
            this.lblTonKho.Text = "0";
            // 
            // labelSoLuong
            // 
            this.labelSoLuong.AutoSize = true;
            this.labelSoLuong.Location = new System.Drawing.Point(30, 200);
            this.labelSoLuong.Name = "labelSoLuong";
            this.labelSoLuong.Size = new System.Drawing.Size(96, 16);
            this.labelSoLuong.TabIndex = 14;
            this.labelSoLuong.Text = "Số lượng nhập:";
            // 
            // txtSoLuong
            // 
            this.txtSoLuong.Location = new System.Drawing.Point(120, 198);
            this.txtSoLuong.Name = "txtSoLuong";
            this.txtSoLuong.Size = new System.Drawing.Size(100, 22);
            this.txtSoLuong.TabIndex = 5;
            // 
            // labelGiaNhap
            // 
            this.labelGiaNhap.AutoSize = true;
            this.labelGiaNhap.Location = new System.Drawing.Point(230, 200);
            this.labelGiaNhap.Name = "labelGiaNhap";
            this.labelGiaNhap.Size = new System.Drawing.Size(64, 16);
            this.labelGiaNhap.TabIndex = 13;
            this.labelGiaNhap.Text = "Giá nhập:";
            // 
            // txtGiaNhap
            // 
            this.txtGiaNhap.Location = new System.Drawing.Point(300, 198);
            this.txtGiaNhap.Name = "txtGiaNhap";
            this.txtGiaNhap.Size = new System.Drawing.Size(100, 22);
            this.txtGiaNhap.TabIndex = 6;
            this.txtGiaNhap.TextChanged += new System.EventHandler(this.txtGiaNhap_TextChanged);
            // 
            // btnThemVaoHD
            // 
            this.btnThemVaoHD.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnThemVaoHD.ForeColor = System.Drawing.Color.White;
            this.btnThemVaoHD.Location = new System.Drawing.Point(410, 195);
            this.btnThemVaoHD.Name = "btnThemVaoHD";
            this.btnThemVaoHD.Size = new System.Drawing.Size(120, 30);
            this.btnThemVaoHD.TabIndex = 12;
            this.btnThemVaoHD.Text = "Thêm vào HD";
            this.btnThemVaoHD.UseVisualStyleBackColor = false;
            this.btnThemVaoHD.Click += new System.EventHandler(this.btnThemVaoHD_Click);
            // 
            // dgvChiTiet
            // 
            this.dgvChiTiet.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvChiTiet.Location = new System.Drawing.Point(30, 240);
            this.dgvChiTiet.Name = "dgvChiTiet";
            this.dgvChiTiet.RowHeadersWidth = 51;
            this.dgvChiTiet.Size = new System.Drawing.Size(700, 250);
            this.dgvChiTiet.TabIndex = 7;
            // 
            // btnLuuHoaDon
            // 
            this.btnLuuHoaDon.BackColor = System.Drawing.Color.Green;
            this.btnLuuHoaDon.ForeColor = System.Drawing.Color.White;
            this.btnLuuHoaDon.Location = new System.Drawing.Point(350, 565);
            this.btnLuuHoaDon.Name = "btnLuuHoaDon";
            this.btnLuuHoaDon.Size = new System.Drawing.Size(120, 35);
            this.btnLuuHoaDon.TabIndex = 2;
            this.btnLuuHoaDon.Text = "Lưu Hóa Đơn";
            this.btnLuuHoaDon.UseVisualStyleBackColor = false;
            this.btnLuuHoaDon.Click += new System.EventHandler(this.btnLuuHoaDon_Click);
            // 
            // btnLamMoi
            // 
            this.btnLamMoi.Location = new System.Drawing.Point(480, 565);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(100, 35);
            this.btnLamMoi.TabIndex = 1;
            this.btnLamMoi.Text = "Làm mới";
            this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);
            // 
            // btnDong
            // 
            this.btnDong.Location = new System.Drawing.Point(590, 565);
            this.btnDong.Name = "btnDong";
            this.btnDong.Size = new System.Drawing.Size(100, 35);
            this.btnDong.TabIndex = 0;
            this.btnDong.Text = "Đóng";
            this.btnDong.Click += new System.EventHandler(this.btnDong_Click_1);
            // 
            // labelGhiChu
            // 
            this.labelGhiChu.AutoSize = true;
            this.labelGhiChu.Location = new System.Drawing.Point(30, 500);
            this.labelGhiChu.Name = "labelGhiChu";
            this.labelGhiChu.Size = new System.Drawing.Size(54, 16);
            this.labelGhiChu.TabIndex = 11;
            this.labelGhiChu.Text = "Ghi chú:";
            // 
            // txtGhiChu
            // 
            this.txtGhiChu.Location = new System.Drawing.Point(120, 498);
            this.txtGhiChu.Multiline = true;
            this.txtGhiChu.Name = "txtGhiChu";
            this.txtGhiChu.Size = new System.Drawing.Size(610, 60);
            this.txtGhiChu.TabIndex = 8;
            // 
            // labelTongTien
            // 
            this.labelTongTien.AutoSize = true;
            this.labelTongTien.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.labelTongTien.Location = new System.Drawing.Point(30, 570);
            this.labelTongTien.Name = "labelTongTien";
            this.labelTongTien.Size = new System.Drawing.Size(110, 25);
            this.labelTongTien.TabIndex = 10;
            this.labelTongTien.Text = "Tổng tiền:";
            // 
            // txtTongTien
            // 
            this.txtTongTien.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.txtTongTien.Location = new System.Drawing.Point(146, 570);
            this.txtTongTien.Name = "txtTongTien";
            this.txtTongTien.ReadOnly = true;
            this.txtTongTien.Size = new System.Drawing.Size(180, 30);
            this.txtTongTien.TabIndex = 9;
            // 
            // cboNCC
            // 
            this.cboNCC.FormattingEnabled = true;
            this.cboNCC.Location = new System.Drawing.Point(400, 64);
            this.cboNCC.Name = "cboNCC";
            this.cboNCC.Size = new System.Drawing.Size(180, 24);
            this.cboNCC.TabIndex = 23;
            // 
            // txtMaNCC
            // 
            this.txtMaNCC.Location = new System.Drawing.Point(120, 64);
            this.txtMaNCC.Name = "txtMaNCC";
            this.txtMaNCC.Size = new System.Drawing.Size(180, 22);
            this.txtMaNCC.TabIndex = 24;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(320, 67);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 16);
            this.label1.TabIndex = 25;
            this.label1.Text = "Chọn NCC";
            // 
            // frmNhapHang
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(780, 620);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtMaNCC);
            this.Controls.Add(this.cboNCC);
            this.Controls.Add(this.btnDong);
            this.Controls.Add(this.btnLamMoi);
            this.Controls.Add(this.btnLuuHoaDon);
            this.Controls.Add(this.txtTongTien);
            this.Controls.Add(this.labelTongTien);
            this.Controls.Add(this.txtGhiChu);
            this.Controls.Add(this.labelGhiChu);
            this.Controls.Add(this.dgvChiTiet);
            this.Controls.Add(this.btnThemVaoHD);
            this.Controls.Add(this.txtGiaNhap);
            this.Controls.Add(this.labelGiaNhap);
            this.Controls.Add(this.txtSoLuong);
            this.Controls.Add(this.labelSoLuong);
            this.Controls.Add(this.lblTonKho);
            this.Controls.Add(this.labelTonKho);
            this.Controls.Add(this.lblTenSP);
            this.Controls.Add(this.btnTimSP);
            this.Controls.Add(this.txtMaSP);
            this.Controls.Add(this.labelMaSP);
            this.Controls.Add(this.labelMaNCC);
            this.Controls.Add(this.dtNgayNhap);
            this.Controls.Add(this.labelNgayNhap);
            this.Controls.Add(this.txtMaHDN);
            this.Controls.Add(this.labelMaHDN);
            this.Name = "frmNhapHang";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hóa Đơn Nhập Hàng";
            this.Load += new System.EventHandler(this.frmNhapHang_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTiet)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelMaHDN;
        private System.Windows.Forms.TextBox txtMaHDN;
        private System.Windows.Forms.Label labelNgayNhap;
        private System.Windows.Forms.DateTimePicker dtNgayNhap;
        private System.Windows.Forms.Label labelMaNCC;
        private System.Windows.Forms.Label labelMaSP;
        private System.Windows.Forms.TextBox txtMaSP;
        private System.Windows.Forms.Button btnTimSP;
        private System.Windows.Forms.Label lblTenSP;
        private System.Windows.Forms.Label labelTonKho;
        private System.Windows.Forms.Label lblTonKho;
        private System.Windows.Forms.Label labelSoLuong;
        private System.Windows.Forms.TextBox txtSoLuong;
        private System.Windows.Forms.Label labelGiaNhap;
        private System.Windows.Forms.TextBox txtGiaNhap;
        private System.Windows.Forms.Button btnThemVaoHD;
        private System.Windows.Forms.DataGridView dgvChiTiet;
        private System.Windows.Forms.Button btnLuuHoaDon;
        private System.Windows.Forms.Button btnLamMoi;
        private System.Windows.Forms.Button btnDong;
        private System.Windows.Forms.Label labelGhiChu;
        private System.Windows.Forms.TextBox txtGhiChu;
        private System.Windows.Forms.Label labelTongTien;
        private System.Windows.Forms.TextBox txtTongTien;
        private System.Windows.Forms.ComboBox cboNCC;
        private System.Windows.Forms.TextBox txtMaNCC;
        private System.Windows.Forms.Label label1;
    }
}