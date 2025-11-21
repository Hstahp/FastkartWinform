using DAL;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Data;
using System.IO;
using System.Collections.Generic;

namespace BLL
{
    public class ReportBLL
    {
        // Chỉ dùng DAL, không dùng _context nữa
        private DashboardDAL _dashboardRepo;

        public ReportBLL()
        {
            _dashboardRepo = new DashboardDAL();
        }

        public bool ExportSalesReport(DateTime fromDate, DateTime toDate, string savePath)
        {
            try
            {
                // Lấy dữ liệu từ DAL (DataTable)
                DataTable dt = _dashboardRepo.GetOrdersByDateRange(fromDate, toDate);

                Document document = new Document(PageSize.A4, 25, 25, 30, 30);
                PdfWriter.GetInstance(document, new FileStream(savePath, FileMode.Create));
                document.Open();

                // Cấu hình Font
                string fontPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "arial.ttf");
                BaseFont baseFont = BaseFont.CreateFont(fontPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);

                // Khai báo rõ iTextSharp.text.Font để tránh lỗi
                iTextSharp.text.Font titleFont = new iTextSharp.text.Font(baseFont, 18, iTextSharp.text.Font.BOLD);
                iTextSharp.text.Font headerFont = new iTextSharp.text.Font(baseFont, 12, iTextSharp.text.Font.BOLD);
                iTextSharp.text.Font normalFont = new iTextSharp.text.Font(baseFont, 10, iTextSharp.text.Font.NORMAL);

                // Tiêu đề
                Paragraph title = new Paragraph("BÁO CÁO DOANH THU", titleFont);
                title.Alignment = Element.ALIGN_CENTER;
                title.SpacingAfter = 10f;
                document.Add(title);

                Paragraph period = new Paragraph($"Từ ngày: {fromDate:dd/MM/yyyy} - Đến ngày: {toDate:dd/MM/yyyy}", normalFont);
                period.Alignment = Element.ALIGN_CENTER;
                period.SpacingAfter = 20f;
                document.Add(period);

                // Tính tổng quan từ DataTable
                decimal totalRevenue = 0;
                foreach (DataRow row in dt.Rows)
                {
                    totalRevenue += Convert.ToDecimal(row["TotalAmount"]);
                }
                int totalOrders = dt.Rows.Count;

                Paragraph summary = new Paragraph($"Tổng đơn hàng: {totalOrders} | Tổng doanh thu: {totalRevenue:N0} VNĐ", headerFont);
                summary.SpacingAfter = 15f;
                document.Add(summary);

                // Tạo bảng PDF
                PdfPTable table = new PdfPTable(5);
                table.WidthPercentage = 100;
                table.SetWidths(new float[] { 1f, 2.5f, 2f, 2f, 1.5f });

                AddTableHeader(table, new string[] { "Mã ĐH", "Khách hàng", "Ngày đặt", "Tổng tiền", "Trạng thái" }, headerFont);

                foreach (DataRow row in dt.Rows)
                {
                    table.AddCell(new PdfPCell(new Phrase("#" + row["Uid"].ToString(), normalFont)));
                    table.AddCell(new PdfPCell(new Phrase(row["FullName"].ToString(), normalFont)));
                    table.AddCell(new PdfPCell(new Phrase(Convert.ToDateTime(row["OrderDate"]).ToString("dd/MM/yyyy HH:mm"), normalFont)));
                    table.AddCell(new PdfPCell(new Phrase(Convert.ToDecimal(row["TotalAmount"]).ToString("N0") + " VNĐ", normalFont)));
                    table.AddCell(new PdfPCell(new Phrase(row["Status"].ToString(), normalFont)));
                }

                document.Add(table);
                document.Close();
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Export Error: {ex.Message}");
                return false;
            }
        }

