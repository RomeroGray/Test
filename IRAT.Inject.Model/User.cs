namespace IRAT.Inject.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("User")]
    public partial class User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            AuditTrails = new HashSet<AuditTrail>();
            Logs = new HashSet<Log>();
            MatchTables = new HashSet<MatchTable>();
            NotificationRFs = new HashSet<NotificationRF>();
            PermissionRfs = new HashSet<PermissionRf>();
            Settings = new HashSet<Setting>();
        }

        public int UserId { get; set; }

        [Required]
        [StringLength(500)]
        public string Gid { get; set; }

        [Required]
        [StringLength(250)]
        public string Firstname { get; set; }

        [Required]
        [StringLength(250)]
        public string Lastname { get; set; }

        [Required]
        [StringLength(250)]
        public string Username { get; set; }

        [Required]
        [StringLength(250)]
        public string Password { get; set; }

        [Required]
        [StringLength(250)]
        public string Email { get; set; }

        [StringLength(500)]
        public string Image { get; set; }

        public int PermissionGroupId { get; set; }

        public int LocationId { get; set; }

        public bool Status { get; set; }

        public bool Islocked { get; set; }

        public DateTime? LastlockedDate { get; set; }

        public bool LoginStatus { get; set; }

        public int? FailedLoginAttempts { get; set; }

        public DateTime? LastDateLogin { get; set; }

        public DateTime? LastPasswordChangedDate { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        [StringLength(150)]
        public string IPAddress { get; set; }

        public bool Deleted { get; set; }

        public DateTime DateCreated { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AuditTrail> AuditTrails { get; set; }

        public virtual Location Location { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Log> Logs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MatchTable> MatchTables { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NotificationRF> NotificationRFs { get; set; }

        public virtual PermissionGroup PermissionGroup { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PermissionRf> PermissionRfs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Setting> Settings { get; set; }
    }
}
