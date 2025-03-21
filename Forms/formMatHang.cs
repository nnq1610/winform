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
    public partial class formMatHang: Form
    {
        DbHelper db = new DbHelper();
        public formMatHang()
        {
            InitializeComponent();
            LoadMatHang();
        }
        private void formMatHang_Load(object sender, EventArgs e)
        {
            LoadTheme();
        }
        private void LoadMatHang()
        {
            string query = "SELECT * FROM MatHang";
            DataTable dt = db.ExecuteQuery(query);
            dgvMatHang.ForeColor = System.Drawing.Color.Black;
            dgvMatHang.DataSource = dt;
            dgvMatHang.AutoGenerateColumns = true;
            dgvMatHang.ClearSelection();
            dgvMatHang.Refresh();
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

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTenMatHang.Text) || string.IsNullOrWhiteSpace(txtDonGia.Text) || string.IsNullOrWhiteSpace(txtSoLuongTon.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(txtDonGia.Text, out decimal donGia) || donGia <= 0)
            {
                MessageBox.Show("Đơn giá phải là số dương!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtSoLuongTon.Text, out int soLuongTon) || soLuongTon < 0)
            {
                MessageBox.Show("Số lượng tồn phải là số không âm!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string tenMatHang = txtTenMatHang.Text.Trim();

            string query = "INSERT INTO MatHang (TenMatHang, DonGia, SoLuongTon) VALUES (@TenMatHang, @DonGia, @SoLuongTon)";

            Dictionary<string, object> parameters = new Dictionary<string, object>
        {
            { "@TenMatHang", tenMatHang },
            { "@DonGia", donGia },
            { "@SoLuongTon", soLuongTon },
        };

            if (db.ExecuteNonQuery(query, parameters) > 0)
            {
                MessageBox.Show("Thêm mặt hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadMatHang();
            }
            else
            {
                MessageBox.Show("Thêm mặt hàng thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dgvMatHang.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn mặt hàng để sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int maMatHang = Convert.ToInt32(dgvMatHang.SelectedRows[0].Cells["MaMatHang"].Value);
            string tenMatHang = txtTenMatHang.Text.Trim();

            if (!decimal.TryParse(txtDonGia.Text, out decimal donGia) || donGia <= 0 ||
                !int.TryParse(txtSoLuongTon.Text, out int soLuongTon) || soLuongTon < 0)
            {
                MessageBox.Show("Giá và số lượng phải hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string query = "UPDATE MatHang SET TenMatHang = @TenMatHang, DonGia = @DonGia, SoLuongTon = @SoLuongTon WHERE MaMatHang = @MaMatHang";

            Dictionary<string, object> parameters = new Dictionary<string, object>
        {
            { "@TenMatHang", tenMatHang },
            { "@DonGia", donGia },
            { "@SoLuongTon", soLuongTon },
                {"@MaMatHang", maMatHang }
        };

            if (db.ExecuteNonQuery(query, parameters) > 0)
            {
                MessageBox.Show("Cập nhật mặt hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadMatHang();
            }
            else
            {
                MessageBox.Show("Cập nhật thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvMatHang.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn mặt hàng để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int maMatHang = Convert.ToInt32(dgvMatHang.SelectedRows[0].Cells["MaMatHang"].Value);

            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa mặt hàng này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No) return;

            string query = "DELETE FROM MatHang WHERE MaMatHang = @MaMatHang";
            Dictionary<string, object> parameters = new Dictionary<string, object> { { "@MaMatHang", maMatHang } };

            if (db.ExecuteNonQuery(query, parameters) > 0)
            {
                MessageBox.Show("Xóa mặt hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadMatHang();
            }
            else
            {
                MessageBox.Show("Xóa thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtDonGia.Clear();
            txtSoLuongTon.Clear();
            txtTenMatHang.Clear();
        }
    }
}
