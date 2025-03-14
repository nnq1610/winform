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
            CoutNhanVien();
        }
        private void CoutNhanVien()
        {
            string query = "SELECT COUNT(*) FROM NhanVien";
            object result = db.ExecuteScalar(query);
            if (result != null && int.TryParse(result.ToString(), out int count))
            {
                txtTongNhanVien.Text = count.ToString();
            }
            else
            {
                txtTongNhanVien.Text = "0";
            }
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
            string chucVu = cmbChucVu.SelectedItem.ToString(); 

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
                CoutNhanVien();
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

            string checkAccountQuery = "SELECT COUNT(*) FROM TaiKhoan WHERE MaNhanVien = @MaNhanVien";
            Dictionary<string, object> checkParams = new Dictionary<string, object> { { "@MaNhanVien", selectedNhanVienID } };
            int accountCount = Convert.ToInt32(db.ExecuteScalar(checkAccountQuery, checkParams));

            string checkInvoiceQuery = "SELECT COUNT(*) FROM HoaDon WHERE MaNhanVien = @MaNhanVien";
            int invoiceCount = Convert.ToInt32(db.ExecuteScalar(checkInvoiceQuery, checkParams));

            string message = "Bạn có chắc chắn muốn xoá nhân viên này?";
            if (accountCount > 0 && invoiceCount > 0)
            {
                message = "Nhân viên này có tài khoản và hóa đơn liên kết.\nNếu tiếp tục, cả tài khoản và hóa đơn của họ cũng sẽ bị xóa.\nBạn có chắc chắn muốn tiếp tục?";
            }
            else if (accountCount > 0)
            {
                message = "Nhân viên này có tài khoản liên kết. Nếu tiếp tục, tài khoản của họ cũng sẽ bị xóa.\nBạn có chắc chắn muốn tiếp tục?";
            }
            else if (invoiceCount > 0)
            {
                message = "Nhân viên này có hóa đơn liên kết. Nếu tiếp tục, tất cả hóa đơn của họ sẽ bị xóa.\nBạn có chắc chắn muốn tiếp tục?";
            }

            DialogResult result = MessageBox.Show(message, "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.No)
                return;

            if (accountCount > 0)
            {
                string deleteAccountQuery = "DELETE FROM TaiKhoan WHERE MaNhanVien = @MaNhanVien";
                db.ExecuteNonQuery(deleteAccountQuery, checkParams);
            }

            if (invoiceCount > 0)
            {
                string deleteInvoiceQuery = "DELETE FROM HoaDon WHERE MaNhanVien = @MaNhanVien";
                db.ExecuteNonQuery(deleteInvoiceQuery, checkParams);
            }

            string deleteNhanVienQuery = "DELETE FROM NhanVien WHERE MaNhanVien = @MaNhanVien";
            if (db.ExecuteNonQuery(deleteNhanVienQuery, checkParams) > 0)
            {
                MessageBox.Show("Xóa nhân viên, tài khoản và hóa đơn thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadNhanVien();
                CoutNhanVien();
                selectedNhanVienID = -1;
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

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string searchValue = txtTimKiem.Text.Trim();
            string query;
            Dictionary<string, object> parameters = new Dictionary<string, object>();

            if (int.TryParse(searchValue, out int maNhanVien))
            {
                query = "SELECT * FROM NhanVien WHERE MaNhanVien = @Search";
                parameters.Add("@Search", maNhanVien);
            }
            else
            {
                query = "SELECT * FROM NhanVien WHERE HoTen LIKE @Search";
                parameters.Add("@Search", "%" + searchValue + "%");
            }

            dgvNhanVien.DataSource = db.ExecuteQuery(query, parameters);
        }
        private void dgv_Cell_Clicks(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvNhanVien.Rows[e.RowIndex];

                selectedNhanVienID = Convert.ToInt32(row.Cells["MaNhanVien"].Value);
                txtHoTen.Text = row.Cells["HoTen"].Value.ToString();
                txtSoDienThoai.Text = row.Cells["SoDienThoai"].Value.ToString();
                txtLuongCoBan.Text = row.Cells["LuongCoBan"].Value.ToString();

                string chucVu = row.Cells["ChucVu"].Value.ToString();
                if (cmbChucVu.Items.Contains(chucVu))
                {
                    cmbChucVu.SelectedItem = chucVu;
                }
            }
        }

    }
}
