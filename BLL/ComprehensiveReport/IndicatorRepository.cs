using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.Entity;
using TSS.Models;

namespace TSS.BLL
{
    public class IndicatorRepository : Repository<Indicator, int>
    {
        public override IList<Indicator> GetAll()
        {
            using (var db = new Context())
            {
                return db.Indicators
                   .Include(i => i.Specialty)
                   .ToList();
            }
        }

        public List<Indicator> GetBySpecialty(string specialtyId)
        {
            using (var db = new Context())
            {
                return db.Indicators
                    .Where(i => i.Specialty.Id == specialtyId)
                    .Include(i => i.Specialty)
                    .ToList();
            }
        }
    }
}
