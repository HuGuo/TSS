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
                result = e.EquipmentDetails.ToList();
            }

            return result;
        }

        public string GetValue(string equipmentId, string lable)
        {
            string result = string.Empty;

            Guid eId;
            if (Guid.TryParse(equipmentId, out eId)) {
                Context.Configuration.ProxyCreationEnabled = false;
                Context.EquipmentDetails.Load();
                var d = Context.EquipmentDetails.Where(o =>
                    o.EquipmentId == eId && o.Lable == lable)
                    .FirstOrDefault();
                if (d != null) {
                    result = d.Value;
                }
            }

            return result;
        }
    }
}
