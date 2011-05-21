using System;
using System.Collections.Generic;
using System.Linq;
using TSS.Models;

namespace TSS.BLL
{
    public static class Equipments
    {
        public static IList<Equipment> GetAll(string category, string specialty)
        {
            using (var context = new Context()) {
                return context.Equipments.Where(e =>
                    e.EquipmentCategoryId.Equals(Guid.Parse(category)) &&
                    e.SpecialtyId.Equals(specialty)).ToList();
            }
        }

        public static void Add(string name, string category, string specialty)
        {
            using (var context = new Context()) {
                context.Equipments.Add(new Equipment {
                    Id = Guid.NewGuid(),
                    Name = name,
                    EquipmentCategoryId = Guid.Parse(category),
                    SpecialtyId = specialty
                });

                context.SaveChanges();
            }
        }

        public static void Update(Equipment equipment)
        {
            using (var context = new Context()) {
                var o = context.Equipments.Find(equipment.Id);
                if (o != null) {
                    o.Name = equipment.Name;
                    o.SpecialtyId = equipment.SpecialtyId;
                    o.EquipmentCategoryId = equipment.EquipmentCategoryId;

                    context.SaveChanges();
                }
            }
        }

        public static void Delete(Equipment equipment)
        {
            throw new NotImplementedException();
        }
    }
}
