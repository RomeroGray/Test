namespace IRAT.Inject.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SeqControlNumber")]
    public partial class SeqControlNumber
    {
        [Key]
        public int SeqControlId { get; set; }

        [Required]
        public string Gid { get; set; }

        public int RunNo { get; set; }
    }
}
