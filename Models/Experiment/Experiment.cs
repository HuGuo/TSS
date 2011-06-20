using System;
using System.Collections.Generic;

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
        public string Title { get; set; }
        /// <summary>
        /// 实验结果 
        /// 0 不合格
        /// 1 合格
        /// </summary>
        public int Result { get; set; }
        public virtual ICollection<ExpData> Expdatas { get; set; }
        public virtual ICollection<ExpAttachment> Attachments { get; set; }
    }
}
