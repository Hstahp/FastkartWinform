using Helpers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using DTO;

namespace GUI.Payment
{
    public partial class frmMoMoWaitPayment : Form
    {
        private int _orderId;
        private int _paymentUid;
        private decimal _amount;
        private string _requestId;
        private List<OrderItemDTO> _orderItems;
        private Timer _pollingTimer;
        private int _elapsedSeconds = 0;
        private const int TIMEOUT_SECONDS = 300; // 5 phút
        private bool _isFormClosing = false;

        public bool PaymentSuccess { get; private set; }

        public frmMoMoWaitPayment(int orderId, int paymentUid, decimal amount, string requestId, List<OrderItemDTO> orderItems)
        {
            InitializeComponent();
            _orderId = orderId;
            _paymentUid = paymentUid;
            _amount = amount;
            _requestId = requestId;
            _orderItems = orderItems;
        }

        private void frmMoMoWaitPayment_Load(object sender, EventArgs e)
        {
            // Hiển thị số tiền
            lblAmount.Text = $"Amount of money: {_amount:N0} VNĐ";

            // Gán sự kiện cho button Cancel
            btnCancel.Click += BtnCancel_Click;

            // Bắt đầu polling
            StartPolling();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            _isFormClosing = true;
            PaymentSuccess = false;
            this.DialogResult = DialogResult.Cancel;
        }

        private void StartPolling()
        {
            _pollingTimer = new Timer();
            _pollingTimer.Interval = 3000; // 3 giây
            _pollingTimer.Tick += async (s, e) =>
            {
                if (_isFormClosing || this.IsDisposed)
                {
                    _pollingTimer.Stop();
                    return;
                }

                _elapsedSeconds += 3;
                int remaining = TIMEOUT_SECONDS - _elapsedSeconds;

                // Update timer label
                if (lblTimer != null && !lblTimer.IsDisposed)
                {
                    lblTimer.Text = $"Remaining time: {TimeSpan.FromSeconds(remaining):mm\\:ss}";
                }

                // Timeout
                if (_elapsedSeconds >= TIMEOUT_SECONDS)
                {
                    _pollingTimer.Stop();
                    if (!_isFormClosing)
                    {
                        MessageBox.Show("Payment waiting time has expired!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        this.DialogResult = DialogResult.Cancel;
                    }
                    return;
                }

                // Check payment status
                bool isPaid = await CheckPaymentStatusAsync();
                if (isPaid && !_isFormClosing)
                {
                    _pollingTimer.Stop();
                    PaymentSuccess = true;
                    MessageBox.Show("✅ Payment successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                }
            };
            _pollingTimer.Start();
        }

        private async Task<bool> CheckPaymentStatusAsync()
        {
            try
            {
                string orderId_str = $"ORDER{_orderId}";

                var queryResponse = await MoMoPaymentHelper.QueryTransactionStatusAsync(orderId_str, _requestId);

                System.Diagnostics.Debug.WriteLine($"[MoMo Query] Result Code: {queryResponse.resultCode}");

                // resultCode:
                // 0 = Thanh toán thành công
                // 1000 = Đang xử lý (chưa thanh toán)
                // 9000 = Giao dịch chưa được thực hiện
                if (queryResponse.resultCode == 0)
                {
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"❌ [MoMo Query] Error: {ex.Message}");
                return false;
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            _isFormClosing = true;
            _pollingTimer?.Stop();
            _pollingTimer?.Dispose();
            base.OnFormClosing(e);
        }
    }
}