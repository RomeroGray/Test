namespace IRAT.Inject.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AuditTrail")]
    public partial class AuditTrail
    {
        public int AuditTrailId { get; set; }

        public int? UserId { get; set; }

        public int AuditTypeId { get; set; }

        public DateTime? Date { get; set; }

        [Required]
        [StringLength(200)]
        public string IPAddress { get; set; }

        [Required]
        public string Description { get; set; }

        public virtual AuditType AuditType { get; set; }

        public virtual User User { get; set; }
    }
}
