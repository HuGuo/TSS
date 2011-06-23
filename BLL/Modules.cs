using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using TSS.Models;
using System.Data.Entity;

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
