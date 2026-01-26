using DAL;
using DAL.EF;
using DTO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw; // ✅ THÊM
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;

namespace BLL
{
    /// <summary>
    /// Business Logic Layer cho Invoice (Hóa đơn bán hàng)
    /// Chuyên xử lý xuất PDF hóa đơn chi tiết cho từng đơn hàng
    /// </summary>
    public class InvoiceBLL
    {
        private OrderDAL _orderDAL;
        private PaymentDAL _paymentDAL;

        public InvoiceBLL()
        {
            _orderDAL = new OrderDAL();
            _paymentDAL = new PaymentDAL();
        }

        /// <summary>
        /// Xuất hóa đơn PDF cho 1 đơn hàng cụ thể
        /// </summary>
        public bool ExportInvoiceToPDF(int orderUid, string savePath)
        {
            try
            {
                // 1. Lấy thông tin Order
                var order = GetOrderDetailsForInvoice(orderUid);
                if (order == null)
                {
                    System.Diagnostics.Debug.WriteLine($"Order not found: {orderUid}");
                    return false;
                }

                // 2. Lấy thông tin Payment
                var payment = _paymentDAL.GetPaymentByOrderUid(orderUid);

                // 3. Tạo PDF
                Document document = new Document(PageSize.A4, 40, 40, 40, 40);
                PdfWriter.GetInstance(document, new FileStream(savePath, FileMode.Create));
                document.Open();

                // Font configuration
                string fontPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "arial.ttf");
                BaseFont baseFont = BaseFont.CreateFont(fontPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);

                var titleFont = new iTextSharp.text.Font(baseFont, 20, iTextSharp.text.Font.BOLD);
                var headerFont = new iTextSharp.text.Font(baseFont, 12, iTextSharp.text.Font.BOLD);
                var normalFont = new iTextSharp.text.Font(baseFont, 10, iTextSharp.text.Font.NORMAL);
                var boldFont = new iTextSharp.text.Font(baseFont, 10, iTextSharp.text.Font.BOLD);

                // === HEADER: Thông tin cửa hàng ===
                Paragraph storeName = new Paragraph("FASTKART SUPERMARKET", titleFont);
                storeName.Alignment = Element.ALIGN_CENTER;
                storeName.SpacingAfter = 5f;
                document.Add(storeName);

                Paragraph storeInfo = new Paragraph("123 Main Street, City | Phone: (123) 456-7890", normalFont);
                storeInfo.Alignment = Element.ALIGN_CENTER;
                storeInfo.SpacingAfter = 10f;
                document.Add(storeInfo);

                // Đường kẻ ngang
                LineSeparator line = new LineSeparator(1f, 100f, BaseColor.GRAY, Element.ALIGN_CENTER, -2);
                document.Add(new Chunk(line));
                document.Add(new Paragraph(" ", normalFont)); // Spacing

                // === INVOICE TITLE ===
                Paragraph invoiceTitle = new Paragraph("HÓA ĐƠN BÁN HÀNG", headerFont);
                invoiceTitle.Alignment = Element.ALIGN_CENTER;
                invoiceTitle.SpacingAfter = 15f;
                document.Add(invoiceTitle);

                // === Thông tin đơn hàng ===
                PdfPTable infoTable = new PdfPTable(2);
                infoTable.WidthPercentage = 100;
                infoTable.SetWidths(new float[] { 1f, 1f });
                infoTable.SpacingAfter = 15f;

                // Left column
                PdfPCell leftCell = new PdfPCell();
                leftCell.Border = Rectangle.NO_BORDER;
                leftCell.AddElement(new Phrase($"Mã đơn hàng: #{order.Uid}", boldFont));
                leftCell.AddElement(new Phrase($"Ngày: {order.OrderDate:dd/MM/yyyy HH:mm}", normalFont));
                leftCell.AddElement(new Phrase($"Người tạo: {order.CreatedBy ?? "N/A"}", normalFont));

                // Right column
                PdfPCell rightCell = new PdfPCell();
                rightCell.Border = Rectangle.NO_BORDER;
                rightCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                rightCell.AddElement(new Phrase($"Trạng thái: {order.Status}", boldFont));
                
                if (payment != null)
                {
                    rightCell.AddElement(new Phrase($"Thanh toán: {payment.PaymentMethod}", normalFont));
                    rightCell.AddElement(new Phrase($"TT Thanh toán: {payment.PaymentStatus}", normalFont));
                }

                infoTable.AddCell(leftCell);
                infoTable.AddCell(rightCell);
                document.Add(infoTable);

                // === Bảng sản phẩm ===
                PdfPTable itemsTable = new PdfPTable(5);
                itemsTable.WidthPercentage = 100;
                itemsTable.SetWidths(new float[] { 1f, 3f, 1.5f, 2f, 2f });
                itemsTable.SpacingAfter = 15f;

                // Header
                AddTableHeader(itemsTable, new string[] { "STT", "Sản phẩm", "SL", "Đơn giá", "Thành tiền" }, headerFont);

                // Items
                int stt = 1;
                foreach (var item in order.OrderItems)
                {
                    itemsTable.AddCell(new PdfPCell(new Phrase(stt++.ToString(), normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER });
                    itemsTable.AddCell(new PdfPCell(new Phrase(item.ProductName, normalFont)));
                    itemsTable.AddCell(new PdfPCell(new Phrase(item.Quantity.ToString(), normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER });
                    itemsTable.AddCell(new PdfPCell(new Phrase($"{item.PriceAtPurchase:N0}", normalFont)) { HorizontalAlignment = Element.ALIGN_RIGHT });
                    itemsTable.AddCell(new PdfPCell(new Phrase($"{item.SubTotal:N0}", normalFont)) { HorizontalAlignment = Element.ALIGN_RIGHT });
                }

                document.Add(itemsTable);

                // === Tổng tiền ===
                PdfPTable totalTable = new PdfPTable(2);
                totalTable.WidthPercentage = 50;
                totalTable.HorizontalAlignment = Element.ALIGN_RIGHT;
                totalTable.SpacingBefore = 10f;

                AddSummaryRow(totalTable, "Tạm tính:", $"{order.SubTotal:N0} VNĐ", normalFont, boldFont);
                AddSummaryRow(totalTable, "Giảm giá:", $"-{order.DiscountAmount:N0} VNĐ", normalFont, boldFont);
                AddSummaryRow(totalTable, "Thuế VAT:", $"{order.TaxAmount:N0} VNĐ", normalFont, boldFont);

                // Total row with color
                PdfPCell totalLabelCell = new PdfPCell(new Phrase("TỔNG CỘNG:", headerFont));
                totalLabelCell.Border = Rectangle.TOP_BORDER;
                totalLabelCell.BorderColor = BaseColor.BLACK;
                totalLabelCell.BorderWidth = 2f;
                totalLabelCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                totalLabelCell.Padding = 5f;

                PdfPCell totalValueCell = new PdfPCell(new Phrase($"{order.TotalAmount:N0} VNĐ", headerFont));
                totalValueCell.Border = Rectangle.TOP_BORDER;
                totalValueCell.BorderColor = BaseColor.BLACK;
                totalValueCell.BorderWidth = 2f;
                totalValueCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                totalValueCell.Padding = 5f;
                totalValueCell.BackgroundColor = new BaseColor(255, 245, 230);

                totalTable.AddCell(totalLabelCell);
                totalTable.AddCell(totalValueCell);

                document.Add(totalTable);

                // === Footer ===
                document.Add(new Paragraph(" ", normalFont)); // Spacing
                document.Add(new Chunk(line));

                Paragraph footer = new Paragraph("Cảm ơn quý khách! Hẹn gặp lại!", normalFont);
                footer.Alignment = Element.ALIGN_CENTER;
                footer.SpacingBefore = 10f;
                document.Add(footer);

                document.Close();

                System.Diagnostics.Debug.WriteLine($"✅ Invoice PDF created: {savePath}");
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"❌ Export Invoice Error: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Lấy chi tiết Order từ DB (bao gồm OrderItems)
        /// </summary>
        private OrderDTO GetOrderDetailsForInvoice(int orderUid)
        {
            try
            {
                using (var conn = new SqlConnection(Common.AppConstants.DBConnectDocker))
                {
                    conn.Open();

                    // Query Order
                    string orderQuery = @"
                        SELECT o.Uid, o.OrderDate, o.TotalAmount, o.SubTotal, o.TaxAmount, o.DiscountAmount,
                               o.Status, o.OrderNote, o.CreatedBy,
                               u.FullName AS CustomerName
                        FROM [Order] o
                        LEFT JOIN Users u ON o.UserUid = u.Uid
                        WHERE o.Uid = @OrderUid AND o.Deleted = 0";

                    var cmd = new SqlCommand(orderQuery, conn);
                    cmd.Parameters.AddWithValue("@OrderUid", orderUid);

                    var reader = cmd.ExecuteReader();
                    if (!reader.Read())
                    {
                        reader.Close();
                        return null;
                    }

                    var order = new OrderDTO
                    {
                        Uid = reader.GetInt32(0),
                        OrderDate = reader.GetDateTime(1),
                        TotalAmount = reader.GetDecimal(2),
                        SubTotal = reader.GetDecimal(3),
                        TaxAmount = reader.GetDecimal(4),
                        DiscountAmount = reader.GetDecimal(5),
                        Status = reader.GetString(6),
                        OrderNote = reader.IsDBNull(7) ? "" : reader.GetString(7),
                        CreatedBy = reader.IsDBNull(8) ? "" : reader.GetString(8),
                        CustomerName = reader.IsDBNull(9) ? "Guest" : reader.GetString(9),
                        OrderItems = new List<OrderItemDTO>()
                    };

                    reader.Close();

                    // Query OrderItems
                    string itemsQuery = @"
                        SELECT oi.Uid, oi.ProductUid, p.ProductName, p.Sku,
                               oi.Quantity, oi.PriceAtPurchase, oi.DiscountAmount, oi.SubTotal
                        FROM OrderItem oi
                        INNER JOIN Product p ON oi.ProductUid = p.Uid
                        WHERE oi.OrderUid = @OrderUid";

                    cmd = new SqlCommand(itemsQuery, conn);
                    cmd.Parameters.AddWithValue("@OrderUid", orderUid);

                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        order.OrderItems.Add(new OrderItemDTO
                        {
                            Uid = reader.GetInt32(0),
                            ProductUid = reader.GetInt32(1),
                            ProductName = reader.GetString(2),
                            ProductSku = reader.IsDBNull(3) ? "" : reader.GetString(3),
                            Quantity = reader.GetInt32(4),
                            PriceAtPurchase = reader.GetDecimal(5),
                            DiscountAmount = reader.GetDecimal(6),
                            SubTotal = reader.GetDecimal(7)
                        });
                    }

                    reader.Close();
                    return order;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error getting order details: {ex.Message}");
                return null;
            }
        }

        // === HELPER METHODS ===

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

        private void AddSummaryRow(PdfPTable table, string label, string value, 
            iTextSharp.text.Font labelFont, iTextSharp.text.Font valueFont)
        {
            PdfPCell labelCell = new PdfPCell(new Phrase(label, labelFont));
            labelCell.Border = Rectangle.NO_BORDER;
            labelCell.HorizontalAlignment = Element.ALIGN_RIGHT;
            labelCell.Padding = 3f;

            PdfPCell valueCell = new PdfPCell(new Phrase(value, valueFont));
            valueCell.Border = Rectangle.NO_BORDER;
            valueCell.HorizontalAlignment = Element.ALIGN_RIGHT;
            valueCell.Padding = 3f;

            table.AddCell(labelCell);
            table.AddCell(valueCell);
        }
    }
}