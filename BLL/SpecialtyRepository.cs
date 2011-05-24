using System.Collections.Generic;
using System.Linq;
using TSS.Models;

namespace TSS.BLL
{
    public class SpecialtyRepository
    {
        public SpecialtyRepository() { }

        public IList<Specialty> GetAll() 
        {
            using (Context db=new Context()) {
                return db.Specialties.ToList();
            }
        }

        public Specialty Get(string code) 
        {
            using (Context db=new Context()) {
                Specialty entity = db.Specialties.Find(code);
                return entity;
            }
        }
    }
}
