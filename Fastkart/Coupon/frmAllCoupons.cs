using System;
using System.Drawing;
using System.Windows.Forms;
using BLL;
using DTO;

namespace GUI.Coupon
{
    public partial class frmAllCoupons : Form
    {
        private CouponBLL _couponBLL;

        // Sự kiện bắn ra ngoài cho frmMainAdmin bắt
        public event EventHandler RequestAddCoupon;
        public event EventHandler<int> RequestEditCoupon;

        public frmAllCoupons()
        {
            InitializeComponent(); // Bắt buộc phải có
            _couponBLL = new CouponBLL();

            SetupCustomGrid(); // Trang trí thêm cho Grid
            LoadData();
        }

        private void SetupCustomGrid()
        {
            // Tinh chỉnh DataGridView cho đẹp (Code bổ sung cho Designer)
            dgvCoupons.BackgroundColor = Color.White;
            dgvCoupons.BorderStyle = BorderStyle.None;
            dgvCoupons.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvCoupons.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvCoupons.EnableHeadersVisualStyles = false;

            dgvCoupons.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(37, 99, 235); // Blue
            dgvCoupons.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvCoupons.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvCoupons.ColumnHeadersHeight = 45;

            dgvCoupons.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dgvCoupons.DefaultCellStyle.SelectionBackColor = Color.FromArgb(239, 246, 255);
            dgvCoupons.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgvCoupons.RowTemplate.Height = 40;
            dgvCoupons.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            // QUAN TRỌNG: Chặn tự sinh cột rác
            dgvCoupons.AutoGenerateColumns = false;
        }

        public void LoadData(string keyword = "")
        {
            try
            {
                _couponBLL = new CouponBLL();
                dgvCoupons.DataSource = _couponBLL.GetAllCoupons(keyword);
            }
            catch { }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadData(txtSearch.Text.Trim());
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            RequestAddCoupon?.Invoke(this, EventArgs.Empty);
        }

        private void dgvCoupons_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            // Lấy ID (Đảm bảo cột colId tồn tại)
            if (dgvCoupons.Rows[e.RowIndex].Cells["colId"].Value == null) return;
            int id = Convert.ToInt32(dgvCoupons.Rows[e.RowIndex].Cells["colId"].Value);

            // Xử lý nút Sửa (Tên cột là colEdit)
            if (dgvCoupons.Columns[e.ColumnIndex].Name == "colEdit")
            {
                RequestEditCoupon?.Invoke(this, id);
            }
            // Xử lý nút Xóa (Tên cột là colDelete)
            else if (dgvCoupons.Columns[e.ColumnIndex].Name == "colDelete")
            {
                if (MessageBox.Show("Xóa mã này?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    string error = "";
                    if (_couponBLL.DeleteCoupon(id, out error)) LoadData();
                    else MessageBox.Show(error);
                }
            }
        }
    }
}