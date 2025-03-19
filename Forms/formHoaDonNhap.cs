using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Karaokelamlai.Forms
{
    public partial class formHoaDonNhap : Form
    {
        DbHelper db = new DbHelper();

        public formHoaDonNhap()
        {
            InitializeComponent();
            LoadMatHang();
            LoadHoaDonNhap();
            dtpNgayNhap.Value = DateTime.Today; // Set ngày nhập mặc định là hôm nay
        }
        private void formHoaDonNhap_Load(object sender, EventArgs e)
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
            label5.ForeColor = Themecolor.SecondaryColor;
        }
        private void LoadMatHang()
        {
            string query = "SELECT MaMatHang, TenMatHang, DonGia FROM MatHang";
            DataTable dt = db.ExecuteQuery(query);

            cboMatHang.DataSource = dt;
            cboMatHang.DisplayMember = "TenMatHang";
            cboMatHang.ValueMember = "MaMatHang";
        }

        private void LoadHoaDonNhap()
        {
            string query = "SELECT hdn.MaHoaDonNhap, mh.TenMatHang, hdn.SoLuongNhap, hdn.TongTien, hdn.NgayNhap " +
                           "FROM HoaDonNhap hdn JOIN MatHang mh ON hdn.MaMatHang = mh.MaMatHang";
            DataTable dt = db.ExecuteQuery(query);

            dgvHoaDonNhap.ForeColor = Color.Black;
            dgvHoaDonNhap.DataSource = dt;
            dgvHoaDonNhap.AutoGenerateColumns = true;
            dgvHoaDonNhap.ClearSelection();
            dgvHoaDonNhap.Refresh();
        }

        private void cboMatHang_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboMatHang.SelectedValue != null)
            {
                int maMatHang = Convert.ToInt32(cboMatHang.SelectedValue);
                string query = "SELECT DonGia FROM MatHang WHERE MaMatHang = @MaMatHang";
                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "@MaMatHang", maMatHang }
                };

                object result = db.ExecuteScalar(query, parameters);
                if (result != null)
                {
                    txtDonGia.Text = result.ToString();
                    TinhTongTien();
                }
            }
        }

        private void txtSoLuong_TextChanged(object sender, EventArgs e)
        {
            TinhTongTien();
        }

        private void TinhTongTien()
        {
            if (decimal.TryParse(txtDonGia.Text, out decimal donGia) &&
                int.TryParse(txtSoLuong.Text, out int soLuong))
            {
                decimal tongTien = donGia * soLuong;
                txtTongTien.Text = tongTien.ToString("N2");
            }
            else
            {
                txtTongTien.Text = "0";
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (cboMatHang.SelectedValue == null || string.IsNullOrWhiteSpace(txtSoLuong.Text))
            {
                MessageBox.Show("Vui lòng chọn mặt hàng và nhập số lượng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int maMatHang = Convert.ToInt32(cboMatHang.SelectedValue);
            int soLuong = int.Parse(txtSoLuong.Text);
            decimal tongTien = decimal.Parse(txtTongTien.Text);
            DateTime ngayNhap = dtpNgayNhap.Value.Date; // Lấy ngày nhập từ DateTimePicker

            string query = "INSERT INTO HoaDonNhap (MaMatHang, SoLuongNhap, TongTien, NgayNhap) VALUES (@MaMatHang, @SoLuongNhap, @TongTien, @NgayNhap)";
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@MaMatHang", maMatHang },
                { "@SoLuongNhap", soLuong },
                { "@TongTien", tongTien },
                { "@NgayNhap", ngayNhap }
            };

            if (db.ExecuteNonQuery(query, parameters) > 0)
            {
                MessageBox.Show("Lưu hóa đơn thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadHoaDonNhap();
            }
            else
            {
                MessageBox.Show("Lưu thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvHoaDonNhap.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn hóa đơn để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int maHoaDonNhap = Convert.ToInt32(dgvHoaDonNhap.SelectedRows[0].Cells["MaHoaDonNhap"].Value);

            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa hóa đơn này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No) return;

            string query = "DELETE FROM HoaDonNhap WHERE MaHoaDonNhap = @MaHoaDonNhap";
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@MaHoaDonNhap", maHoaDonNhap }
            };

            if (db.ExecuteNonQuery(query, parameters) > 0)
            {
                MessageBox.Show("Xóa hóa đơn thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadHoaDonNhap();
            }
            else
            {
                MessageBox.Show("Xóa thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        
    }
}
