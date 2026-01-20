using BLL;
using DTO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace GUI
{
    public partial class frmDashboard : Form
    {
        private DashboardBLL _dashboardBLL;
        private ReportBLL _reportBLL;

        public frmDashboard()
        {
            InitializeComponent();

            // 1. Khởi tạo Logic
            _dashboardBLL = new DashboardBLL();
            _reportBLL = new ReportBLL();

            // 2. Khởi tạo Giao diện Responsive
            InitializeResponsiveLayout();

            // 3. Đăng ký sự kiện Load
            this.Load += FrmDashboard_Load;
        }

        private void FrmDashboard_Load(object sender, EventArgs e)
        {
            LoadDashboardData();
        }

        // ================================================================
        // PHẦN 1: LOGIC & DỮ LIỆU
        // ================================================================

        private void LoadDashboardData()
        {
            try
            {
                var kpi = _dashboardBLL.GetKPIData();
                lblTotalUsers.Text = kpi.TotalUsers.ToString();
                lblTotalProducts.Text = kpi.TotalProducts.ToString();
                lblTotalRevenue.Text = $"{kpi.TotalRevenue:N0} đ";
                lblTotalOrders.Text = kpi.TotalOrders.ToString();
                lblTodayRevenue.Text = $"{kpi.TodayRevenue:N0} đ";
                lblTodayOrders.Text = kpi.TodayOrders.ToString();
                lblLowStock.Text = kpi.LowStockCount.ToString();
                lblAvgOrder.Text = $"{kpi.AverageOrderValue:N0} đ";

                LoadRevenueChart();
                LoadTopProductChart();
                LoadOrderStatusChart();
                LoadRecentOrders();
                LoadLowStockProducts();
            }
            catch { }
        }

        private void LoadRevenueChart()
        {
            var data = _dashboardBLL.GetLast7DaysRevenue();
            chartRevenue7Days.Series[0].Points.Clear();

            chartRevenue7Days.Series[0].ChartType = SeriesChartType.Line;
            chartRevenue7Days.Series[0].BorderWidth = 3;
            chartRevenue7Days.Series[0].Color = Color.FromArgb(52, 152, 219);

            foreach (var item in data) chartRevenue7Days.Series[0].Points.AddXY(item.Key, item.Value);
        }

        private void LoadTopProductChart()
        {
            var data = _dashboardBLL.GetTop5BestSellingProducts();
            chartTopProducts.Series[0].Points.Clear();
            chartTopProducts.Series[0].ChartType = SeriesChartType.Column;
            chartTopProducts.Series[0].Color = Color.FromArgb(46, 204, 113);
            chartTopProducts.Series[0]["PointWidth"] = "0.4"; // Độ rộng cột

            foreach (var item in data) chartTopProducts.Series[0].Points.AddXY(item.ProductName, item.TotalQuantity);
        }

        private void LoadOrderStatusChart()
        {
            var data = _dashboardBLL.GetOrderStatusDistribution();
            chartOrderStatus.Series[0].Points.Clear();
            chartOrderStatus.Series[0].ChartType = SeriesChartType.Pie;
            chartOrderStatus.Series[0].IsValueShownAsLabel = true;

            int total = 0; foreach (var item in data) total += item.Value;
            foreach (var item in data)
            {
                double p = total > 0 ? (double)item.Value / total * 100 : 0;
                var pt = chartOrderStatus.Series[0].Points.AddXY(item.Key, item.Value);
                chartOrderStatus.Series[0].Points[pt].Label = $"{item.Value} ({p:0}%)";
                chartOrderStatus.Series[0].Points[pt].LegendText = item.Key;
            }
        }

        private void LoadRecentOrders()
        {
            dgvRecentOrders.DataSource = null;
            dgvRecentOrders.Columns.Clear();
            dgvRecentOrders.DataSource = _dashboardBLL.GetRecentOrders(5);

            if (dgvRecentOrders.Columns.Count > 0)
            {
                if (dgvRecentOrders.Columns.Contains("OrderId"))
                {
                    dgvRecentOrders.Columns["OrderId"].HeaderText = "ID";
                    dgvRecentOrders.Columns["OrderId"].Width = 60;
                }
                if (dgvRecentOrders.Columns.Contains("CustomerName"))
                    dgvRecentOrders.Columns["CustomerName"].HeaderText = "Khách hàng";
                if (dgvRecentOrders.Columns.Contains("TotalAmount"))
                {
                    dgvRecentOrders.Columns["TotalAmount"].HeaderText = "Tổng tiền";
                    dgvRecentOrders.Columns["TotalAmount"].DefaultCellStyle.Format = "N0";
                    dgvRecentOrders.Columns["TotalAmount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
                if (dgvRecentOrders.Columns.Contains("Status"))
                    dgvRecentOrders.Columns["Status"].HeaderText = "Trạng thái";
                if (dgvRecentOrders.Columns.Contains("OrderDate"))
                {
                    dgvRecentOrders.Columns["OrderDate"].HeaderText = "Ngày";
                    dgvRecentOrders.Columns["OrderDate"].DefaultCellStyle.Format = "dd/MM HH:mm";
                }
            }
        }

        private void LoadLowStockProducts()
        {
            dgvLowStock.DataSource = null;
            dgvLowStock.Columns.Clear();
            dgvLowStock.DataSource = _dashboardBLL.GetLowStockProducts(10);

            if (dgvLowStock.Columns.Count > 0)
            {
                if (dgvLowStock.Columns.Contains("ProductName")) dgvLowStock.Columns["ProductName"].HeaderText = "Tên SP";
                if (dgvLowStock.Columns.Contains("StockQuantity"))
                {
                    dgvLowStock.Columns["StockQuantity"].HeaderText = "SL";
                    dgvLowStock.Columns["StockQuantity"].Width = 60;
                    dgvLowStock.Columns["StockQuantity"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
                if (dgvLowStock.Columns.Contains("Sku")) dgvLowStock.Columns["Sku"].HeaderText = "Mã SKU";
            }
        }

        private void BtnSalesReport_Click(object sender, EventArgs e) { ExportReport("DoanhThu", _reportBLL.ExportSalesReport); }
        private void BtnProductReport_Click(object sender, EventArgs e) { ExportReport("TopSP", (f, t, p) => _reportBLL.ExportTopProductsReport(10, p)); }
        private void BtnInventoryReport_Click(object sender, EventArgs e) { ExportReport("TonKho", (f, t, p) => _reportBLL.ExportInventoryReport(p)); }
        private void BtnCustomerReport_Click(object sender, EventArgs e) { ExportReport("KhachHang", (f, t, p) => _reportBLL.ExportTopCustomersReport(10, p)); }

        private void ExportReport(string name, Func<DateTime, DateTime, string, bool> func)
        {
            using (SaveFileDialog sfd = new SaveFileDialog { Filter = "PDF|*.pdf", FileName = $"{name}_{DateTime.Now:yyyyMMdd}.pdf" })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    if (func(DateTime.Now, DateTime.Now, sfd.FileName))
                        MessageBox.Show("Xuất thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        MessageBox.Show("Lỗi xuất file!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // ================================================================
        // PHẦN 2: CODE TẠO GIAO DIỆN RESPONSIVE (ĐÃ SỬA THỨ TỰ)
        // ================================================================

        private void InitializeResponsiveLayout()
        {
            this.BackColor = Color.FromArgb(240, 242, 245);

            Panel mainPanel = new Panel { Dock = DockStyle.Fill, AutoScroll = true, Padding = new Padding(20) };
            this.Controls.Add(mainPanel);

            // -------------------------------------------------------
            // KHỞI TẠO CÁC CONTROL TRƯỚC (CHƯA ADD VÀO PANEL)
            // -------------------------------------------------------

            // 1. KPI Grid
            TableLayoutPanel kpiLayout = new TableLayoutPanel { Dock = DockStyle.Top, Height = 240, ColumnCount = 4, RowCount = 2, Padding = new Padding(0, 0, 0, 20) };
            for (int i = 0; i < 4; i++) kpiLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));

            Label tempLblTotalUsers, tempLblTotalProducts, tempLblTotalRevenue, tempLblTotalOrders;
            Label tempLblTodayRevenue, tempLblTodayOrders, tempLblLowStock, tempLblAvgOrder;

            kpiLayout.Controls.Add(CreateKPICard(out tempLblTotalUsers, "Tổng người dùng", Color.FromArgb(52, 152, 219)), 0, 0);
            kpiLayout.Controls.Add(CreateKPICard(out tempLblTotalProducts, "Tổng sản phẩm", Color.FromArgb(46, 204, 113)), 1, 0);
            kpiLayout.Controls.Add(CreateKPICard(out tempLblTotalRevenue, "Tổng doanh thu", Color.FromArgb(155, 89, 182)), 2, 0);
            kpiLayout.Controls.Add(CreateKPICard(out tempLblTotalOrders, "Tổng đơn hàng", Color.FromArgb(230, 126, 34)), 3, 0);
            kpiLayout.Controls.Add(CreateKPICard(out tempLblTodayRevenue, "Doanh thu hôm nay", Color.FromArgb(52, 73, 94)), 0, 1);
            kpiLayout.Controls.Add(CreateKPICard(out tempLblTodayOrders, "Đơn hàng hôm nay", Color.FromArgb(231, 76, 60)), 1, 1);
            kpiLayout.Controls.Add(CreateKPICard(out tempLblLowStock, "Sắp hết hàng", Color.FromArgb(241, 196, 15)), 2, 1);
            kpiLayout.Controls.Add(CreateKPICard(out tempLblAvgOrder, "Giá trị TB/Đơn", Color.FromArgb(26, 188, 156)), 3, 1);

            // Gán vào Designer controls
            lblTotalUsers = tempLblTotalUsers;
            lblTotalProducts = tempLblTotalProducts;
            lblTotalRevenue = tempLblTotalRevenue;
            lblTotalOrders = tempLblTotalOrders;
            lblTodayRevenue = tempLblTodayRevenue;
            lblTodayOrders = tempLblTodayOrders;
            lblLowStock = tempLblLowStock;
            lblAvgOrder = tempLblAvgOrder;

            // 2. Charts Top
            TableLayoutPanel chartsTopLayout = new TableLayoutPanel { Dock = DockStyle.Top, Height = 350, ColumnCount = 2, Padding = new Padding(0, 0, 0, 20) };
            chartsTopLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65F));
            chartsTopLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35F));

            chartRevenue7Days = CreateBaseChart("Doanh thu 7 ngày qua");
            ConfigureChartSeries(chartRevenue7Days, SeriesChartType.Line);
            chartsTopLayout.Controls.Add(chartRevenue7Days, 0, 0);

            chartOrderStatus = CreateBaseChart("Trạng thái đơn hàng");
            ConfigureChartSeries(chartOrderStatus, SeriesChartType.Pie);
            chartsTopLayout.Controls.Add(chartOrderStatus, 1, 0);

            // 3. Chart Bot
            Panel chartBotPanel = new Panel { Dock = DockStyle.Top, Height = 350, Padding = new Padding(0, 0, 0, 20) };
            chartTopProducts = CreateBaseChart("Top 5 sản phẩm bán chạy");
            ConfigureChartSeries(chartTopProducts, SeriesChartType.Column);
            chartBotPanel.Controls.Add(chartTopProducts);

            // 4. Tables
            TableLayoutPanel tablesLayout = new TableLayoutPanel { Dock = DockStyle.Top, Height = 300, ColumnCount = 2, Padding = new Padding(0, 0, 0, 20) };
            tablesLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tablesLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));

            dgvRecentOrders = CreateBaseGrid();
            tablesLayout.Controls.Add(CreateGridContainer("📋 Đơn hàng gần đây", dgvRecentOrders), 0, 0);
            dgvLowStock = CreateBaseGrid();
            tablesLayout.Controls.Add(CreateGridContainer("⚠️ Sắp hết hàng", dgvLowStock), 1, 0);

            // 5. Export Buttons
            FlowLayoutPanel exportPanel = new FlowLayoutPanel { Dock = DockStyle.Top, Height = 60, BackColor = Color.White, Padding = new Padding(10), FlowDirection = FlowDirection.LeftToRight };
            exportPanel.Controls.Add(new Label { Text = "📄 Xuất báo cáo:", Font = new Font("Segoe UI", 11, FontStyle.Bold), AutoSize = true, Margin = new Padding(0, 10, 20, 0) });
            exportPanel.Controls.Add(CreateExportButton("Báo cáo doanh thu", Color.FromArgb(52, 152, 219), BtnSalesReport_Click));
            exportPanel.Controls.Add(CreateExportButton("Top sản phẩm", Color.FromArgb(46, 204, 113), BtnProductReport_Click));
            exportPanel.Controls.Add(CreateExportButton("Báo cáo tồn kho", Color.FromArgb(155, 89, 182), BtnInventoryReport_Click));
            exportPanel.Controls.Add(CreateExportButton("Top khách hàng", Color.FromArgb(230, 126, 34), BtnCustomerReport_Click));


            // -------------------------------------------------------
            // QUAN TRỌNG: ADD NGƯỢC TỪ DƯỚI LÊN TRÊN
            // (Vì Dock = Top: Cái nào Add sau cùng sẽ nằm Trên Cùng)
            // -------------------------------------------------------

            mainPanel.Controls.Add(exportPanel);      // 5. Nằm dưới cùng
            mainPanel.Controls.Add(tablesLayout);     // 4. Nằm trên Export
            mainPanel.Controls.Add(chartBotPanel);    // 3. Nằm trên Tables
            mainPanel.Controls.Add(chartsTopLayout);  // 2. Nằm trên Chart Bot
            mainPanel.Controls.Add(kpiLayout);        // 1. Nằm trên cùng (KPI)
        }

        // --- HELPERS ---
        private Panel CreateKPICard(out Label lblVal, string title, Color bg)
        {
            Panel card = new Panel { Dock = DockStyle.Fill, BackColor = bg, Margin = new Padding(5) };
            Label lblTitle = new Label { Text = title, ForeColor = Color.White, Font = new Font("Segoe UI", 10, FontStyle.Bold), Location = new Point(15, 15), AutoSize = true };
            lblVal = new Label { Text = "0", ForeColor = Color.White, Font = new Font("Segoe UI", 16, FontStyle.Bold), Location = new Point(15, 45), AutoSize = true };
            card.Controls.Add(lblTitle); card.Controls.Add(lblVal);
            return card;
        }

        private Chart CreateBaseChart(string title)
        {
            Chart c = new Chart { Dock = DockStyle.Fill, BackColor = Color.White, Margin = new Padding(5) };
            ChartArea a = new ChartArea("MainArea");
            a.AxisX.MajorGrid.LineColor = a.AxisY.MajorGrid.LineColor = Color.LightGray;
            c.ChartAreas.Add(a);
            c.Titles.Add(new Title(title) { Font = new Font("Segoe UI", 12, FontStyle.Bold), ForeColor = Color.FromArgb(52, 73, 94), Alignment = ContentAlignment.TopLeft });
            return c;
        }

        private void ConfigureChartSeries(Chart c, SeriesChartType type)
        {
            Series s = new Series("S1") { ChartType = type };
            if (type == SeriesChartType.Line) { s.BorderWidth = 3; s.Color = Color.FromArgb(52, 152, 219); s.MarkerStyle = MarkerStyle.Circle; }
            if (type == SeriesChartType.Column) s.Color = Color.FromArgb(46, 204, 113);
            if (type == SeriesChartType.Pie) s["PieLabelStyle"] = "Outside";
            c.Series.Add(s);
            if (type != SeriesChartType.Column) c.Legends.Add(new Legend { Docking = Docking.Bottom });
        }

        private DataGridView CreateBaseGrid()
        {
            DataGridView dgv = new DataGridView { Dock = DockStyle.Fill, BackgroundColor = Color.White, AllowUserToAddRows = false, ReadOnly = true, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill, SelectionMode = DataGridViewSelectionMode.FullRowSelect, BorderStyle = BorderStyle.None, RowHeadersVisible = false };
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 9);
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(52, 152, 219);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.EnableHeadersVisualStyles = false;
            return dgv;
        }

        private Panel CreateGridContainer(string title, DataGridView dgv)
        {
            Panel p = new Panel { Dock = DockStyle.Fill, BackColor = Color.White, Margin = new Padding(5) };
            p.Controls.Add(dgv);
            p.Controls.Add(new Label { Text = title, Font = new Font("Segoe UI", 11, FontStyle.Bold), Dock = DockStyle.Top, Height = 30, Padding = new Padding(5) });
            return p;
        }

        private Button CreateExportButton(string text, Color bg, EventHandler handler)
        {
            Button b = new Button { Text = text, BackColor = bg, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 9, FontStyle.Bold), Size = new Size(160, 38), Margin = new Padding(5), Cursor = Cursors.Hand };
            b.FlatAppearance.BorderSize = 0;
            b.Click += handler;
            return b;
        }
    }
}