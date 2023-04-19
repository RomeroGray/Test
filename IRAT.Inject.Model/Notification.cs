namespace IRAT.Inject.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Notification")]
    public partial class Notification
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Notification()
        {
            NotificationRFs = new HashSet<NotificationRF>();
        }

        public int NotificationId { get; set; }

        public int NotificationTypeId { get; set; }

        [Required]
        [StringLength(200)]
        public string Description { get; set; }

        public DateTime DateCreate { get; set; }

        public bool Delete { get; set; }

        public virtual NotificationType NotificationType { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NotificationRF> NotificationRFs { get; set; }
    }
}
