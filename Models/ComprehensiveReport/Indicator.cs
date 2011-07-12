using System;
using System.Collections.Generic;

namespace TSS.Models
{
    [Serializable]
    public class Indicator
    {
        public int Id { get; set; }
        public string IndicatorName { get; set; }
        public string IndivatorUnit { get; set; }

        public string SpecialtyId { get; set; }
        public virtual Specialty Specialty { get; set; }

        public virtual IList<IndicatorAnalysis> IndicatorAnalysises { get; set; }
    }
}
