using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class PermissionDTO
    {
        public int RoleId { get; set; } 
        public int FunctionId { get; set; }
        public int PermissionTypeId { get; set; }
        public bool Allowed { get; set; }
    }
}
