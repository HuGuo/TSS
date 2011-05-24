using System;
using System.ComponentModel;
using System.Collections.Generic;

namespace TSS.Models
{
    public class MaintenanceExperiment
    {
        public int id { get; set; }
        public string CurrentCycle { get; set; }
        public DateTime ExperimentTime { get; set; }

        public string MaintenanceCycleId { get; set; }
        public virtual MaintenanceCycle MaintenanceCycle { get; set; }

        public Guid ExperimentId { get; set; }
        public virtual Experiment Experiment { get; set; }
    }
}
