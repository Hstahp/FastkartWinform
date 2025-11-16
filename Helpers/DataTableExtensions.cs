using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper
{
    // Helper extension to safely CopyToDataTable when sequence may be empty
    public static class DataTableExtensions
    {
        public static DataTable CopyToDataTableOrEmpty(this IEnumerable<DataRow> rows)
        {
            if (rows == null) return new DataTable();
            var list = rows.ToList();
            if (list.Count == 0) return new DataTable();
            return list.CopyToDataTable();
        }
    }
}
