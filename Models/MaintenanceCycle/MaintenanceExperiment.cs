using System;
using System.ComponentModel;

namespace TSS.Models
{
    public class MaintenanceExperiment
    {
        public int id { get; set; }
        public string MaintenanceCycle { get; set; }
        public DateTime ExperimentTime { get; set; }
        
        public string EquipmentId { get; set; }
        public virtual Equipment Equipment { get; set; }

        public string ExperimentId { get; set; }
        public virtual Experiment Experiment { get; set; }
    }
}
