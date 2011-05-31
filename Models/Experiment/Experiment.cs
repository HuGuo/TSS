using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TSS.Models
{
    public class Experiment
    {
        public Experiment() { }

        public Guid Id { get; set; }
        public DateTime ExpDate { get; set; }

        public Guid ExpTemplateID { get; set; }
        public virtual ExpTemplate Exptemplate { get; set; }

        public Guid EquipmentID { get; set; }
        public virtual Equipment Equipment { get; set; }

        public string HTML { get; set; }
        public int IsDEL { get; set; }
        public string Title { get; set; }

        public virtual ICollection<ExpData> Expdatas { get; set; }
        public virtual ICollection<ExpAttachment> Attachments { get; set; }
    }
}
