using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using TSS.Models;


namespace TSS.BLL
{
    public class Specialties : Repository<Specialty,string>
    {
        public IList<Specialty> GetAllWithModules()
        {
            return Context.Specialties.Include("Modules").ToList();
        }

        public IList<Module> GetModules(string specialtyId)
        {
            IList<Module> result = null;

            var s = Context.Specialties.Find(specialtyId);
            if (s != null) {
                result = s.Modules.ToList();
            }

            return result;
        }

        public IList<Module> GetNotHasModules(string spcialtyId)
        {
            IList<Module> result = null;

            var s = Context.Specialties.Find(spcialtyId);
            var m = Context.Modules.Find(1); // 只获取专业监督
            if (s != null && m != null) {
                result = m.Submodules.Except(s.Modules).ToList();
            }

            return result;
        }

        public void AddModule(string specialtyId, string moduleId)
        {
            if (!string.IsNullOrEmpty(specialtyId) &&
                !string.IsNullOrEmpty(moduleId)) {
                var s = Context.Specialties.Find(specialtyId);
                var m = Context.Modules.Find(int.Parse(moduleId));
                if (s != null && m != null) {
                    s.Modules.Add(m);

                    Context.SaveChanges();
                }
            }
        }

        public void RemoveModule(string specialtyId, string moduleId)
        {
             if (!string.IsNullOrEmpty(specialtyId) &&
                !string.IsNullOrEmpty(moduleId)) {
                var s = Context.Specialties.Find(specialtyId);
                var m = Context.Modules.Find(int.Parse(moduleId));
                if (s != null && m != null) {
                    s.Modules.Remove(m);

                    Context.SaveChanges();
                }
            }
        }
    }
}
