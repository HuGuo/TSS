using System.Data.Entity;

namespace TSS.Models
{
    public class Context : DbContext
    {
        public Context() : base("TSS") {}

        public DbSet<Equipment> Equipments { get; set; }
        public DbSet<Specialty> Specialties { get; set; }
        public IDbSet<ExpCategory> Expcategory { get; set; }
        public IDbSet<ExpTemplate> Exptemplates { get; set; }
        public IDbSet<Experiment> Experiments { get; set; }
        public IDbSet<ExpData> Expdatas { get; set; }
        public IDbSet<ExpAttachment> Expattachment { get; set; }
        public IDbSet<Credential> Credentials { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
