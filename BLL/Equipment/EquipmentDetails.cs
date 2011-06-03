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
        public IList<EquipmentDetail> GetAllByEquipment(string equipmentId)
        {
            IList<EquipmentDetail> result = null;

            if (!string.IsNullOrEmpty(equipmentId)) {
                var e = Context.Equipments.Find(Guid.Parse(equipmentId));
                if (e != null) {
                    result = Context.EquipmentDetails.Where(d =>
                        d.EquipmentId == e.Id).ToList();
                }
            }

            return result;
        }

        public void Add(string equipmentId, string lable, string value)
        {
            if (!string.IsNullOrEmpty(equipmentId)) {
                var e = Context.Equipments.Find(Guid.Parse(equipmentId));
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
}
