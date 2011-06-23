using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using TSS.Models;

namespace TSS.BLL
{
    public class Equipments : Repository<Equipment>
    {
        public IList<Equipment> GetList(string categoryId, string specialtyId)
        {
            IList<Equipment> result = null;

            Guid cId;
            if (Guid.TryParse(categoryId, out cId) && !string.IsNullOrEmpty(specialtyId)) {
                var c = Context.EquipmentCategories.Find(cId);
                var s = Context.Specialties.Find(specialtyId);
                if (c != null && s != null) {
                    result = Context.Equipments.Where(e =>
                        e.EquipmentCategoryId == c.Id &&
                        e.SpecialtyId == s.Id)
                        .ToList();
                }
            } if (Guid.TryParse(categoryId, out cId)) {
                var c = Context.EquipmentCategories.Find(cId);
                if (c != null) {
                    result = Context.Equipments.Where(e =>
                        e.EquipmentCategory.Id == c.Id)
                        .ToList();
                }
            }
            if (!string.IsNullOrEmpty(specialtyId)) {
                var s = Context.Specialties.Find(specialtyId);
                if (s != null) {
                    result = Context.Equipments.Where(e =>
                        e.Specialty.Id == s.Id)
                        //.Include("EquipmentDetails")
                        .ToList();
                }
            } else {
                result = Context.Equipments
                    //.Include("EquipmentDetails")
                    .Include("EquipmentCategory")
                    .Include("Specialty")
                    .ToList();
            }
            
            return result;
        }

        public void Add(string name, string code, string categoryId, string specialtyId)
        {
            Guid cId;
            if (Guid.TryParse(categoryId, out cId) &&
                !string.IsNullOrEmpty(specialtyId)) {
                var c = Context.EquipmentCategories.Find(cId);
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
