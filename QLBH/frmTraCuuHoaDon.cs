using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QLBH
{
    public partial class frmTraCuuHoaDon : Form
    {
        public frmTraCuuHoaDon()
        {
            InitializeComponent();
        }

        // ─── Form Load ────────────────────────────────────────────────────
        private void frmTraCuuHoaDon_Load(object sender, EventArgs e)
        {
            dtpTu.Value  = DateTime.Today.AddMonths(-1);
            dtpDen.Value = DateTime.Today;
            TimKiem();
        }

        // ─── Tìm kiếm hóa đơn ────────────────────────────────────────────
        private void TimKiem()
        {
            try
            {
                string sql = @"
                    SELECT  h.MaHDB,
                            CONVERT(NVARCHAR(16), h.NgayBan, 103) + ' ' +
                                CONVERT(NVARCHAR(5),  h.NgayBan, 108)   AS NgayBan,
                            ISNULL(h.TenKH, N'Khách lẻ')               AS TenKH,
                            ISNULL(h.SDT,   '')                         AS SDT,
                            n.HoVaTen                                   AS NhanVien,
                            h.TongTien,
                            h.KhachTra,
                            h.TraLai
                    FROM    HoaDonBan h
                    LEFT JOIN NhanVien n ON n.MaThanhVien = h.MaNV
                    WHERE   CAST(h.NgayBan AS DATE) BETWEEN @tu AND @den";

                // Lọc theo tên KH hoặc mã HD
                string tuKhoa = txtTimKiem.Text.Trim();
                if (!string.IsNullOrEmpty(tuKhoa))
                    sql += @" AND (h.MaHDB    LIKE @kw
                                OR h.TenKH   LIKE @kw
                                OR h.SDT     LIKE @kw
                                OR n.HoVaTen LIKE @kw)";

                sql += " ORDER BY h.NgayBan DESC";

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
                    lblTongHD.Text  = "Tổng hóa đơn: " + dt.Rows.Count;

                    // Tính tổng doanh thu
                    decimal tongDT = 0;
                    foreach (DataRow row in dt.Rows)
                        tongDT += Convert.ToDecimal(row["TongTien"]);
                    lblDoanhThu.Text = "Doanh thu: " + tongDT.ToString("N0") + " đ";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ─── Load chi tiết khi click dòng ────────────────────────────────
        private void dgvHoaDon_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            string maHDB = dgvHoaDon.Rows[e.RowIndex].Cells["MaHDB"].Value?.ToString();
            if (string.IsNullOrEmpty(maHDB)) return;

            LoadChiTiet(maHDB);
        }

        private void LoadChiTiet(string maHDB)
        {
            try
            {
                string sql = @"
                    SELECT  ct.MaSP,
                            ct.TenSP        AS [Ten san pham],
                            ct.SoLuong      AS [So luong],
                            ct.DonGia       AS [Don gia],
                            ct.ThanhTien    AS [Thanh tien]
                    FROM    HoaDonBan_ChiTiet ct
                    WHERE   ct.MaHDB = @ma
                    ORDER BY ct.ID";

                using (var conn = DBConnect.Connect())
                using (var cmd  = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@ma", maHDB);
                    var dt = new DataTable();
                    new SqlDataAdapter(cmd).Fill(dt);
                    dgvChiTiet.DataSource = dt;
                }

                lblMaHDB.Text = "Chi tiết HD: " + maHDB;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải chi tiết: " + ex.Message, "Lỗi",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ─── Format cột tiền sau khi bind ─────────────────────────────────
        private void dgvHoaDon_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            FormatCotTien(dgvHoaDon, new[] { "TongTien", "KhachTra", "TraLai" });
        }

        private void dgvChiTiet_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            FormatCotTien(dgvChiTiet, new[] { "Don gia", "Thanh tien" });
        }

        private void FormatCotTien(DataGridView dgv, string[] cols)
        {
            foreach (var col in cols)
            {
                if (dgv.Columns.Contains(col))
                {
                    dgv.Columns[col].DefaultCellStyle.Format    = "N0";
                    dgv.Columns[col].DefaultCellStyle.Alignment =
                        DataGridViewContentAlignment.MiddleRight;
                }
            }
        }

        // ─── Buttons ──────────────────────────────────────────────────────
        private void btnTimKiem_Click(object sender, EventArgs e) => TimKiem();

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtTimKiem.Clear();
            dtpTu.Value  = DateTime.Today.AddMonths(-1);
            dtpDen.Value = DateTime.Today;
            dgvChiTiet.DataSource = null;
            lblMaHDB.Text = "Chi tiết HD: (chọn một hóa đơn)";
            TimKiem();
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
             var frmMain = new FrmMain();
            frmMain.Show();
            Hide();
        }

        // ─── Enter trong ô tìm kiếm ───────────────────────────────────────
        private void txtTimKiem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) TimKiem();
        }
    }
}
