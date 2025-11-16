using System;
using System.Data;
using System.IO;
using ClosedXML.Excel;

namespace ThuVienUtc2.HelpersLib
{
    public static class ExportHelper
    {
        /// <summary>
        /// Xuất DataTable thành file .xlsx. Trả về true nếu thành công.
        /// </summary>
        public static bool ExportDataTableToExcel(DataTable table, string filePath)
        {
            if (table == null) throw new ArgumentNullException(nameof(table));
            if (string.IsNullOrWhiteSpace(filePath)) throw new ArgumentNullException(nameof(filePath));

            try
            {
                using (var wb = new XLWorkbook())
                {
                    var ws = wb.Worksheets.Add(table, "Report");
                    ws.Row(1).Style.Font.Bold = true;
                    ws.Columns().AdjustToContents();
                    wb.SaveAs(filePath);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}