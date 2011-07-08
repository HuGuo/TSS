using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TSS.Models
{
    public class MaintenanceCycle
    {
        public int Id { get; set; }
        public string Cycle { get; set; }
        public Guid EquipmentId { get; set; }
        public string EquipmentModel { get; set; }
        public string MaintenanceType { get; set; }
        public DateTime? InstallTime { get; set; }


        public int MaintenanceClassId { get; set; }
        public virtual MaintenanceClass MaintenanceCalss { get; set; }

        public virtual ICollection<MaintenanceExperiment> MaintenanceExperiments { get; set; }
    }
}
