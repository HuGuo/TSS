using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TSS.Models
{
    class Equipment
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }
        public string Code { get; set; }

        public virtual Specialty Specialty { get; set; }
        public virtual ICollection<EquipmentDetail> EquipmentDetails { get; set; }
    }
}
