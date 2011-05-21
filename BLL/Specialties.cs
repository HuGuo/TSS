using System.Collections.Generic;
using System.Linq;
using TSS.Models;

namespace TSS.BLL
{
    public static class Specialties
    {
        public static TSS.Models.Specialty Get(string id)
        {
            using (var context = new Context()) {
                return context.Specialties.Find(id);
            }
        }

        public static IList<TSS.Models.Specialty> GetAll() 
        {
            using (var context = new Context()) {
                return context.Specialties.ToList();
            }
        }
    }
}