        // =============================================
        // PRODUCT REPORT - Báo cáo sản phẩm
        // =============================================
        public bool ExportTopProductsReport(int topCount, string savePath)
        {
            try
            {
                DataTable dt = _dashboardRepo.GetTopProductsForReport(topCount);

                Document document = new Document(PageSize.A4, 25, 25, 30, 30);
                PdfWriter.GetInstance(document, new FileStream(savePath, FileMode.Create));
                document.Open();

                string fontPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "arial.ttf");
                BaseFont baseFont = BaseFont.CreateFont(fontPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                iTextSharp.text.Font titleFont = new iTextSharp.text.Font(baseFont, 18, iTextSharp.text.Font.BOLD);
                iTextSharp.text.Font headerFont = new iTextSharp.text.Font(baseFont, 12, iTextSharp.text.Font.BOLD);
                iTextSharp.text.Font normalFont = new iTextSharp.text.Font(baseFont, 10, iTextSharp.text.Font.NORMAL);

                Paragraph title = new Paragraph($"TOP {topCount} SẢN PHẨM BÁN CHẠY", titleFont);
                title.Alignment = Element.ALIGN_CENTER;
                title.SpacingAfter = 20f;
                document.Add(title);

                PdfPTable table = new PdfPTable(4);
                table.WidthPercentage = 100;
                table.SetWidths(new float[] { 1f, 3f, 2f, 2f });

                AddTableHeader(table, new string[] { "Hạng", "Tên sản phẩm", "Số lượng bán", "Doanh thu" }, headerFont);

                int rank = 1;
                foreach (DataRow row in dt.Rows)
                {
                    table.AddCell(new PdfPCell(new Phrase((rank++).ToString(), normalFont)));
                    table.AddCell(new PdfPCell(new Phrase(row["ProductName"].ToString(), normalFont)));
                    table.AddCell(new PdfPCell(new Phrase(row["TotalQuantity"].ToString(), normalFont)));
                    table.AddCell(new PdfPCell(new Phrase(Convert.ToDecimal(row["TotalRevenue"]).ToString("N0") + " VNĐ", normalFont)));
                }

                document.Add(table);
                document.Close();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        // =============================================
        // INVENTORY REPORT - Báo cáo tồn kho
        // =============================================
        public bool ExportInventoryReport(string savePath)
        {
            try
            {
                DataTable dt = _dashboardRepo.GetInventoryForReport();

                Document document = new Document(PageSize.A4.Rotate(), 25, 25, 30, 30);
                PdfWriter.GetInstance(document, new FileStream(savePath, FileMode.Create));
                document.Open();

                string fontPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "arial.ttf");
                BaseFont baseFont = BaseFont.CreateFont(fontPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                iTextSharp.text.Font titleFont = new iTextSharp.text.Font(baseFont, 18, iTextSharp.text.Font.BOLD);
                iTextSharp.text.Font headerFont = new iTextSharp.text.Font(baseFont, 11, iTextSharp.text.Font.BOLD);
                iTextSharp.text.Font normalFont = new iTextSharp.text.Font(baseFont, 9, iTextSharp.text.Font.NORMAL);

                Paragraph title = new Paragraph("BÁO CÁO TỒN KHO", titleFont);
                title.Alignment = Element.ALIGN_CENTER;
                title.SpacingAfter = 20f;
                document.Add(title);

                PdfPTable table = new PdfPTable(7);
                table.WidthPercentage = 100;
                table.SetWidths(new float[] { 1f, 3f, 1.5f, 1.5f, 1.5f, 2f, 1.5f });

                AddTableHeader(table, new string[] { "ID", "Tên sản phẩm", "SKU", "Tồn kho", "Giá bán", "Thương hiệu", "Trạng thái" }, headerFont);

                foreach (DataRow row in dt.Rows)
                {
                    table.AddCell(new PdfPCell(new Phrase(row["Uid"].ToString(), normalFont)));
                    table.AddCell(new PdfPCell(new Phrase(row["ProductName"].ToString(), normalFont)));
                    table.AddCell(new PdfPCell(new Phrase(row["Sku"].ToString(), normalFont)));

                    int stock = Convert.ToInt32(row["StockQuantity"]);
                    PdfPCell stockCell = new PdfPCell(new Phrase(stock.ToString(), normalFont));
                    if (stock < 10) stockCell.BackgroundColor = new BaseColor(255, 200, 200);
                    table.AddCell(stockCell);

                    table.AddCell(new PdfPCell(new Phrase(Convert.ToDecimal(row["Price"]).ToString("N0"), normalFont)));
                    table.AddCell(new PdfPCell(new Phrase(row["BrandName"].ToString(), normalFont)));
                    table.AddCell(new PdfPCell(new Phrase(row["StockStatus"].ToString(), normalFont)));
                }

                document.Add(table);
                document.Close();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        // =============================================
        // CUSTOMER REPORT - Báo cáo khách hàng
        // =============================================
        public bool ExportTopCustomersReport(int topCount, string savePath)
        {
            try
            {
                DataTable dt = _dashboardRepo.GetTopCustomersForReport(topCount);

                Document document = new Document(PageSize.A4, 25, 25, 30, 30);
                PdfWriter.GetInstance(document, new FileStream(savePath, FileMode.Create));
                document.Open();

                string fontPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "arial.ttf");
                BaseFont baseFont = BaseFont.CreateFont(fontPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                iTextSharp.text.Font titleFont = new iTextSharp.text.Font(baseFont, 18, iTextSharp.text.Font.BOLD);
                iTextSharp.text.Font headerFont = new iTextSharp.text.Font(baseFont, 12, iTextSharp.text.Font.BOLD);
                iTextSharp.text.Font normalFont = new iTextSharp.text.Font(baseFont, 10, iTextSharp.text.Font.NORMAL);

                Paragraph title = new Paragraph($"TOP {topCount} KHÁCH HÀNG TIỀM NĂNG", titleFont);
                title.Alignment = Element.ALIGN_CENTER;
                title.SpacingAfter = 20f;
                document.Add(title);

                PdfPTable table = new PdfPTable(4);
                table.WidthPercentage = 100;
                table.SetWidths(new float[] { 2.5f, 2.5f, 1.5f, 2f });

                AddTableHeader(table, new string[] { "Tên khách hàng", "Email", "Số ĐH", "Tổng chi tiêu" }, headerFont);

                foreach (DataRow row in dt.Rows)
                {
                    table.AddCell(new PdfPCell(new Phrase(row["FullName"].ToString(), normalFont)));
                    table.AddCell(new PdfPCell(new Phrase(row["Email"].ToString(), normalFont)));
                    table.AddCell(new PdfPCell(new Phrase(row["OrderCount"].ToString(), normalFont)));
                    table.AddCell(new PdfPCell(new Phrase(Convert.ToDecimal(row["TotalSpent"]).ToString("N0") + " VNĐ", normalFont)));
                }

                document.Add(table);
                document.Close();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        // Helper - Đã sửa lỗi Font
        private void AddTableHeader(PdfPTable table, string[] headers, iTextSharp.text.Font font)
        {
            foreach (var header in headers)
            {
                PdfPCell cell = new PdfPCell(new Phrase(header, font));
                cell.BackgroundColor = new BaseColor(52, 152, 219);
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.Padding = 8f;
                table.AddCell(cell);
            }
        }
    }
}