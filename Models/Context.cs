using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace TSS.Models
{
    public class Context : DbContext
    {
        public IDbSet<Specialty> Specialties { get; set; }

        public IDbSet<Module> Modules { get; set; }

        public IDbSet<Employee> Employees { get; set; }

        public IDbSet<Equipment> Equipments { get; set; }
        public IDbSet<EquipmentCategory> EquipmentCategories { get; set; }

        public IDbSet<ExpTemplate> ExpTemplates { get; set; }
        public IDbSet<Experiment> Experiments { get; set; }
        public IDbSet<ExpData> ExpData { get; set; }
        public IDbSet<ExpAttachment> ExpAttachments { get; set; }

        public IDbSet<Certificate> Certificates { get; set; }

        public IDbSet<Document> Documents { get; set; }

        public IDbSet<MaintenanceClass> MaintenanceClasses { get; set; }
        public IDbSet<MaintenanceCycle> MaintenanceCycles { get; set; }
        public IDbSet<MaintenanceExperiment> MaintenanceExperiments { get; set; }

        public IDbSet<Indicator> Indicators { get; set; }
        public IDbSet<ComprehensiveReport> ComprehensiveReports { get; set; }
        public IDbSet<SpecialtyAnalysis> SpecialtyAnalysises { get; set; }
        public IDbSet<IndicatorAnalysis> IndicatorAnalysises { get; set; }

        public Context()
            : base("TSS")
        {
            Database.SetInitializer<Context>(new DatabaseInitializer());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ExpData>()
                .HasKey<int>(p => p.Id)
                .Property<int>(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).IsRequired();

            modelBuilder.Entity<Document>().Ignore(p => p.Childs);
            // modelBuilder.Entity<ExpTemplate>().Property(p => p.HTML).HasColumnType("text");
            // modelBuilder.Entity<Experiment>().Property(p => p.HTML).HasColumnType("text");
        }
    }

    class DatabaseInitializer : CreateDatabaseIfNotExists<Context>
    {
        protected override void Seed(Context context)
        {
            base.Seed(context);

            var modules = new List<Module> {
                    new Module { Id = "1", Name = "设备台帐", Url = "Equipment" },
                    new Module { Id = "2", Name = "实验报告", Url = "Experiment" },
                    new Module { Id = "3", Name = "预试周期", Url = "MaintenanceCycle" },
                    new Module { Id = "4", Name = "监督月报", Url = "Report" },
                    new Module { Id = "5", Name = "人员资质", Url = "Certificate" },
                    new Module { Id = "6", Name = "档案资料", Url = "Document" },
                    new Module{ Id="7", Name="指标分析",Url="ComprehensiveReport"}
            };

            new List<Specialty> {
                new Specialty { Id = "GHY-DC", Name = "电测计量" },
                new Specialty { Id = "GHY-DN", Name = "电能" },
                new Specialty { Id = "GHY-HX", Name = "化学" },
                new Specialty { Id = "GHY-JDBH", Name = "继电保护" },
                new Specialty { Id = "GHY-JN", Name = "节能" },
                new Specialty { Id = "GHY-JS", Name = "金属" },
                new Specialty { Id = "GHY-JY", Name = "绝缘" },
                new Specialty { Id = "GHY-LC", Name = "励磁" },
                new Specialty { Id = "GHY-RG", Name = "热工" }
            }.ForEach(s =>
            {
                s.Modules = modules;
                context.Specialties.Add(s);
            });

            context.SaveChanges();
        }
    }
}
