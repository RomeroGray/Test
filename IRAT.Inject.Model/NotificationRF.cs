namespace IRAT.Inject.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("NotificationRF")]
    public partial class NotificationRF
    {
        public int NotificationRFId { get; set; }

        public int UserId { get; set; }

        public int NotificationId { get; set; }

        public bool IsRead { get; set; }

        public DateTime? DateRead { get; set; }

        public bool Delete { get; set; }

        public virtual Notification Notification { get; set; }

        public virtual User User { get; set; }
    }
}
