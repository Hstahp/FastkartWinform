using System;
using System.Windows.Forms;

namespace GUI.Payment
{
    public partial class frmPaymentMethod : Form
    {
        public string SelectedPaymentMethod { get; private set; }
        private decimal _totalAmount;

        public frmPaymentMethod(decimal totalAmount)
        {
            InitializeComponent();
            _totalAmount = totalAmount;
        }

        private void frmPaymentMethod_Load(object sender, EventArgs e)
        {
            // Hiển thị tổng tiền
            lblTotal.Text = $"Total amount: {_totalAmount:N0} VNĐ";

            // Gán sự kiện cho buttons
            btnCash.Click += BtnCash_Click;
            btnMoMo.Click += BtnMoMo_Click;
            btnCancel.Click += BtnCancel_Click;
        }

        private void BtnCash_Click(object sender, EventArgs e)
        {
            SelectedPaymentMethod = "Cash";
            this.DialogResult = DialogResult.OK;
        }

        private void BtnMoMo_Click(object sender, EventArgs e)
        {
            SelectedPaymentMethod = "MoMo";
            this.DialogResult = DialogResult.OK;
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}