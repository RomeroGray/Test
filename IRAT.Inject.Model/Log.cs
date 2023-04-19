namespace IRAT.Inject.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Log")]
    public partial class Log
    {
        public int Id { get; set; }

        [Required]
        [StringLength(500)]
        public string Gid { get; set; }

        public int? UserId { get; set; }

        public int? LoglevelId { get; set; }

        public int LocationId { get; set; }

        public string Device { get; set; }

        public string Shortmessage { get; set; }

        public string Fullmessage { get; set; }

        public string IPaddress { get; set; }

        public string PageURL { get; set; }

        public string ReferrerURL { get; set; }

        public DateTime DateCreated { get; set; }

        public virtual Location Location { get; set; }

        public virtual Loglevel Loglevel { get; set; }

        public virtual User User { get; set; }
    }
}
