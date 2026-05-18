using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QLBH
{
    public partial class frmTraCuuHDN : Form
    {
        public frmTraCuuHDN()
        {
            InitializeComponent();
        }

        // ─── Form Load ────────────────────────────────────────────────────
        private void frmTraCuuHDN_Load(object sender, EventArgs e)
        {
            dtpTu.Value  = DateTime.Today.AddMonths(-1);
            dtpDen.Value = DateTime.Today;
            TimKiem();
        }

        // ─── Tìm kiếm hóa đơn nhập ───────────────────────────────────────
        private void TimKiem()
        {
            try
            {
                string sql = @"
                    SELECT  h.MaHDN,
                            CONVERT(NVARCHAR(16), h.NgayNhap, 103) + ' ' +
                                CONVERT(NVARCHAR(5), h.NgayNhap, 108)   AS NgayNhap,
                            h.MaNCC,
                            ISNULL(h.TenNCC, N'—')                      AS TenNCC,
                            ISNULL(h.TenNV,  N'—')                      AS NhanVien,
                            h.TongTien,
                            ISNULL(h.GhiChu, '')                        AS GhiChu
                    FROM    HoaDonNhap h
                    WHERE   CAST(h.NgayNhap AS DATE) BETWEEN @tu AND @den";

                string tuKhoa = txtTimKiem.Text.Trim();
                if (!string.IsNullOrEmpty(tuKhoa))
                    sql += @" AND (h.MaHDN  LIKE @kw
                                OR h.MaNCC  LIKE @kw
                                OR h.TenNCC LIKE @kw
                                OR h.TenNV  LIKE @kw)";

                sql += " ORDER BY h.NgayNhap DESC";

                using (var conn = DBConnect.Connect())
                using (var cmd  = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@tu",  dtpTu.Value.Date);
                    cmd.Parameters.AddWithValue("@den", dtpDen.Value.Date);
                    if (!string.IsNullOrEmpty(tuKhoa))
                        cmd.Parameters.AddWithValue("@kw", "%" + tuKhoa + "%");

                    var dt = new DataTable();
                    new SqlDataAdapter(cmd).Fill(dt);

                    dgvHoaDon.DataSource = dt;
                    lblTongHDN.Text = "Tổng hóa đơn nhập: " + dt.Rows.Count;

                    decimal tongTien = 0;
                    foreach (DataRow row in dt.Rows)
                        tongTien += Convert.ToDecimal(row["TongTien"]);
                    lblTongTien.Text = "Tổng chi: " + tongTien.ToString("N0") + " đ";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ─── Click dòng → load chi tiết ──────────────────────────────────
        private void dgvHoaDon_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            string maHDN = dgvHoaDon.Rows[e.RowIndex].Cells["MaHDN"].Value?.ToString();
            if (string.IsNullOrEmpty(maHDN)) return;
            LoadChiTiet(maHDN);
        }

        private void LoadChiTiet(string maHDN)
        {
            try
            {
                string sql = @"
                    SELECT  ct.MaSP,
                            s.TenSanPham        AS [Ten san pham],
                            ct.SoLuong          AS [So luong],
                            ct.GiaNhap          AS [Gia nhap],
                            ct.SoLuong * ct.GiaNhap AS [Thanh tien]
                    FROM    HoaDonNhap_ChiTiet ct
                    JOIN    SanPham s ON s.MaSanPham = ct.MaSP
                    WHERE   ct.MaHDN = @ma
                    ORDER BY ct.ID";

                using (var conn = DBConnect.Connect())
                using (var cmd  = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@ma", maHDN);
                    var dt = new DataTable();
                    new SqlDataAdapter(cmd).Fill(dt);
                    dgvChiTiet.DataSource = dt;
                }

                lblMaHDN.Text = "Chi tiết HDN: " + maHDN;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải chi tiết: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ─── Format cột tiền ─────────────────────────────────────────────
        private void dgvHoaDon_DataBindingComplete(object sender,
            DataGridViewBindingCompleteEventArgs e)
        {
            FormatCotTien(dgvHoaDon, new[] { "TongTien" });
        }

        private void dgvChiTiet_DataBindingComplete(object sender,
            DataGridViewBindingCompleteEventArgs e)
        {
            FormatCotTien(dgvChiTiet, new[] { "Gia nhap", "Thanh tien" });
        }

        private void FormatCotTien(DataGridView dgv, string[] cols)
        {
            foreach (var col in cols)
            {
                if (!dgv.Columns.Contains(col)) continue;
                dgv.Columns[col].DefaultCellStyle.Format    = "N0";
                dgv.Columns[col].DefaultCellStyle.Alignment =
                    DataGridViewContentAlignment.MiddleRight;
            }
        }

        // ─── Buttons ─────────────────────────────────────────────────────
        private void btnTimKiem_Click(object sender, EventArgs e) => TimKiem();

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtTimKiem.Clear();
            dtpTu.Value  = DateTime.Today.AddMonths(-1);
            dtpDen.Value = DateTime.Today;
            dgvChiTiet.DataSource = null;
            lblMaHDN.Text = "Chi tiết HDN: (chọn một hóa đơn)";
            TimKiem();
        }

        private void btnDong_Click(object sender, EventArgs e) => this.Close();

        private void txtTimKiem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) TimKiem();
        }
    }
}
