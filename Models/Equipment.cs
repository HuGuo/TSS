using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TSS.Models
{
    [Table("EQUIPMENT")]
    public class Equipment
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        public string Code { get; set; }

        public Specialty Specialty { get; set; }

        public virtual ICollection<EquipmentDetail> EquipmentDetails { get; set; }
    }
}
