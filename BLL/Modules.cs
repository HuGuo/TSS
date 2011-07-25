using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using TSS.Models;

namespace TSS.BLL
{
    public class Modules : Repository<Module>
    {
        public IList<Module> GetRootModules()
        {
            return Context.Modules.Where(m =>
                m.ParentModuleId == null)
                .Include("Submodules.Submodules")
                .ToList();
        }

        public IList<Module> GetLeafModules()
        {
            return Context.Modules.Where(m =>
                !m.Submodules.Any(_m => _m.ParentModuleId == m.Id))
                .OrderBy(m => m.ParentModuleId)
                .ToList();
        }

        public IList<Module> GetAllWithOrder()
        {
            return GetAll().OrderBy(m => m.Id).ToList();
        }

        public string GetFullName(int id)
        {
            StringBuilder result = new StringBuilder();

            Walking(Get(id), m => m.Name, " - ", result);

            return result.ToString();
        }

        public string GetFullPath(int id)
        {
            StringBuilder result = new StringBuilder();

            Walking(Get(id), m => m.Path, "/", result);

            return result.ToString();
        }

        private void Walking(Module module, Func<Module, string> selector, string separator, StringBuilder output)
        {
            if (module.ParentModule == null) {
                output.Append(string.Format("{0}",
                    selector(module)));
            } else {
                Walking(module.ParentModule, selector, separator, output);
                output.Append(string.Format("{0}{1}",
                    separator, selector(module)));
            }
        }
    }
}
