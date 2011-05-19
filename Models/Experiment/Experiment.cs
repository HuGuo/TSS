using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace TSS.Models
{
    [Table("EXP_VIEWDATA")]
    public class Experiment
    {
        public Experiment() { }

        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("EXP_DATE", TypeName = "smalldatetime")]
        public DateTime ExpDate { get; set; }

        [ForeignKey("Template"), Column("TMP_ID")]
        public int TemplateID { get; set; }
        public virtual ExpTemplate Template { get; set; }

        [Column("EM_ID")]
        public string EquipmentID { get; set; }

        public int IsDEL { get; set; }

        public virtual ICollection<ExpData> Expdatas { get; set; }
        public virtual ICollection<ExpAttachment> Attachments { get; set; }
    }
}
