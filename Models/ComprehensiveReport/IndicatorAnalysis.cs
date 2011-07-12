using System;
using System.Collections.Generic;

namespace TSS.Models
{
    [Serializable]
    public class IndicatorAnalysis
    {
        public int Id { get; set; }
        public string StandardValue { get; set; }
        public string ActualValue { get; set; }
        public string Analysis { get; set; }

        public int SpecialtyAnalysisId { get; set; }
        public virtual SpecialtyAnalysis SpecialtyAnalysis { get; set; }

        public int IndicatorId { get; set; }
        public virtual Indicator Indicator { get; set; }
    }
}
