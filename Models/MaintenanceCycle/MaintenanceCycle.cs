using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TSS.Models
{
    class MaintenanceCycle
    {
        public int Id { get; set; }
        public string EquipmentCode { get; set; }
        public string EquipmentName { get; set; }
        public string EquipmentModel { get; set; }
        public string MaintenanceType { get; set; }
        public DateTime InstallTime { get; set; }
        public string MaintenanceCycle { get; set; }

        public int EquipmentClassId { get; set; }
        public virtual EquipmentCalss EquipmentClass { get; set; }

        public virtual MaintenanceExperiment MaintenanceExperiment { get; set; }
    }
}
