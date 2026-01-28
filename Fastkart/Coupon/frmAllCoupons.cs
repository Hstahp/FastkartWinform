using BLL; // Nhớ check lại namespace BLL của em
using Common;
using DTO; // Nhớ check lại namespace DTO của em
using System;
using System.Drawing;
using System.Windows.Forms;

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
            dgvCoupons.DataError += dgvCoupons_DataError;

            this.Load += frmAllCoupons_Load;
        }

        private void frmAllCoupons_Load(object sender, EventArgs e)
        {
            // ✅ Kiểm tra quyền VIEW
            if (!UserSessionDTO.HasPermission(PermCode.FUNC_COUPON, PermCode.TYPE_VIEW))
            {
                MessageBox.Show("You do not have permission to access Coupon Management!",
                    "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
                return;
            }

            // ✅ Kiểm tra quyền CREATE (ẩn nút Add BÊN TRONG form này)
            if (!UserSessionDTO.HasPermission(PermCode.FUNC_COUPON, PermCode.TYPE_CREATE))
            {
                if (btnAdd != null) btnAdd.Visible = false;
            }

            LoadData();
        }

        // ✅ VẼ ICON THEO QUYỀN (CHỈ 1 LẦN)
        private void dgvCoupons_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && dgvCoupons.Columns[e.ColumnIndex].Name == "colAction")
            {
                e.PaintBackground(e.CellBounds, true);

                bool canEdit = UserSessionDTO.HasPermission(PermCode.FUNC_COUPON, PermCode.TYPE_EDIT);
                bool canDelete = UserSessionDTO.HasPermission(PermCode.FUNC_COUPON, PermCode.TYPE_DELETE);

                int size = 20;
                int padding = 5;
                int startX = e.CellBounds.X + padding;
                int y = e.CellBounds.Y + (e.CellBounds.Height - size) / 2;

                // Vẽ nút Sửa (Nếu có quyền)
                if (canEdit)
                {
                    e.Graphics.DrawImage(Properties.Resources.edit, new Rectangle(startX, y, size, size));
                }

                // Vẽ nút Xóa (Nếu có quyền)
                if (canDelete)
                {
                    e.Graphics.DrawImage(Properties.Resources.delete, new Rectangle(startX + 30, y, size, size));
                }

                e.Handled = true;
            }
        }

        // ✅ XỬ LÝ CLICK THEO QUYỀN (CHỈ 1 LẦN)
        private void dgvCoupons_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            if (dgvCoupons.Columns[e.ColumnIndex].Name == "colAction")
            {
                if (dgvCoupons.Rows[e.RowIndex].Cells["colId"].Value == null) return;

                bool canEdit = UserSessionDTO.HasPermission(PermCode.FUNC_COUPON, PermCode.TYPE_EDIT);
                bool canDelete = UserSessionDTO.HasPermission(PermCode.FUNC_COUPON, PermCode.TYPE_DELETE);

                int relativeX = e.X - dgvCoupons.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false).Left;
                int iconSize = 20;
                int padding = 5;

                int couponId = Convert.ToInt32(dgvCoupons.Rows[e.RowIndex].Cells["colId"].Value);

                // Click SỬA
                if (canEdit && relativeX >= padding && relativeX < padding + iconSize)
                {
                    RequestEditCoupon?.Invoke(this, couponId);
                }
                // Click XÓA
                else if (canDelete && relativeX >= padding + 30 && relativeX < padding + 30 + iconSize)
                {
                    if (MessageBox.Show("Are you sure you want to delete this coupon?",
                        "Confirm",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        string error;
                        if (_couponBLL.DeleteCoupon(couponId, out error))
                        {
                            MessageBox.Show("Deleted successfully!");
                            LoadData();
                        }
                        else
                        {
                            MessageBox.Show($"Error: {error}");
                        }
                    }
                }
            }
        }

        // ✅ XỬ LÝ LỖI FORMAT
        private void dgvCoupons_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true; // Chặn popup lỗi
        }

        // ✅ LOAD DATA
        public void LoadData(string keyword = "")
        {
            try
            {
                dgvCoupons.DataSource = _couponBLL.GetAllCoupons(keyword);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading data: " + ex.Message, "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
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