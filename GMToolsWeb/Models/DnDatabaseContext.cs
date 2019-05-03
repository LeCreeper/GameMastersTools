namespace GMToolsWeb.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class DnDatabaseContext : DbContext
    {
        public DnDatabaseContext()
            : base("name=DnDatabaseContext")
        {
            base.Configuration.ProxyCreationEnabled = false;
        }

        public virtual DbSet<CampaignTable> CampaignTable { get; set; }
        public virtual DbSet<ChapterTable> ChapterTable { get; set; }
        public virtual DbSet<EncounterTable> EncounterTable { get; set; }
        public virtual DbSet<ItemTable> ItemTable { get; set; }
        public virtual DbSet<LocationTable> LocationTable { get; set; }
        public virtual DbSet<LoreCategoryTable> LoreCategoryTable { get; set; }
        public virtual DbSet<LoreSubjectTable> LoreSubjectTable { get; set; }
        public virtual DbSet<NPCTable> NPCTable { get; set; }
        public virtual DbSet<ObstacleTable> ObstacleTable { get; set; }
        public virtual DbSet<PCTable> PCTable { get; set; }
        public virtual DbSet<UserTable> UserTable { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CampaignTable>()
                .HasMany(e => e.ChapterTable)
                .WithRequired(e => e.CampaignTable)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EncounterTable>()
                .HasMany(e => e.ObstacleTable)
                .WithRequired(e => e.EncounterTable)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<LoreCategoryTable>()
                .HasMany(e => e.LoreSubjectTable)
                .WithRequired(e => e.LoreCategoryTable)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserTable>()
                .Property(e => e.UserName)
                .IsUnicode(false);

            modelBuilder.Entity<UserTable>()
                .Property(e => e.UserPassword)
                .IsUnicode(false);

            modelBuilder.Entity<UserTable>()
                .HasMany(e => e.CampaignTable)
                .WithRequired(e => e.UserTable)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserTable>()
                .HasMany(e => e.EncounterTable)
                .WithRequired(e => e.UserTable)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserTable>()
                .HasMany(e => e.ItemTable)
                .WithRequired(e => e.UserTable)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserTable>()
                .HasMany(e => e.LocationTable)
                .WithRequired(e => e.UserTable)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserTable>()
                .HasMany(e => e.LoreCategoryTable)
                .WithRequired(e => e.UserTable)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserTable>()
                .HasMany(e => e.NPCTable)
                .WithRequired(e => e.UserTable)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserTable>()
                .HasMany(e => e.PCTable)
                .WithRequired(e => e.UserTable)
                .WillCascadeOnDelete(false);
        }
    }
}
