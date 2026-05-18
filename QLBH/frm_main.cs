using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using QLBH.Properties;
using System.IO;

namespace QLBH
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
            LoadDsHangHoa();
            pic.Image             = Resources.noimage;
            dateNgayNhap.Format   = DateTimePickerFormat.Custom;
            dateNgayNhap.CustomFormat = "dd/MM/yyyy";
        }

        // ─── Load danh sách hàng hóa ──────────────────────────────────────
        private void LoadDsHangHoa()
        {
            var sql = @"SELECT MaSanPham, TenSanPham, KieuDang, GiaNhap, GiaXuat,
                               TinhTrang,
                               NgayNhap = CONVERT(NVARCHAR(10), NgayNhap, 103),
                               SoLuongNhap, DaBan, ChatLieu, HangSanXuat, AnhSanPham
                        FROM   SanPham";
            try
            {
                using (var conn = DBConnect.Connect())
                using (var cmd  = new SqlCommand(sql, conn))
                using (var dr   = cmd.ExecuteReader())
                {
                    dgv.Rows.Clear();
                    while (dr.Read())
                    {
                        var i   = dgv.Rows.Add();
                        var row = dgv.Rows[i];
                        row.Cells["MaSanPham"].Value   = dr["MaSanPham"];
                        row.Cells["TenSanPham"].Value  = dr["TenSanPham"];
                        row.Cells["KieuDang"].Value    = dr["KieuDang"];
                        row.Cells["GiaNhap"].Value     = dr["GiaNhap"];
                        row.Cells["GiaXuat"].Value     = dr["GiaXuat"];
                        row.Cells["TinhTrang"].Value   = dr["TinhTrang"];
                        row.Cells["NgayNhap"].Value    = dr["NgayNhap"];
                        row.Cells["SoLuongNhap"].Value = dr["SoLuongNhap"];
                        row.Cells["DaBan"].Value       = dr["DaBan"];
                        row.Cells["ChatLieu"].Value    = dr["ChatLieu"];
                        row.Cells["HangSanXuat"].Value = dr["HangSanXuat"];
                        row.Cells["AnhSanPham"].Value  = dr["AnhSanPham"];
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Loi tai du lieu: " + ex.Message, "Loi",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ─── Form Load ────────────────────────────────────────────────────
        private void FrmMain_Load(object sender, EventArgs e)
        {
            PhanQuyen();
        }

        // ─── Phân quyền: ẩn hẳn nút/menu với Nhân viên ───────────────────
        private void PhanQuyen()
        {
            bool laMQL = (frmDangNhap.ChucVu == "Quản lý");

            // Ẩn/hiện menu quản lý nhân viên, nhà cung cấp
            nhânViênToolStripMenuItem.Visible       = laMQL;
            traCứuNhânViênToolStripMenuItem.Visible = laMQL;
            nhàCungCấpToolStripMenuItem.Visible     = laMQL;
            nhàCungCấpToolStripMenuItem1.Visible    = laMQL;

            // Nhập hàng + tra cứu HDN: chỉ Quản lý mới được dùng
            nhậpHàngToolStripMenuItem.Visible    = laMQL;
            traCuuHDNToolStripMenuItem.Visible   = laMQL;

            // Ẩn hẳn nút CRUD sản phẩm với Nhân viên
            btnThem.Visible = laMQL;
            btnSua.Visible  = laMQL;
            btnXoa.Visible  = laMQL;

            // Hiển thị thông tin quyền hạn
            lblQuyenHan.Text      = laMQL
                ? "Quản lý — toàn quyền"
                : "Nhân viên — chỉ xem & bán hàng";
            lblQuyenHan.ForeColor = laMQL ? Color.DarkGreen : Color.DarkOrange;
        }

        // ─── Thêm sản phẩm ────────────────────────────────────────────────
        private void btnThem_Click(object sender, EventArgs e)
        {
            if (frmDangNhap.ChucVu != "Quản lý") return;
            try
            {
                var sql = @"INSERT INTO dbo.SanPham
                            (MaSanPham,TenSanPham,KieuDang,GiaNhap,GiaXuat,
                             TinhTrang,NgayNhap,SoLuongNhap,DaBan,ChatLieu,HangSanXuat,AnhSanPham)
                            VALUES
                            (@MaSanPham,@TenSanPham,@KieuDang,@GiaNhap,@GiaXuat,
                             @TinhTrang,@NgayNhap,@SoLuongNhap,@DaBan,@ChatLieu,@HangSanXuat,@AnhSanPham)";

                using (var conn = DBConnect.Connect())
                using (var cmd  = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("MaSanPham",   txtMa.Text);
                    cmd.Parameters.AddWithValue("TenSanPham",  txtTen.Text);
                    cmd.Parameters.AddWithValue("KieuDang",    txtKieu.Text);
                    cmd.Parameters.AddWithValue("GiaNhap",     txtGiaNhap.Text);
                    cmd.Parameters.AddWithValue("GiaXuat",     txtGiaXuat.Text);
                    cmd.Parameters.AddWithValue("TinhTrang",   cbTinhTrang.Text);
                    cmd.Parameters.AddWithValue("NgayNhap",    dateNgayNhap.Value);
                    cmd.Parameters.AddWithValue("SoLuongNhap", txtSoLuong.Text);
                    cmd.Parameters.AddWithValue("DaBan",       txtDaBan.Text);
                    cmd.Parameters.AddWithValue("ChatLieu",    txtChatLieu.Text);
                    cmd.Parameters.AddWithValue("HangSanXuat", txtHang.Text);
                    cmd.Parameters.AddWithValue("AnhSanPham",  txtAnhSanPham.Text);

                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        MessageBox.Show("Them du lieu thanh cong!", "Thong bao",
                                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadDsHangHoa();
                        btnNhapLai_Click(null, null);
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex.HResult == -2146232060)
                    MessageBox.Show("Ma san pham da ton tai!", "Loi",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                    MessageBox.Show(ex.Message, "Loi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ─── Sửa sản phẩm ────────────────────────────────────────────────
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (frmDangNhap.ChucVu != "Quản lý") return;
            if (dgv.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui long chon san pham can sua!", "Thong bao",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                var sql = @"UPDATE SanPham SET
                                TenSanPham=@TenSanPham, KieuDang=@KieuDang,
                                GiaNhap=@GiaNhap, GiaXuat=@GiaXuat, TinhTrang=@TinhTrang,
                                NgayNhap=@NgayNhap, SoLuongNhap=@SoLuongNhap, DaBan=@DaBan,
                                ChatLieu=@ChatLieu, HangSanXuat=@HangSanXuat, AnhSanPham=@AnhSanPham
                            WHERE MaSanPham = @MaSanPham";

                using (var conn = DBConnect.Connect())
                using (var cmd  = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("MaSanPham",   dgv.SelectedRows[0].Cells["MaSanPham"].Value);
                    cmd.Parameters.AddWithValue("TenSanPham",  txtTen.Text);
                    cmd.Parameters.AddWithValue("KieuDang",    txtKieu.Text);
                    cmd.Parameters.AddWithValue("GiaNhap",     txtGiaNhap.Text);
                    cmd.Parameters.AddWithValue("GiaXuat",     txtGiaXuat.Text);
                    cmd.Parameters.AddWithValue("TinhTrang",   cbTinhTrang.Text);
                    cmd.Parameters.AddWithValue("NgayNhap",    dateNgayNhap.Value);
                    cmd.Parameters.AddWithValue("SoLuongNhap", txtSoLuong.Text);
                    cmd.Parameters.AddWithValue("DaBan",       txtDaBan.Text);
                    cmd.Parameters.AddWithValue("ChatLieu",    txtChatLieu.Text);
                    cmd.Parameters.AddWithValue("HangSanXuat", txtHang.Text);
                    cmd.Parameters.AddWithValue("AnhSanPham",  txtAnhSanPham.Text);

                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        MessageBox.Show("Cap nhat thanh cong!", "Thong bao",
                                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadDsHangHoa();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Loi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ─── Xóa sản phẩm ────────────────────────────────────────────────
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (frmDangNhap.ChucVu != "Quản lý") return;
            if (dgv.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui long chon san pham can xoa!", "Thong bao",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (MessageBox.Show("Ban co that su muon xoa san pham nay?", "Canh bao",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                var maSP = dgv.SelectedRows[0].Cells["MaSanPham"].Value.ToString();
                using (var conn = DBConnect.Connect())
                {
                    using (var cmd = new SqlCommand(
                        "SELECT COUNT(*) FROM HoaDonBan_ChiTiet WHERE MaSP=@ma", conn))
                    {
                        cmd.Parameters.AddWithValue("@ma", maSP);
                        if ((int)cmd.ExecuteScalar() > 0)
                        {
                            MessageBox.Show(
                                "San pham da co trong hoa don, khong the xoa!\n" +
                                "Hay chuyen trang thai sang 'Het hang'.",
                                "Thong bao", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                    using (var cmd = new SqlCommand(
                        "DELETE SanPham WHERE MaSanPham=@ma", conn))
                    {
                        cmd.Parameters.AddWithValue("@ma", maSP);
                        if (cmd.ExecuteNonQuery() > 0)
                        {
                            MessageBox.Show("Xoa thanh cong!", "Thong bao",
                                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadDsHangHoa();
                            btnNhapLai_Click(null, null);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Loi: " + ex.Message, "Loi",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ─── Nhập lại ─────────────────────────────────────────────────────
        private void btnNhapLai_Click(object sender, EventArgs e)
        {
            txtMa.ReadOnly = false;
            txtMa.Clear(); txtTen.Clear(); txtKieu.Clear();
            txtGiaNhap.Clear(); txtGiaXuat.Clear();
            txtSoLuong.Clear(); txtDaBan.Clear();
            txtChatLieu.Clear(); txtHang.Clear(); txtAnhSanPham.Clear();
            cbTinhTrang.SelectedIndex = -1;
            dateNgayNhap.ResetText();
            pic.Image = Resources.noimage;
            txtMa.Focus();
        }

        // ─── Click dòng DGV ───────────────────────────────────────────────
        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || dgv.SelectedRows.Count == 0) return;
            var row = dgv.SelectedRows[0];
            txtMa.Text         = row.Cells["MaSanPham"].Value?.ToString();
            txtMa.ReadOnly     = true;
            txtTen.Text        = row.Cells["TenSanPham"].Value?.ToString();
            txtKieu.Text       = row.Cells["KieuDang"].Value?.ToString();
            txtGiaNhap.Text    = row.Cells["GiaNhap"].Value?.ToString();
            txtGiaXuat.Text    = row.Cells["GiaXuat"].Value?.ToString();
            txtSoLuong.Text    = row.Cells["SoLuongNhap"].Value?.ToString();
            txtDaBan.Text      = row.Cells["DaBan"].Value?.ToString();
            txtChatLieu.Text   = row.Cells["ChatLieu"].Value?.ToString();
            txtHang.Text       = row.Cells["HangSanXuat"].Value?.ToString();
            txtAnhSanPham.Text = row.Cells["AnhSanPham"].Value?.ToString();

            foreach (var item in cbTinhTrang.Items)
                if (item.ToString() == row.Cells["TinhTrang"].Value?.ToString())
                    cbTinhTrang.SelectedItem = item;

            try
            {
                dateNgayNhap.Value = DateTime.ParseExact(
                    row.Cells["NgayNhap"].Value?.ToString(),
                    "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            catch { }

            try
            {
                string tenAnh = row.Cells["AnhSanPham"].Value?.ToString();

                if (string.IsNullOrWhiteSpace(tenAnh))
                {
                    pic.Image = Resources.noimage;
                    return;
                }

                string duongDan = Path.Combine(
                    Application.StartupPath,
                    @"C:\Users\Lenovo\Downloads\File Đồ Án\QLCuaHangThoiTranglql\QLBH\HinhAnh",
                    Path.GetFileName(tenAnh));

                duongDan = Path.GetFullPath(duongDan);

                if (File.Exists(duongDan))
                {
                    pic.Image = Image.FromFile(duongDan);
                }
                else
                {
                    pic.Image = Resources.noimage;
                    MessageBox.Show("Không tìm thấy ảnh:\n" + duongDan);
                }
            }
            catch
            {
                pic.Image = Resources.noimage;
            }
        }

        // ─── STT ──────────────────────────────────────────────────────────
        private void dgv_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            dgv.Rows[e.RowIndex].Cells["STT"].Value = e.RowIndex + 1;
        }

        // ─── Chọn ảnh ─────────────────────────────────────────────────────
        private void btnChon_Click(object sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog
            {
                Title  = "Chon anh san pham",
                Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp|All Files|*.*"
            })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    txtAnhSanPham.Text = ofd.FileName;
                    pic.Image = Image.FromFile(ofd.FileName);
                }
            }
        }

        // ─── Menu điều hướng ──────────────────────────────────────────────
        private void hóaĐơnToolStripMenuItem_Click(object sender, EventArgs e)
        { new frmhoadon().Show(); this.Hide(); }

        private void kháchHàngToolStripMenuItem_Click(object sender, EventArgs e)
        { new frmKhachHang().Show(); this.Hide(); }

        private void nhânViênToolStripMenuItem_Click(object sender, EventArgs e)
        { new frmNhanVien().Show(); this.Hide(); }

        private void nhàCungCấpToolStripMenuItem_Click(object sender, EventArgs e)
        { new frmNhaCC().Show(); this.Hide(); }

        private void nhàCungCấpToolStripMenuItem1_Click(object sender, EventArgs e)
        { new frmTraCuuNCC().Show(); this.Hide(); }

        private void traCứuNhânViênToolStripMenuItem_Click(object sender, EventArgs e)
        { new frmTraCuuNV().Show(); this.Hide(); }

        private void kháchHàngToolStripMenuItem2_Click(object sender, EventArgs e)
        { new frmTraCuuKH().Show(); this.Hide(); }

        private void traCứuSảnPhẩmToolStripMenuItem_Click(object sender, EventArgs e)
        { new frmTraCuuHH().Show(); this.Hide(); }

        private void reportToolStripMenuItem_Click(object sender, EventArgs e)
        { new FrmBaoBieu().Show(); this.Hide(); }

        // ── Nhập hàng (chỉ Quản lý) ──────────────────────────────────────
        private void nhậpHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (frmDangNhap.ChucVu != "Quản lý")
            {
                MessageBox.Show("Bạn không có quyền truy cập chức năng này!",
                    "Từ chối", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            new frmNhapHang().Show();
        }

        // ── Tra cứu hóa đơn bán (cả hai role) ────────────────────────────
        private void hóaĐơnToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            new frmTraCuuHoaDon().Show();
        }

        // ── Tra cứu hóa đơn nhập (chỉ Quản lý) 
        private void traCuuHDNToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (frmDangNhap.ChucVu != "Quản lý")
            {
                MessageBox.Show("Bạn không có quyền truy cập chức năng này!",
                    "Từ chối", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            new frmTraCuuHoaDon().Show();
        }

        private void dangXuatToolStripMenuItem_Click(object sender, EventArgs e)
        { new frmDangNhap().Show(); this.Hide(); }

        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        { new frmDangNhap().Show(); this.Hide(); }

        private void thoatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Ban co that su muon thoat?", "Canh bao",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                Application.Exit();
        }

        // ─── KeyPress helpers ─────────────────────────────────────────────
        private void NhapSo(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
                e.Handled = true;
            if (e.KeyChar == '.' && ((TextBox)sender).Text.Contains("."))
                e.Handled = true;
        }

        private void txtMa_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(((TextBox)sender).Text))
            {
                MessageBox.Show("Ma khong duoc de trong!", "Canh bao",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.Cancel = true;
            }
        }

        private void nhậpHàngToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            { new frmNhapHang().Show(); this.Hide(); }
        }

        private void hóaĐơnToolStripMenuItem1_Click_1(object sender, EventArgs e)
        {
            frmTraCuuHoaDon frmTraCuuHoaDon = new frmTraCuuHoaDon();
            frmTraCuuHoaDon.Show();
            this.Hide();
        }

        private void thốngKêToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmthongke frmthongke = new frmthongke();
            frmthongke.Show();
            this.Hide();
        }
    }
}
