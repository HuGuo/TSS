using System;

namespace TSS.Models
{
    public class EquipmentDetail
    {
        public Guid Id { get; set; }

        public string Lable { get; set; }
        public string Value { get; set; }

        public Guid EquipmentId { get; set; }
        public virtual Equipment Equipment { get; set; }
    }
}
