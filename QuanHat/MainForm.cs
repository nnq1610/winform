using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace QuanHat
{
    public partial class MainForm : Form
    {
        private string userRole;
        DbHelper db = new DbHelper();
        private int selectedIDTK = -1;

        public MainForm(string userRole)
        {
            InitializeComponent();
            LoadChucVu();
            LoadNhanVien();
            LoadTaiKhoan();
            ConfigureAccess();
            this.userRole = userRole;
        }
        private void ConfigureAccess()
        {
            if (userRole == "Nhân viên")
            {
                tabNhanSu.Visible = false;
                tabTaiKhoan.Visible = false;
            }
        }
        private void LoadTaiKhoan()
        {
            string query = "SELECT * FROM TaiKhoan";
            DataTable dt = db.ExecuteQuery(query);
            dgvTaiKhoan.DataSource = dt;
        }

        private void LoadChucVu()
        {
            List<string> chucVu = new List<string> { "Quản lý", "Nhân viên" };
            cboChucVu.DataSource = chucVu;
            cboChucVu.SelectedIndex = 0;
        }

        private void LoadNhanVien()
        {
            string query = "SELECT MaNhanVien, HoTen FROM NhanVien";
            DataTable dt = db.ExecuteQuery(query);

            cboNhanVien.DataSource = dt;
            cboNhanVien.DisplayMember = "HoTen";
            cboNhanVien.ValueMember = "MaNhanVien";
            cboNhanVien.SelectedIndex = 0;
        }

        private bool IsTenDangNhapExists(string tenDangNhap)
        {
            string query = "SELECT COUNT(*) FROM TaiKhoan WHERE TenDangNhap = @TenDangNhap";
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@TenDangNhap", tenDangNhap }
            };
            int count = Convert.ToInt32(db.ExecuteScalar(query, parameters));
            return count > 0;
        }

        private void button11_Click(object sender, EventArgs e) 
        {
            if (string.IsNullOrEmpty(txtTenDangNhap.Text) || string.IsNullOrEmpty(txtMatKhau.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string tenDangNhap = txtTenDangNhap.Text.Trim();
            string matKhau = txtMatKhau.Text.Trim();
            string vaiTro = cboChucVu.SelectedItem.ToString();
            string maNhanVien = cboNhanVien.SelectedValue.ToString();

            if (IsTenDangNhapExists(tenDangNhap))
            {
                MessageBox.Show("Tên đăng nhập đã tồn tại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string query = "INSERT INTO TaiKhoan (TenDangNhap, MatKhau, VaiTro, MaNhanVien) VALUES (@TenDangNhap, @MatKhau, @VaiTro, @MaNhanVien)";
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@TenDangNhap", tenDangNhap },
                { "@MatKhau", matKhau },
                { "@VaiTro", vaiTro },
                { "@MaNhanVien", maNhanVien }
            };

            if (db.ExecuteNonQuery(query, parameters) > 0)
            {
                MessageBox.Show("Thêm tài khoản thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadTaiKhoan();
            }
            else
            {
                MessageBox.Show("Thêm tài khoản thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvTaiKhoan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvTaiKhoan.Rows[e.RowIndex];
                txtTenDangNhap.Text = row.Cells["TenDangNhap"].Value.ToString();
                txtMatKhau.Text = row.Cells["MatKhau"].Value.ToString();
                cboChucVu.SelectedItem = row.Cells["VaiTro"].Value.ToString();
                cboNhanVien.SelectedValue = row.Cells["MaNhanVien"].Value.ToString();
                selectedIDTK = Convert.ToInt32(row.Cells["IDTaiKhoan"].Value);

            }
        }

        private void button10_Click(object sender, EventArgs e) 
        {
            if (selectedIDTK == -1)
            {
                MessageBox.Show("Vui lòng chọn tài khoản cần sửa!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(txtTenDangNhap.Text) || string.IsNullOrEmpty(txtMatKhau.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string tenDangNhap = txtTenDangNhap.Text.Trim();
            string matKhau = txtMatKhau.Text.Trim();
            string vaiTro = cboChucVu.SelectedItem.ToString();
            string maNhanVien = cboNhanVien.SelectedValue.ToString();

            string query = "UPDATE TaiKhoan SET TenDangNhap = @TenDangNhap, MatKhau = @MatKhau, VaiTro = @VaiTro, MaNhanVien = @MaNhanVien WHERE IDTaiKhoan = @ID";
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@TenDangNhap", tenDangNhap },
                { "@MatKhau", matKhau },
                { "@VaiTro", vaiTro },
                { "@MaNhanVien", maNhanVien },
                { "@ID", selectedIDTK }
            };

            if (db.ExecuteNonQuery(query, parameters) > 0)
            {
                MessageBox.Show("Cập nhật tài khoản thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadTaiKhoan();
            }
            else
            {
                MessageBox.Show("Cập nhật thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button9_Click(object sender, EventArgs e) // Delete account
        {
            if (selectedIDTK == -1)
            {
                MessageBox.Show("Vui lòng chọn tài khoản cần xoá!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xoá tài khoản này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.No)
                return;

            string query = "DELETE FROM TaiKhoan WHERE IDTaiKhoan = @ID";
            Dictionary<string, object> parameters = new Dictionary<string, object> { { "@ID", selectedIDTK } };

            if (db.ExecuteNonQuery(query, parameters) > 0)
            {
                MessageBox.Show("Xóa tài khoản thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadTaiKhoan();
            }
            else
            {
                MessageBox.Show("Xóa thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            formDangNhap loginForm = new formDangNhap();
            loginForm.ShowDialog();
            this.Show();
        }

     

        private void btnPhongHat_Click(object sender, EventArgs e)
        {
            formPhongHat ph = new formPhongHat();
            ph.Show();
        }

        private void btnMatHang_Click(object sender, EventArgs e)
        {
            formMatHang mh = new formMatHang();
            mh.Show();
        }

        private void btnKhachHang_Click(object sender, EventArgs e)
        {
            FormKhachHang kh = new FormKhachHang();
            kh.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FormDatPhong dp = new FormDatPhong();
            dp.Show();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            formHoaDon hd = new formHoaDon();
            hd.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            formNhanVien nv = new formNhanVien();
            nv.Show();
        }
    }
}