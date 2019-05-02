namespace GMToolsWeb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LoreCategoryTable")]
    public partial class LoreCategoryTable
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LoreCategoryTable()
        {
            LoreSubjectTable = new HashSet<LoreSubjectTable>();
        }

        [Key]
        public int LoreCategoryId { get; set; }

        [Required]
        [StringLength(60)]
        public string LoreCategoryName { get; set; }

        [Required]
        [StringLength(450)]
        public string LoreCategoryDescription { get; set; }

        public int UserId { get; set; }

        public virtual UserTable UserTable { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LoreSubjectTable> LoreSubjectTable { get; set; }
    }
}
