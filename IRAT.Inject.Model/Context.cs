using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace IRAT.Inject.Model
{
    public partial class Context : DbContext
    {
        public Context()
            : base("name=Context")
        {
        }

        public virtual DbSet<AuditTrail> AuditTrails { get; set; }
        public virtual DbSet<AuditType> AuditTypes { get; set; }
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<Log> Logs { get; set; }
        public virtual DbSet<Loglevel> Loglevels { get; set; }
        public virtual DbSet<MatchTable> MatchTables { get; set; }
        public virtual DbSet<Notification> Notifications { get; set; }
        public virtual DbSet<NotificationRF> NotificationRFs { get; set; }
        public virtual DbSet<NotificationType> NotificationTypes { get; set; }
        public virtual DbSet<Permission> Permissions { get; set; }
        public virtual DbSet<PermissionGroup> PermissionGroups { get; set; }
        public virtual DbSet<PermissionRf> PermissionRfs { get; set; }
        public virtual DbSet<RunResult> RunResults { get; set; }
        public virtual DbSet<SeqControlNumber> SeqControlNumbers { get; set; }
        public virtual DbSet<Setting> Settings { get; set; }
        public virtual DbSet<Status> Status { get; set; }
        public virtual DbSet<Tier> Tiers { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AuditTrail>()
                .Property(e => e.IPAddress)
                .IsUnicode(false);

            modelBuilder.Entity<AuditTrail>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<AuditType>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<AuditType>()
                .HasMany(e => e.AuditTrails)
                .WithRequired(e => e.AuditType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Company>()
                .Property(e => e.Gid)
                .IsUnicode(false);

            modelBuilder.Entity<Company>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Company>()
                .Property(e => e.Address)
                .IsUnicode(false);

            modelBuilder.Entity<Company>()
                .Property(e => e.City)
                .IsUnicode(false);

            modelBuilder.Entity<Company>()
                .Property(e => e.Parish)
                .IsUnicode(false);

            modelBuilder.Entity<Company>()
                .Property(e => e.Country)
                .IsUnicode(false);

            modelBuilder.Entity<Company>()
                .Property(e => e.State)
                .IsUnicode(false);

            modelBuilder.Entity<Company>()
                .Property(e => e.Tele)
                .IsUnicode(false);

            modelBuilder.Entity<Company>()
                .Property(e => e.Fax)
                .IsUnicode(false);

            modelBuilder.Entity<Company>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Company>()
                .Property(e => e.Website)
                .IsUnicode(false);

            modelBuilder.Entity<Company>()
                .Property(e => e.Blog)
                .IsUnicode(false);

            modelBuilder.Entity<Company>()
                .Property(e => e.Facebook)
                .IsUnicode(false);

            modelBuilder.Entity<Company>()
                .Property(e => e.Instagram)
                .IsUnicode(false);

            modelBuilder.Entity<Company>()
                .Property(e => e.Twitter)
                .IsUnicode(false);

            modelBuilder.Entity<Company>()
                .Property(e => e.Youtube)
                .IsUnicode(false);

            modelBuilder.Entity<Company>()
                .Property(e => e.GoogleP)
                .IsUnicode(false);

            modelBuilder.Entity<Company>()
                .Property(e => e.Image)
                .IsUnicode(false);

            modelBuilder.Entity<Company>()
                .Property(e => e.APIKey)
                .IsUnicode(false);

            modelBuilder.Entity<Company>()
                .Property(e => e.URL)
                .IsUnicode(false);

            modelBuilder.Entity<Company>()
                .Property(e => e.Username)
                .IsUnicode(false);

            modelBuilder.Entity<Company>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<Country>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Country>()
                .HasMany(e => e.Locations)
                .WithRequired(e => e.Country)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Country>()
                .HasMany(e => e.MatchTables)
                .WithRequired(e => e.Country)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Location>()
                .Property(e => e.Gid)
                .IsUnicode(false);

            modelBuilder.Entity<Location>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Location>()
                .Property(e => e.Address)
                .IsUnicode(false);

            modelBuilder.Entity<Location>()
                .Property(e => e.Telehpone)
                .IsUnicode(false);

            modelBuilder.Entity<Location>()
                .HasMany(e => e.Logs)
                .WithRequired(e => e.Location)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Location>()
                .HasMany(e => e.Users)
                .WithRequired(e => e.Location)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Log>()
                .Property(e => e.Gid)
                .IsUnicode(false);

            modelBuilder.Entity<Log>()
                .Property(e => e.Device)
                .IsUnicode(false);

            modelBuilder.Entity<Log>()
                .Property(e => e.Shortmessage)
                .IsUnicode(false);

            modelBuilder.Entity<Log>()
                .Property(e => e.Fullmessage)
                .IsUnicode(false);

            modelBuilder.Entity<Log>()
                .Property(e => e.IPaddress)
                .IsUnicode(false);

            modelBuilder.Entity<Log>()
                .Property(e => e.PageURL)
                .IsUnicode(false);

            modelBuilder.Entity<Log>()
                .Property(e => e.ReferrerURL)
                .IsUnicode(false);

            modelBuilder.Entity<Loglevel>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<MatchTable>()
                .Property(e => e.Gid)
                .IsUnicode(false);

            modelBuilder.Entity<MatchTable>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<MatchTable>()
                .Property(e => e.PickupZone)
                .IsUnicode(false);

            modelBuilder.Entity<MatchTable>()
                .Property(e => e.Supplier)
                .IsUnicode(false);

            modelBuilder.Entity<Notification>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Notification>()
                .HasMany(e => e.NotificationRFs)
                .WithRequired(e => e.Notification)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NotificationType>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<NotificationType>()
                .HasMany(e => e.Notifications)
                .WithRequired(e => e.NotificationType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Permission>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Permission>()
                .HasMany(e => e.PermissionRfs)
                .WithRequired(e => e.Permission)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PermissionGroup>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<PermissionGroup>()
                .HasMany(e => e.PermissionRfs)
                .WithRequired(e => e.PermissionGroup)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PermissionGroup>()
                .HasMany(e => e.Users)
                .WithRequired(e => e.PermissionGroup)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<RunResult>()
                .Property(e => e.Gid)
                .IsUnicode(false);

            modelBuilder.Entity<RunResult>()
                .Property(e => e.Filename)
                .IsUnicode(false);

            modelBuilder.Entity<RunResult>()
                .Property(e => e.Comments)
                .IsUnicode(false);

            modelBuilder.Entity<SeqControlNumber>()
                .Property(e => e.Gid)
                .IsUnicode(false);

            modelBuilder.Entity<Setting>()
                .Property(e => e.Gid)
                .IsUnicode(false);

            modelBuilder.Entity<Setting>()
                .Property(e => e.Label)
                .IsUnicode(false);

            modelBuilder.Entity<Setting>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Setting>()
                .Property(e => e.Value)
                .IsUnicode(false);

            modelBuilder.Entity<Setting>()
                .Property(e => e.ToolTip)
                .IsUnicode(false);

            modelBuilder.Entity<Status>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Status>()
                .HasMany(e => e.RunResults)
                .WithRequired(e => e.Status)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Tier>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Tier>()
                .HasMany(e => e.MatchTables)
                .WithRequired(e => e.Tier)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Gid)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Firstname)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Lastname)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Username)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Image)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.IPAddress)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.MatchTables)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.NotificationRFs)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Settings)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);
        }
    }
}
