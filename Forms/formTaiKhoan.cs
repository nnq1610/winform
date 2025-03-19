using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Karaokelamlai.Forms
{
    public partial class formTaiKhoan : Form
    {
        DbHelper db = new DbHelper();

        public formTaiKhoan()
        {
            InitializeComponent();
            LoadTaiKhoan();
            LoadNhanVien();
            cboVaiTro.Items.AddRange(new string[] { "Admin", "Nhân viên" });
            cboVaiTro.SelectedIndex = 0;
        }
        private void formTaiKhoan_Load(object sender, EventArgs e)
        {
            LoadTheme();
        }
        private void ApplyTheme(Control parent)
        {
            foreach (Control ctrl in parent.Controls)
            {
                if (ctrl is Button btn)
                {
                    btn.BackColor = Themecolor.ChangeColorBrightness(Themecolor.PrimaryColor, 0.2);
                    btn.ForeColor = Color.White;
                    btn.FlatAppearance.BorderColor = Themecolor.SecondaryColor;
                }
                else
                {
                    ApplyTheme(ctrl); // Đệ quy nếu control có chứa control con
                }
            }
        }
        private void LoadTheme()
        {
            ApplyTheme(this);
            label1.ForeColor = Themecolor.SecondaryColor;
            label2.ForeColor = Themecolor.SecondaryColor;
            label3.ForeColor = Themecolor.SecondaryColor;
            label4.ForeColor = Themecolor.SecondaryColor;
        }
        private void LoadTaiKhoan()
        {
            string query = "SELECT * FROM TaiKhoan";
            DataTable dt = db.ExecuteQuery(query);
            dgvTaiKhoan.ForeColor = System.Drawing.Color.Black;
            dgvTaiKhoan.DataSource = dt;
            dgvTaiKhoan.AutoGenerateColumns = true;
            dgvTaiKhoan.ClearSelection();
            dgvTaiKhoan.Refresh();
        }

        private void LoadNhanVien()
        {
            string query = "SELECT MaNhanVien, HoTen FROM NhanVien";
            DataTable dt = db.ExecuteQuery(query);
            cboMaNhanVien.DataSource = dt;
            cboMaNhanVien.DisplayMember = "HoTen";
            cboMaNhanVien.ValueMember = "MaNhanVien";
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTenDangNhap.Text) ||
                string.IsNullOrWhiteSpace(txtMatKhau.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string tenDangNhap = txtTenDangNhap.Text.Trim();
            string matKhau = txtMatKhau.Text.Trim();
            string vaiTro = cboVaiTro.SelectedItem.ToString();
            int? maNhanVien = cboMaNhanVien.SelectedValue != null ? (int?)cboMaNhanVien.SelectedValue : null;

            string query = "INSERT INTO TaiKhoan (TenDangNhap, MatKhau, VaiTro, MaNhanVien) " +
                           "VALUES (@TenDangNhap, @MatKhau, @VaiTro, @MaNhanVien)";
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@TenDangNhap", tenDangNhap },
                { "@MatKhau", matKhau },
                { "@VaiTro", vaiTro },
                { "@MaNhanVien", maNhanVien ?? (object)DBNull.Value }
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

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dgvTaiKhoan.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn tài khoản để sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int idTaiKhoan = Convert.ToInt32(dgvTaiKhoan.SelectedRows[0].Cells["IDTaiKhoan"].Value);
            string tenDangNhap = txtTenDangNhap.Text.Trim();
            string matKhau = txtMatKhau.Text.Trim();
            string vaiTro = cboVaiTro.SelectedItem.ToString();
            int? maNhanVien = cboMaNhanVien.SelectedValue != null ? (int?)cboMaNhanVien.SelectedValue : null;

            string query = "UPDATE TaiKhoan SET TenDangNhap = @TenDangNhap, MatKhau = @MatKhau, VaiTro = @VaiTro, MaNhanVien = @MaNhanVien " +
                           "WHERE IDTaiKhoan = @IDTaiKhoan";

            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@TenDangNhap", tenDangNhap },
                { "@MatKhau", matKhau },
                { "@VaiTro", vaiTro },
                { "@MaNhanVien", maNhanVien ?? (object)DBNull.Value },
                { "@IDTaiKhoan", idTaiKhoan }
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

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvTaiKhoan.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn tài khoản để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int idTaiKhoan = Convert.ToInt32(dgvTaiKhoan.SelectedRows[0].Cells["IDTaiKhoan"].Value);

            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa tài khoản này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No) return;

            string query = "DELETE FROM TaiKhoan WHERE IDTaiKhoan = @IDTaiKhoan";
            Dictionary<string, object> parameters = new Dictionary<string, object> { { "@IDTaiKhoan", idTaiKhoan } };

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

        
    }
}
