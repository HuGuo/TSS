using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TSS.Models;

namespace TSS.BLL
{
    public static class EquipmentCategories
    {
        public static IList<EquipmentCategory> GetAll()
        {
            using (var context = new Context()) {
                return context.EquipmentCategories.ToList();
            }
        }

        public static void Add(string name, string parentId)
        {
            using (var context = new Context()) {
                context.EquipmentCategories.Add(new TSS.Models.EquipmentCategory {
                    Id = Guid.NewGuid(),
                    Name = name,
                    ParentId = string.IsNullOrEmpty(parentId) ? (Guid?)null : Guid.Parse(parentId)
                });

                context.SaveChanges();
            }
        }

        public static void Update(EquipmentCategory equipmentCategory)
        {
            using (var context = new Context()) {
                var o = context.EquipmentCategories.Find(equipmentCategory.Id);
                if (o != null) {
                    o.Name = equipmentCategory.Name;
                    o.ParentId = equipmentCategory.ParentId;

                    context.SaveChanges();
                }
            }
        }

        public static void Delete(EquipmentCategory equipmentCategory)
        {
            throw new NotImplementedException();
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

        private static StringBuilder Walking(IList<EquipmentCategory> categories, Guid? id)
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
