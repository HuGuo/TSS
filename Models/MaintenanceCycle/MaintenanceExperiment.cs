using System;
using System.Collections.Generic;

namespace TSS.Models
{
    [Serializable]
    public class MaintenanceExperiment
    {
        public int Id { get; set; }
        public Guid ExperimentId { get; set; }
        public string CurrentCycle { get; set; }
        public DateTime NextExpTime { get; set; }
        public DateTime? LastExpTime { get; set; }

        public int MaintenanceCycleId { get; set; }
        public virtual MaintenanceCycle MaintenanceCycle { get; set; }
    }
}
