using System;

namespace TSS.Models
{
    public class ExpAttachment
    {
        public ExpAttachment() { }

        public Guid Id { get; set; }
        public string FielName { get; set; }

        public Guid ExperimentID { get; set; }
        public virtual Experiment Experiment { get; set; }
    }
}

