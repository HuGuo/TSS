using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using TSS.Models;

namespace TSS.BLL
{
    public class Modules : Repository<Module>
    {
        public IList<Module> GetRootModules()
        {
            return Context.Modules.Where(m =>
                m.ParentModuleId == null &&
                m.Id != 1).Include("Submodules.Submodules").ToList();
        }
    }
}
