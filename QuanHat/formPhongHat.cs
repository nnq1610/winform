using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace QuanHat
{
    public partial class formPhongHat : Form
    {
        DbHelper db = new DbHelper();

        public formPhongHat()
        {
            InitializeComponent();
            LoadComboBoxData();
            LoadPhong();
        }

        private void LoadComboBoxData()
        {
            cboLoaiPhong.Items.Clear();
            cboLoaiPhong.Items.Add("VIP");
            cboLoaiPhong.Items.Add("Thường");
            cboLoaiPhong.SelectedIndex = 0;

            cboTrangThai.Items.Clear();
            cboTrangThai.Items.Add("Trống");
            cboTrangThai.Items.Add("Đang sử dụng");
            cboTrangThai.Items.Add("Đang dọn dẹp");
            cboTrangThai.SelectedIndex = 0;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTenPhong.Text) || string.IsNullOrWhiteSpace(txtGiaPhong.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(txtGiaPhong.Text, out decimal giaPhong) || giaPhong <= 0)
            {
                MessageBox.Show("Giá phòng phải là số dương!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string tenPhong = txtTenPhong.Text.Trim();
            string loaiPhong = cboLoaiPhong.SelectedItem.ToString();
            string trangThai = cboTrangThai.SelectedItem.ToString();

            string query = "INSERT INTO PhongHat (TenPhong, LoaiPhong, GiaGio, TrangThai) VALUES (@TenPhong, @LoaiPhong, @GiaGio, @TrangThai)";

            DbHelper db = new DbHelper();
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "@TenPhong", tenPhong },
                    { "@LoaiPhong", loaiPhong },
                    { "@GiaGio", giaPhong },
                    { "@TrangThai", trangThai }
                };

                if (db.ExecuteNonQuery(query, parameters) > 0)
                {
                    MessageBox.Show("Thêm phòng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadPhong();
                }
                else
                {
                    MessageBox.Show("Thêm phòng thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void LoadPhong()
        {
            string query = "SELECT * FROM PhongHat";
            {
                dgvPhongHat.DataSource = null; // Reset trước khi cập nhật (tránh lỗi)
                dgvPhongHat.DataSource = db.ExecuteQuery(query);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if(dgvPhongHat.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn phòng để sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtTenPhong.Text) || string.IsNullOrWhiteSpace(txtGiaPhong.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(txtGiaPhong.Text, out decimal giaPhong) || giaPhong <= 0)
            {
                MessageBox.Show("Giá phòng phải là số dương!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int phongId = Convert.ToInt32(dgvPhongHat.SelectedRows[0].Cells["MaPhong"].Value);
            string tenPhong = txtTenPhong.Text.Trim();
            string loaiPhong = cboLoaiPhong.SelectedItem.ToString();
            string trangThai = cboTrangThai.SelectedItem.ToString();

            string query = "UPDATE PhongHat SET TenPhong = @TenPhong, LoaiPhong = @LoaiPhong, GiaGio = @GiaGio, TrangThai = @TrangThai WHERE MaPhong = @PhongID";

            Dictionary<string, object> parameters = new Dictionary<string, object>
    {
        { "@TenPhong", tenPhong },
        { "@LoaiPhong", loaiPhong },
        { "@GiaGio", giaPhong },
        { "@TrangThai", trangThai },
        { "@PhongID", phongId }
    };
            if (db.ExecuteNonQuery(query, parameters) > 0)
            {
                MessageBox.Show("Cập nhật phòng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadPhong();
            }
            else
            {
                MessageBox.Show("Cập nhật phòng thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnXoa_Click_1(object sender, EventArgs e)
        {
            if (dgvPhongHat.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn phòng để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (dgvPhongHat.CurrentRow == null)
            {
                MessageBox.Show("No row selected!", "Debug");
                return;
            }
            int phongId = Convert.ToInt32(dgvPhongHat.CurrentRow.Cells["MaPhong"].Value);

            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa phòng này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No)
            {
                return;
            }

            string query = "DELETE FROM PhongHat WHERE MaPhong = @PhongID";
            Dictionary<string, object> parameters = new Dictionary<string, object>
    {
        { "@PhongID", phongId }
    };

            if (db.ExecuteNonQuery(query, parameters) > 0)
            {
                MessageBox.Show("Xóa phòng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadPhong();
            }
            else
            {
                MessageBox.Show("Xóa phòng thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex > 0 )
            {
                DataGridViewRow row = dgvPhongHat.Rows[e.RowIndex];
                txtTenPhong.Text = row.Cells["TenPhong"].Value.ToString();
                txtGiaPhong.Text = row.Cells["GiaGio"].Value.ToString();
                cboLoaiPhong.SelectedItem = row.Cells["TrangThai"].Value.ToString();
            }
        }

        private void dgvPhongHat_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
