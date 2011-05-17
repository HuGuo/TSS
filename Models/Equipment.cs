using System;
using System.Collections.Generic;

namespace TSS.Models
{
    class Equipment
    {
        public Guid EquipmentId { get; set; }

        public string Name { get; set; }
        
        public virtual ICollection<EquipmentDetail> EquipmentDetails { get; set; }
    }
}
