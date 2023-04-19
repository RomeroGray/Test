namespace IRAT.Inject.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Country")]
    public partial class Country
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Country()
        {
            Locations = new HashSet<Location>();
            MatchTables = new HashSet<MatchTable>();
        }

        public int CountryId { get; set; }

        [Required]
        [StringLength(100)]
        public string Description { get; set; }

        [StringLength(2)]
        public string TwoLetterIsoCode { get; set; }

        [StringLength(3)]
        public string ThreeLetterIsoCode { get; set; }

        public bool AllowsBilling { get; set; }

        public bool AllowsShipping { get; set; }

        public int NumericIsoCode { get; set; }

        public bool Published { get; set; }

        public int DisplayOrder { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Location> Locations { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MatchTable> MatchTables { get; set; }
    }
}
