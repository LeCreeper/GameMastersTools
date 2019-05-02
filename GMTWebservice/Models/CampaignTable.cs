namespace GMTWebservice
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CampaignTable")]
    public partial class CampaignTable
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CampaignTable()
        {
            ChapterTable = new HashSet<ChapterTable>();
        }

        [Key]
        public int CampaignId { get; set; }

        [Required]
        [StringLength(60)]
        public string CampaignName { get; set; }

        [StringLength(450)]
        public string CampaignDescription { get; set; }

        public int UserId { get; set; }

        public virtual UserTable UserTable { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChapterTable> ChapterTable { get; set; }
    }
}
