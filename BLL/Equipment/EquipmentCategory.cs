using System;
using System.Collections.Generic;
using System.Linq;
using TSS.Models;

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

        public static void Add(string name, Guid? parentId)
        {
            using (var context = new Context()) {
                context.EquipmentCategories.Add(new TSS.Models.EquipmentCategory {
                    Id = Guid.NewGuid(),
                    Name = name,
                    ParentId = parentId
                });

                context.SaveChanges();
            }
        }
    }
}
