using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Karaokelamlai.Forms
{
    public partial class formKhachHang: Form
    {
        private DbHelper db = new DbHelper();
        public formKhachHang()
        {
            InitializeComponent();
            LoadCustomers();
            LoadGioiTinh();
        }
        private void formKhachHang_Load(object sender, EventArgs e)
        {
            LoadTheme();    
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
            dgvKhachHang.ForeColor = System.Drawing.Color.Black;
            dgvKhachHang.DataSource = db.ExecuteQuery(query);
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
        }
        private void btnTimKiem_Click(object sender, EventArgs e)
        {

        }

        private void btnThem_Click(object sender, EventArgs e)
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

        private void btnSua_Click(object sender, EventArgs e)
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

        private void btnXoa_Click(object sender, EventArgs e)
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

        private void dgvKhachHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvKhachHang.Rows[e.RowIndex];
                txtHoTen.Text = row.Cells["HoTen"].Value.ToString();
                txtSoDienThoai.Text = row.Cells["SoDienThoai"].Value.ToString();

                cboGioiTinh.SelectedItem = row.Cells["GioiTinh"].Value?.ToString();

            }
        }
    }
}
