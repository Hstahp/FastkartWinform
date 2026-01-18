using BLL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace GUI.Order
{
    public partial class frmPayment : UserControl
    {
        private PaymentBLL paymentBLL = new PaymentBLL();
        private OrderBLL orderBLL = new OrderBLL();

        // Biến lưu trữ ID thanh toán đang được chọn
        private int selectedPaymentUid = -1;

        public frmPayment()
        {
            InitializeComponent();
            InitializeCustomSettings();
        }

        private void InitializeCustomSettings()
        {
            dgvPayments.AutoGenerateColumns = true;
            dgvPayments.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            LoadPaymentData();
        }

        private void LoadPaymentData()
        {
            List<PaymentDTO> payments = paymentBLL.GetPaymentList() ?? new List<PaymentDTO>();
            dgvPayments.DataSource = payments;

            SetGridColumnHeaders();

            grpPaymentInfo.Visible = false;
            grpOrderInfo.Visible = false;
        }

        private void SetGridColumnHeaders()
        {
            if (dgvPayments.Columns == null) return;

            if (dgvPayments.Columns["Uid"] != null) dgvPayments.Columns["Uid"].HeaderText = "Mã Thanh Toán";
            if (dgvPayments.Columns["OrderUid"] != null) dgvPayments.Columns["OrderUid"].HeaderText = "Mã Đơn Hàng";
            if (dgvPayments.Columns["Amount"] != null) dgvPayments.Columns["Amount"].HeaderText = "Số Tiền";
            if (dgvPayments.Columns["PaymentStatus"] != null) dgvPayments.Columns["PaymentStatus"].HeaderText = "Trạng Thái";
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadPaymentData();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            var keyword = txtSearchKeyword.Text?.Trim();
            var list = paymentBLL.GetPaymentList() ?? new List<PaymentDTO>();

            if (string.IsNullOrEmpty(keyword))
            {
                dgvPayments.DataSource = list;
            }
            else
            {
                int id;
                var filtered = list.Where(p =>
                    (int.TryParse(keyword, out id) && (p.Uid == id || p.OrderUid == id)) ||
                    (!string.IsNullOrEmpty(p.BankTransactionCode) && p.BankTransactionCode.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0) ||
                    (!string.IsNullOrEmpty(p.QrPaymentCode) && p.QrPaymentCode.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0) ||
                    (!string.IsNullOrEmpty(p.PaymentStatus) && p.PaymentStatus.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0)
                ).ToList();

                dgvPayments.DataSource = filtered;
            }

            SetGridColumnHeaders();
        }

        private void dgvPayments_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvPayments.SelectedRows.Count > 0)
            {
                var cell = dgvPayments.SelectedRows[0].Cells["Uid"];
                if (cell != null && cell.Value != null && int.TryParse(cell.Value.ToString(), out int uid))
                {
                    selectedPaymentUid = uid;
                    DisplayPaymentDetails(selectedPaymentUid);

                    grpPaymentInfo.Visible = true;
                    grpOrderInfo.Visible = true;
                    grpPaymentInfo.BringToFront();
                }
            }
        }

        private void DisplayPaymentDetails(int paymentUid)
        {
            var dto = paymentBLL.GetPaymentList().FirstOrDefault(p => p.Uid == paymentUid);
            if (dto == null) return;

            txtPaymentID.Text = dto.Uid.ToString();
            txtAmount.Text = dto.Amount.ToString("N0");
            txtPaymentStatus.Text = dto.PaymentStatus ?? "";
            txtTransactionDate.Text = dto.TransactionDate?.ToString("g") ?? "";
            txtBankTransactionCode.Text = dto.BankTransactionCode ?? "";

            txtOrderUid.Text = dto.OrderUid.ToString();
            var orderTotal = orderBLL.GetOrderTotalAmount(dto.OrderUid);
            txtOrderTotal.Text = orderTotal.HasValue ? orderTotal.Value.ToString("N0") : "";
            txtOrderStatus.Text = orderBLL.GetOrderStatus(dto.OrderUid) ?? "";
        }

        private void btnMarkPaid_Click(object sender, EventArgs e)
        {
            if (selectedPaymentUid > 0)
            {
                if (paymentBLL.ProcessPaymentSuccess(selectedPaymentUid))
                {
                    MessageBox.Show("Cập nhật trạng thái thành 'Đã thanh toán' thành công.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadPaymentData();
                }
                else
                {
                    MessageBox.Show("Không thể cập nhật trạng thái thanh toán.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnViewOrder_Click(object sender, EventArgs e)
        {
            if (selectedPaymentUid <= 0) return;

            var dto = paymentBLL.GetPaymentList().FirstOrDefault(p => p.Uid == selectedPaymentUid);
            if (dto == null) return;

            var total = orderBLL.GetOrderTotalAmount(dto.OrderUid);
            MessageBox.Show($"Mã đơn: {dto.OrderUid}\nTổng tiền: {(total.HasValue ? total.Value.ToString("N0") : "N/A")}", "Chi tiết đơn hàng", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
