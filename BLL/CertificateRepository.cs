using System;
using System.Collections.Generic;
using System.Linq;
using TSS.Models;

namespace TSS.BLL
{
    public class CertificateRepository:Repository<Certificate,Guid>
    {
        public CertificateRepository() { }

        public IList<Certificate> GetBySpecialty(string id) 
        {
            using (Context db=new Context()) {
                return db.Certificates.Where(p => p.SpecialtyId == id).ToList();
            }
        }

        public IList<Certificate> GetBySpecialty(int pageIndex,int pageSize,string id,out int rowCount)
        {
            using (Context db = new Context()) {
                rowCount = db.Certificates.Count(p => p.SpecialtyId == id);
                int skipCount = pageSize * (pageIndex - 1);
                return db.Certificates.Where(p => p.SpecialtyId == id)
                    .Skip(skipCount)
                    .Take(pageSize).ToList();
            }
        }

        public IList<Certificate> Serach(string userNameOrNO,string specialty="") {
            using (Context db=new Context()) {
                var query = db.Certificates.Where(p => p.EpmloyeeName.Contains(userNameOrNO) || p.Number==userNameOrNO);
                if (!string.IsNullOrWhiteSpace(specialty)) {
                    query.Where(p => p.SpecialtyId == specialty);
                }
                return query.ToList();
            }
        }
    }
}
