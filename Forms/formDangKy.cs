using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Karaokelamlai.Forms
{
    public partial class formDangKy : Form
    {
        DbHelper db = new DbHelper(); // Kết nối CSDL

        public formDangKy()
        {
            InitializeComponent();
            // Thêm các vai trò vào ComboBox
            cbChucVu.Items.AddRange(new string[] { "Quản lý", "Nhân viên" });
            cbChucVu.SelectedIndex = 0; // Mặc định chọn "User"
        }

        private void btnDangKy_Click(object sender, EventArgs e)
        {
            string tenDangNhap = txtTenDangNhap.Text.Trim();
            string matKhau = txtMatKhau.Text.Trim();
            string xacNhanMatKhau = txtNhapLaiMatKhau.Text.Trim();
            string vaiTro = cbChucVu.SelectedItem.ToString(); // Lấy vai trò đã chọn

            // Kiểm tra dữ liệu hợp lệ
            if (string.IsNullOrWhiteSpace(tenDangNhap) || string.IsNullOrWhiteSpace(matKhau) || string.IsNullOrWhiteSpace(xacNhanMatKhau))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (matKhau != xacNhanMatKhau)
            {
                MessageBox.Show("Mật khẩu xác nhận không khớp!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Kiểm tra tên đăng nhập đã tồn tại chưa
            string checkQuery = "SELECT COUNT(*) FROM TaiKhoan WHERE TenDangNhap = @TenDangNhap";
            Dictionary<string, object> checkParams = new Dictionary<string, object>
            {
                { "@TenDangNhap", tenDangNhap }
            };

            object result = db.ExecuteScalar(checkQuery, checkParams);
            int count = Convert.ToInt32(result);

            if (count > 0)
            {
                MessageBox.Show("Tên đăng nhập đã tồn tại, vui lòng chọn tên khác!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Thêm tài khoản vào CSDL
            string insertQuery = "INSERT INTO TaiKhoan (TenDangNhap, MatKhau, VaiTro) VALUES (@TenDangNhap, @MatKhau, @VaiTro)";
            Dictionary<string, object> insertParams = new Dictionary<string, object>
            {
                { "@TenDangNhap", tenDangNhap },
                { "@MatKhau", matKhau }, // Có thể mã hóa mật khẩu tại đây
                { "@VaiTro", vaiTro }
            };

            int rowsAffected = db.ExecuteNonQuery(insertQuery, insertParams);

            if (rowsAffected > 0)
            {
                MessageBox.Show("Đăng ký thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Đăng ký thất bại, vui lòng thử lại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHienMatKhau_Click(object sender, EventArgs e)
        {
            // Đảo trạng thái ẩn/hiện mật khẩu
            txtMatKhau.UseSystemPasswordChar = !txtMatKhau.UseSystemPasswordChar;
            txtNhapLaiMatKhau.UseSystemPasswordChar = !txtNhapLaiMatKhau.UseSystemPasswordChar;

            // Đổi icon của nút (tùy chọn)
            btnHienMatKhau.Text = txtMatKhau.UseSystemPasswordChar ? "👁️" : "🙈";
        }
    }
}
