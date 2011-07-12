using System;
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

    public class Role
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Right> Rights { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
    }

    public class Right
    {
        public int Id { get; set; }
        public int ModuleId { get; set; }
        public Guid RoleId { get; set; }
        public virtual Role Role { get; set; }

        public int Permission { get; set; }
    }
}
