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
            
            PaymentSuccess = false; // ✅ Initialize explicitly
        }

        private void frmMoMoWaitPayment_Load(object sender, EventArgs e)
        {
            lblAmount.Text = $"Amount of money: {_amount:N0} VNĐ";
            btnCancel.Click += BtnCancel_Click;
            StartPolling();
            
            System.Diagnostics.Debug.WriteLine($"🔍 [MoMo Wait] Form loaded - OrderId: {_orderId}, PaymentId: {_paymentUid}");
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine($"⚠️ [MoMo Wait] User cancelled");
            
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
                        System.Diagnostics.Debug.WriteLine($"⏰ [MoMo Wait] Timeout reached");
                        MessageBox.Show("Payment waiting time has expired!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        this.DialogResult = DialogResult.Cancel;
                    }
                    return;
                }

                // Check payment status
                bool isPaid = await CheckPaymentStatusAsync();
                
                System.Diagnostics.Debug.WriteLine($"🔍 [MoMo Wait] Poll #{_elapsedSeconds/3}: isPaid={isPaid}");
                
                if (isPaid && !_isFormClosing)
                {
                    _pollingTimer.Stop();
                    
                    // ✅ QUAN TRỌNG: Set property TRƯỚC
                    PaymentSuccess = true;
                    
                    System.Diagnostics.Debug.WriteLine($"✅ [MoMo Wait] Payment SUCCESS detected!");
                    System.Diagnostics.Debug.WriteLine($"✅ [MoMo Wait] PaymentSuccess = {PaymentSuccess}");
                    
                    // ✅ CRITICAL FIX: KHÔNG SHOW MessageBox Ở ĐÂY
                    // MessageBox.Show() sẽ làm DialogResult bị reset hoặc form đóng sớm
                    
                    System.Diagnostics.Debug.WriteLine($"✅ [MoMo Wait] Setting DialogResult.OK");
                    this.DialogResult = DialogResult.OK;
                    
                    // Form sẽ tự đóng và control quay về frmPOS
                }
            };
            
            _pollingTimer.Start();
            System.Diagnostics.Debug.WriteLine($"✅ [MoMo Wait] Polling timer started (interval: 3s, timeout: {TIMEOUT_SECONDS}s)");
        }

        private async Task<bool> CheckPaymentStatusAsync()
        {
            try
            {
                string orderId_str = $"ORDER{_orderId}";

                System.Diagnostics.Debug.WriteLine($"[MoMo Query] Checking status for: {orderId_str}, RequestId: {_requestId}");

                var queryResponse = await MoMoPaymentHelper.QueryTransactionStatusAsync(orderId_str, _requestId);

                if (queryResponse == null)
                {
                    System.Diagnostics.Debug.WriteLine($"❌ [MoMo Query] Response is NULL");
                    return false;
                }

                System.Diagnostics.Debug.WriteLine($"[MoMo Query] Result Code: {queryResponse.resultCode}");
                System.Diagnostics.Debug.WriteLine($"[MoMo Query] Message: {queryResponse.message}");

                // resultCode:
                // 0 = Thanh toán thành công
                // 1000 = Đang xử lý (chưa thanh toán)
                // 9000 = Giao dịch chưa được thực hiện
                if (queryResponse.resultCode == 0)
                {
                    System.Diagnostics.Debug.WriteLine($"✅ [MoMo Query] Payment SUCCESS detected!");
                    return true;
                }

                System.Diagnostics.Debug.WriteLine($"⏳ [MoMo Query] Not yet paid (code: {queryResponse.resultCode})");
                return false;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"❌ [MoMo Query] Exception: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"   Stack: {ex.StackTrace}");
                return false;
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine($"🔍 [MoMo Wait] OnFormClosing called");
            System.Diagnostics.Debug.WriteLine($"   PaymentSuccess = {PaymentSuccess}");
            System.Diagnostics.Debug.WriteLine($"   DialogResult = {this.DialogResult}");
            
            _isFormClosing = true;
            _pollingTimer?.Stop();
            _pollingTimer?.Dispose();
            base.OnFormClosing(e);
        }
    }
}