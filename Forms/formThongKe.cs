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
    public partial class formThongKe: Form
    {
        DbHelper db = new DbHelper();
        public formThongKe()
        {
            InitializeComponent();
            LoadDoanhThu();
        }
        private void formThongKe_Load(object sender, EventArgs e)
        {
            LoadTheme();

        }
        private void LoadDoanhThu()
        {
            string queryDoanhThu = @"
        SELECT NgayLap, SUM(TongTien) AS DoanhThu
        FROM HoaDon
        WHERE NgayLap BETWEEN @From AND @To
        GROUP BY NgayLap
        ORDER BY NgayLap";

            string queryBanNhieuNhat = @"
        SELECT TOP 1 cthd.MaMatHang, mh.TenMatHang, SUM(cthd.SoLuong) AS SoLuongBan
        FROM ChiTietHoaDon cthd
        JOIN MatHang mh ON cthd.MaMatHang = mh.MaMatHang
        GROUP BY cthd.MaMatHang, mh.TenMatHang
        ORDER BY SUM(cthd.SoLuong) DESC";

            string queryBanItNhat = @"
        SELECT TOP 1 cthd.MaMatHang, mh.TenMatHang, SUM(cthd.SoLuong) AS SoLuongBan
        FROM ChiTietHoaDon cthd
        JOIN MatHang mh ON cthd.MaMatHang = mh.MaMatHang
        GROUP BY cthd.MaMatHang, mh.TenMatHang
        ORDER BY SUM(cthd.SoLuong) ASC";

            Dictionary<string, object> parameters = new Dictionary<string, object>
    {
        { "@From", dtpFrom.Value.Date },
        { "@To", dtpTo.Value.Date }
    };

            DataTable dtDoanhThu = db.ExecuteQuery(queryDoanhThu, parameters);
            dgvDoanhThu.DataSource = dtDoanhThu;

            decimal totalRevenue = dtDoanhThu.AsEnumerable().Sum(row => row.Field<decimal>("DoanhThu"));
            txtDoanhThu.Text = $"Tổng Doanh Thu: {totalRevenue:N0} VNĐ";

            chartDoanhThu.Series[0].Points.Clear();
            foreach (DataRow row in dtDoanhThu.Rows)
            {
                DateTime ngayLap = Convert.ToDateTime(row["NgayLap"]);
                double doanhThu = Convert.ToDouble(row["DoanhThu"]);

                chartDoanhThu.Series[0].Points.AddXY(ngayLap.ToString("dd/MM"), doanhThu);
            }
            chartDoanhThu.ChartAreas[0].AxisX.Interval = 1;

            DataTable dtBanNhieuNhat = db.ExecuteQuery(queryBanNhieuNhat);
            if (dtBanNhieuNhat.Rows.Count > 0)
            {
                string tenMatHang = dtBanNhieuNhat.Rows[0]["TenMatHang"].ToString();
                int soLuongBan = Convert.ToInt32(dtBanNhieuNhat.Rows[0]["SoLuongBan"]);
                txtBanChayNhat.Text = $"{tenMatHang} ({soLuongBan} sản phẩm)";
            }
            else
            {
                txtBanChayNhat.Text = "Không có dữ liệu";
            }

            DataTable dtBanItNhat = db.ExecuteQuery(queryBanItNhat);
            if (dtBanItNhat.Rows.Count > 0)
            {
                string tenMatHang = dtBanItNhat.Rows[0]["TenMatHang"].ToString();
                int soLuongBan = Convert.ToInt32(dtBanItNhat.Rows[0]["SoLuongBan"]);
                txtBanItNhat.Text = $"{tenMatHang} ({soLuongBan} sản phẩm)";
            }
            else
            {
                txtBanItNhat.Text = "Không có dữ liệu";
            }
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
            
        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            LoadDoanhThu();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
