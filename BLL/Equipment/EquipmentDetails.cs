using System;
using System.Collections.Generic;
using System.Linq;
using TSS.Models;

namespace TSS.BLL
{
    public class EquipmentDetails : Repository<EquipmentDetail>
    {
        public IList<EquipmentDetail> GetList(Guid equipmentId)
        {
            IList<EquipmentDetail> result = null;

            var e = Context.Equipments.Find(equipmentId);
            if (e != null) {
                result = e.EquipmentDetails.ToList();
            }

            return result;
        }

        public string GetValue(string equipmentId, string lable)
        {
            string result = string.Empty;

            Guid eId;
            if (Guid.TryParse(equipmentId, out eId)) {
                var d = Context.EquipmentDetails.Where(o =>
                    o.EquipmentId == eId && o.Lable == lable).SingleOrDefault();
                if (d != null) {
                    result = d.Value;
                }
            }

            return result;
        }
    }
}
