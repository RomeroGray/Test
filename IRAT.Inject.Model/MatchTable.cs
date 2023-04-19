namespace IRAT.Inject.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MatchTable")]
    public partial class MatchTable
    {
        public int MatchTableId { get; set; }

        [Required]
        public string Gid { get; set; }

        public int ProductNumber { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string PickupZone { get; set; }

        [Required]
        public string Supplier { get; set; }

        public int TierId { get; set; }

        public decimal Cost { get; set; }

        public int CountryId { get; set; }

        public int UserId { get; set; }

        public bool Deleted { get; set; }

        public DateTime DateCreated { get; set; }

        public virtual Country Country { get; set; }

        public virtual Tier Tier { get; set; }

        public virtual User User { get; set; }
    }
}
