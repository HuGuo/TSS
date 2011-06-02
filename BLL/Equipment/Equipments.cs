using System;
using System.Collections.Generic;
using System.Linq;
using TSS.Models;

namespace TSS.BLL
{
    public class Equipments : Repository<Equipment, Guid>
    {
        public IList<Equipment> GetAll(string categoryId, string specialtyId)
        {
            return GetAll().Where(e => {
                return e.EquipmentCategoryId.Equals(Guid.Parse(categoryId)) &&
                    e.SpecialtyId.Equals(specialtyId);
            }).ToList();
        }

        public void Add(string name, string categoryId, string specialtyId)
        {
            Add(new Equipment {
                Id = Guid.NewGuid(),
                Name = name,
                EquipmentCategoryId = Guid.Parse(categoryId),
                SpecialtyId = specialtyId
            });
        }
    }
}
