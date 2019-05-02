namespace GMTWebservice
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ObstacleTable")]
    public partial class ObstacleTable
    {
        [Key]
        public int ObstacleId { get; set; }

        [Required]
        [StringLength(60)]
        public string ObstacleName { get; set; }

        [Required]
        public string ObstacleDescription { get; set; }

        public int EncounterId { get; set; }

        public virtual EncounterTable EncounterTable { get; set; }
    }
}
