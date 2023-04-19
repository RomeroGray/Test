namespace IRAT.Inject.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Location")]
    public partial class Location
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Location()
        {
            Logs = new HashSet<Log>();
            Users = new HashSet<User>();
        }

        public int LocationId { get; set; }

        [Required]
        public string Gid { get; set; }

        [Required]
        [StringLength(50)]
        public string Description { get; set; }

        [StringLength(140)]
        public string Address { get; set; }

        public int CountryId { get; set; }

        [StringLength(200)]
        public string Telehpone { get; set; }

        public DateTime DateCreated { get; set; }

        public bool Deleted { get; set; }

        public virtual Country Country { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Log> Logs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<User> Users { get; set; }
    }
}
