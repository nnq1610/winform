using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace QuanHat
{
    public partial class MainForm : Form
    {
        private string userRole;
        DbHelper db = new DbHelper();
        private int selectedIDTK = -1;
        private Button currrentButton;
        private Random random;
        private int tempIndex;

        public MainForm(string userRole)
        {
            InitializeComponent();
            this.userRole = userRole;
           
            random = new Random();
        }

        private Color SelectThemeColor()
        {
            int index = random.Next(Themecolor.colorList.Count);
            while (tempIndex == index)
            {
                index = random.Next(Themecolor.colorList.Count);
            }
            tempIndex = index;
            string color = Themecolor.colorList[index];
            return ColorTranslator.FromHtml(color);
        }
        private void ActivateButton(object btnSender)
        {
            if (btnSender != null)
            {
                if (currrentButton != (Button)btnSender)
                {
                    DisableButton();
                    Color color = SelectThemeColor();
                    currrentButton = (Button)btnSender;
                    currrentButton.BackColor = color;
                    currrentButton.ForeColor = Color.White;
                    currrentButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    panelTitleBar.BackColor = color;
                    //panelLogo.BackColor = Themecolor.ChangeColorBrightness(color, -0.3);
                }
            }
        }
        private void DisableButton()
        {
            foreach (Control previousBtn in panelMenuu.Controls)
            {
                if (previousBtn.GetType() == typeof(Button))
                {
                    previousBtn.BackColor = Color.FromArgb(51, 51, 76);
                    previousBtn.ForeColor = Color.Gainsboro;
                    previousBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                }
            }
        }
        
       
        private bool IsTenDangNhapExists(string tenDangNhap)
        {
            string query = "SELECT COUNT(*) FROM TaiKhoan WHERE TenDangNhap = @TenDangNhap";
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@TenDangNhap", tenDangNhap }
            };
            int count = Convert.ToInt32(db.ExecuteScalar(query, parameters));
            return count > 0;
        }

       

       

       

        private void button9_Click(object sender, EventArgs e) // Delete account
        {
            if (selectedIDTK == -1)
            {
                MessageBox.Show("Vui lòng chọn tài khoản cần xoá!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xoá tài khoản này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.No)
                return;

            string query = "DELETE FROM TaiKhoan WHERE IDTaiKhoan = @ID";
            Dictionary<string, object> parameters = new Dictionary<string, object> { { "@ID", selectedIDTK } };

            if (db.ExecuteNonQuery(query, parameters) > 0)
            {
                MessageBox.Show("Xóa tài khoản thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
               
            }
            else
            {
                MessageBox.Show("Xóa thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            formDangNhap loginForm = new formDangNhap();
            loginForm.ShowDialog();
            this.Show();
        }

     

        private void btnPhongHat_Click(object sender, EventArgs e)
        {
            formPhongHat ph = new formPhongHat();
            ph.Show();
            ActivateButton(sender);
        }

        private void btnMatHang_Click(object sender, EventArgs e)
        {
            formMatHang mh = new formMatHang();
            mh.Show();
            ActivateButton(sender);
        }

        private void btnKhachHang_Click(object sender, EventArgs e)
        {
            FormKhachHang kh = new FormKhachHang();
            kh.Show();
            ActivateButton(sender);
        }

        private void btnDP_Click(object sender, EventArgs e)
        {
            FormDatPhong dp = new FormDatPhong();
            dp.Show();
            ActivateButton(sender);
        }

        private void btnHoaDon_Click(object sender, EventArgs e)
        {
            formHoaDon hd = new formHoaDon();
            hd.Show();
            ActivateButton(sender);
        }

        private void btnNhanSu_Click(object sender, EventArgs e)
        {
            formNhanVien nv = new formNhanVien();
            nv.Show();
            ActivateButton(sender);
        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            formThongKe tk = new formThongKe();
            tk.Show();
            ActivateButton(sender);
        }

        

        
    }
}