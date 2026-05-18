using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QLBH
{
    public partial class frmDangNhap : Form
    {
        public static string MaNhanVien    { get; set; }
        public static string HoTenNhanVien { get; set; }
        public static string ChucVu        { get; set; }

        public frmDangNhap()
        {
            InitializeComponent();
        }

        private void frmDangNhap_Load(object sender, EventArgs e)
        {
            txtMatKhau.PasswordChar = '*';
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTenDangNhap.Text) ||
                string.IsNullOrWhiteSpace(txtMatKhau.Text))
            {
                MessageBox.Show("Vui lòng nhập tên đăng nhập và mật khẩu!",
                                "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string sql = @"SELECT MaThanhVien, HoVaTen, ChucVu
                           FROM   NhanVien
                           WHERE  TenDangNhap = @TenDN
                             AND  MatKhau     = @MK";
            try
            {
                using (var conn = DBConnect.Connect())
                using (var cmd  = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@TenDN", txtTenDangNhap.Text.Trim());
                    cmd.Parameters.AddWithValue("@MK",    txtMatKhau.Text);

                    using (var dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            MaNhanVien    = dr["MaThanhVien"].ToString();
                            HoTenNhanVien = dr["HoVaTen"].ToString();
                            ChucVu        = dr["ChucVu"] == DBNull.Value
                                            ? "Nhân viên"
                                            : dr["ChucVu"].ToString();

                            MessageBox.Show(
                                $"Đăng nhập thành công!\nChào {HoTenNhanVien} ({ChucVu})",
                                "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            new FrmMain().Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Tài khoản hoặc mật khẩu không chính xác!",
                                            "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txtMatKhau.Clear();
                            txtMatKhau.Focus();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối CSDL:\n" + ex.Message,
                                "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e) => this.Close();

        private void btnXemMatKhau_Click(object sender, EventArgs e)
        {
            // Dùng (char)0 thay vi '\0' de tranh loi CS1011
            txtMatKhau.PasswordChar = (txtMatKhau.PasswordChar == '*') ? (char)0 : '*';
        }
    }
}
