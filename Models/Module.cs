using System.Collections.Generic;

namespace TSS.Models
{
    public class Module
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }

        public int? ParentModuleId { get; set; }
        public virtual Module ParentModule { get; set; }

        public virtual ICollection<Module> Submodules { get; set; }

        public virtual ICollection<Specialty> Specialties { get; set; }
    }
}