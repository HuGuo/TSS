using System;
using System.Collections.Generic;

namespace TSS.Models
{
    public class Role
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Right> Rights { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
