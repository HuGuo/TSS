using System.Collections.Generic;
using System.Data.Entity;

namespace TSS.Models
{
    public class Context : DbContext
    {
        public IDbSet<Equipment> Equipments { get; set; }
        public IDbSet<Specialty> Specitalties { get; set; }
        public IDbSet<ExpData> Expdatas { get; set; }
        public IDbSet<ExpAttachment> Expattachment { get; set; }
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
            }.ForEach(s => context.Specitalties.Add(s));

            context.SaveChanges();
        }
    }
}
