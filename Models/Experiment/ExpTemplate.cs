using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TSS.Models
{
    [Table("EXP_TEMPLATE")]
    public class ExpTemplate
    {
        public ExpTemplate() { }

        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.DatabaseGeneratedOption.None)]
        [ForeignKey("Category")]
        public int Id { get; set; }

        public string Title { get; set; }
        public string HTML { get; set; }
        public string SP_CODE { get; set; }

        public int IsDEL { get; set; }
        public virtual ExpCategory Category { get; set; }

        public virtual ICollection<Experiment> Experiments { get; set; }
    }
}
