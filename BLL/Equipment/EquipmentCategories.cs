using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TSS.Models;

namespace TSS.BLL
{
    public class EquipmentCategories : Repository<EquipmentCategory, Guid>
    {
        public void Add(string name, string parentId)
        {
            Add(new TSS.Models.EquipmentCategory {
                Id = Guid.NewGuid(),
                Name = name,
                ParentCategory = Context.EquipmentCategories.Find(Guid.Parse(parentId))
            });
        }

        public StringBuilder GetXml()
        {
            var result = new StringBuilder();

            result.Append("<?xml version=\"1.0\"?>\n\n");

            result.Append("<Categories>");
            result.Append(Walking(GetAll(), null));
            result.Append("</Categories>");
            
            return result;
        }

        private static StringBuilder Walking(IList<EquipmentCategory> categories, EquipmentCategory parent)
        {
            StringBuilder result = new StringBuilder();

            foreach (var p in categories.Where(c => c.ParentCategory == parent)) {
                result.Append(string.Format(
                    "<category id=\"{0}\" name=\"{1}\">", p.Id, p.Name));
                result.Append(Walking(categories, p));
                result.Append("</category>");
            }

            return result;
        }
    }
}
