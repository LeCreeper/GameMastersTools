namespace GMTWebservice
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LoreSubjectTable")]
    public partial class LoreSubjectTable
    {
        [Key]
        public int LoreSubjectId { get; set; }

        [Required]
        [StringLength(60)]
        public string LoreSubjectName { get; set; }

        [Required]
        public string LoreSubjectDescription { get; set; }

        public int LoreCategoryId { get; set; }

        public virtual LoreCategoryTable LoreCategoryTable { get; set; }
    }
}
