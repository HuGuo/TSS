using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TSS.Models
{
    public class MaintenanceClass
    {
        public int Id { get; set; }
        public string equipmentClassName { get; set; }

        public string SpecialtyId { get; set; }
        public virtual Specialty Specialty { get; set; }

        public ICollection<MaintenanceCycle> MaintenanceCycles { get; set; }
    }
}
