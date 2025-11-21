using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class PermissionMatrixDTO
    {
        public int FunctionId { get; set; }
        public string FunctionName { get; set; }
        public int PermissionTypeId { get; set; }
        public string PermissionTypeName { get; set; }
        public Dictionary<int, bool> RolePermissions { get; set; } = new Dictionary<int, bool>();
    }
}
