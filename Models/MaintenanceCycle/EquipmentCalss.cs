using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TSS.Models
{
    public class EquipmentCalss
    {
        public int Id { get; set; }
        public string equipmentClassName { get; set; }

        public string SpecialtyId { get; set; }
        public virtual Specialty Specialty { get; set; }
    }
}
