using System;
using System.Drawing;
using System.Windows.Forms;
using BLL; // Nhớ check lại namespace BLL của em
using DTO; // Nhớ check lại namespace DTO của em

namespace GUI.Coupon
{
    public partial class frmAllCoupons : Form
    {
        private CouponBLL _couponBLL;

        // Sự kiện bắn ra ngoài cho Form Cha
        public event EventHandler RequestAddCoupon;
        public event EventHandler<int> RequestEditCoupon;

        public frmAllCoupons()
        {
            InitializeComponent();
            _couponBLL = new CouponBLL();

            // 1. CẤU HÌNH GRID
            dgvCoupons.AutoGenerateColumns = false;

            // 2. ĐĂNG KÝ SỰ KIỆN
            dgvCoupons.CellMouseClick += dgvCoupons_CellMouseClick;
            dgvCoupons.CellPainting += dgvCoupons_CellPainting;
            dgvCoupons.MouseMove += dgvCoupons_MouseMove;

            // --- QUAN TRỌNG: SỬA LỖI FORMAT EXCEPTION ---
            // Thêm dòng này để xử lý khi dữ liệu null mà cột lại là Image
            dgvCoupons.DataError += dgvCoupons_DataError;

            LoadData();
        }

        // --- HÀM XỬ LÝ LỖI (MỚI THÊM) ---
        private void dgvCoupons_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            // Hàm này chặn cái popup lỗi khó chịu kia lại
            // Nếu lỗi xảy ra ở cột Image (cột Action) hoặc bất kỳ lỗi format nào, ta chỉ cần Cancel nó đi
            e.Cancel = true;
        }

        public void LoadData(string keyword = "")
        {
            try
            {
                _couponBLL = new CouponBLL();
                dgvCoupons.DataSource = _couponBLL.GetAllCoupons(keyword);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message);
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadData(txtSearch.Text.Trim());
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            RequestAddCoupon?.Invoke(this, EventArgs.Empty);
        }

        // --- VẼ 2 ICON ---
        private void dgvCoupons_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && dgvCoupons.Columns[e.ColumnIndex].Name == "colAction")
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.ContentForeground);

                Image imgEdit = GUI.Properties.Resources.icon_edit;
                Image imgDelete = GUI.Properties.Resources.icon_delete;

                int cellWidth = e.CellBounds.Width;
                int cellHeight = e.CellBounds.Height;
                int iconSize = 16;

                // Tính toán vị trí
                int xEdit = e.CellBounds.X + (cellWidth / 4) - (iconSize / 2);
                int yEdit = e.CellBounds.Y + (cellHeight - iconSize) / 2;

                int xDelete = e.CellBounds.X + (3 * cellWidth / 4) - (iconSize / 2);
                int yDelete = e.CellBounds.Y + (cellHeight - iconSize) / 2;

                if (imgEdit != null) e.Graphics.DrawImage(imgEdit, new Rectangle(xEdit, yEdit, iconSize, iconSize));
                if (imgDelete != null) e.Graphics.DrawImage(imgDelete, new Rectangle(xDelete, yDelete, iconSize, iconSize));

                e.Handled = true;
            }
        }

        // --- XỬ LÝ CLICK ---
        private void dgvCoupons_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            if (dgvCoupons.Columns[e.ColumnIndex].Name == "colAction")
            {
                if (dgvCoupons.Rows[e.RowIndex].Cells["colId"].Value == null) return;
                int id = Convert.ToInt32(dgvCoupons.Rows[e.RowIndex].Cells["colId"].Value);

                int cellWidth = dgvCoupons.Columns[e.ColumnIndex].Width;

                if (e.X < cellWidth / 2)
                {
                    // EDIT
                    RequestEditCoupon?.Invoke(this, id);
                }
                else
                {
                    // DELETE
                    DialogResult result = MessageBox.Show(
                        "Bạn có chắc chắn muốn xóa mã giảm giá này không?",
                        "Xác nhận xóa",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning);

                    if (result == DialogResult.Yes)
                    {
                        string error = "";
                        if (_couponBLL.DeleteCoupon(id, out error))
                        {
                            LoadData();
                        }
                        else
                        {
                            MessageBox.Show("Lỗi: " + error, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        private void dgvCoupons_MouseMove(object sender, MouseEventArgs e)
        {
            var hit = dgvCoupons.HitTest(e.X, e.Y);
            if (hit.RowIndex >= 0 && hit.ColumnIndex >= 0)
            {
                if (dgvCoupons.Columns[hit.ColumnIndex].Name == "colAction")
                {
                    dgvCoupons.Cursor = Cursors.Hand;
                    return;
                }
            }
            dgvCoupons.Cursor = Cursors.Default;
        }
    }
}