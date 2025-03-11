using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace QuanHat
{
    public partial class formHoaDon : Form
    {
        private DbHelper db = new DbHelper();

        public formHoaDon()
        {
            InitializeComponent();
            LoadMatHang();
            LoadPhong();
            LoadNhanVien();
            LoadPhuongThucThanhToan();
        }
        private void LoadPhuongThucThanhToan()
        {
            List<string> phuongThuc = new List<string> { "Tiền mặt", "Chuyển khoản" };
            cboPhuongThucThanhToan.DataSource = phuongThuc;
        }
        private void LoadNhanVien()
        {
            string query = "select MaNhanVien, HoTen, ChucVu FROM NhanVien";
            DataTable dt = db.ExecuteQuery(query);
            cboNhanVien.DataSource = dt;
            cboNhanVien.DisplayMember = "HoTen";
            cboNhanVien.ValueMember = "MaNhanVien";
        }
        private void LoadMatHang()
        {
            string query = "SELECT TenMatHang, DonGia FROM MatHang";
            DataTable dt = db.ExecuteQuery(query);

            dgvMatHang.Rows.Clear();
            foreach (DataRow row in dt.Rows)
            {
                dgvMatHang.Rows.Add(false, row["TenMatHang"], row["DonGia"], 0, 0);
            }
        }

        private void LoadPhong()
        {
            string query = "SELECT MaDatPhong, TongTien FROM DatPhong";
            DataTable dt = db.ExecuteQuery(query);

            cboPhong.DataSource = dt;
            cboPhong.DisplayMember = "MaDatPhong";
            cboPhong.ValueMember = "TongTien";
        }

        private void dgvMatHang_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && (e.ColumnIndex == 0 || e.ColumnIndex == 3)) // Checkbox or quantity change
            {
                CalculateRowTotal(e.RowIndex);
            }
        }

        private void dgvMatHang_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == 3) // Quantity column
            {
                CalculateRowTotal(e.RowIndex);
            }
        }

        private void CalculateRowTotal(int rowIndex)
        {
            if (rowIndex < 0 || rowIndex >= dgvMatHang.Rows.Count)
                return;

            bool isChecked = Convert.ToBoolean(dgvMatHang.Rows[rowIndex].Cells[0].Value);
            int soLuong = 0;
            decimal donGia = 0, thanhTien = 0;

            int.TryParse(dgvMatHang.Rows[rowIndex].Cells[3].Value?.ToString(), out soLuong);
            decimal.TryParse(dgvMatHang.Rows[rowIndex].Cells[2].Value?.ToString(), out donGia);

            if (isChecked)
            {
                thanhTien = soLuong * donGia;
            }

            dgvMatHang.Rows[rowIndex].Cells[4].Value = thanhTien;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            decimal tongTienHang = 0;

            foreach (DataGridViewRow row in dgvMatHang.Rows)
            {
                bool isChecked = Convert.ToBoolean(row.Cells[0].Value);
                if (isChecked)
                {
                    decimal thanhTien = 0;
                    decimal.TryParse(row.Cells[4].Value?.ToString(), out thanhTien);
                    tongTienHang += thanhTien;
                }
            }

            decimal giaPhong = 0;
            decimal.TryParse(cboPhong.SelectedValue?.ToString(), out giaPhong);

            decimal tongHoaDon = giaPhong + tongTienHang;

            txtThanhTien.Text = tongHoaDon.ToString();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            
                string ngayLap = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                decimal tongTien = decimal.Parse(txtThanhTien.Text);
                int maNhanVien = Convert.ToInt32(cboNhanVien.SelectedValue);
                int maDatPhong = Convert.ToInt32(cboPhong.SelectedValue);
            string phuongThucThanhToan = cboPhuongThucThanhToan.SelectedItem.ToString();

                string query = "INSERT INTO HoaDon (NgayLap, TongTien, MaNhanVien, PhuongThucThanhToan) VALUES " +
                               $"('{ngayLap}', {tongTien}, {(maNhanVien)}, '{phuongThucThanhToan}')";

                Dictionary<string, object> parameters = new Dictionary<string, object>
        {
            { "@NgayLap", ngayLap },
            { "@TongTien", tongTien },
            { "@MaNhanVien", maNhanVien },
                    {"@MaDatPhong", maDatPhong }
        };

                if (db.ExecuteNonQuery(query, parameters) > 0)
                {
                    MessageBox.Show("Luu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadMatHang();
                }
                else
                {
                    MessageBox.Show("Thêm  thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

    }
}
