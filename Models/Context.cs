using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace TSS.Models
{
    public class Context : DbContext
    {
        public IDbSet<Specialty> Specialties { get; set; }

        public IDbSet<Module> Modules { get; set; }

        public IDbSet<Employee> Employees { get; set; }

        public IDbSet<Equipment> Equipments { get; set; }
        public IDbSet<EquipmentCategory> EquipmentCategories { get; set; }
        public IDbSet<EquipmentDetail> EquipmentDetails { get; set; }

        public IDbSet<ExpTemplate> ExpTemplates { get; set; }
        public IDbSet<Experiment> Experiments { get; set; }
        public IDbSet<ExpData> ExpData { get; set; }
        public IDbSet<ExpAttachment> ExpAttachments { get; set; }

        public IDbSet<Certificate> Certificates { get; set; }

        public IDbSet<Document> Documents { get; set; }

        public IDbSet<MaintenanceClass> MaintenanceClasses { get; set; }
        public IDbSet<MaintenanceCycle> MaintenanceCycles { get; set; }
        public IDbSet<MaintenanceExperiment> MaintenanceExperiments { get; set; }
        public IDbSet<Role> Roles { get; set; }
        public IDbSet<Right> Rights { get; set; }
        public IDbSet<ExpRecord> ExpRecords { get; set; }
        public IDbSet<ComprehensiveReport> ComprehensiveReports { get; set; }
        public IDbSet<SpecialtyAnalysis> SpecialtyAnalysises { get; set; }
        public IDbSet<IndicatorAnalysis> IndicatorAnalysises { get; set; }
        public IDbSet<Indicator> Indicators { get; set; }
        public Context()
            : base("TSS") {
            Database.SetInitializer<Context>(new DatabaseInitializer());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) {

            modelBuilder.Entity<ExpData>()
                .HasKey<int>(p => p.Id)
                .Property<int>(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).IsRequired();

            modelBuilder.Entity<Document>().Ignore(p => p.Childs);
            modelBuilder.Entity<ExpTemplate>().Property(p => p.HTML).HasColumnType("text");
            modelBuilder.Entity<Experiment>().Property(p => p.HTML).HasColumnType("text");
            modelBuilder.Entity<Right>().HasKey(p => p.Id).Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<ExpAttachment>().HasKey(p => p.Id).Property(p => p.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
        }
    }

    class DatabaseInitializer : DropCreateDatabaseIfModelChanges<Context>
    {
        protected override void Seed(Context context) {
            base.Seed(context);

        }
    }
}