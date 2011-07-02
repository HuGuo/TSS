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
        protected override void Seed(Context context)
        {
            base.Seed(context);

            var systemManagementModules = new List<Module> {
                new Module { Name = "��Ա����", Url = "Employee" },
                new Module { Name = "�豸����", Url = "Equipment" },
                new Module { Name = "�������", Url = "Experiment" },
                new Module { Name = "����������", Url = "Workflow" },
                new Module { Name = "ģ�����", Url = "Module" }
            };

            var specialtyModules = new List<Module> {
                    new Module { Name = "�豸̨��", Url = "Equipment" },
                    new Module { Name = "ʵ�鱨��", Url = "Experiment" },
                    new Module { Name = "Ԥ������", Url = "MaintenanceCycle" },
                    new Module { Name = "�ල�±�", Url = "ComprehensiveReport" },
                    new Module { Name = "��Ա����", Url = "Certificate" },
                    new Module { Name = "��������", Url = "Document" }
            };

            new List<Module> {
                new Module { Id = 1, Name = "רҵ�ල", Submodules = specialtyModules },
                new Module { Id = 2, Name = "�ල��̬" },
                new Module { Id = 3, Name = "�ල��ϵ" },
                new Module { Id = 4, Name = "�ල����" },
                new Module { Id = 5, Name = "ϵͳ����", Url= "SystemManagement", Submodules = systemManagementModules }
            }.ForEach(m =>
            {
                context.Modules.Add(m);
            });

            new List<Specialty> {
                new Specialty { Id = "GHY-DC", Name = "������" },
                new Specialty { Id = "GHY-DN", Name = "����" },
                new Specialty { Id = "GHY-HX", Name = "��ѧ" },
                new Specialty { Id = "GHY-JDBH", Name = "�̵籣��" },
                new Specialty { Id = "GHY-JN", Name = "����" },
                new Specialty { Id = "GHY-JS", Name = "����" },
                new Specialty { Id = "GHY-JY", Name = "��Ե" },
                new Specialty { Id = "GHY-LC", Name = "����" },
                new Specialty { Id = "GHY-RG", Name = "�ȹ�" }
            }.ForEach(s =>
            {
                s.Modules = specialtyModules;
                context.Specialties.Add(s);
            });

            context.SaveChanges();
        }
    }
}