namespace GMToolsWeb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ItemTable")]
    public partial class ItemTable
    {
        [Key]
        public int ItemId { get; set; }

        [Required]
        [StringLength(60)]
        public string ItemName { get; set; }

        [Required]
        public string ItemDescription { get; set; }

        public int UserId { get; set; }

        public virtual UserTable UserTable { get; set; }
    }
}
