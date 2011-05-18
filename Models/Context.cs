using System.Data.Entity;

namespace TSS.Models
{
    public class Context : DbContext
    {
        public Context() : base("TSS") {}

        public DbSet<Equipment> Equipments { get; set; }
        public DbSet<Specialty> Specialties { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
