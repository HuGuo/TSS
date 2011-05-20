using System;
using System.Collections.Generic;

namespace TSS.Models
{
    public class EquipmentCategory
    {
        public Guid Id { get; set; }

        public String Name { get; set; }

        public Guid? ParentId { get; set; }

        public virtual ICollection<Equipment> Equipments { get; set; }
    }
}
