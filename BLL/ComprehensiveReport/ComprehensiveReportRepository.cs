using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.Entity;
using TSS.Models;

namespace TSS.BLL
{
    public class ComprehensiveReportRepository : Repository<ComprehensiveReport, int>
    {
        public ComprehensiveReport Get(int comprehensiveReportId)
        {
            using (var db = new Context())
            {
                return db.ComprehensiveReports
                    .Where(c => c.Id == comprehensiveReportId)
                    .Include(c => c.SpecialtyAnalysises
                        .Select(s => s.IndicatorAnalysises
                        .Select(i => i.Indicator.Specialty)))
                    .SingleOrDefault();
            }
        }

        public ComprehensiveReport Get(int reportYear, int reportMonth)
        {
            using (var db = new Context())
            {
                return db.ComprehensiveReports
                    .Where(c => c.ReportYear == reportYear
                        && c.ReportMonth == reportMonth)
                    .Include(c => c.SpecialtyAnalysises
                        .Select(s => s.IndicatorAnalysises
                        .Select(i => i.Indicator.Specialty)))
                    .SingleOrDefault();
            }
        }

        public bool IsExist(int reportYear, int reportMonth)
        {
            return Get(reportYear, reportMonth) != null;
        }
    }
}
