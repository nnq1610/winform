using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Karaokelamlai.Forms
{
    public partial class formHoaDon : Form
    {
        private DbHelper db = new DbHelper();

        private decimal  tienPhong = 0;
        public formHoaDon()
        {
            InitializeComponent();
            InitializeDataGridView();
            LoadData();
        }

        private void InitializeDataGridView()
        {
            dgvChiTiet.Columns.Clear();
            dgvChiTiet.Columns.Add("MaMatHang", "Mã Mặt Hàng");
            dgvChiTiet.Columns.Add("TenMatHang", "Tên Mặt Hàng");
            dgvChiTiet.Columns.Add("SoLuong", "Số Lượng");
            dgvChiTiet.Columns.Add("DonGia", "Đơn Giá");
            dgvChiTiet.Columns.Add("ThanhTien", "Thành Tiền");
        }

        private void LoadData()
        {
            LoadKhachHang();
            LoadNhanVien();
            LoadMatHang();
            LoadPhong();
        }

        private void LoadKhachHang()
        {
            cboKhachHang.DataSource = db.ExecuteQuery("SELECT MaKhachHang, HoTen FROM KhachHang");
            cboKhachHang.DisplayMember = "HoTen";
            cboKhachHang.ValueMember = "MaKhachHang";
        }


        private void LoadNhanVien()
        {
            cboNhanVien.DataSource = db.ExecuteQuery("SELECT MaNhanVien, HoTen FROM NhanVien");
            cboNhanVien.DisplayMember = "HoTen";
            cboNhanVien.ValueMember = "MaNhanVien";
        }

        private void LoadMatHang()
        {
            cboMatHang.DataSource = db.ExecuteQuery("SELECT MaMatHang, TenMatHang, DonGia FROM MatHang");
            cboMatHang.DisplayMember = "TenMatHang";
            cboMatHang.ValueMember = "MaMatHang";
        }

        private void LoadPhong()
        {
            cboPhong.DataSource = db.ExecuteQuery("SELECT MaDatPhong, TongTien FROM DatPhong");
            cboPhong.DisplayMember = "MaDatPhong";
            cboPhong.ValueMember = "MaDatPhong";
        }

        private void btnTinhTien_Click(object sender, EventArgs e)
        {
            decimal tongTienHang = 0;
            foreach (DataGridViewRow row in dgvChiTiet.Rows)
            {
                if (row.Cells[4].Value != null)
                    tongTienHang += Convert.ToDecimal(row.Cells[4].Value);
            }

            decimal giaPhong = Convert.ToDecimal(cboPhong.SelectedValue ?? 0);
            txtTongHoaDon.Text = (tongTienHang + giaPhong).ToString();
        }
        
        private decimal GetDonGia(int maMatHang)
        {
            DataTable dt = db.ExecuteQuery($"SELECT DonGia FROM MatHang WHERE MaMatHang = {maMatHang}");
            return dt.Rows.Count > 0 ? Convert.ToDecimal(dt.Rows[0]["DonGia"]) : 0;
        }      

        private int lastInsertedHoaDonId = -1; 
        private decimal tien = 0;
        private void btnTaoHoaDon_Click(object sender, EventArgs e)
        {
            try
            {
                string ngayLap = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                int maNhanVien = Convert.ToInt32(cboNhanVien.SelectedValue);
                int maKhachHang = Convert.ToInt32(cboKhachHang.SelectedValue);
                int maDatPhong = Convert.ToInt32(cboPhong.SelectedValue);
                decimal tongTien = 0; 

                string insertHoaDonQuery = "INSERT INTO HoaDon (NgayLap, MaKhachHang, TongTien, MaNhanVien, MaDatPhong) " +
                                           "OUTPUT INSERTED.MaHoaDon VALUES (@NgayLap, @MaKhachHang, @TongTien, @MaNhanVien, @MaDatPhong)";

                Dictionary<string, object> parameters = new Dictionary<string, object>
        {
            { "@NgayLap", ngayLap },
            { "@MaKhachHang", maKhachHang },
            { "@TongTien", tongTien },
            { "@MaNhanVien", maNhanVien },
            { "@MaDatPhong", maDatPhong }
        };

                object result = db.ExecuteScalar(insertHoaDonQuery, parameters);
                if (result != null)
                {
                    lastInsertedHoaDonId = Convert.ToInt32(result);
                    MessageBox.Show($"Hóa đơn đã được tạo thành công! Mã hóa đơn: {lastInsertedHoaDonId}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Lỗi khi tạo hóa đơn!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLuuChiTietHoaDon_Click(object sender, EventArgs e)
        {
            if (lastInsertedHoaDonId == -1)
            {
                MessageBox.Show("Vui lòng tạo hóa đơn trước!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                decimal tongTien = 0;

                foreach (DataGridViewRow row in dgvChiTiet.Rows)
                {
                    if (row.Cells["MaMatHang"].Value == null) continue;

                    int maMatHang = Convert.ToInt32(row.Cells["MaMatHang"].Value);
                    int soLuong = Convert.ToInt32(row.Cells["SoLuong"].Value);
                    decimal donGia = Convert.ToDecimal(row.Cells["DonGia"].Value);
                    decimal thanhTien = Convert.ToDecimal(row.Cells["ThanhTien"].Value);

                    tongTien += thanhTien;
                    string insertChiTietQuery = "INSERT INTO ChiTietHoaDon (MaHoaDon, MaMatHang, SoLuong, DonGia) " +
                                                "VALUES (@MaHoaDon, @MaMatHang, @SoLuong, @DonGia)";

                    Dictionary<string, object> chiTietParams = new Dictionary<string, object>
            {
                { "@MaHoaDon", lastInsertedHoaDonId },
                { "@MaMatHang", maMatHang },
                { "@SoLuong", soLuong },
                { "@DonGia", donGia },
            };

                    db.ExecuteNonQuery(insertChiTietQuery, chiTietParams);
                }
                tongTien += tienPhong;
                txtTongHoaDon.Text = tongTien.ToString();

                string updateQuery = "UPDATE HoaDon SET TongTien = @TongTien WHERE MaHoaDon = @MaHoaDon";
                Dictionary<string, object> updateParams = new Dictionary<string, object>
        {
            { "@TongTien", tongTien },
            { "@MaHoaDon", lastInsertedHoaDonId }
        };
                db.ExecuteNonQuery(updateQuery, updateParams);

                MessageBox.Show("Lưu chi tiết hóa đơn thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnThemVaoDgv_Click(object sender, EventArgs e)
        {
            if (cboMatHang.SelectedIndex == -1 || nudSoLuong.Value <= 0) return;

            int maMatHang = (int)cboMatHang.SelectedValue;
            string tenMatHang = cboMatHang.Text;
            decimal donGia = GetDonGia(maMatHang);
            int soLuong = (int)nudSoLuong.Value;
            decimal thanhTien = donGia * soLuong;

            foreach (DataGridViewRow row in dgvChiTiet.Rows)
            {
                if (row.Cells[0].Value != null && row.Cells[0].Value.ToString() == maMatHang.ToString())
                {
                    int existingSoLuong = Convert.ToInt32(row.Cells[2].Value);
                    row.Cells[2].Value = existingSoLuong + soLuong;
                    row.Cells[4].Value = (existingSoLuong + soLuong) * donGia;
                    return;
                }
            }

            dgvChiTiet.Rows.Add(maMatHang, tenMatHang, soLuong, donGia, thanhTien);
        }

        private void btnXoaDgv_Click(object sender, EventArgs e)
        {
            if (dgvChiTiet.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow row in dgvChiTiet.SelectedRows)
                {
                    dgvChiTiet.Rows.Remove(row);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một mặt hàng để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void cboPhong_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboPhong.SelectedItem is DataRowView drv)
            {
                int maDatPhong = Convert.ToInt32(drv["MaDatPhong"]);
                DataTable dt = db.ExecuteQuery($"SELECT TongTien FROM DatPhong WHERE MaDatPhong = {maDatPhong}");

                txtTienPhong.Text = dt.Rows.Count > 0 ? dt.Rows[0]["TongTien"].ToString() : "0";
                tienPhong = Convert.ToDecimal(txtTienPhong.Text);
            }
        }



        private void cboMatHang_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboMatHang.SelectedItem is DataRowView drv)
            {
                int maMatHang = Convert.ToInt32(drv["MaMatHang"]);
                DataTable dt = db.ExecuteQuery($"SELECT DonGia FROM MatHang WHERE MaMatHang = {maMatHang}");

                if (dt.Rows.Count > 0)
                {
                    txtDonGia.Text = dt.Rows[0]["DonGia"].ToString();
                }
                else
                {
                    txtDonGia.Text = "0";
                }
            }
        }


        private void Tông_Click(object sender, EventArgs e)
        {

        }
    }
}

