using System;
using System.Collections.Generic;
using System.Linq;
using TSS.Models;

namespace TSS.BLL
{
    public class CertificateRepository : Repository<Certificate>
    {
        public IList<Certificate> GetBySpecialty(string id) {
            return Context.Certificates.Where(p => p.SpecialtyId == id).ToList();
        }

        public IList<Certificate> GetBySpecialty(int pageIndex , int pageSize , string id , out int rowCount) {
            rowCount = Context.Certificates.Count(p => p.SpecialtyId == id);
            int skipCount = pageSize * (pageIndex - 1);
            return Context.Certificates.Where(p => p.SpecialtyId == id)
                .Skip(skipCount)
                .Take(pageSize).ToList();
        }

        public IList<Certificate> Serach(string userNameOrNO , string specialty = "") {
            var query = Context.Certificates.Where(p => p.EpmloyeeName.Contains(userNameOrNO) || p.Number == userNameOrNO);
            if (!string.IsNullOrWhiteSpace(specialty)) {
                query = query.Where(p => p.SpecialtyId == specialty);
            }
            return query.ToList();
        }
    }
}
