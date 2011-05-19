﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TSS.Models
{
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
                
        public virtual ICollection<ExpCategory> Childs { get; set; }

        [ForeignKey("ParentId")]
        public  virtual ExpCategory Parent { get; set; }

        public virtual ICollection<ExpTemplate> TemplateList { get; set; }
    }
}
