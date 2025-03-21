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
            LoadNhanVien();
            dtpNgayNhap.Value = DateTime.Today;
        }
        private void LoadNhanVien()
        {
            string query = "SELECT HoTen, MaNhanVien from NhanVien";
            DataTable dt = db.ExecuteQuery(query);
            cboNhanVien.DataSource = dt;
            cboNhanVien.DisplayMember = "HoTen";
            cboNhanVien.ValueMember = "MaNhanVien";
        }
        private void formHoaDonNhap_Load(object sender, EventArgs e)
        {
            ApplyTheme(this);
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
                    ApplyTheme(ctrl);
                }
            }
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
            string query = "SELECT hdn.MaHDN, mh.TenMatHang, hdn.SoLuongTon, hdn.TongTien, hdn.NgayNhap " +
                           "FROM HoaDonNhap hdn JOIN MatHang mh ON hdn.MaMatHang = mh.MaMatHang";
            DataTable dt = db.ExecuteQuery(query);

            dgvHoaDonNhap.ForeColor = Color.Black;
            dgvHoaDonNhap.DataSource = dt;
            dgvHoaDonNhap.ClearSelection();
        }


        private void nudSoLuong_ValueChanged(object sender, EventArgs e)
        {
            if (nudSoLuong.Value < 1)
            {
                nudSoLuong.Value = 1; 
            }

            TinhTongTien();
        }

       
        private void TinhTongTien()
        {
            if (decimal.TryParse(txtDonGia.Text, out decimal donGia) &&
                int.TryParse(nudSoLuong.Value.ToString(), out int soLuong))
            {
                txtTongTien.Text = (donGia * soLuong).ToString();
            }
            else
            {
                txtTongTien.Text = "0";
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (cboMatHang.SelectedValue == null || cboNhanVien.SelectedValue == null || nudSoLuong.Value <= 0)
            {
                MessageBox.Show("Vui lòng chọn mặt hàng, nhân viên và nhập số lượng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int maMatHang = Convert.ToInt32(cboMatHang.SelectedValue);
            string tenMatHang = cboMatHang.Text;
            int maNhanVien = Convert.ToInt32(cboNhanVien.SelectedValue);
            int soLuong = (int)nudSoLuong.Value;

            TinhTongTien();
            if (!decimal.TryParse(txtTongTien.Text, out decimal tongTien))
            {
                MessageBox.Show("Tổng tiền không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DateTime ngayNhap = dtpNgayNhap.Value.Date;

            string query = "INSERT INTO HoaDonNhap (MaMatHang, TenMatHang, SoLuongTon, TongTien, NgayNhap, NhaCungCap, MaNhanVien) " +
                           "VALUES (@MaMatHang, @TenMatHang, @SoLuongNhap, @TongTien, @NgayNhap, @NhaCungCap, @MaNhanVien)";

            Dictionary<string, object> parameters = new Dictionary<string, object>
    {
        { "@MaMatHang", maMatHang },
        { "@TenMatHang", tenMatHang },
        { "@SoLuongNhap", soLuong },
        { "@TongTien", tongTien },
        { "@NgayNhap", ngayNhap },
        { "@NhaCungCap", txtNCC.Text },
        { "@MaNhanVien", maNhanVien }
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

            string checkQuery = "SELECT COUNT (*) FROM MatHang Where MaMatHang = @MaMatHang";
            Dictionary<string, object> checkParam = new Dictionary<string, object>
            {
                {"@MaMatHang", maMatHang }
            };
            int count = Convert.ToInt32(db.ExecuteScalar(checkQuery, checkParam));
                {
                if (count > 0)
                {
                    string updateQuery = "Update MatHang set SoLuongTon = SoLuongTon + @SoLuongTon Where MaMatHang = @MaMatHang";
                    Dictionary<string, object> c1 = new Dictionary<string, object>
                    {
                        { "SoLuongTon", soLuong },
                        { "MaMatHang", maMatHang }
                    };
                    db.ExecuteNonQuery(updateQuery, c1);

                }
                else
                {
                    string insertQuery = "INSERT INTO MatHang (MaMatHang, TenMatHang, DonGia, SoLuongTon) VALUES (@MaMatHang, @TenMatHang, @DonGia, @SoLuongTon)";
                    Dictionary<string, object> insertParams = new Dictionary<string, object>
                    {
                        { "@MaMatHang", maMatHang },
                        { "@TenMatHang", tenMatHang },
                        { "@DonGia", txtDonGia.Text },
                        { "@SoLuongTon", soLuong }
                    };
                    db.ExecuteNonQuery(insertQuery, insertParams);
                }
                }

            }



        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvHoaDonNhap.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn hóa đơn để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int maHoaDonNhap = Convert.ToInt32(dgvHoaDonNhap.SelectedRows[0].Cells["MaHDN"].Value);

            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa hóa đơn này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No) return;

            string query = "DELETE FROM HoaDonNhap WHERE MaHDN = @MaHoaDonNhap";
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
