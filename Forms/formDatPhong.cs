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
    public partial class formDatPhong: Form
    {
        private DbHelper db = new DbHelper();
        public formDatPhong()
        {
            InitializeComponent();
            LoadRooms();
            LoadBookings();
            LoadGioiTinh();
        }
        private void formDatPhong_Load(object sender, EventArgs e)
        {
            LoadTheme();
        }
        private void LoadGioiTinh()
        {
            cboGioiTinh.Items.Clear();
            cboGioiTinh.Items.Add("Nam");
            cboGioiTinh.Items.Add("Nữ");
            cboGioiTinh.Items.Add("Khác");

            cboGioiTinh.SelectedIndex = 0;
        }

        private void LoadRooms()
        {
            string query = "SELECT MaPhong, TenPhong, GiaGio, TrangThai FROM PhongHat WHERE TrangThai != N'Đang sử dụng'";
            DataTable dt = db.ExecuteQuery(query);

            cboPhong.DataSource = dt;
            cboPhong.DisplayMember = "TenPhong";
            cboPhong.ValueMember = "MaPhong";
        }


        private void LoadBookings()
        {
            string query = "SELECT * FROM DatPhong";
            dgvDatPhong.DataSource = db.ExecuteQuery(query);
        }

        private decimal CalculateTotalAmount()
        {
            if (cboPhong.SelectedValue == null)
                return 0;

            string query = "SELECT GiaGio FROM PhongHat WHERE MaPhong = @MaPhong";
            Dictionary<string, object> parameters = new Dictionary<string, object>
    {
        { "@MaPhong", cboPhong.SelectedValue.ToString() }
    };

            DataTable dt = db.ExecuteQuery(query, parameters);

            if (dt.Rows.Count == 0)
                return 0;

            decimal GiaGio = Convert.ToDecimal(dt.Rows[0]["GiaGio"]);
            TimeSpan duration = dtpKetThuc.Value - dtpBatDau.Value;

            if (duration.TotalHours <= 0)  
            {
                MessageBox.Show("Thời gian kết thúc phải lớn hơn thời gian bắt đầu!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return 0;
            }

            decimal totalAmount = GiaGio * (decimal)duration.TotalHours;

            return totalAmount;
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
                else if (ctrl is Label lbl)
                {
                    lbl.ForeColor = Themecolor.SecondaryColor;
                }
                else if (ctrl is Panel || ctrl is GroupBox)
                {
                    ctrl.BackColor = Themecolor.PrimaryColor;
                }

                if (ctrl.HasChildren)
                {
                    ApplyTheme(ctrl);
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
            label6.ForeColor = Themecolor.SecondaryColor;
            label7.ForeColor = Themecolor.SecondaryColor;
        }




        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dgvDatPhong.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn phòng để sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string query = "UPDATE DatPhong SET HoTenKhach = @HoTenKhach, SoDienThoai = @SoDienThoai, " +
                           "ThoiGianBatDau = @ThoiGianBatDau, ThoiGianKetThuc = @ThoiGianKetThuc " +
                           "WHERE MaPhong = @MaPhong";

            Dictionary<string, object> parameters = new Dictionary<string, object>
            {

                { "@MaPhong", cboPhong.SelectedItem.ToString() },
                { "@HoTenKhach", txtHoTenKhach.Text },
                { "@SoDienThoai", txtSoDienThoai.Text },
                { "@ThoiGianBatDau", dtpBatDau.Value },
                { "@ThoiGianKetThuc", dtpKetThuc.Value },
            };

            if (db.ExecuteNonQuery(query, parameters) > 0)
            {
                MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadBookings();
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

        private void btnXoa_Click(object sender, EventArgs e)
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
                { "@MaPhong",maPhong }
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

        private void btnTinhTien_Click(object sender, EventArgs e)
        {
            decimal tongTien = CalculateTotalAmount();
            txtTongTien.Text = tongTien.ToString();
        }

       

        private void dgvDatPhong_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvDatPhong.Rows[e.RowIndex];
                txtHoTenKhach.Text = row.Cells["HoTenKhach"].Value.ToString();
                txtSoDienThoai.Text = row.Cells["SoDienThoai"].Value.ToString();
                txtTongTien.Text = row.Cells["TongTien"].Value.ToString();

                string maPhong = row.Cells["MaPhong"].Value?.ToString();
                if (cboPhong.Items.Contains(maPhong))
                {
                    cboPhong.SelectedItem = maPhong;
                }


            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtHoTenKhach.Text) || txtHoTenKhach.Text == "Nhap ten cua ban....." ||
                string.IsNullOrWhiteSpace(txtSoDienThoai.Text) || txtSoDienThoai.Text == "Nhap so dien thoai cua ban....." ||
                cboPhong.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal tongTien = CalculateTotalAmount();
            if (tongTien <= 0)
            {
                return; 
            }

            string query = "INSERT INTO DatPhong (MaPhong, HoTenKhach, SoDienThoai, ThoiGianBatDau, ThoiGianKetThuc, TongTien) " +
                           "VALUES (@MaPhong, @HoTenKhach, @SoDienThoai, @ThoiGianBatDau, @ThoiGianKetThuc, @TongTien)";

            Dictionary<string, object> parameters = new Dictionary<string, object>
    {
        { "@MaPhong", cboPhong.SelectedValue.ToString() },
        { "@HoTenKhach", txtHoTenKhach.Text },
        { "@SoDienThoai", txtSoDienThoai.Text },
        { "@ThoiGianBatDau", dtpBatDau.Value },
        { "@ThoiGianKetThuc", dtpKetThuc.Value },
        { "@TongTien", tongTien }
    };

            if (db.ExecuteNonQuery(query, parameters) > 0)
            {
                string updateRoomQuery = "UPDATE PhongHat SET TrangThai = N'Đang sử dụng' WHERE MaPhong = @MaPhong";
                Dictionary<string, object> updateParams = new Dictionary<string, object>
        {
            { "@MaPhong", cboPhong.SelectedValue.ToString() }
        };

                db.ExecuteNonQuery(updateRoomQuery, updateParams);

                MessageBox.Show("Đặt phòng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadBookings();
                LoadRooms();
            }
            else
            {
                MessageBox.Show("Đặt phòng thất bại. Vui lòng thử lại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            txtHoTenKhach.Clear();
            txtSoDienThoai.Clear();
            txtTongTien.Clear();
        }
    }
}
