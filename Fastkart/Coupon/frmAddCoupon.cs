using System;
using System.Drawing;
using System.Windows.Forms;
using BLL;
using DTO;

namespace GUI.Coupon
{
    public partial class frmAddCoupon : Form
    {
        // 1. DECLARE CONTROLS
        private TextBox txtCode, txtDesc;
        private ComboBox cboType;
        private NumericUpDown numValue, numMinOrder, numLimit;
        private DateTimePicker dtpStart, dtpEnd;
        private Button btnSave, btnCancel;
        private Label lblTitle;

        // 2. LOGIC
        private CouponBLL _couponBLL;
        private int _uid = 0; // 0 = Add New, >0 = Edit

        public frmAddCoupon(int id = 0)
        {
            _couponBLL = new CouponBLL();
            _uid = id;

            // Initialize Custom UI
            InitializeCustomUI();

            // If Edit Mode -> Load old data
            if (_uid > 0)
            {
                lblTitle.Text = "UPDATE COUPON";
                LoadDataForEdit();
            }
        }

        // --- CUSTOM UI INITIALIZATION (No Designer needed) ---
        private void InitializeCustomUI()
        {
            // Form Settings
            this.Size = new Size(500, 650);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = Color.White;
            this.Text = _uid == 0 ? "Add New Coupon" : "Update Coupon";

            // Title Label
            lblTitle = new Label
            {
                Text = "ADD NEW COUPON",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.FromArgb(37, 99, 235), // Blue
                AutoSize = true,
                Top = 20,
                Left = 140
            };
            this.Controls.Add(lblTitle);

            // Layout Coordinates
            int y = 70;
            int labelX = 40;
            int inputX = 160;
            int inputW = 280;
            int gap = 50;

            // Helper function to add labels quickly
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

            // 1. Coupon Code
            AddLabel("Coupon Code:", y);
            txtCode = new TextBox { Top = y, Left = inputX, Width = inputW, Font = new Font("Segoe UI", 10), CharacterCasing = CharacterCasing.Upper };
            this.Controls.Add(txtCode); y += gap;

            // 2. Description
            AddLabel("Description:", y);
            txtDesc = new TextBox { Top = y, Left = inputX, Width = inputW, Font = new Font("Segoe UI", 10) };
            this.Controls.Add(txtDesc); y += gap;

            // 3. Discount Type
            AddLabel("Discount Type:", y);
            cboType = new ComboBox { Top = y, Left = inputX, Width = inputW, Font = new Font("Segoe UI", 10), DropDownStyle = ComboBoxStyle.DropDownList };
            cboType.Items.AddRange(new object[] { "Percentage (%)", "Fixed Amount (VND)" });
            cboType.SelectedIndex = 0;
            this.Controls.Add(cboType); y += gap;

            // 4. Discount Value
            AddLabel("Discount Value:", y);
            numValue = new NumericUpDown { Top = y, Left = inputX, Width = inputW, Font = new Font("Segoe UI", 10), Maximum = 1000000000, ThousandsSeparator = true };
            this.Controls.Add(numValue); y += gap;

            // 5. Min Order Value
            AddLabel("Min Order Value:", y);
            numMinOrder = new NumericUpDown { Top = y, Left = inputX, Width = inputW, Font = new Font("Segoe UI", 10), Maximum = 1000000000, ThousandsSeparator = true };
            this.Controls.Add(numMinOrder); y += gap;

            // 6. Usage Limit
            AddLabel("Usage Limit:", y);
            numLimit = new NumericUpDown { Top = y, Left = inputX, Width = inputW, Font = new Font("Segoe UI", 10), Maximum = 10000 };
            this.Controls.Add(numLimit); y += gap;

            // 7. Start Date
            AddLabel("Start Date:", y);
            dtpStart = new DateTimePicker { Top = y, Left = inputX, Width = inputW, Font = new Font("Segoe UI", 10), Format = DateTimePickerFormat.Short };
            this.Controls.Add(dtpStart); y += gap;

            // 8. End Date
            AddLabel("End Date:", y);
            dtpEnd = new DateTimePicker { Top = y, Left = inputX, Width = inputW, Font = new Font("Segoe UI", 10), Format = DateTimePickerFormat.Short };
            dtpEnd.Value = DateTime.Now.AddDays(30); // Default +30 days
            this.Controls.Add(dtpEnd); y += 70;

            // --- BUTTONS ---
            // Save Button (Blue)
            btnSave = new Button
            {
                Text = "SAVE",
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

            // Cancel Button (Gray)
            btnCancel = new Button
            {
                Text = "CANCEL",
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

        // --- LOAD DATA FOR EDIT ---
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

                txtCode.Enabled = false; // Prevent editing Code in Edit mode
            }
        }

        // --- SAVE EVENT ---
        private void BtnSave_Click(object sender, EventArgs e)
        {
            // Simple Validation
            if (string.IsNullOrWhiteSpace(txtCode.Text)) { MessageBox.Show("Please enter Coupon Code!", "Warning"); return; }
            if (dtpStart.Value > dtpEnd.Value) { MessageBox.Show("End Date must be after Start Date!", "Warning"); return; }

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
                MessageBox.Show("Saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK; // Notify parent form to reload
                this.Close();
            }
            else
            {
                MessageBox.Show("Error: " + error, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}