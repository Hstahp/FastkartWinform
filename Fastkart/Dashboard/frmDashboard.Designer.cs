namespace GUI
{
    partial class frmDashboard
    {
        private System.ComponentModel.IContainer components = null;

        // --- KHAI BÁO BIẾN CONTROL (QUAN TRỌNG: Phải có dòng này thì Logic mới hiểu) ---
        public System.Windows.Forms.DataVisualization.Charting.Chart chartRevenue7Days;
        public System.Windows.Forms.DataVisualization.Charting.Chart chartTopProducts;
        public System.Windows.Forms.DataVisualization.Charting.Chart chartOrderStatus;
        public System.Windows.Forms.DataGridView dgvRecentOrders;
        public System.Windows.Forms.DataGridView dgvLowStock;
        public System.Windows.Forms.Label lblTotalUsers, lblTotalProducts, lblTotalRevenue, lblTotalOrders;
        public System.Windows.Forms.Label lblTodayRevenue, lblTodayOrders, lblLowStock, lblAvgOrder;

        // Các biến panel/layout (dùng nội bộ designer)
        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelKPI;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelCharts;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelTables;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelExport;
        private System.Windows.Forms.Button btnSalesReport, btnProductReport, btnInventoryReport, btnCustomerReport;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            // Khởi tạo các control
            this.mainPanel = new System.Windows.Forms.Panel();
            this.tableLayoutPanelKPI = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanelCharts = new System.Windows.Forms.TableLayoutPanel();
            this.chartRevenue7Days = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chartOrderStatus = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chartTopProducts = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tableLayoutPanelTables = new System.Windows.Forms.TableLayoutPanel();
            this.dgvRecentOrders = new System.Windows.Forms.DataGridView();
            this.dgvLowStock = new System.Windows.Forms.DataGridView();
            this.flowLayoutPanelExport = new System.Windows.Forms.FlowLayoutPanel();

            // (Các dòng khởi tạo Label và Button - giữ ngắn gọn để bạn dễ copy)
            // ... Code tạo giao diện chi tiết đã có ở câu trả lời trước ...
            // Vì bạn muốn code giao diện tách biệt, bạn hãy dùng lại nội dung InitializeComponent
            // từ câu trả lời dài trước đó của tôi, NHƯNG ĐẢM BẢO PHẦN KHAI BÁO BIẾN Ở TRÊN CÙNG PHẢI CÓ.
        }

        // ... (Các hàm Helper CreateKpiPanel, CreateBaseChart... giữ nguyên) ...

        #endregion
    }
}