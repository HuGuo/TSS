using System.Collections.Generic;
using System.Data.Entity;

namespace TSS.Models
{
    public class Context : DbContext
    {
        public IDbSet<Equipment> Equipments { get; set; }
        public IDbSet<EquipmentCategory> EquipmentCategories { get; set; }
        public IDbSet<Specialty> Specialties { get; set; }

        public IDbSet<Experiment> Experiments { get; set; }
        public IDbSet<ExpData> ExpData { get; set; }
        public IDbSet<ExpAttachment> ExpAttachments { get; set; }
        public IDbSet<ExpTemplate> ExpTemplates { get; set; }
        public IDbSet<ExpCategory> ExpCategories { get; set; }

        public IDbSet<Certificate> Certificates { get; set; }

        public Context()
            : base("TSS")
        {
             Database.SetInitializer<Context>(new DatabaseInitializer());        
        }
    }

    class DatabaseInitializer : DropCreateDatabaseIfModelChanges<Context>
    {
        protected override void Seed(Context context)
        {
            base.Seed(context);

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
            }.ForEach(s => context.Specialties.Add(s));

            context.SaveChanges();
        }
    }
}
