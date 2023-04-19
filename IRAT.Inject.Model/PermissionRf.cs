namespace IRAT.Inject.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PermissionRf")]
    public partial class PermissionRf
    {
        public int PermissionRfId { get; set; }

        public int PermissionGroupId { get; set; }

        public int PermissionId { get; set; }

        public int? UserId { get; set; }

        public DateTime DateCreated { get; set; }

        public bool Deleted { get; set; }

        public virtual Permission Permission { get; set; }

        public virtual PermissionGroup PermissionGroup { get; set; }

        public virtual User User { get; set; }
    }
}
