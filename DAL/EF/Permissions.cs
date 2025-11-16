namespace DAL.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Permissions
    {
        [Key]
        public int Uid { get; set; }

        public int RoleId { get; set; }

        public int FunctionId { get; set; }

        public int PermissionTypeId { get; set; }

        public bool Allowed { get; set; }

        public virtual Functions Functions { get; set; }

        public virtual PermissionTypes PermissionTypes { get; set; }

        public virtual Roles Roles { get; set; }
    }
}
