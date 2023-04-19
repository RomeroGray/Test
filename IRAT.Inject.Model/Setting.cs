namespace IRAT.Inject.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Setting")]
    public partial class Setting
    {
        public int SettingId { get; set; }

        [Required]
        [StringLength(500)]
        public string Gid { get; set; }

        public int? DisplayOrder { get; set; }

        public string Label { get; set; }

        public int ControlId { get; set; }

        [StringLength(300)]
        public string Name { get; set; }

        [StringLength(2000)]
        public string Value { get; set; }

        public string ToolTip { get; set; }

        public int UserId { get; set; }

        public DateTime DateCreated { get; set; }

        public virtual User User { get; set; }
    }
}
