namespace IRAT.Inject.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RunResult")]
    public partial class RunResult
    {
        public int RunResultId { get; set; }

        [Required]
        public string Gid { get; set; }

        public int RunNo { get; set; }

        public int StatusId { get; set; }

        [Required]
        public string Filename { get; set; }

        public string Comments { get; set; }

        public DateTime DateCreated { get; set; }

        public bool Deleted { get; set; }

        public virtual Status Status { get; set; }
    }
}
