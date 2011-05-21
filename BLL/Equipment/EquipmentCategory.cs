using System;
using System.Collections.Generic;
using System.Linq;
using TSS.Models;
using System.Text;

namespace TSS.BLL
{
    public static class EquipmentCategory
    {
        public static IList<TSS.Models.EquipmentCategory> GetAll()
        {
            using (var context = new Context()) {
                return (from c in context.EquipmentCategories
                        select c)
                        .ToList();
            }
        }

        public static void Add(string name, string parentId)
        {
            using (var context = new Context()) {
                context.EquipmentCategories.Add(new TSS.Models.EquipmentCategory {
                    Id = Guid.NewGuid(),
                    Name = name,
                    ParentId = string.IsNullOrEmpty(parentId) ? (Guid?)null : new Guid(parentId)
                });

                context.SaveChanges();
            }
        }

        public static StringBuilder GetXml()
        {
            var result = new StringBuilder();

            result.Append("<?xml version=\"1.0\"?>\n\n");

            result.Append("<Categories>");
            result.Append(Walking(GetAll(), null));
            result.Append("</Categories>");
            
            return result;
        }

        private static StringBuilder Walking(IList<TSS.Models.EquipmentCategory> categories, Guid? id)
        {
            StringBuilder result = new StringBuilder();

            foreach (var p in categories.Where(c => c.ParentId == id)) {
                result.Append(string.Format(
                    "<category id=\"{0}\" name=\"{1}\">", p.Id, p.Name));
                result.Append(Walking(categories, p.Id));
                result.Append("</category>");
            }

            return result;
        }
    }
}
