namespace GMTWebservice
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LocationTable")]
    public partial class LocationTable
    {
        [Key]
        public int LocationId { get; set; }

        [Required]
        [StringLength(60)]
        public string LocationName { get; set; }

        [Required]
        public string LocationDescription { get; set; }

        public int UserId { get; set; }

        public virtual UserTable UserTable { get; set; }
    }
}
