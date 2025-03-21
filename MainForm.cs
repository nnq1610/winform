using Karaokelamlai;
using Karaokelamlai.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Karaokelamlai
{
    public partial class MainForm: Form
    {
        private string userRole;
        DbHelper db = new DbHelper();
        private int selectedIDTK = -1;
        private Button currrentButton;
        private Random random;
        private int tempIndex;
        private Form activeForm;
        public MainForm(string userRole)
        {
            InitializeComponent();
            random = new Random();
            this.userRole = userRole;
            btnCLoseChilddForm.Visible = false;

            if (userRole != "Admin")
            {
                btnNhanSu.Visible = false;
                btnTaiKhoan.Visible = false;
            }
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
                    panelTitle.BackColor = color;
                    panelLogo.BackColor = Themecolor.ChangeColorBrightness(color, -0.3);
                    Themecolor.PrimaryColor = color;
                    Themecolor.SecondaryColor = Themecolor.ChangeColorBrightness(color, -0.3);
                    btnCLoseChilddForm.Visible = true;
                }
            }
        }
        private void DisableButton()
        {
            foreach (Control previousBtn in panelMenu.Controls)
            {
                if (previousBtn.GetType() == typeof(Button))
                {
                    previousBtn.BackColor = Color.FromArgb(51, 51, 76);
                    previousBtn.ForeColor = Color.Gainsboro;
                    previousBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                }
            }
        }
        private void OpenChildForm(Form childForm, object btnSender)
        {
            if (activeForm != null)
            {
                activeForm.Close();
            }
            ActivateButton(btnSender);
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            this.panelDestopPane.Controls.Add(childForm);
            this.panelDestopPane.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
            lblTitle.Text = childForm.Text;
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
            
            OpenChildForm(new Forms.formPhongHat(), sender);
        }

        private void btnMatHang_Click(object sender, EventArgs e)
        {
            
           OpenChildForm(new Forms.formMatHang(), sender);
        }

        private void btnKhachHang_Click(object sender, EventArgs e)
        {
            
           OpenChildForm(new Forms.formKhachHang(), sender);
        }

        private void btnDP_Click(object sender, EventArgs e)
        {
           
            OpenChildForm(new Forms.formDatPhong(), sender);
        }

        private void btnHoaDon_Click(object sender, EventArgs e)
        {
            
            OpenChildForm(new Forms.formHoaDon(), sender);
        }

        private void btnNhanSu_Click(object sender, EventArgs e)
        {
            
            OpenChildForm(new Forms.formNhanVien(), sender);
        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            
            OpenChildForm(new Forms.formThongKe(), sender);
        }

        private void btnTaiKhoan_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.formTaiKhoan(), sender);
        }

        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            this.Hide();
            formDangNhap loginForm = new formDangNhap();
            loginForm.ShowDialog();
            this.Show();
        }

        private void btnCLoseChilddForm_Click(object sender, EventArgs e)
        {
            if(activeForm != null)
            {
                activeForm.Close();
            }
            Reset();
        }

        private void Reset()
        {
            DisableButton();
            lblTitle.Text = "Quản Lý Karaoke";
            panelTitle.BackColor = Color.FromArgb(0, 150, 136);
            panelLogo.BackColor = Color.FromArgb(39, 39, 58);
            currrentButton = null;
            btnCLoseChilddForm.Visible = false;
        }

        private void btnHoaDonNhap_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.formHoaDonNhap(), sender);
        }

        private void lblTitle_Click(object sender, EventArgs e)
        {

        }
    }
}
