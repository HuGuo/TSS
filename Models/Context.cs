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
        public IDbSet<ComprehensiveReport> ComprehensiveReports { get; set; }
        public IDbSet<SpecialtyAnalysis> SpecialtyAnalysises { get; set; }
        public IDbSet<IndicatorAnalysis> IndicatorAnalysises { get; set; }
        public IDbSet<Indicator> Indicators { get; set; }

        public IDbSet<Role> Roles { get; set; }
        public IDbSet<Right> Rights { get; set; }
        public IDbSet<ExpRecord> ExpRecords { get; set; }

        public IDbSet<SupervisionNewType> SupervisionNewTypes { get; set; }
        public IDbSet<SupervisionNew> SupervisionNews { get; set; }

        public Context()
            : base("TSS")
        {
            Database.SetInitializer<Context>(new DatabaseInitializer());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

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

    class DatabaseInitializer : CreateDatabaseIfNotExists<Context>
    {
        protected override void Seed(Context context)
        {
            base.Seed(context);

            var systemManagementModules = new List<Module> {
                new Module { Id=12, Name = "人员管理", Url = "Employee" },
                new Module { Id=13, Name = "设备管理", Url = "Equipment" },
                new Module { Id=14, Name = "实验管理", Url = "Experiment" },
                new Module { Id=15, Name = "工作流管理", Url = "Workflow" },
                new Module { Id=16, Name = "模块管理", Url = "Module" }
            };

            var specialtyModules = new List<Module> {
                new Module { Name = "设备台帐", Url = "Equipment" },
                new Module { Name = "实验报告", Url = "Experiment" },
                new Module { Name = "预试周期", Url = "MaintenanceCycle" },
                new Module { Name = "监督月报", Url = "Report" },
                new Module { Name = "人员资质", Url = "Certificate" },
                new Module { Name = "档案资料", Url = "Document" }
            };

            new List<Module> {
                new Module { Id = 1, Name = "专业监督", Submodules = specialtyModules },
                new Module { Id = 2, Name = "监督动态" },
                new Module { Id = 3, Name = "监督体系" },
                new Module { Id = 4, Name = "监督管理" },
                new Module { Id = 5, Name = "系统管理", Url= "SystemManagement", Submodules = systemManagementModules }
            }.ForEach(m => {
                context.Modules.Add(m);
            });

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
            }.ForEach(s => {
                s.Modules = specialtyModules;
                context.Specialties.Add(s);
            });

            new List<SupervisionNewType>{
                new SupervisionNewType{ Id=1, TypeName="计划总结"},
                new SupervisionNewType{ Id=1, TypeName="会议纪要"},
                new SupervisionNewType{ Id=1, TypeName="提示"},
                new SupervisionNewType{ Id=1, TypeName="技术监督综合信息"},
                new SupervisionNewType{ Id=1, TypeName="发电营运部提示"},
                new SupervisionNewType{ Id=1, TypeName="技术中心专业研究提示"},
                new SupervisionNewType{ Id=1, TypeName="技术监督简报"},
                new SupervisionNewType{ Id=1, TypeName="技术监督标准与新技术"}
            }.ForEach(s=>{
                context.SupervisionNewTypes.Add(s);
            });


            context.SaveChanges();
        }
    }
}