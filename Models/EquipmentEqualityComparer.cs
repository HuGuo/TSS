using System.Collections.Generic;

namespace TSS.Models
{
    public class EquipmentEqualityComparer:IEqualityComparer<TSS.Models.Equipment>
    {
        #region IEqualityComparer<Equipment> Members

        public bool Equals(Equipment x , Equipment y) {
            if (x==null || y==null) {
                return false;
            }
            return x.Id.Equals(y.Id);
        }

        public int GetHashCode(Equipment obj) {            
            return obj.Id.GetHashCode();
        }

        #endregion
    }
}
