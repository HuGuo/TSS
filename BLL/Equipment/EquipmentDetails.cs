using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using TSS.Models;

namespace TSS.BLL
{
    public class EquipmentDetails : Repository<EquipmentDetail, Guid>
    {
        public IList<EquipmentDetail> GetList(Guid equipmentId)
        {
            IList<EquipmentDetail> result = null;

            var e = Context.Equipments.Find(equipmentId);
            if (e != null) {
                result = Context.EquipmentDetails.Where(d =>
                    d.EquipmentId == e.Id).ToList();
            }

            return result;
        }

        public void Add(Guid equipmentId, string lable, string value)
        {
            var e = Context.Equipments.Find(equipmentId);
            if (e != null) {
                Add(new EquipmentDetail {
                    Id = Guid.NewGuid(),
                    Lable = lable,
                    Value = value,
                    Equipment = e
                });
            }
        }
    }
}
