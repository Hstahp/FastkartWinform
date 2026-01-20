using System;
using System.Drawing;
using System.Windows.Forms;
using BLL;
using DTO;

namespace GUI.Coupon
{
    public partial class frmAddCoupon : Form
    {
        // 1. KHAI BÁO CONTROL
        private TextBox txtCode, txtDesc;
        private ComboBox cboType;
        private NumericUpDown numValue, numMinOrder, numLimit;
        private DateTimePicker dtpStart, dtpEnd;
        private Button btnSave, btnCancel;
        private Label lblTitle;

        // 2. LOGIC
        private CouponBLL _couponBLL;
        private int _uid = 0; // 0 = Thêm mới, >0 = Sửa

        public frmAddCoupon(int id = 0)
        {
            _couponBLL = new CouponBLL();
            _uid = id;

            // Tự vẽ giao diện
            InitializeCustomUI();

            // Nếu là Sửa -> Load dữ liệu cũ lên
            if (_uid > 0)
            {
                lblTitle.Text = "CẬP NHẬT MÃ GIẢM GIÁ";
                LoadDataForEdit();
            }
        }

        // --- HÀM TỰ VẼ GIAO DIỆN (Không cần Designer) ---
        private void InitializeCustomUI()
        {
            // Cài đặt Form
            this.Size = new Size(500, 650);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = Color.White;
            this.Text = _uid == 0 ? "Thêm Mã Mới" : "Cập Nhật Mã";

            // Title
            lblTitle = new Label
            {
                Text = "THÊM MÃ KHUYẾN MÃI",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.FromArgb(37, 99, 235), // Xanh dương
                AutoSize = true,
                Top = 20,
                Left = 140
            };
            this.Controls.Add(lblTitle);

            // Các biến toạ độ để căn chỉnh
            int y = 70;
            int labelX = 40;
            int inputX = 160;
            int inputW = 280;
            int gap = 50;

            // Hàm hỗ trợ vẽ nhanh Label
            void AddLabel(string text, int top)
            {
                this.Controls.Add(new Label
                {
                    Text = text,
                    Top = top + 3,
                    Left = labelX,
                    Font = new Font("Segoe UI", 10, FontStyle.Bold),
                    AutoSize = true,
                    ForeColor = Color.FromArgb(55, 65, 81)
                });
            }

            // 1. Mã Code
            AddLabel("Mã Coupon:", y);
            txtCode = new TextBox { Top = y, Left = inputX, Width = inputW, Font = new Font("Segoe UI", 10), CharacterCasing = CharacterCasing.Upper };
            this.Controls.Add(txtCode); y += gap;

            // 2. Mô tả
            AddLabel("Mô tả ngắn:", y);
            txtDesc = new TextBox { Top = y, Left = inputX, Width = inputW, Font = new Font("Segoe UI", 10) };
            this.Controls.Add(txtDesc); y += gap;

            // 3. Loại giảm giá
            AddLabel("Loại giảm:", y);
            cboType = new ComboBox { Top = y, Left = inputX, Width = inputW, Font = new Font("Segoe UI", 10), DropDownStyle = ComboBoxStyle.DropDownList };
            cboType.Items.AddRange(new object[] { "Phần trăm (%)", "Tiền mặt (VND)" });
            cboType.SelectedIndex = 0;
            this.Controls.Add(cboType); y += gap;

            // 4. Giá trị giảm
            AddLabel("Giá trị giảm:", y);
            numValue = new NumericUpDown { Top = y, Left = inputX, Width = inputW, Font = new Font("Segoe UI", 10), Maximum = 1000000000, ThousandsSeparator = true };
            this.Controls.Add(numValue); y += gap;

            // 5. Đơn tối thiểu
            AddLabel("Đơn tối thiểu:", y);
            numMinOrder = new NumericUpDown { Top = y, Left = inputX, Width = inputW, Font = new Font("Segoe UI", 10), Maximum = 1000000000, ThousandsSeparator = true };
            this.Controls.Add(numMinOrder); y += gap;

            // 6. Giới hạn số lượng
            AddLabel("Số lượng phát:", y);
            numLimit = new NumericUpDown { Top = y, Left = inputX, Width = inputW, Font = new Font("Segoe UI", 10), Maximum = 10000 };
            this.Controls.Add(numLimit); y += gap;

            // 7. Ngày bắt đầu
            AddLabel("Ngày bắt đầu:", y);
            dtpStart = new DateTimePicker { Top = y, Left = inputX, Width = inputW, Font = new Font("Segoe UI", 10), Format = DateTimePickerFormat.Short };
            this.Controls.Add(dtpStart); y += gap;

            // 8. Ngày kết thúc
            AddLabel("Ngày kết thúc:", y);
            dtpEnd = new DateTimePicker { Top = y, Left = inputX, Width = inputW, Font = new Font("Segoe UI", 10), Format = DateTimePickerFormat.Short };
            dtpEnd.Value = DateTime.Now.AddDays(30); // Mặc định +30 ngày
            this.Controls.Add(dtpEnd); y += 70;

            // --- NÚT BẤM ---
            // Nút Lưu (Xanh dương)
            btnSave = new Button
            {
                Text = "LƯU MÃ",
                Top = y,
                Left = 160,
                Width = 110,
                Height = 45,
                BackColor = Color.FromArgb(37, 99, 235),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.Click += BtnSave_Click;

            // Nút Hủy (Xám)
            btnCancel = new Button
            {
                Text = "HỦY",
                Top = y,
                Left = 280,
                Width = 110,
                Height = 45,
                BackColor = Color.FromArgb(229, 231, 235),
                ForeColor = Color.Black,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.Click += (s, e) => this.Close();

            this.Controls.Add(btnSave);
            this.Controls.Add(btnCancel);
        }

        // --- LOGIC LOAD DỮ LIỆU ĐỂ SỬA ---
        private void LoadDataForEdit()
        {
            var coupon = _couponBLL.GetCouponById(_uid);
            if (coupon != null)
            {
                txtCode.Text = coupon.Code;
                txtDesc.Text = coupon.Description;
                cboType.SelectedIndex = coupon.DiscountType - 1;
                numValue.Value = coupon.DiscountValue;
                numMinOrder.Value = coupon.MinOrderValue;
                numLimit.Value = coupon.UsageLimit;
                dtpStart.Value = coupon.StartDate;
                dtpEnd.Value = coupon.EndDate;

                txtCode.Enabled = false; // Không cho sửa mã Code khi đang Edit
            }
        }

        // --- SỰ KIỆN LƯU ---
        private void BtnSave_Click(object sender, EventArgs e)
        {
            // Validate đơn giản
            if (string.IsNullOrWhiteSpace(txtCode.Text)) { MessageBox.Show("Vui lòng nhập Mã Coupon!", "Cảnh báo"); return; }
            if (dtpStart.Value > dtpEnd.Value) { MessageBox.Show("Ngày kết thúc phải lớn hơn ngày bắt đầu!", "Cảnh báo"); return; }

            string error = "";
            CouponDTO dto = new CouponDTO
            {
                Uid = _uid,
                Code = txtCode.Text.Trim().ToUpper(),
                Description = txtDesc.Text,
                DiscountType = cboType.SelectedIndex + 1,
                DiscountValue = numValue.Value,
                MinOrderValue = numMinOrder.Value,
                UsageLimit = (int)numLimit.Value,
                StartDate = dtpStart.Value,
                EndDate = dtpEnd.Value,
                IsActive = true
            };

            bool result = (_uid == 0) ? _couponBLL.AddCoupon(dto, out error) : _couponBLL.UpdateCoupon(dto, out error);

            if (result)
            {
                MessageBox.Show("Lưu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK; // Báo cho Form cha biết để reload
                this.Close();
            }
            else
            {
                MessageBox.Show("Lỗi: " + error, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}