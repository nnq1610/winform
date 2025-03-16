using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanHat
{
    public partial class formThongKe: Form
    {
        DbHelper db = new DbHelper();

        public formThongKe()
        {
            InitializeComponent();
        }
        private void LoadBestSeller()
        {
            string query = "S";
        }
        private void LoadDoanhThu()
        {
            string query = "SELECT NgayLap, SUM(TongTien) AS DoanhThu FROM HoaDon WHERE NgayLap BETWEEN @From AND @To GROUP BY NgayLap";
            Dictionary<string, object> parameters = new Dictionary<string, object>
    {
        { "@From", dtpFrom.Value.Date },
        { "@To", dtpTo.Value.Date }
    };
            DataTable dt = db.ExecuteQuery(query, parameters);
            dgvDoanhThu.DataSource = dt;
            MessageBox.Show(query);

            double totalRevenue =(double) dt.AsEnumerable().Sum(row => row.Field<decimal>("DoanhThu"));
            txtDoanhThu.Text = $"Tổng Doanh Thu: {totalRevenue:N0} VNĐ";

            chartDoanhThu.Series[0].Points.Clear();
            foreach (DataRow row in dt.Rows)
            {
                chartDoanhThu.Series[0].Points.AddXY(row["NgayLap"].ToString(), Convert.ToDouble(row["DoanhThu"]));
            }
        }

        private void chartDoanhThu_Click(object sender, EventArgs e)
        {

        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            LoadDoanhThu();
            LoadBestSeller();


        }

    }
}
