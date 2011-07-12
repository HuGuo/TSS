using System;
using System.Collections.Generic;

namespace TSS.Models
{
    [Serializable]
    public class ComprehensiveReport
    {
        public int Id { get; set; }
        public int ReportYear { get; set; }
        public int ReportMonth { get; set; }
        public string Comment { get; set; }

        public virtual ICollection<SpecialtyAnalysis> SpecialtyAnalysises { get; set; }
    }
}
