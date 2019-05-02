namespace GMToolsWeb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("EncounterTable")]
    public partial class EncounterTable
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EncounterTable()
        {
            ObstacleTable = new HashSet<ObstacleTable>();
        }

        [Key]
        public int EncounterId { get; set; }

        [Required]
        [StringLength(60)]
        public string EncounterName { get; set; }

        [Required]
        public string EncounterDescription { get; set; }

        public int UserId { get; set; }

        public virtual UserTable UserTable { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ObstacleTable> ObstacleTable { get; set; }
    }
}
