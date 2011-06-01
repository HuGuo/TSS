using System;
using System.Collections.Generic;
using System.Linq;
using TSS.Models;

namespace TSS.BLL
{
    public class Equipments : Repository<Equipment, Guid>
    {
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
