using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.Entity;
using TSS.Models;

namespace TSS.BLL
{
    public class IndicatorAnalysisRepository : Repository<IndicatorAnalysis, int>
    {
        public List<IndicatorAnalysis> GetMuch(int specialtyAnalysisId)
        {
            using (var db = new Context())
            {
                return db.IndicatorAnalysises
                    .Where(i => i.SpecialtyAnalysisId == specialtyAnalysisId)
                    .Include(i => i.SpecialtyAnalysis)
                    .Include(i => i.Indicator.Specialty)
                    .ToList();
            }
        }
    }
}
