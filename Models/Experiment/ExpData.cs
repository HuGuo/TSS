using System;
using System.ComponentModel.DataAnnotations;

namespace TSS.Models
{
    public class ExpData
    {
        public ExpData() { }
        
        public int Id { get; set; }
        public string GUID { get; set; }
        public decimal? Value { get; set; }

        public Guid ExperimentId { get; set; }
        public virtual Experiment Experiment { get; set; }
    }
}
