using System;
using System.Collections.Generic;

namespace TSS.Models
{
    public class EquipmentCategory
    {
        public Guid Id { get; set; }

        public String Name { get; set; }

        public Guid? ParentCategoryId { get; set; }
        public virtual EquipmentCategory ParentCategory { get; set; }
        public virtual ICollection<EquipmentCategory> Subcategories { get; set; }

        public virtual ICollection<Equipment> Equipments { get; set; }
    }
}
