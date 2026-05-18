using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QLBH
{
    public partial class frmNhapHang : Form
    {
        public frmNhapHang()
        {
            InitializeComponent();
            SetupDGV();
            txtSoLuong.KeyPress += NhapSoNguyen;
            txtGiaNhap.KeyPress += NhapSoNguyen;
            dtNgayNhap.Value = DateTime.Now;
            GenerateMaHDN();
            LoadDanhSachNCC();

            // Gán event phòng khi Designer chưa wire
            cboNCC.SelectedIndexChanged += cboNCC_SelectedIndexChanged;
        }

        // ─── Cài cột DGV ──────────────────────────────────────────────────
        private void SetupDGV()
        {
            dgvChiTiet.Columns.Clear();
            dgvChiTiet.Columns.Add("MaSP", "Mã SP");
            dgvChiTiet.Columns.Add("TenSP", "Tên sản phẩm");
            dgvChiTiet.Columns.Add("GiaNhap", "Giá nhập");
            dgvChiTiet.Columns.Add("SoLuong", "Số lượng");
            dgvChiTiet.Columns.Add("ThanhTien", "Thành tiền");

            dgvChiTiet.Columns["GiaNhap"].DefaultCellStyle.Format = "N0";
            dgvChiTiet.Columns["ThanhTien"].DefaultCellStyle.Format = "N0";
            dgvChiTiet.Columns["GiaNhap"].ReadOnly = false;
            dgvChiTiet.Columns["ThanhTien"].DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleRight;
            // dgvChiTiet.Columns["TenSP"].Width = 220;
            dgvChiTiet.CellEndEdit += DgvChiTiet_CellEndEdit;

        }
        private void DgvChiTiet_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var row = dgvChiTiet.Rows[e.RowIndex];
            string colName = dgvChiTiet.Columns[e.ColumnIndex].Name;

            if (colName == "GiaNhap" || colName == "SoLuong")
            {
                if (decimal.TryParse(row.Cells["GiaNhap"].Value?.ToString(), out decimal gia) &&
                    int.TryParse(row.Cells["SoLuong"].Value?.ToString(), out int sl))
                {
                    row.Cells["ThanhTien"].Value = gia * sl;
                    TinhTongTien();
                }
            }

        }

        // ─── Sinh mã HDN ──────────────────────────────────────────────────
        private void GenerateMaHDN()
        {
            try
            {
                using (var conn = DBConnect.Connect())
                using (var cmd = new SqlCommand(
                    "SELECT COUNT(*) FROM HoaDonNhap", conn))
                {
                    int count = (int)cmd.ExecuteScalar();
                    txtMaHDN.Text = "HDN" + (count + 1).ToString("D3");
                }
            }
            catch { txtMaHDN.Text = "HDN001"; }
        }

        // ─── Load NCC vào ComboBox ────────────────────────────────────────
        private void LoadDanhSachNCC()
        {
            try
            {
                using (var conn = DBConnect.Connect())
                using (var cmd = new SqlCommand(
                    "SELECT MaNCC, TenNCC FROM NhaCungCap ORDER BY MaNCC", conn))
                {
                    var dt = new DataTable();
                    new SqlDataAdapter(cmd).Fill(dt);

                    cboNCC.DataSource = dt;
                    cboNCC.DisplayMember = "TenNCC";
                    cboNCC.ValueMember = "MaNCC";

                    // Quan trọng: đặt sau khi bind để không trigger event sớm
                    cboNCC.SelectedIndex = -1;
                    txtMaNCC.Clear();
                }
            }
            catch { }
        }

        // ─── Chọn NCC → hiện mã vào txtMaNCC ────────────────────────────
        // FIX: đây là chỗ bạn bị mất — cần đảm bảo event này được gán
        private void cboNCC_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboNCC.SelectedItem is DataRowView row)
            {
                txtMaNCC.Text = row["MaNCC"]?.ToString() ?? "";
            }
        }

        // ─── Lấy MaNCC / TenNCC ──────────────────────────────────────────
        private string GetMaNCC()
        {
            if (cboNCC.SelectedItem is DataRowView row)
                return row["MaNCC"]?.ToString() ?? "";
            return txtMaNCC.Text.Trim();
        }

        private string GetTenNCC()
        {
            if (cboNCC.SelectedItem is DataRowView row)
                return row["TenNCC"]?.ToString() ?? "";
            return "";
        }

        // ─── Tìm sản phẩm ────────────────────────────────────────────────
        private void btnTimSP_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaSP.Text)) return;
            try
            {
                using (var conn = DBConnect.Connect())
                using (var cmd = new SqlCommand(
                    @"SELECT TenSanPham, GiaNhap,
                             ISNULL(SoLuongNhap,0) - ISNULL(DaBan,0) AS TonKho
                      FROM   SanPham WHERE MaSanPham = @Ma", conn))
                {
                    cmd.Parameters.AddWithValue("@Ma", txtMaSP.Text.Trim());
                    using (var dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            lblTenSP.Text = dr["TenSanPham"].ToString();
                            lblTonKho.Text = Convert.ToInt32(dr["TonKho"]).ToString();
                            txtGiaNhap.Text = Convert.ToDecimal(dr["GiaNhap"]).ToString();
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy sản phẩm!", "Lỗi",
                                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                            XoaChiTietSP();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi");
            }
        }

        // ─── Thêm SP vào DGV ─────────────────────────────────────────────
        private void btnThemVaoHD_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaSP.Text) ||
                string.IsNullOrWhiteSpace(txtSoLuong.Text) ||
                string.IsNullOrWhiteSpace(lblTenSP.Text))
            {
                MessageBox.Show("Vui lòng tìm sản phẩm và nhập số lượng!", "Cảnh báo",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!int.TryParse(txtSoLuong.Text, out int sl) || sl <= 0)
            {
                MessageBox.Show("Số lượng phải lớn hơn 0!", "Cảnh báo",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!decimal.TryParse(txtGiaNhap.Text, out decimal giaNhap) || giaNhap <= 0)
            {
                MessageBox.Show("Giá nhập không hợp lệ!", "Cảnh báo",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Gộp nếu SP đã có
            foreach (DataGridViewRow row in dgvChiTiet.Rows)
            {
                if (row.Cells["MaSP"].Value?.ToString() != txtMaSP.Text.Trim()) continue;
                int slCu = Convert.ToInt32(row.Cells["SoLuong"].Value);
                row.Cells["SoLuong"].Value = slCu + sl;
                row.Cells["ThanhTien"].Value = (slCu + sl) * giaNhap;
                TinhTongTien();
                XoaChiTietSP();
                return;
            }

            dgvChiTiet.Rows.Add(txtMaSP.Text.Trim(), lblTenSP.Text,
                                 giaNhap, sl, giaNhap * sl);
            TinhTongTien();
            XoaChiTietSP();
        }

        // ─── Tính tổng tiền ───────────────────────────────────────────────
        private void TinhTongTien()
        {
            decimal tong = 0;
            foreach (DataGridViewRow row in dgvChiTiet.Rows)
                if (row.Cells["ThanhTien"].Value != null)
                    tong += Convert.ToDecimal(row.Cells["ThanhTien"].Value);
            txtTongTien.Text = tong.ToString("N0");
        }

        // ─── Lưu hóa đơn nhập ────────────────────────────────────────────
        private void btnLuuHoaDon_Click(object sender, EventArgs e)
        {
            if (dgvChiTiet.Rows.Count == 0)
            {
                MessageBox.Show("Hóa đơn chưa có sản phẩm!", "Cảnh báo",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string maNCC = GetMaNCC();
            string tenNCC = GetTenNCC();

            if (string.IsNullOrWhiteSpace(maNCC))
            {
                MessageBox.Show("Vui lòng chọn Nhà Cung Cấp!", "Cảnh báo",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                decimal tongTien = decimal.Parse(
                    txtTongTien.Text.Replace(".", "").Replace(",", "").Trim());

                string maNV = frmDangNhap.MaNhanVien;
                string tenNV = frmDangNhap.HoTenNhanVien;

                using (var conn = DBConnect.Connect())
                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        // 1. Header hóa đơn nhập
                        using (var cmd = new SqlCommand(@"
                            INSERT INTO HoaDonNhap
                                (MaHDN, NgayNhap, MaNCC, TenNCC, MaNV, TenNV,
                                 TongTien, GhiChu)
                            VALUES
                                (@mahdn, @ngay, @mancc, @tenncc, @manv, @tennv,
                                 @tong, @ghichu)",
                            conn, trans))
                        {
                            cmd.Parameters.AddWithValue("@mahdn", txtMaHDN.Text);
                            cmd.Parameters.AddWithValue("@ngay", dtNgayNhap.Value);
                            cmd.Parameters.AddWithValue("@mancc", maNCC);
                            cmd.Parameters.AddWithValue("@tenncc", tenNCC);
                            cmd.Parameters.AddWithValue("@manv", maNV);
                            cmd.Parameters.AddWithValue("@tennv", tenNV);
                            cmd.Parameters.AddWithValue("@tong", tongTien);
                            cmd.Parameters.AddWithValue("@ghichu",
                                string.IsNullOrWhiteSpace(txtGhiChu.Text)
                                    ? (object)DBNull.Value
                                    : txtGhiChu.Text.Trim());
                            cmd.ExecuteNonQuery();
                        }

                        // 2. Chi tiết + cập nhật tồn kho
                        foreach (DataGridViewRow row in dgvChiTiet.Rows)
                        {
                            if (row.Cells["MaSP"].Value == null) continue;
                            string maSP = row.Cells["MaSP"].Value.ToString();
                            int soLuong = Convert.ToInt32(row.Cells["SoLuong"].Value);
                            decimal giaNhap = Convert.ToDecimal(row.Cells["GiaNhap"].Value);

                            using (var cmd = new SqlCommand(@"
                            INSERT INTO HoaDonNhap_ChiTiet
                             (MaHDN, MaSP, SoLuong, DonGiaNhap)
                             VALUES
                            (@mahdn, @masp, @sl, @gn)",
                               conn, trans))
                            {
                                cmd.Parameters.AddWithValue("@mahdn", txtMaHDN.Text);
                                cmd.Parameters.AddWithValue("@masp", maSP);
                                cmd.Parameters.AddWithValue("@sl", soLuong);
                                cmd.Parameters.AddWithValue("@gn", giaNhap);
                                cmd.ExecuteNonQuery();
                            }

                            using (var cmd = new SqlCommand(@"
                                UPDATE SanPham
                                SET    SoLuongNhap = ISNULL(SoLuongNhap,0) + @sl,
                                       GiaNhap     = @gn,
                                       NgayNhap    = @ngay,
                                       TinhTrang   = N'Còn hàng'
                                WHERE  MaSanPham   = @masp",
                                conn, trans))
                            {
                                cmd.Parameters.AddWithValue("@sl", soLuong);
                                cmd.Parameters.AddWithValue("@gn", giaNhap);
                                cmd.Parameters.AddWithValue("@ngay", dtNgayNhap.Value);
                                cmd.Parameters.AddWithValue("@masp", maSP);
                                cmd.ExecuteNonQuery();
                            }
                        }

                        trans.Commit();

                        MessageBox.Show(
                            $"Lưu thành công!\n" +
                            $"Mã HDN    : {txtMaHDN.Text}\n" +
                            $"NCC       : {tenNCC} ({maNCC})\n" +
                            $"NV nhập   : {tenNV}\n" +
                            $"Tổng tiền : {tongTien:N0} VNĐ",
                            "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Về lại FrmMain sau khi lưu thành công
                        QuayVeMain();
                    }
                    catch { trans.Rollback(); throw; }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ─── Xóa dòng DGV ────────────────────────────────────────────────
        private void btnXoaDong_Click(object sender, EventArgs e)
        {
            if (dgvChiTiet.SelectedRows.Count > 0)
            {
                dgvChiTiet.Rows.RemoveAt(dgvChiTiet.SelectedRows[0].Index);
                TinhTongTien();
            }
        }

        // ─── Làm mới ─────────────────────────────────────────────────────
        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            dgvChiTiet.Rows.Clear();
            txtTongTien.Clear();
            txtGhiChu.Clear();
            txtMaNCC.Clear();
            cboNCC.SelectedIndex = -1;
            GenerateMaHDN();
            XoaChiTietSP();
        }

        // ─── Đóng → về FrmMain ───────────────────────────────────────────
        private void btnDong_Click(object sender, EventArgs e) => QuayVeMain();

        private void QuayVeMain()
        {
            new FrmMain().Show();
            this.Hide();
        }

        // ─── Helpers ─────────────────────────────────────────────────────
        private void XoaChiTietSP()
        {
            txtMaSP.Clear();
            txtSoLuong.Clear();
            txtGiaNhap.Clear();
            lblTenSP.Text = "";
            lblTonKho.Text = "0";
            txtMaSP.Focus();
        }

        private void NhapSoNguyen(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;
        }

        private void txtMaSP_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) btnTimSP_Click(null, null);
        }

        private void btnDong_Click_1(object sender, EventArgs e)
        {
            FrmMain frmMain = new FrmMain();
            frmMain.Show();
            Hide();
        }

        private void txtGiaNhap_TextChanged(object sender, EventArgs e)
        {

        }

        private void frmNhapHang_Load(object sender, EventArgs e)
        {

        }
    }
}