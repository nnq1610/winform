using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace QuanHat
{
    public partial class formNhanVien : Form
    {
        DbHelper db = new DbHelper();
        private int selectedNhanVienID = -1;

        public formNhanVien()
        {
            InitializeComponent();
            LoadChucVuComboBox();  
            LoadNhanVien();
        }

        private void LoadNhanVien()
        {
            string query = "SELECT * FROM NhanVien";
            dgvNhanVien.DataSource = db.ExecuteQuery(query);
        }

        private void LoadChucVuComboBox()
        {
            List<string> chucVuList = new List<string> { "Quản lý", "Nhân viên", "Thu ngân" };

            cmbChucVu.DataSource = chucVuList;  
            cmbChucVu.SelectedIndex = 0;  
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtHoTen.Text) ||
                string.IsNullOrEmpty(txtLuongCoBan.Text) ||
                string.IsNullOrEmpty(txtSoDienThoai.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtLuongCoBan.Text, out int luongCoban) || luongCoban <= 0)
            {
                MessageBox.Show("Lương phải là số dương!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string hoTen = txtHoTen.Text.Trim();
            string soDienThoai = txtSoDienThoai.Text.Trim();
            string chucVu = cmbChucVu.SelectedItem.ToString();  // Lấy giá trị từ ComboBox

            string query = "INSERT INTO NhanVien (HoTen, SoDienThoai, ChucVu, LuongCoBan) VALUES (@HoTen, @SoDienThoai, @ChucVu, @LuongCoBan)";
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@HoTen", hoTen },
                { "@LuongCoBan", luongCoban },
                { "@SoDienThoai", soDienThoai },
                { "@ChucVu", chucVu }
            };

            if (db.ExecuteNonQuery(query, parameters) > 0)
            {
                MessageBox.Show("Thêm nhân viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadNhanVien();
            }
            else
            {
                MessageBox.Show("Thêm nhân viên thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            {
                if (selectedNhanVienID == -1)
                {
                    MessageBox.Show("Vui lòng chọn nhân viên cần sửa!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrEmpty(txtHoTen.Text) ||
                    string.IsNullOrEmpty(txtLuongCoBan.Text) ||
                    string.IsNullOrEmpty(txtSoDienThoai.Text))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!int.TryParse(txtLuongCoBan.Text, out int luongCoban) || luongCoban <= 0)
                {
                    MessageBox.Show("Lương phải là số dương!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string hoTen = txtHoTen.Text.Trim();
                string soDienThoai = txtSoDienThoai.Text.Trim();
                string chucVu = cmbChucVu.SelectedItem.ToString();

                string query = "UPDATE NhanVien SET HoTen = @HoTen, SoDienThoai = @SoDienThoai, ChucVu = @ChucVu, LuongCoBan = @LuongCoBan WHERE MaNhanVien = @MaNhanVien";
                Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@HoTen", hoTen },
                { "@LuongCoBan", luongCoban },
                { "@SoDienThoai", soDienThoai },
                { "@ChucVu", chucVu },
                { "@MaNhanVien", selectedNhanVienID }
            };

                if (db.ExecuteNonQuery(query, parameters) > 0)
                {
                    MessageBox.Show("Cập nhật nhân viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadNhanVien();
                }
                else
                {
                    MessageBox.Show("Cập nhật thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (selectedNhanVienID == -1)
            {
                MessageBox.Show("Vui lòng chọn nhân viên cần xoá!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xoá nhân viên này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.No)
                return;

            string query = "DELETE FROM NhanVien WHERE MaNhanVien = @MaNhanVien";
            Dictionary<string, object> parameters = new Dictionary<string, object> { { "@MaNhanVien", selectedNhanVienID } };

            if (db.ExecuteNonQuery(query, parameters) > 0)
            {
                MessageBox.Show("Xóa nhân viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadNhanVien();
            }
            else
            {
                MessageBox.Show("Xóa thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void dgvNhanVien_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
