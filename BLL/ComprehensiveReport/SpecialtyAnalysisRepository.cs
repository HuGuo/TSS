using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.Entity;
using TSS.Models;

namespace TSS.BLL
{
    public class SpecialtyAnalysisRepository : Repository<SpecialtyAnalysis, int>
    {
        public override SpecialtyAnalysis Get(int specialtyAnalysisId)
        {
            return Context.SpecialtyAnalysises
                .Where(s => s.Id == specialtyAnalysisId)
                .Include(s => s.ComprehensiveReport)
                .Include(s => s.IndicatorAnalysises
                .Select(i => i.Indicator.Specialty))
                .SingleOrDefault();
        }

        public SpecialtyAnalysis GetForEdit(int specialtyAnalysisId)
        {
            return Context.SpecialtyAnalysises
                .Where(s => s.Id == specialtyAnalysisId)
                .SingleOrDefault();
        }

        public List<SpecialtyAnalysis> GetMuch(string specialtyId)
        {
            return Context.SpecialtyAnalysises
                .Where(s => s.SpecialtyId == specialtyId)
                .Include(s => s.ComprehensiveReport)
                .Include(s => s.IndicatorAnalysises
                    .Select(i => i.Indicator.Specialty))
                    .ToList();
        }

        public List<SpecialtyAnalysis> GetMuch(int year, int mon, string specialtyId)
        {
            return Context.SpecialtyAnalysises
                .Where(s => s.ComprehensiveReport.ReportYear == year
                    && s.ComprehensiveReport.ReportMonth == mon
                    && s.SpecialtyId == specialtyId)
                    .ToList();
        }

        public bool IsExistWhileAdd(int year, int mon, string specialtyId)
        {
            return GetMuch(year, mon, specialtyId).Count > 0;
        }

        public bool IsExistWhileAdd(SpecialtyAnalysis specialtyAnalysis)
        {
            return IsExistWhileAdd(specialtyAnalysis.ComprehensiveReport.ReportYear
                , specialtyAnalysis.ComprehensiveReport.ReportMonth,
                specialtyAnalysis.SpecialtyId);
        }

        public bool IsExistWhileEdit(int year, int mon, string specialtyId, int specialtyAnalysisId)
        {
            return GetMuch(year, mon, specialtyId)
                .Where(s => s.Id != specialtyAnalysisId)
                .ToList().Count > 0;
        }

        public bool IsExistWhileEdit(SpecialtyAnalysis specialtyAnalysis)
        {
            return IsExistWhileEdit(
                specialtyAnalysis.ComprehensiveReport.ReportYear,
                specialtyAnalysis.ComprehensiveReport.ReportMonth,
                specialtyAnalysis.SpecialtyId,
                specialtyAnalysis.Id);
        }
    }
}
