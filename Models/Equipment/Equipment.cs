using System;
using System.Collections.Generic;

namespace TSS.Models
{
    public class Equipment
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        public string Code { get; set; }

        public string SpecialtyId { get; set; }
        public virtual Specialty Specialty { get; set; }

        public Guid EquipmentCategoryId { get; set; }
        public virtual EquipmentCategory EquipmentCategory { get; set; }

        public virtual ICollection<EquipmentDetail> EquipmentDetails { get; set; }
    }
}
