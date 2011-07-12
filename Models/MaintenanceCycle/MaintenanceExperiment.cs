using System;

namespace TSS.Models
{
    [Serializable]
    public class MaintenanceExperiment
    {
        public int Id { get; set; }
        public Guid ExperimentId { get; set; }
        public string CurrentCycle { get; set; }
        public DateTime? ExpectantTime { get; set; }
        public DateTime ActualTime { get; set; }

        public int MaintenanceCycleId { get; set; }
        public virtual MaintenanceCycle MaintenanceCycle { get; set; }
    }
}
