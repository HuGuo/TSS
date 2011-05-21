using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace TSS.Models
{
    [Table("EXP_CATEGORY")]
    public class ExpCategory
    {
        public ExpCategory() { }
        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [MaxLength(250)]
        public string Name { get; set; }

        public string SP_Code { get; set; }

        [Column("PID")]        
        public int? ParentId { get; set; }

        [NotMapped]
        public ICollection<ExpCategory> Childs { get; set; }

        //[ForeignKey("ParentId")]
        //public  virtual ExpCategory Parent { get; set; }
        //public virtual ICollection<ExpTemplate> TemplateList { get; set; }

        //public virtual ExpTemplate Template { get; set; }
    }
}
