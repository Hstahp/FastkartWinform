using System;
using System.Drawing;
using System.Windows.Forms;
using BLL; // Thay bằng namespace thực tế của bạn
using DTO; // Thay bằng namespace thực tế của bạn

namespace GUI.Coupon
{
    public partial class frmAllCoupons : Form
    {
        private CouponBLL _couponBLL;

        // Sự kiện bắn ra ngoài cho Form Cha (MainAdmin) bắt
        public event EventHandler RequestAddCoupon;
        public event EventHandler<int> RequestEditCoupon;

        public frmAllCoupons()
        {
            InitializeComponent();
            _couponBLL = new CouponBLL();

            // 1. CẤU HÌNH GRID
            dgvCoupons.AutoGenerateColumns = false; // Chặn tự sinh cột thừa

            // 2. ĐĂNG KÝ SỰ KIỆN HOVER (Để hiện hình bàn tay khi rê vào nút)
            dgvCoupons.CellMouseEnter += dgvCoupons_CellMouseEnter;
            dgvCoupons.CellMouseLeave += dgvCoupons_CellMouseLeave;

            LoadData();
        }

        public void LoadData(string keyword = "")
        {
            try
            {
                // Refresh logic layer
                _couponBLL = new CouponBLL();
                dgvCoupons.DataSource = _couponBLL.GetAllCoupons(keyword);
            }
            catch (Exception ex)
            {
                // Log error if needed
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadData(txtSearch.Text.Trim());
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // Kích hoạt sự kiện thêm mới
            RequestAddCoupon?.Invoke(this, EventArgs.Empty);
        }

        // --- XỬ LÝ CLICK VÀO ICON ---
        private void dgvCoupons_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Bỏ qua nếu click vào Header hoặc vùng trống
            if (e.RowIndex < 0) return;

            // Kiểm tra ID có tồn tại không
            if (dgvCoupons.Rows[e.RowIndex].Cells["colId"].Value == null) return;
            int id = Convert.ToInt32(dgvCoupons.Rows[e.RowIndex].Cells["colId"].Value);

            // Lấy tên cột vừa click
            string colName = dgvCoupons.Columns[e.ColumnIndex].Name;

            // 1. Nút Sửa (Edit)
            if (colName == "colEdit")
            {
                RequestEditCoupon?.Invoke(this, id);
            }
            // 2. Nút Xóa (Delete)
            else if (colName == "colDelete")
            {
                DialogResult result = MessageBox.Show(
                    "Are you sure you want to delete this coupon?",
                    "Confirm Delete",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    string error = "";
                    if (_couponBLL.DeleteCoupon(id, out error))
                    {
                        LoadData(); // Load lại dữ liệu
                        // MessageBox.Show("Deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Error: " + error, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        // --- HIỆU ỨNG CON TRỎ CHUỘT (HAND CURSOR) ---
        private void dgvCoupons_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            // Nếu rê chuột vào vùng dữ liệu của cột Edit hoặc Delete
            if (e.RowIndex >= 0)
            {
                string colName = dgvCoupons.Columns[e.ColumnIndex].Name;
                if (colName == "colEdit" || colName == "colDelete")
                {
                    dgvCoupons.Cursor = Cursors.Hand; // Đổi thành bàn tay
                }
            }
        }

        private void dgvCoupons_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            dgvCoupons.Cursor = Cursors.Default; // Trả về chuột thường
        }
    }
}