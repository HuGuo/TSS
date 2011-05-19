﻿using System.Collections.Generic;

namespace TSS.Models
{
    public class Specialty
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Equipment> Equipments { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
    }
}