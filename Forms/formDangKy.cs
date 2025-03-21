using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace Karaokelamlai.Forms
{
    public partial class formDangKy : Form
    {
        DbHelper db = new DbHelper();

        public formDangKy()
        {
            InitializeComponent();
        }

        private void btnDangKy_Click(object sender, EventArgs e)
        {
            string tenDangNhap = txtTenDangNhap.Text.Trim();
            string matKhau = txtMatKhau.Text.Trim();
            string xacNhanMatKhau = txtNhapLaiMatKhau.Text.Trim();
            string maNhanVien = txtId.Text.Trim();

            if (string.IsNullOrWhiteSpace(tenDangNhap) || string.IsNullOrWhiteSpace(matKhau) ||
                string.IsNullOrWhiteSpace(xacNhanMatKhau) || string.IsNullOrWhiteSpace(maNhanVien))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (matKhau != xacNhanMatKhau)
            {
                MessageBox.Show("Mật khẩu xác nhận không khớp!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string checkIdQuery = "SELECT COUNT(*) FROM TaiKhoan WHERE MaNhanVien = @MaNhanVien";
            Dictionary<string, object> checkIdParams = new Dictionary<string, object>
            {
                { "@MaNhanVien", maNhanVien }
            };

            object idResult = db.ExecuteScalar(checkIdQuery, checkIdParams);
            int idCount = Convert.ToInt32(idResult);

            if (idCount > 0)
            {
                MessageBox.Show("Nhân viên này đã có tài khoản. Vui lòng liên hệ quản lý!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string roleQuery = "SELECT ChucVu FROM NhanVien WHERE MaNhanVien = @MaNhanVien";
            object roleResult = db.ExecuteScalar(roleQuery, checkIdParams);

            if (roleResult == null)
            {
                MessageBox.Show("Mã nhân viên không tồn tại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string vaiTro = roleResult.ToString();

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


            string insertQuery = "INSERT INTO TaiKhoan (TenDangNhap, MatKhau, MaNhanVien, VaiTro) " +
                                 "VALUES (@TenDangNhap, @MatKhau, @MaNhanVien, @VaiTro)";
            Dictionary<string, object> insertParams = new Dictionary<string, object>
            {
                { "@TenDangNhap", tenDangNhap },
                { "@MatKhau", txtMatKhau.Text },
                { "@MaNhanVien", maNhanVien },
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
            txtMatKhau.UseSystemPasswordChar = !txtMatKhau.UseSystemPasswordChar;
            txtNhapLaiMatKhau.UseSystemPasswordChar = !txtNhapLaiMatKhau.UseSystemPasswordChar;

            btnHienMatKhau.Text = txtMatKhau.UseSystemPasswordChar ? "👁️" : "🙈";
        }
    }
}
