using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace QLBH
{
    public partial class frmhoadon : Form
    {
        // Lưu lại để dùng khi in
        private string _maHDB = "";
        private decimal _tongTien = 0;
        private decimal _khachTra = 0;
        private decimal _traLai = 0;

        public frmhoadon()
        {
            InitializeComponent();
            SetupDataGridView();
            txtSoLuong.KeyPress += NhapSoNguyen;
            txtKhachThanhToan.KeyPress += NhapSoThapPhan;
            txtKhachThanhToan.TextChanged += (s, e) => TinhTien();
        }

        // ─── Cài cột DGV ──────────────────────────────────────────────────
        private void SetupDataGridView()
        {
            dgvHoaDon.Columns.Clear();
            dgvHoaDon.Columns.Add("MaSanPham", "Mã SP");
            dgvHoaDon.Columns.Add("SanPham", "Sản phẩm");
            dgvHoaDon.Columns.Add("DonGia", "Đơn giá");
            dgvHoaDon.Columns.Add("SoLuong", "Số lượng");
            dgvHoaDon.Columns.Add("ThanhTien", "Thành tiền");

            dgvHoaDon.Columns["DonGia"].DefaultCellStyle.Format
                = dgvHoaDon.Columns["ThanhTien"].DefaultCellStyle.Format = "N0";
            dgvHoaDon.Columns["DonGia"].DefaultCellStyle.Alignment
                = dgvHoaDon.Columns["ThanhTien"].DefaultCellStyle.Alignment
                = DataGridViewContentAlignment.MiddleRight;

            dgvHoaDon.Columns["MaSanPham"].ReadOnly = true;
            dgvHoaDon.Columns["SanPham"].ReadOnly = true;
            dgvHoaDon.Columns["DonGia"].ReadOnly = true;
            dgvHoaDon.Columns["ThanhTien"].ReadOnly = true;

            dgvHoaDon.CellEndEdit += (s, ev) =>
            {
                if (dgvHoaDon.Columns[ev.ColumnIndex].Name != "SoLuong") return;
                var row = dgvHoaDon.Rows[ev.RowIndex];
                if (row.Cells["DonGia"].Value == null) return;
                if (int.TryParse(row.Cells["SoLuong"].Value?.ToString(), out int sl) && sl > 0)
                {
                    decimal dg = Convert.ToDecimal(row.Cells["DonGia"].Value);
                    row.Cells["ThanhTien"].Value = sl * dg;
                }
                TinhTien();
            };
        }

        // ─── Thêm sản phẩm ────────────────────────────────────────────────
        private void btnThemSP_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaSP.Text) ||
                string.IsNullOrWhiteSpace(txtSoLuong.Text))
            {
                MessageBox.Show("Vui lòng nhập mã SP và số lượng!", "Cảnh báo",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!int.TryParse(txtSoLuong.Text, out int soLuong) || soLuong <= 0)
            {
                MessageBox.Show("Số lượng phải là số nguyên dương!", "Cảnh báo",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (var conn = DBConnect.Connect())
                using (var cmd = new SqlCommand(
                    @"SELECT TenSanPham, GiaXuat,
                             ISNULL(SoLuongNhap,0) - ISNULL(DaBan,0) AS TonKho
                      FROM   SanPham WHERE MaSanPham = @Ma", conn))
                {
                    cmd.Parameters.AddWithValue("@Ma", txtMaSP.Text.Trim());
                    using (var dr = cmd.ExecuteReader())
                    {
                        if (!dr.Read())
                        {
                            MessageBox.Show("Mã sản phẩm không tồn tại!", "Lỗi",
                                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        int tonKho = Convert.ToInt32(dr["TonKho"]);
                        string tenSP = dr["TenSanPham"].ToString();
                        decimal giaXuat = Convert.ToDecimal(dr["GiaXuat"]);

                        if (tonKho < soLuong)
                        {
                            MessageBox.Show($"'{tenSP}' chỉ còn {tonKho} cái!", "Không đủ hàng",
                                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        // Gộp nếu đã có
                        foreach (DataGridViewRow existRow in dgvHoaDon.Rows)
                        {
                            if (existRow.Cells["MaSanPham"].Value?.ToString()
                                != txtMaSP.Text.Trim()) continue;

                            int slCu = Convert.ToInt32(existRow.Cells["SoLuong"].Value);
                            if (slCu + soLuong > tonKho)
                            {
                                MessageBox.Show($"Tổng vượt tồn kho ({tonKho})!", "Cảnh báo",
                                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                            existRow.Cells["SoLuong"].Value = slCu + soLuong;
                            existRow.Cells["ThanhTien"].Value = (slCu + soLuong) * giaXuat;
                            TinhTien();
                            XoaONhap();
                            return;
                        }

                        int idx = dgvHoaDon.Rows.Add();
                        dgvHoaDon.Rows[idx].Cells["MaSanPham"].Value = txtMaSP.Text.Trim();
                        dgvHoaDon.Rows[idx].Cells["SanPham"].Value = tenSP;
                        dgvHoaDon.Rows[idx].Cells["DonGia"].Value = giaXuat;
                        dgvHoaDon.Rows[idx].Cells["SoLuong"].Value = soLuong;
                        dgvHoaDon.Rows[idx].Cells["ThanhTien"].Value = soLuong * giaXuat;
                        TinhTien();
                        XoaONhap();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ─── Tính tiền ────────────────────────────────────────────────────
        private void TinhTien()
        {
            decimal tong = 0;
            foreach (DataGridViewRow row in dgvHoaDon.Rows)
            {
                if (row.Cells["SoLuong"].Value == null ||
                    row.Cells["DonGia"].Value == null) continue;
                decimal tt = Convert.ToDecimal(row.Cells["SoLuong"].Value)
                           * Convert.ToDecimal(row.Cells["DonGia"].Value);
                row.Cells["ThanhTien"].Value = tt;
                tong += tt;
            }
            txtThanhTien.Text = tong.ToString("N0");

            if (decimal.TryParse(
                    txtKhachThanhToan.Text.Replace(",", "").Replace(".", ""),
                    out decimal kh))
                txtTienTraLai.Text = (kh - tong).ToString("N0");
            else
                txtTienTraLai.Clear();
        }

        private void btnTinhTien_Click(object sender, EventArgs e) => TinhTien();

        // ─── Xuất + Lưu hóa đơn ──────────────────────────────────────────
        private void btnXuatHoaDon_Click(object sender, EventArgs e)
        {
            if (dgvHoaDon.Rows.Count == 0)
            {
                MessageBox.Show("Hóa đơn chưa có sản phẩm!", "Cảnh báo",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!decimal.TryParse(
                    txtKhachThanhToan.Text.Replace(",", "").Replace(".", ""),
                    out decimal khachTra) || khachTra <= 0)
            {
                MessageBox.Show("Vui lòng nhập tiền khách thanh toán!", "Cảnh báo",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Tính tổng
            decimal tongTien = 0;
            int tongSL = 0;
            foreach (DataGridViewRow row in dgvHoaDon.Rows)
            {
                if (row.Cells["ThanhTien"].Value == null) continue;
                tongTien += Convert.ToDecimal(row.Cells["ThanhTien"].Value);
                tongSL += Convert.ToInt32(row.Cells["SoLuong"].Value);
            }

            if (khachTra < tongTien)
            {
                MessageBox.Show("Tiền khách đưa không đủ!", "Cảnh báo",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal traLai = khachTra - tongTien;

            try
            {
                // Sinh mã HDB
                string maHDB;
                using (var conn0 = DBConnect.Connect())
                using (var cmd0 = new SqlCommand(
                    "SELECT COUNT(*) FROM HoaDonBan", conn0))
                {
                    int count = (int)cmd0.ExecuteScalar();
                    maHDB = "HDB" + (count + 1).ToString("D3");
                }

                // Lưu DB trong transaction
                using (var conn = DBConnect.Connect())
                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        // 1. Header
                        using (var cmd = new SqlCommand(@"
                            INSERT INTO HoaDonBan
                                (MaHDB, NgayBan, MaNV, TenKH, SDT, DiaChi,
                                 TongTien, KhachTra, TraLai)
                            VALUES
                                (@hdb, GETDATE(), @nv, @tenKH, @sdt, @dc,
                                 @tong, @kh, @tl)",
                            conn, trans))
                        {
                            cmd.Parameters.AddWithValue("@hdb", maHDB);
                            cmd.Parameters.AddWithValue("@nv", frmDangNhap.MaNhanVien ?? "");
                            cmd.Parameters.AddWithValue("@tenKH", txtTenKH.Text.Trim());
                            cmd.Parameters.AddWithValue("@sdt", txtSDT.Text.Trim());
                            cmd.Parameters.AddWithValue("@dc", txtDiaChi.Text.Trim());
                            cmd.Parameters.AddWithValue("@tong", tongTien);
                            cmd.Parameters.AddWithValue("@kh", khachTra);
                            cmd.Parameters.AddWithValue("@tl", traLai);
                            cmd.ExecuteNonQuery();
                        }

                        // 2. Chi tiết + trừ tồn kho
                        foreach (DataGridViewRow row in dgvHoaDon.Rows)
                        {
                            if (row.Cells["MaSanPham"].Value == null) continue;
                            string maSP = row.Cells["MaSanPham"].Value.ToString();
                            string tenSP = row.Cells["SanPham"].Value?.ToString() ?? "";
                            int sl = Convert.ToInt32(row.Cells["SoLuong"].Value);
                            decimal donGia = Convert.ToDecimal(row.Cells["DonGia"].Value);

                            // Chi tiết (ThanhTien là computed, KHÔNG insert)
                            using (var cmd = new SqlCommand(@"
                                INSERT INTO HoaDonBan_ChiTiet
                                    (MaHDB, MaSP, TenSP, SoLuong, DonGia)
                                VALUES
                                    (@hdb, @sp, @tenSP, @sl, @dg)",
                                conn, trans))
                            {
                                cmd.Parameters.AddWithValue("@hdb", maHDB);
                                cmd.Parameters.AddWithValue("@sp", maSP);
                                cmd.Parameters.AddWithValue("@tenSP", tenSP);
                                cmd.Parameters.AddWithValue("@sl", sl);
                                cmd.Parameters.AddWithValue("@dg", donGia);
                                cmd.ExecuteNonQuery();
                            }

                            // Trừ tồn kho
                            using (var cmd = new SqlCommand(@"
                                UPDATE SanPham
                                SET    DaBan = ISNULL(DaBan,0) + @sl
                                WHERE  MaSanPham = @ma",
                                conn, trans))
                            {
                                cmd.Parameters.AddWithValue("@sl", sl);
                                cmd.Parameters.AddWithValue("@ma", maSP);
                                cmd.ExecuteNonQuery();
                            }
                        }

                        trans.Commit();
                    }
                    catch { trans.Rollback(); throw; }
                }

                // Lưu thông tin để in
                _maHDB = maHDB;
                _tongTien = tongTien;
                _khachTra = khachTra;
                _traLai = traLai;

                // Lưu KH nếu mua nhiều
                if (tongSL > 20 && !string.IsNullOrWhiteSpace(txtTenKH.Text))
                    LuuHoacCapNhatKhachHang();

                // Hiện PrintPreviewDialog
                var pd = new PrintDocument();
                pd.PrintPage += PrintHoaDon;

                using (var preview = new PrintPreviewDialog())
                {
                    preview.Document = pd;
                    preview.Width = 700;
                    preview.Height = 900;
                    preview.Text = $"Preview hóa đơn {maHDB}";
                    preview.ShowDialog();
                }

                btnLamMoi_Click(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ─── Vẽ hóa đơn lên PrintDocument ───────────────────────────────
        private void PrintHoaDon(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            float x = 50f;
            float y = 30f;
            float row = 22f;  // khoảng cách dòng

            var fTitle = new Font("Arial", 16, FontStyle.Bold);
            var fBold = new Font("Arial", 10, FontStyle.Bold);
            var fNormal = new Font("Arial", 10, FontStyle.Regular);
            var fSmall = new Font("Arial", 9, FontStyle.Regular);
            var brush = Brushes.Black;

            // ── Tiêu đề ────────────────────────────────────────────────────
            g.DrawString("HÓA ĐƠN BÁN HÀNG", fTitle, brush,
                         e.PageBounds.Width / 2f - 110f, y);
            y += 35f;

            g.DrawString($"Mã HĐ : {_maHDB}", fBold, brush, x, y); y += row;
            g.DrawString($"Ngày  : {DateTime.Now:dd/MM/yyyy  HH:mm}", fNormal, brush, x, y); y += row;
            g.DrawString($"NV bán: {frmDangNhap.HoTenNhanVien}", fNormal, brush, x, y); y += row;
            g.DrawString($"Khách : {txtTenKH.Text}", fNormal, brush, x, y); y += row;
            g.DrawString($"SDT   : {txtSDT.Text}", fNormal, brush, x, y); y += row;
            g.DrawString($"Địa chỉ: {txtDiaChi.Text}", fNormal, brush, x, y); y += row;

            // ── Đường kẻ ──────────────────────────────────────────────────
            y += 5f;
            g.DrawLine(Pens.Black, x, y, e.PageBounds.Width - x, y);
            y += 8f;

            // ── Header bảng ────────────────────────────────────────────────
            float c1 = x, c2 = x + 200f, c3 = x + 320f, c4 = x + 390f, c5 = x + 460f;
            g.DrawString("Sản phẩm", fBold, brush, c1, y);
            g.DrawString("Đơn giá", fBold, brush, c2, y);
            g.DrawString("SL", fBold, brush, c3, y);
            g.DrawString("Thành tiền", fBold, brush, c4, y);
            y += row;
            g.DrawLine(Pens.Black, x, y, e.PageBounds.Width - x, y);
            y += 8f;

            // ── Chi tiết ───────────────────────────────────────────────────
            foreach (DataGridViewRow dr in dgvHoaDon.Rows)
            {
                if (dr.Cells["SanPham"].Value == null) continue;
                string ten = dr.Cells["SanPham"].Value.ToString();
                decimal dg = Convert.ToDecimal(dr.Cells["DonGia"].Value);
                int sl = Convert.ToInt32(dr.Cells["SoLuong"].Value);
                decimal tt = Convert.ToDecimal(dr.Cells["ThanhTien"].Value);

                g.DrawString(ten, fSmall, brush, c1, y);
                g.DrawString(dg.ToString("N0"), fSmall, brush, c2, y);
                g.DrawString(sl.ToString(), fSmall, brush, c3, y);
                g.DrawString(tt.ToString("N0"), fSmall, brush, c4, y);
                y += row;
            }

            // ── Tổng kết ───────────────────────────────────────────────────
            y += 5f;
            g.DrawLine(Pens.Black, x, y, e.PageBounds.Width - x, y);
            y += 10f;

            g.DrawString($"Tổng tiền : {_tongTien:N0} đ", fBold, brush, c2, y); y += row;
            g.DrawString($"Khách trả : {_khachTra:N0} đ", fNormal, brush, c2, y); y += row;
            g.DrawString($"Trả lại   : {_traLai:N0} đ", fBold, brush, c2, y); y += row * 2;

            g.DrawString("Cảm ơn quý khách! Hẹn gặp lại.", fNormal, brush,
                         e.PageBounds.Width / 2f - 120f, y);
        }

        // ─── Làm mới ──────────────────────────────────────────────────────
        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtTenKH.Clear(); txtSDT.Clear(); txtDiaChi.Clear();
            txtThanhTien.Clear(); txtKhachThanhToan.Clear(); txtTienTraLai.Clear();
            dgvHoaDon.Rows.Clear();
            XoaONhap();
        }

        private void btnDong_Click(object sender, EventArgs e)
        { new FrmMain().Show(); Hide(); }

        // ─── Lưu / Cập nhật khách hàng ───────────────────────────────────
        private void LuuHoacCapNhatKhachHang()
        {
            try
            {
                using (var conn = DBConnect.Connect())
                using (var cmd = new SqlCommand(
                    "SELECT COUNT(*) FROM KhachHang WHERE SoDienThoai=@sdt", conn))
                {
                    cmd.Parameters.AddWithValue("@sdt", txtSDT.Text.Trim());
                    bool toTon = (int)cmd.ExecuteScalar() > 0;

                    if (toTon)
                    {
                        using (var upd = new SqlCommand(@"
                            UPDATE KhachHang
                            SET HoVaTen=@ten, DiaChi=@dc
                            WHERE SoDienThoai=@sdt", conn))
                        {
                            upd.Parameters.AddWithValue("@ten", txtTenKH.Text.Trim());
                            upd.Parameters.AddWithValue("@dc", txtDiaChi.Text.Trim());
                            upd.Parameters.AddWithValue("@sdt", txtSDT.Text.Trim());
                            upd.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        string maKH = "KH" + DateTime.Now.ToString("yyMMddHHmmss");
                        using (var ins = new SqlCommand(@"
                            INSERT INTO KhachHang
                                (MaKH, HoVaTen, DiaChi, GioiTinh, SoDienThoai, NgaySinh, Email)
                            VALUES
                                (@maKH, @ten, @dc, N'Khác', @sdt, GETDATE(), '')", conn))
                        {
                            ins.Parameters.AddWithValue("@maKH", maKH);
                            ins.Parameters.AddWithValue("@ten", txtTenKH.Text.Trim());
                            ins.Parameters.AddWithValue("@dc", txtDiaChi.Text.Trim());
                            ins.Parameters.AddWithValue("@sdt", txtSDT.Text.Trim());
                            ins.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi lưu KH: " + ex.Message);
            }
        }

        // ─── Helpers ──────────────────────────────────────────────────────
        private void XoaONhap()
        { txtMaSP.Clear(); txtSoLuong.Clear(); txtMaSP.Focus(); }

        private void NhapSoNguyen(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;
        }

        private void NhapSoThapPhan(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
                e.Handled = true;
            if (e.KeyChar == '.' && ((TextBox)sender).Text.Contains("."))
                e.Handled = true;
        }

        private void frmhoadon_Load(object sender, EventArgs e)
        {

        }
    }
}