﻿using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using TSS.Models;

namespace TSS.BLL
{
    public class IndicatorAnalysisRepository : Repository<IndicatorAnalysis>
    {
        public List<IndicatorAnalysis> GetMuchBySpecialtyAnalysisId(int specialtyAnalysisId)
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

        public List<IndicatorAnalysis> GetMuchByIndicatorId(int indicatorId)
        {
            using (var db = new Context())
            {
                return db.IndicatorAnalysises
                    .Where(i => i.IndicatorId == indicatorId)
                    .ToList();
            }
        }

        public bool IsExistOnIndicator(int indicatorId)
        {
            return GetMuchByIndicatorId(indicatorId).Count > 0;
        }
    }
}
