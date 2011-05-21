using System.Collections.Generic;
using System.Linq;
using TSS.Models;

namespace TSS.BLL
{
    public static class Specialty
    {
        public static IList<TSS.Models.Specialty> GetAll() 
        {
            using (var context = new Context()) {
                return context.Specialties.ToList();
            }
        }

        public static TSS.Models.Specialty Get(string id)
        {
            using (var context = new Context()) {
                return context.Specialties.First(s =>
                    s.Id.Equals(id));
            }
        }
    }
}
