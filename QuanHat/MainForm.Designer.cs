namespace QuanHat
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.quanLyKaraokeDataSet1 = new QuanHat.QuanLyKaraokeDataSet();
            this.btnPhongHat = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.panelMenuu = new System.Windows.Forms.Panel();
            this.btnTaiKhoan = new System.Windows.Forms.Button();
            this.btnThongKe = new System.Windows.Forms.Button();
            this.btnNhanSu = new System.Windows.Forms.Button();
            this.btnHoaDon = new System.Windows.Forms.Button();
            this.btnDP = new System.Windows.Forms.Button();
            this.btnKhachHang = new System.Windows.Forms.Button();
            this.btnMatHang = new System.Windows.Forms.Button();
            this.panelTitleBar = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.quanLyKaraokeDataSet1)).BeginInit();
            this.panelMenuu.SuspendLayout();
            this.panelTitleBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // quanLyKaraokeDataSet1
            // 
            this.quanLyKaraokeDataSet1.DataSetName = "QuanLyKaraokeDataSet";
            this.quanLyKaraokeDataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // btnPhongHat
            // 
            this.btnPhongHat.BackColor = System.Drawing.Color.Transparent;
            this.btnPhongHat.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnPhongHat.FlatAppearance.BorderSize = 0;
            this.btnPhongHat.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnPhongHat.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnPhongHat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPhongHat.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnPhongHat.Image = ((System.Drawing.Image)(resources.GetObject("btnPhongHat.Image")));
            this.btnPhongHat.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPhongHat.Location = new System.Drawing.Point(0, 0);
            this.btnPhongHat.Name = "btnPhongHat";
            this.btnPhongHat.Size = new System.Drawing.Size(217, 60);
            this.btnPhongHat.TabIndex = 1;
            this.btnPhongHat.Text = "Phòng Hát";
            this.btnPhongHat.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPhongHat.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnPhongHat.UseVisualStyleBackColor = false;
            this.btnPhongHat.Click += new System.EventHandler(this.btnPhongHat_Click);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.Transparent;
            this.button3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.button3.FlatAppearance.BorderSize = 0;
            this.button3.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Red;
            this.button3.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.ForeColor = System.Drawing.Color.Gainsboro;
            this.button3.Image = ((System.Drawing.Image)(resources.GetObject("button3.Image")));
            this.button3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button3.Location = new System.Drawing.Point(0, 508);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(217, 53);
            this.button3.TabIndex = 4;
            this.button3.Text = "Đăng Xuất";
            this.button3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button3.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // panelMenuu
            // 
            this.panelMenuu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(76)))));
            this.panelMenuu.Controls.Add(this.btnTaiKhoan);
            this.panelMenuu.Controls.Add(this.btnThongKe);
            this.panelMenuu.Controls.Add(this.button3);
            this.panelMenuu.Controls.Add(this.btnNhanSu);
            this.panelMenuu.Controls.Add(this.btnHoaDon);
            this.panelMenuu.Controls.Add(this.btnDP);
            this.panelMenuu.Controls.Add(this.btnKhachHang);
            this.panelMenuu.Controls.Add(this.btnMatHang);
            this.panelMenuu.Controls.Add(this.btnPhongHat);
            this.panelMenuu.Location = new System.Drawing.Point(1, 0);
            this.panelMenuu.Name = "panelMenuu";
            this.panelMenuu.Size = new System.Drawing.Size(217, 561);
            this.panelMenuu.TabIndex = 1;
            // 
            // btnTaiKhoan
            // 
            this.btnTaiKhoan.BackColor = System.Drawing.Color.Transparent;
            this.btnTaiKhoan.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnTaiKhoan.FlatAppearance.BorderSize = 0;
            this.btnTaiKhoan.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnTaiKhoan.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnTaiKhoan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTaiKhoan.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnTaiKhoan.Image = ((System.Drawing.Image)(resources.GetObject("btnTaiKhoan.Image")));
            this.btnTaiKhoan.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnTaiKhoan.Location = new System.Drawing.Point(0, 420);
            this.btnTaiKhoan.Name = "btnTaiKhoan";
            this.btnTaiKhoan.Size = new System.Drawing.Size(217, 60);
            this.btnTaiKhoan.TabIndex = 13;
            this.btnTaiKhoan.Text = "Tài Khoản";
            this.btnTaiKhoan.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnTaiKhoan.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnTaiKhoan.UseVisualStyleBackColor = false;
            // 
            // btnThongKe
            // 
            this.btnThongKe.BackColor = System.Drawing.Color.Transparent;
            this.btnThongKe.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnThongKe.FlatAppearance.BorderSize = 0;
            this.btnThongKe.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnThongKe.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnThongKe.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThongKe.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnThongKe.Image = ((System.Drawing.Image)(resources.GetObject("btnThongKe.Image")));
            this.btnThongKe.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnThongKe.Location = new System.Drawing.Point(0, 360);
            this.btnThongKe.Name = "btnThongKe";
            this.btnThongKe.Size = new System.Drawing.Size(217, 60);
            this.btnThongKe.TabIndex = 12;
            this.btnThongKe.Text = "Thống Kê";
            this.btnThongKe.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnThongKe.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnThongKe.UseVisualStyleBackColor = false;
            this.btnThongKe.Click += new System.EventHandler(this.btnThongKe_Click);
            // 
            // btnNhanSu
            // 
            this.btnNhanSu.BackColor = System.Drawing.Color.Transparent;
            this.btnNhanSu.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnNhanSu.FlatAppearance.BorderSize = 0;
            this.btnNhanSu.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnNhanSu.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnNhanSu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNhanSu.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnNhanSu.Image = ((System.Drawing.Image)(resources.GetObject("btnNhanSu.Image")));
            this.btnNhanSu.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNhanSu.Location = new System.Drawing.Point(0, 300);
            this.btnNhanSu.Name = "btnNhanSu";
            this.btnNhanSu.Size = new System.Drawing.Size(217, 60);
            this.btnNhanSu.TabIndex = 11;
            this.btnNhanSu.Text = "Nhân Viên";
            this.btnNhanSu.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNhanSu.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnNhanSu.UseVisualStyleBackColor = false;
            this.btnNhanSu.Click += new System.EventHandler(this.btnNhanSu_Click);
            // 
            // btnHoaDon
            // 
            this.btnHoaDon.BackColor = System.Drawing.Color.Transparent;
            this.btnHoaDon.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnHoaDon.FlatAppearance.BorderSize = 0;
            this.btnHoaDon.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnHoaDon.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnHoaDon.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHoaDon.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnHoaDon.Image = ((System.Drawing.Image)(resources.GetObject("btnHoaDon.Image")));
            this.btnHoaDon.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnHoaDon.Location = new System.Drawing.Point(0, 240);
            this.btnHoaDon.Name = "btnHoaDon";
            this.btnHoaDon.Size = new System.Drawing.Size(217, 60);
            this.btnHoaDon.TabIndex = 10;
            this.btnHoaDon.Text = "Hóa Đơn";
            this.btnHoaDon.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnHoaDon.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnHoaDon.UseVisualStyleBackColor = false;
            this.btnHoaDon.Click += new System.EventHandler(this.btnHoaDon_Click);
            // 
            // btnDP
            // 
            this.btnDP.BackColor = System.Drawing.Color.Transparent;
            this.btnDP.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnDP.FlatAppearance.BorderSize = 0;
            this.btnDP.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnDP.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnDP.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDP.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnDP.Image = ((System.Drawing.Image)(resources.GetObject("btnDP.Image")));
            this.btnDP.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDP.Location = new System.Drawing.Point(0, 180);
            this.btnDP.Name = "btnDP";
            this.btnDP.Size = new System.Drawing.Size(217, 60);
            this.btnDP.TabIndex = 9;
            this.btnDP.Text = "Đặt Phòng";
            this.btnDP.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDP.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnDP.UseVisualStyleBackColor = false;
            this.btnDP.Click += new System.EventHandler(this.btnDP_Click);
            // 
            // btnKhachHang
            // 
            this.btnKhachHang.BackColor = System.Drawing.Color.Transparent;
            this.btnKhachHang.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnKhachHang.FlatAppearance.BorderSize = 0;
            this.btnKhachHang.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnKhachHang.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnKhachHang.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnKhachHang.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnKhachHang.Image = ((System.Drawing.Image)(resources.GetObject("btnKhachHang.Image")));
            this.btnKhachHang.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnKhachHang.Location = new System.Drawing.Point(0, 120);
            this.btnKhachHang.Name = "btnKhachHang";
            this.btnKhachHang.Size = new System.Drawing.Size(217, 60);
            this.btnKhachHang.TabIndex = 7;
            this.btnKhachHang.Text = "Khách Hàng";
            this.btnKhachHang.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnKhachHang.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnKhachHang.UseVisualStyleBackColor = false;
            this.btnKhachHang.Click += new System.EventHandler(this.btnKhachHang_Click);
            // 
            // btnMatHang
            // 
            this.btnMatHang.BackColor = System.Drawing.Color.Transparent;
            this.btnMatHang.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnMatHang.FlatAppearance.BorderSize = 0;
            this.btnMatHang.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnMatHang.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnMatHang.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMatHang.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnMatHang.Image = ((System.Drawing.Image)(resources.GetObject("btnMatHang.Image")));
            this.btnMatHang.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMatHang.Location = new System.Drawing.Point(0, 60);
            this.btnMatHang.Name = "btnMatHang";
            this.btnMatHang.Size = new System.Drawing.Size(217, 60);
            this.btnMatHang.TabIndex = 6;
            this.btnMatHang.Text = "Mặt Hàng";
            this.btnMatHang.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMatHang.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnMatHang.UseVisualStyleBackColor = false;
            this.btnMatHang.Click += new System.EventHandler(this.btnMatHang_Click);
            // 
            // panelTitleBar
            // 
            this.panelTitleBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(150)))), ((int)(((byte)(136)))));
            this.panelTitleBar.Controls.Add(this.lblTitle);
            this.panelTitleBar.Location = new System.Drawing.Point(217, 0);
            this.panelTitleBar.Name = "panelTitleBar";
            this.panelTitleBar.Size = new System.Drawing.Size(631, 60);
            this.panelTitleBar.TabIndex = 2;
            // 
            // lblTitle
            // 
            this.lblTitle.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(210, 9);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(216, 37);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Quản Lý Karaoke";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(848, 562);
            this.Controls.Add(this.panelTitleBar);
            this.Controls.Add(this.panelMenuu);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)(this.quanLyKaraokeDataSet1)).EndInit();
            this.panelMenuu.ResumeLayout(false);
            this.panelTitleBar.ResumeLayout(false);
            this.panelTitleBar.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private QuanLyKaraokeDataSet quanLyKaraokeDataSet1;
        private System.Windows.Forms.Button btnPhongHat;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Panel panelMenuu;
        private System.Windows.Forms.Button btnNhanSu;
        private System.Windows.Forms.Button btnHoaDon;
        private System.Windows.Forms.Button btnDP;
        private System.Windows.Forms.Button btnKhachHang;
        private System.Windows.Forms.Button btnMatHang;
        private System.Windows.Forms.Button btnThongKe;
        private System.Windows.Forms.Button btnTaiKhoan;
        private System.Windows.Forms.Panel panelTitleBar;
        private System.Windows.Forms.Label lblTitle;
    }
}

