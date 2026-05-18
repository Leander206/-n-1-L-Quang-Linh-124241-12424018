using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QLBH
{
    public partial class frmTraCuuNCC : Form
    {
        public frmTraCuuNCC()
        {
            InitializeComponent();
        }

        private void frmTraCuuNCC_Load(object sender, EventArgs e)
        {
            TimKiem(); // Load toàn bộ NCC khi mở form
        }

        // Thêm số thứ tự datagridview
        private void dgvKQ_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            dgvKQ.Rows[e.RowIndex].Cells["STT"].Value = e.RowIndex + 1;
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            FrmMain frmMain = new FrmMain();
            frmMain.Show();
            Hide();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            TimKiem();
        }

        private void TimKiem()
        {
            try
            {
                var sql = @"SELECT MaNCC, TenNCC, SDT, DiaChi, Email
                            FROM NhaCungCap
                            WHERE MaNCC  LIKE N'%' + @TuKhoa + '%'
                               OR TenNCC LIKE N'%' + @TuKhoa + '%'
                               OR SDT    LIKE N'%' + @TuKhoa + '%'
                               OR DiaChi LIKE N'%' + @TuKhoa + '%'
                               OR Email  LIKE N'%' + @TuKhoa + '%'";

                SqlCommand cmd = new SqlCommand(sql, DBConnect.Connect());
                cmd.Parameters.AddWithValue("TuKhoa", txtTuKhoa.Text);

                var dr = cmd.ExecuteReader();

                // Xóa dữ liệu cũ trong DataGridView
                dgvKQ.Rows.Clear();

                // Lặp qua từng dòng kết quả, thêm vào DataGridView
                while (dr.Read())
                {
                    var i = dgvKQ.Rows.Add();
                    var row = dgvKQ.Rows[i];

                    row.Cells["MaNCC"].Value = dr["MaNCC"];
                    row.Cells["TenNCC"].Value = dr["TenNCC"];
                    row.Cells["SDT"].Value = dr["SDT"];
                    row.Cells["DiaChi"].Value = dr["DiaChi"];
                    row.Cells["Email"].Value = dr["Email"];
                }

                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tra cứu: " + ex.Message, "Thông báo",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Nhấn Enter trong ô tìm kiếm → tìm luôn
        private void txtTuKhoa_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                TimKiem();
        }

        private void dgvKQ_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void label13_Click(object sender, EventArgs e)
        {
        }
    }
}