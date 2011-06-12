using System;
using System.Collections.Generic;

namespace TSS.Models
{
    public class Employee
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Sex { get; set; }

        public string SpecialtyId { get; set; }
        public virtual Specialty Specialty { get; set; }

        public virtual ICollection<Role> Roles { get; set; }
    }
}
