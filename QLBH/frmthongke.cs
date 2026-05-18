using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QLBH
{
    public partial class frmthongke : Form
    {
        public frmthongke()
        {
            InitializeComponent();
            LoadThongKe();
        }

        private void LoadThongKe()
        {
            try
            {
                using (var conn = DBConnect.Connect())
                {
                    // 1. Tổng số sản phẩm trong kho
                    using (var cmd = new SqlCommand(
                        "SELECT COUNT(*) FROM SanPham", conn))
                        lblTongSP.Text = ((int)cmd.ExecuteScalar()).ToString("N0");

                    // 2. Tổng số lượng đã nhập (từ hóa đơn nhập)
                    using (var cmd = new SqlCommand(
                        "SELECT ISNULL(SUM(SoLuong), 0) FROM HoaDonNhap_ChiTiet", conn))
                        lblTongNhap.Text = Convert.ToInt32(cmd.ExecuteScalar()).ToString("N0");

                    // 3. Tổng số lượng đã bán (từ hóa đơn bán)
                    using (var cmd = new SqlCommand(
                        "SELECT ISNULL(SUM(SoLuong), 0) FROM HoaDonBan_ChiTiet", conn))
                        lblDaBan.Text = Convert.ToInt32(cmd.ExecuteScalar()).ToString("N0");

                    // 4. Doanh thu thực tế (từ hóa đơn bán)
                    using (var cmd = new SqlCommand(
                        "SELECT ISNULL(SUM(TongTien), 0) FROM HoaDonBan", conn))
                        lblDoanhThu.Text = Convert.ToDecimal(cmd.ExecuteScalar())
                                                  .ToString("N0") + " VNĐ";

                    // 5. Tổng số hóa đơn bán
                    using (var cmd = new SqlCommand(
                        "SELECT COUNT(*) FROM HoaDonBan", conn))
                        lblTongHDB.Text = ((int)cmd.ExecuteScalar()).ToString("N0");

                    // 6. Tổng số hóa đơn nhập
                    using (var cmd = new SqlCommand(
                        "SELECT COUNT(*) FROM HoaDonNhap", conn))
                        lblTongHDN.Text = ((int)cmd.ExecuteScalar()).ToString("N0");

                    // 7. Tổng tiền nhập hàng (chi phí)
                    using (var cmd = new SqlCommand(
                        "SELECT ISNULL(SUM(TongTien), 0) FROM HoaDonNhap", conn))
                        lblTongNhapTien.Text = Convert.ToDecimal(cmd.ExecuteScalar())
                                                      .ToString("N0") + " VNĐ";

                    // 8. Lợi nhuận = Doanh thu - Chi phí nhập
                    using (var cmd1 = new SqlCommand(
                        "SELECT ISNULL(SUM(TongTien),0) FROM HoaDonBan", conn))
                    using (var cmd2 = new SqlCommand(
                        "SELECT ISNULL(SUM(TongTien),0) FROM HoaDonNhap", conn))
                    {
                        decimal doanhThu = Convert.ToDecimal(cmd1.ExecuteScalar());
                        decimal chiPhi = Convert.ToDecimal(cmd2.ExecuteScalar());
                        decimal loiNhuan = doanhThu - chiPhi;

                        lblLoiNhuan.Text = loiNhuan.ToString("N0") + " VNĐ";
                        lblLoiNhuan.ForeColor = loiNhuan >= 0
                            ? System.Drawing.Color.DarkGreen
                            : System.Drawing.Color.Red;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thống kê: " + ex.Message, "Lỗi",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadThongKe();
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            FrmMain frmMain = new FrmMain();
            frmMain.Show();
            Hide();

        }

        private void frmthongke_Load(object sender, EventArgs e)
        {

        }
    }
}
