using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TSS.BLL
{
    public static class Equipment
    {
        public static IList<TSS.Models.Equipment> GetAll()
        {
            using (var context = new TSS.Models.Context()) {
                return context.Equipments.ToList();
            }
        }

        public static IList<TSS.Models.Equipment> GetAll(string category, string specialty)
        {
            using (var context = new TSS.Models.Context()) {
                return context.Equipments.Where(e =>
                    e.EquipmentCategoryId.Equals(new Guid(category)) &&
                    e.SpecialtyId.Equals(specialty)).ToList();
            }
        }

        public static void Add(string name, string category, string specialty)
        {
            using (var content = new TSS.Models.Context()) {
                content.Equipments.Add(new TSS.Models.Equipment {
                    Id = Guid.NewGuid(),
                    Name = name,
                    EquipmentCategoryId = new Guid(category),
                    SpecialtyId = specialty
                });

                content.SaveChanges();
            }
        }
    }
}
