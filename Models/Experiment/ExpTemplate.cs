using System;
using System.Collections.Generic;

namespace TSS.Models
{
    public class ExpTemplate
    {
        public ExpTemplate() { }

        public Guid Id { get; set; }

        public string Title { get; set; }
        public string HTML { get; set; }
        public string SpecialtyId { get; set; }
        public virtual Specialty Specialty { get; set; }
        public int Enable { get; set; }

        public Guid? ExpCategoryId { get; set; }
        public virtual ExpCategory Expcategory { get; set; }

        public virtual ICollection<Experiment> Experiments { get; set; }
        public virtual ICollection<Equipment> Equipments { get; set; }
        public virtual ICollection<ExpRecord> ExpRecords { get; set; }
    }
}
