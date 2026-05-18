using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLBH
{
    public partial class frmNhaCC : Form
    {
        public frmNhaCC()
        {
            InitializeComponent();
            LoadNhaCC();
        }
        private void LoadNhaCC()
        {
            var sql = "SELECT MaNCC, TenNCC, SDT, DiaChi, Email FROM NhaCungCap";

            var cmd = new SqlCommand(sql, DBConnect.Connect());
            var dr = cmd.ExecuteReader();

            // Xóa dữ liệu cũ trong datagridview
            dgvNhaCC.Rows.Clear();

            // Lặp qua từng dòng trong bảng NhaCungCap, thêm vào datagridview
            while (dr.Read())
            {
                var i = dgvNhaCC.Rows.Add();
                var row = dgvNhaCC.Rows[i];
                row.Cells["MaNCC"].Value = dr["MaNCC"];
                row.Cells["TenNhaCC"].Value = dr["TenNCC"];
                row.Cells["SDT"].Value = dr["SDT"];
                row.Cells["DiaChi"].Value = dr["DiaChi"];
                row.Cells["Email"].Value = dr["Email"];
            }

            dr.Close();
        }
        private void XoaTrong()
        {
            txtMaNCC.ReadOnly = false;
            txtMaNCC.Clear();
            txtTenNCC.Clear();
            txtSDT.Clear();
            txtDiaChi.Clear();
            txtEmail.Clear();
            txtMaNCC.Focus();
        }

        private void btnThemNCC_Click(object sender, EventArgs e)
        {
            try
            {
                var sql =
                    "INSERT INTO NhaCungCap (MaNCC, TenNCC, SDT, DiaChi, Email) " +
                    "VALUES (@MaNCC, @TenNCC, @SDT, @DiaChi, @Email)";

                var cmd = new SqlCommand(sql, DBConnect.Connect());
                cmd.Parameters.AddWithValue("MaNCC", txtMaNCC.Text.Trim());
                cmd.Parameters.AddWithValue("TenNCC", txtTenNCC.Text.Trim());
                cmd.Parameters.AddWithValue("SDT", txtSDT.Text.Trim());
                cmd.Parameters.AddWithValue("DiaChi", txtDiaChi.Text.Trim());
                cmd.Parameters.AddWithValue("Email", txtEmail.Text.Trim());

                var kq = cmd.ExecuteNonQuery();
                if (kq > 0)
                {
                    MessageBox.Show("Thêm dữ liệu thành công!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadNhaCC();
                    XoaTrong();
                }
                else
                {
                    MessageBox.Show("Thêm dữ liệu thất bại!", "Thông báo lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception exception)
            {
                if (exception.HResult == -2146232060)
                {
                    MessageBox.Show("Mã nhà cung cấp đã tồn tại!", "Thông báo lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show(exception.Message, "Thông báo lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnSuaNCC_Click(object sender, EventArgs e)
        {
            try
            {
                var sql =
                    "UPDATE NhaCungCap " +
                    "SET TenNCC = @TenNCC, SDT = @SDT, DiaChi = @DiaChi, Email = @Email " +
                    "WHERE MaNCC = @MaNCC";

                var cmd = new SqlCommand(sql, DBConnect.Connect());
                cmd.Parameters.AddWithValue("MaNCC", txtMaNCC.Text.Trim());
                cmd.Parameters.AddWithValue("TenNCC", txtTenNCC.Text.Trim());
                cmd.Parameters.AddWithValue("SDT", txtSDT.Text.Trim());
                cmd.Parameters.AddWithValue("DiaChi", txtDiaChi.Text.Trim());
                cmd.Parameters.AddWithValue("Email", txtEmail.Text.Trim());

                var kq = cmd.ExecuteNonQuery();
                if (kq > 0)
                {
                    MessageBox.Show("Cập nhật dữ liệu thành công!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadNhaCC();
                }
                else
                {
                    MessageBox.Show("Cập nhật dữ liệu thất bại!", "Thông báo lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Thông báo lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoaNCC_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvNhaCC.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Vui lòng chọn nhà cung cấp cần xóa!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (MessageBox.Show("Bạn có thật sự muốn xóa nhà cung cấp này?", "Cảnh báo",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    var maNCC = dgvNhaCC.SelectedRows[0].Cells["MaNCC"].Value.ToString();
                    var sql = "DELETE NhaCungCap WHERE MaNCC = @MaNCC";

                    var cmd = new SqlCommand(sql, DBConnect.Connect());
                    cmd.Parameters.AddWithValue("MaNCC", maNCC);

                    var kq = cmd.ExecuteNonQuery();
                    if (kq > 0)
                    {
                        MessageBox.Show("Xóa dữ liệu thành công!", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadNhaCC();
                        XoaTrong();
                    }
                    else
                    {
                        MessageBox.Show("Xóa dữ liệu thất bại!", "Thông báo lỗi",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception exception)
            {
                // Lỗi khóa ngoại: NCC đang có lô hàng / hóa đơn nhập liên quan
                if (exception.Message.Contains("REFERENCE") || exception.Message.Contains("FOREIGN KEY"))
                {
                    MessageBox.Show(
                        "Không thể xóa! Nhà cung cấp này đang có dữ liệu liên quan (lô hàng / hóa đơn nhập).",
                        "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show(exception.Message, "Thông báo lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnNhapLaiNCC_Click(object sender, EventArgs e)
        {
            XoaTrong();
        }

        private void btnDongNCC_Click(object sender, EventArgs e)
        {
            FrmMain frmMain = new FrmMain();
            frmMain.Show();
            this.Hide();
        }


        private void frmNhaCC_Load(object sender, EventArgs e)
        {

        }

        private void dgvNhaCC_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                txtMaNCC.Text = dgvNhaCC.SelectedRows[0].Cells["MaNCC"].Value.ToString();
                txtMaNCC.ReadOnly = true; // Không cho sửa mã
                txtTenNCC.Text = dgvNhaCC.SelectedRows[0].Cells["TenNhaCC"].Value.ToString();
                txtSDT.Text = dgvNhaCC.SelectedRows[0].Cells["SDT"].Value.ToString();
                txtDiaChi.Text = dgvNhaCC.SelectedRows[0].Cells["DiaChi"].Value.ToString();
                txtEmail.Text = dgvNhaCC.SelectedRows[0].Cells["Email"].Value.ToString();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Thông báo lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvNhaCC_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtSDT_TextChanged(object sender, EventArgs e)
        {

        }
        private void NhapSo(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;
        }
        private void NhapSoNguyen(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;
        }

        private void NhapSoThapPhan(object sender, KeyPressEventArgs e)
        {
            TextBox txt = sender as TextBox;
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
                e.Handled = true;
            if (e.KeyChar == '.' && txt.Text.Contains(".")) e.Handled = true;
        }

        private void txtMa_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(((TextBox)sender).Text))
            {
                MessageBox.Show("Mã không được để trống!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.Cancel = true;
            }
        }
    }
}
