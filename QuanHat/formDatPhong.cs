using QuanHat;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace QuanHat
{
    public partial class FormDatPhong : Form
    {
        private DbHelper db = new DbHelper();

        public FormDatPhong()
        {
            InitializeComponent();
            LoadRooms();
            LoadBookings();
        }

        // Load danh sách phòng
        private void LoadRooms()
        {
            string query = "SELECT MaPhong, GiaGio, TrangThai FROM PhongHat";
            DataTable dt = db.ExecuteQuery(query);

            // Clear previous data to prevent duplication
            cboPhong.Items.Clear();
            cboTrangThai.Items.Clear();

            foreach (DataRow row in dt.Rows)
            {
                cboPhong.Items.Add(row["MaPhong"].ToString());
                cboTrangThai.Items.Add(row["TrangThai"].ToString());
            }
        }


        private void LoadBookings()
        {
            string query = "SELECT * FROM DatPhong";
            dgvDatPhong.DataSource = db.ExecuteQuery(query);
        }

        private decimal CalculateTotalAmount()
        {
            if (cboPhong.SelectedItem == null)
                return 0;

            string query = "SELECT GiaGio FROM PhongHat WHERE MaPhong = @MaPhong";
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@MaPhong", cboPhong.SelectedItem.ToString() }
            };

            DataTable dt = db.ExecuteQuery(query, parameters);

            decimal GiaGio = Convert.ToDecimal(dt.Rows[0]["GiaGio"]);
            TimeSpan duration = dtpKetThuc.Value - dtpBatDau.Value;
            decimal totalAmount = GiaGio * (decimal)duration.TotalHours;

            return totalAmount;
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (cboPhong.SelectedItem == null || string.IsNullOrWhiteSpace(txtHoTenKhach.Text) || string.IsNullOrWhiteSpace(txtSoDienThoai.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            decimal totalAmount = CalculateTotalAmount();
            txtTongTien.Text = totalAmount.ToString();

            string query = "INSERT INTO DatPhong (MaPhong, HoTenKhach, SoDienThoai, ThoiGianBatDau, ThoiGianKetThuc, TrangThai, TongTien) " +
                           "VALUES (@MaPhong, @HoTenKhach, @SoDienThoai, @ThoiGianBatDau, @ThoiGianKetThuc, @TrangThai, @TongTien)";
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@MaPhong", cboPhong.SelectedItem.ToString() },
                { "@HoTenKhach", txtHoTenKhach.Text },
                { "@SoDienThoai", txtSoDienThoai.Text },
                { "@ThoiGianBatDau", dtpBatDau.Value },
                { "@ThoiGianKetThuc", dtpKetThuc.Value },
                { "@TrangThai", cboTrangThai.SelectedItem.ToString() },
                {"TongTien", totalAmount }
            };

            if (db.ExecuteNonQuery(query, parameters) > 0)
            {
                MessageBox.Show("Thêm đặt phòng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadBookings();
            }
        }

        // Cập nhật đặt phòng
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvDatPhong.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn phòng để sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string query = "UPDATE DatPhong SET HoTenKhach = @HoTenKhach, SoDienThoai = @SoDienThoai, " +
                           "ThoiGianBatDau = @ThoiGianBatDau, ThoiGianKetThuc = @ThoiGianKetThuc, TrangThai = @TrangThai " +
                           "WHERE MaPhong = @MaPhong";

            Dictionary<string, object> parameters = new Dictionary<string, object>
            {

                { "@MaPhong", cboPhong.SelectedItem.ToString() },
                { "@HoTenKhach", txtHoTenKhach.Text },
                { "@SoDienThoai", txtSoDienThoai.Text },
                { "@ThoiGianBatDau", dtpBatDau.Value },
                { "@ThoiGianKetThuc", dtpKetThuc.Value },
                { "@TrangThai", cboTrangThai.SelectedItem.ToString() }
            };

            if (db.ExecuteNonQuery(query, parameters) > 0)
            {
                MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadBookings();
            }
        }

        // Xóa đặt phòng
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvDatPhong.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn phòng cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string maPhong = dgvDatPhong.SelectedRows[0].Cells["MaPhong"].Value?.ToString();

            if (string.IsNullOrEmpty(maPhong))
            {
                MessageBox.Show("Không thể xác định mã phòng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string query = "DELETE FROM DatPhong WHERE MaPhong = @MaPhong";
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@MaPhong", cboPhong.SelectedItem.ToString() }
            };

            if (db.ExecuteNonQuery(query, parameters) > 0)
            {
                MessageBox.Show("Xóa đặt phòng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadBookings();
            }
            else
            {
                MessageBox.Show("Không thể xóa đặt phòng. Vui lòng thử lại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtHoTenKhach_Enter(object sender, EventArgs e)
        {
            if (txtHoTenKhach.Text == "Nhap ten cua ban.....")
            {
                txtHoTenKhach.Text = "";
                txtHoTenKhach.BackColor = Color.LightYellow;
                txtHoTenKhach.ForeColor = Color.Black;
            }

        }

        private void txtHoTenKhach_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtHoTenKhach.Text))
            {
                txtHoTenKhach.Text = "Nhap ten cua ban.....";
                txtHoTenKhach.BackColor = Color.White;
                txtHoTenKhach.ForeColor = Color.Gray;
            }

        }

        private void txtSoDienThoai_Enter(object sender, EventArgs e)
        {
            if (txtSoDienThoai.Text == "Nhap so dien thoai cua ban.....")
            {
                txtSoDienThoai.Text = "";
                txtSoDienThoai.BackColor = Color.LightYellow;
                txtSoDienThoai.ForeColor = Color.Black;
            }
        }

        private void txtSoDienThoai_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSoDienThoai.Text))
            {
                txtSoDienThoai.Text = "Nhap so dien thoai cua ban.....";
                txtSoDienThoai.BackColor = Color.White;
                txtSoDienThoai.ForeColor = Color.Gray;
            }
        }

        private void FormDatPhong_Load(object sender, EventArgs e)
        {

        }
    }
}
