namespace GMToolsWeb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ChapterTable")]
    public partial class ChapterTable
    {
        [Key]
        public int ChapterId { get; set; }

        [Required]
        [StringLength(60)]
        public string ChapterName { get; set; }

        public string ChapterDescription { get; set; }

        public int CampaignId { get; set; }

        public virtual CampaignTable CampaignTable { get; set; }
    }
}
