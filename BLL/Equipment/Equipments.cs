using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using TSS.Models;

namespace TSS.BLL
{
    public class Equipments : Repository<Equipment, Guid>
    {
        public IList<Equipment> GetList(string categoryId)
        {
            IList<Equipment> result = null;

            if (string.IsNullOrEmpty(categoryId)) {
                result = Context.Equipments
                    .Include("EquipmentCategory")
                    .Include("Specialty")
                    .ToList();
            } else {
                var c = Context.EquipmentCategories.Find(Guid.Parse(categoryId));
                if (c != null) {
                    result = Context.Equipments.Where(e => e.EquipmentCategory.Id == c.Id)
                        .Include("EquipmentCategory")
                        .Include("Specialty")
                        .ToList();
                }
            }

            return result;
        }

        public IList<Equipment> GetList(string categoryId, string specialtyId)
        {
            IList<Equipment> result = null;

            if (!string.IsNullOrEmpty(categoryId) &&
                !string.IsNullOrEmpty(specialtyId)) {
                var c = Context.EquipmentCategories.Find(Guid.Parse(categoryId));
                var s = Context.Specialties.Find(specialtyId);
                if (c != null && s != null) {
                    result = Context.Equipments.Include("EquipmentDetails").Where(e =>
                        e.EquipmentCategoryId == c.Id &&
                        e.SpecialtyId == s.Id
                    ).ToList();
                }
            }

            return result;
        }

        public void Add(string name, string code, string categoryId, string specialtyId)
        {
            if (!string.IsNullOrEmpty(categoryId) &&
                !string.IsNullOrEmpty(specialtyId)) {
                var c = Context.EquipmentCategories.Find(Guid.Parse(categoryId));
                var s = Context.Specialties.Find(specialtyId);
                if (c != null && s != null) {
                    Add(new Equipment {
                        Id = Guid.NewGuid(),
                        Name = name,
                        Code = code,
                        EquipmentCategory = c,
                        Specialty = s
                    });
                }
            }
        }
    }
}
