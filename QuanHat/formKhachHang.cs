using QuanHat;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace QuanHat
{
    public partial class FormKhachHang : Form
    {
        private DbHelper db = new DbHelper();

        public FormKhachHang()
        {
            InitializeComponent();
            LoadCustomers();
            LoadGioiTinh();
        }
        private void LoadGioiTinh()
        {
            cboGioiTinh.Items.Add("Nam");
            cboGioiTinh.Items.Add("Nữ");
            cboGioiTinh.Items.Add("Khác");
            cboGioiTinh.SelectedIndex = 0; 
        }

        private void LoadCustomers()
        {
            string query = "SELECT * FROM KhachHang";
            dgvKhachHang.DataSource = db.ExecuteQuery(query);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtHoTen.Text) || string.IsNullOrWhiteSpace(txtSoDienThoai.Text) || cboGioiTinh.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string query = "INSERT INTO KhachHang (HoTen, SoDienThoai, GioiTinh) VALUES (@HoTen, @SoDienThoai, @GioiTinh)";
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@HoTen", txtHoTen.Text },
                { "@SoDienThoai", txtSoDienThoai.Text },
                { "@GioiTinh", cboGioiTinh.SelectedItem.ToString() }
            };

            if (db.ExecuteNonQuery(query, parameters) > 0)
            {
                MessageBox.Show("Thêm khách hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadCustomers();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvKhachHang.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn khách hàng để sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string maKhachHang = dgvKhachHang.SelectedRows[0].Cells["MaKhachHang"].Value.ToString();

            string query = "UPDATE KhachHang SET HoTen = @HoTen, SoDienThoai = @SoDienThoai, GioiTinh = @GioiTinh WHERE MaKhachHang = @MaKhachHang";
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@MaKhachHang", maKhachHang },
                { "@HoTen", txtHoTen.Text },
                { "@SoDienThoai", txtSoDienThoai.Text },
                { "@GioiTinh", cboGioiTinh.SelectedItem.ToString() }
            };

            if (db.ExecuteNonQuery(query, parameters) > 0)
            {
                MessageBox.Show("Cập nhật khách hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadCustomers();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvKhachHang.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn khách hàng để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string maKhachHang = dgvKhachHang.SelectedRows[0].Cells["MaKhachHang"].Value.ToString();

            string query = "DELETE FROM KhachHang WHERE MaKhachHang = @MaKhachHang";
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@MaKhachHang", maKhachHang }
            };

            if (db.ExecuteNonQuery(query, parameters) > 0)
            {
                MessageBox.Show("Xóa khách hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadCustomers();
            }
        }

        private void txtHoTen_Enter(object sender, EventArgs e)
        {
            if (txtHoTen.Text == "Nhập tên khách hàng...")
            {
                txtHoTen.Text = "";
                txtHoTen.ForeColor = Color.Black;
            }
        }

        private void txtHoTen_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtHoTen.Text))
            {
                txtHoTen.Text = "Nhập tên khách hàng...";
                txtHoTen.ForeColor = Color.Gray;
            }
        }

        private void txtSoDienThoai_Enter(object sender, EventArgs e)
        {
            if (txtSoDienThoai.Text == "Nhập số điện thoại...")
            {
                txtSoDienThoai.Text = "";
                txtSoDienThoai.ForeColor = Color.Black;
            }
        }

        private void txtSoDienThoai_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSoDienThoai.Text))
            {
                txtSoDienThoai.Text = "Nhập số điện thoại...";
                txtSoDienThoai.ForeColor = Color.Gray;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cboGioiTinh_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
