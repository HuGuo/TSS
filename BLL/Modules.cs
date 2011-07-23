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
        public IList<Module> GetSpecialtyModules()
        {
            return Context.Modules.Where(m =>
                m.ParentModuleId == 1)
                .Include("Submodules")
                .ToList();
        }

        public IList<Module> GetRootModulesWithoutSpecialty()
        {
            return Context.Modules.Where(m =>
                m.ParentModuleId == null && m.Id != 1)
                .Include("Submodules.Submodules")
                .ToList();
        }

        public IList<Module> GetLeafModules()
        {
            return Context.Modules.Where(m =>
                !m.Submodules.Any(_m => _m.ParentModuleId == m.Id))
                .ToList();
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

        private void Walking(Module m, Func<Module, string> accessor, string separator, StringBuilder output)
        {
            if (m.ParentModule == null) {
                output.Append(string.Format("{0}",
                    accessor(m)));
            } else {
                Walking(m.ParentModule, accessor, separator, output);
                output.Append(string.Format("{0}{1}",
                    separator, accessor(m)));
            }
        }
    }
}
