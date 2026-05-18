namespace QLBH
{
    partial class frmthongke
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
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
            this.lblTongSP = new System.Windows.Forms.Label();
            this.lblTongNhap = new System.Windows.Forms.Label();
            this.lblDaBan = new System.Windows.Forms.Label();
            this.lblDoanhThu = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnDong = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblTongNhapTien = new System.Windows.Forms.Label();
            this.lblTongHDN = new System.Windows.Forms.Label();
            this.lblTongHDB = new System.Windows.Forms.Label();
            this.lblLoiNhuan = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblTongSP
            // 
            this.lblTongSP.AutoSize = true;
            this.lblTongSP.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold);
            this.lblTongSP.Location = new System.Drawing.Point(306, 9);
            this.lblTongSP.Name = "lblTongSP";
            this.lblTongSP.Size = new System.Drawing.Size(27, 29);
            this.lblTongSP.TabIndex = 0;
            this.lblTongSP.Text = "0";
            // 
            // lblTongNhap
            // 
            this.lblTongNhap.AutoSize = true;
            this.lblTongNhap.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold);
            this.lblTongNhap.Location = new System.Drawing.Point(306, 50);
            this.lblTongNhap.Name = "lblTongNhap";
            this.lblTongNhap.Size = new System.Drawing.Size(27, 29);
            this.lblTongNhap.TabIndex = 1;
            this.lblTongNhap.Text = "0";
            // 
            // lblDaBan
            // 
            this.lblDaBan.AutoSize = true;
            this.lblDaBan.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold);
            this.lblDaBan.Location = new System.Drawing.Point(306, 86);
            this.lblDaBan.Name = "lblDaBan";
            this.lblDaBan.Size = new System.Drawing.Size(27, 29);
            this.lblDaBan.TabIndex = 2;
            this.lblDaBan.Text = "0";
            // 
            // lblDoanhThu
            // 
            this.lblDoanhThu.AutoSize = true;
            this.lblDoanhThu.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold);
            this.lblDoanhThu.ForeColor = System.Drawing.Color.DarkGreen;
            this.lblDoanhThu.Location = new System.Drawing.Point(306, 262);
            this.lblDoanhThu.Name = "lblDoanhThu";
            this.lblDoanhThu.Size = new System.Drawing.Size(87, 29);
            this.lblDoanhThu.TabIndex = 3;
            this.lblDoanhThu.Text = "0 VNĐ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label1.Location = new System.Drawing.Point(76, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(181, 25);
            this.label1.TabIndex = 4;
            this.label1.Text = "Tổng số sản phẩm:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label2.Location = new System.Drawing.Point(76, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(192, 25);
            this.label2.TabIndex = 5;
            this.label2.Text = "Tổng số lượng nhập:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(76, 90);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(155, 25);
            this.label3.TabIndex = 6;
            this.label3.Text = "Tổng số đã bán:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label4.Location = new System.Drawing.Point(76, 262);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(108, 25);
            this.label4.TabIndex = 7;
            this.label4.Text = "Doanh thu:";
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(77, 348);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(180, 40);
            this.btnRefresh.TabIndex = 8;
            this.btnRefresh.Text = "Làm mới thống kê";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnDong
            // 
            this.btnDong.Location = new System.Drawing.Point(372, 348);
            this.btnDong.Name = "btnDong";
            this.btnDong.Size = new System.Drawing.Size(180, 40);
            this.btnDong.TabIndex = 9;
            this.btnDong.Text = "Đóng";
            this.btnDong.UseVisualStyleBackColor = true;
            this.btnDong.Click += new System.EventHandler(this.btnDong_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(76, 215);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(149, 25);
            this.label5.TabIndex = 12;
            this.label5.Text = "Tổng tiền nhập:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label6.Location = new System.Drawing.Point(76, 169);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(168, 25);
            this.label6.TabIndex = 11;
            this.label6.Text = "Số hóa đơn nhập:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label7.Location = new System.Drawing.Point(76, 123);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(157, 25);
            this.label7.TabIndex = 10;
            this.label7.Text = "Số hóa đơn bán:";
            // 
            // lblTongNhapTien
            // 
            this.lblTongNhapTien.AutoSize = true;
            this.lblTongNhapTien.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold);
            this.lblTongNhapTien.Location = new System.Drawing.Point(306, 211);
            this.lblTongNhapTien.Name = "lblTongNhapTien";
            this.lblTongNhapTien.Size = new System.Drawing.Size(27, 29);
            this.lblTongNhapTien.TabIndex = 15;
            this.lblTongNhapTien.Text = "0";
            // 
            // lblTongHDN
            // 
            this.lblTongHDN.AutoSize = true;
            this.lblTongHDN.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold);
            this.lblTongHDN.Location = new System.Drawing.Point(306, 166);
            this.lblTongHDN.Name = "lblTongHDN";
            this.lblTongHDN.Size = new System.Drawing.Size(27, 29);
            this.lblTongHDN.TabIndex = 14;
            this.lblTongHDN.Text = "0";
            // 
            // lblTongHDB
            // 
            this.lblTongHDB.AutoSize = true;
            this.lblTongHDB.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold);
            this.lblTongHDB.Location = new System.Drawing.Point(306, 120);
            this.lblTongHDB.Name = "lblTongHDB";
            this.lblTongHDB.Size = new System.Drawing.Size(27, 29);
            this.lblTongHDB.TabIndex = 13;
            this.lblTongHDB.Text = "0";
            // 
            // lblLoiNhuan
            // 
            this.lblLoiNhuan.AutoSize = true;
            this.lblLoiNhuan.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold);
            this.lblLoiNhuan.ForeColor = System.Drawing.Color.DarkGreen;
            this.lblLoiNhuan.Location = new System.Drawing.Point(306, 306);
            this.lblLoiNhuan.Name = "lblLoiNhuan";
            this.lblLoiNhuan.Size = new System.Drawing.Size(87, 29);
            this.lblLoiNhuan.TabIndex = 16;
            this.lblLoiNhuan.Text = "0 VNĐ";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label8.Location = new System.Drawing.Point(76, 306);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(177, 25);
            this.label8.TabIndex = 17;
            this.label8.Text = "Lợi nhuận ước tính:";
            // 
            // frmthongke
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 400);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.lblLoiNhuan);
            this.Controls.Add(this.lblTongNhapTien);
            this.Controls.Add(this.lblTongHDN);
            this.Controls.Add(this.lblTongHDB);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btnDong);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.lblDoanhThu);
            this.Controls.Add(this.lblDaBan);
            this.Controls.Add(this.lblTongNhap);
            this.Controls.Add(this.lblTongSP);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "frmthongke";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Thống Kê Cửa Hàng";
            this.Load += new System.EventHandler(this.frmthongke_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        // Controls
        private System.Windows.Forms.Label lblTongSP;
        private System.Windows.Forms.Label lblTongNhap;
        private System.Windows.Forms.Label lblDaBan;
        private System.Windows.Forms.Label lblDoanhThu;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnDong;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblTongNhapTien;
        private System.Windows.Forms.Label lblTongHDN;
        private System.Windows.Forms.Label lblTongHDB;
        private System.Windows.Forms.Label lblLoiNhuan;
        private System.Windows.Forms.Label label8;
    }
}