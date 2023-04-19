namespace IRAT.Inject.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Company")]
    public partial class Company
    {
        public int CompanyId { get; set; }

        [Required]
        [StringLength(500)]
        public string Gid { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(400)]
        public string Address { get; set; }

        [StringLength(400)]
        public string City { get; set; }

        [StringLength(400)]
        public string Parish { get; set; }

        [StringLength(400)]
        public string Country { get; set; }

        [StringLength(400)]
        public string State { get; set; }

        [StringLength(400)]
        public string Tele { get; set; }

        [StringLength(400)]
        public string Fax { get; set; }

        [StringLength(400)]
        public string Email { get; set; }

        [StringLength(400)]
        public string Website { get; set; }

        [StringLength(400)]
        public string Blog { get; set; }

        [StringLength(400)]
        public string Facebook { get; set; }

        [StringLength(400)]
        public string Instagram { get; set; }

        [StringLength(400)]
        public string Twitter { get; set; }

        [StringLength(400)]
        public string Youtube { get; set; }

        [StringLength(400)]
        public string GoogleP { get; set; }

        [StringLength(400)]
        public string Image { get; set; }

        public string APIKey { get; set; }

        [StringLength(400)]
        public string URL { get; set; }

        [StringLength(400)]
        public string Username { get; set; }

        [StringLength(400)]
        public string Password { get; set; }

        public bool Published { get; set; }

        public DateTime DateCreated { get; set; }
    }
}
