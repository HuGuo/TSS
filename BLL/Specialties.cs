using System.Collections.Generic;
using System.Linq;
using TSS.Models;

namespace TSS.BLL
{
    public class Specialties : Repository<Specialty, string>
    {
        public IList<Module> GetModules(string specialtyId)
        {
            return Context.Specialties.Find(specialtyId).Modules.ToList();
        }

        public IList<Module> GetNotHasModules(string spcialtyId)
        {
            return Context.Modules.ToList().Except(Context.Specialties.Find(spcialtyId).Modules).ToList();
        }

        public void AddModule(string specialtyId, int moduleId)
        {
            Context.Specialties.Find(specialtyId).Modules.Add(
                Context.Modules.Find(moduleId));
            Context.SaveChanges();
        }

        public void RemoveModule(string specialtyId, int moduleId)
        {
            Context.Specialties.Find(specialtyId).Modules.Remove(
                Context.Modules.Find(moduleId));
            Context.SaveChanges();
        }
    }
}
