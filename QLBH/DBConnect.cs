using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QLBH
{
    public static class DBConnect
    {
        private static string StrConn =
            @"Server=LAPTOP-URJUI0ET;Database=QLBH;Integrated Security=True";

        public static SqlConnection Connect()
        {
            try
            {
                SqlConnection conn = new SqlConnection(StrConn);

                if (conn.State == System.Data.ConnectionState.Closed)
                    conn.Open();

                return conn;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,
                    "Lỗi kết nối cơ sở dữ liệu",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return null;
            }
        }
    }
}