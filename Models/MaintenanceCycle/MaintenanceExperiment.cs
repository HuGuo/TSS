using System;
using System.ComponentModel;
using System.Collections.Generic;

namespace TSS.Models
{
    public class MaintenanceExperiment
    {
        public int Id { get; set; }
        public Guid ExperimentId { get; set; }
        public string CurrentCycle { get; set; }
        public DateTime ExperimentTime { get; set; }

        public int MaintenanceCycleId { get; set; }
        public virtual MaintenanceCycle MaintenanceCycle { get; set; }
    }
}
