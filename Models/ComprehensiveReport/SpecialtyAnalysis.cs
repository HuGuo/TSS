using System.Collections.Generic;

namespace TSS.Models
{
    [Serializable]
    public class SpecialtyAnalysis
    {
        public int Id { get; set; }
        public string Analysis { get; set; }

        public string SpecialtyId { get; set; }
        public virtual Specialty Specialty { get; set; }

        public int ComprehensiveReportId { get; set; }
        public virtual ComprehensiveReport ComprehensiveReport { get; set; }

        public virtual IList<IndicatorAnalysis> IndicatorAnalysises { get; set; }
    }
}
