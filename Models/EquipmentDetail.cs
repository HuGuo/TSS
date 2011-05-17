using System;

namespace TSS.Models
{
    class EquipmentDetail
    {
        public Guid EquipmentDetailId { get; set; }

        public string Lable { get; set; }
        public string Value { get; set; }

        public virtual Equipment Equipment { get; set; }
    }
}
