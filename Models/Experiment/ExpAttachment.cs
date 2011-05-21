using System.ComponentModel.DataAnnotations;

namespace TSS.Models
{
    [Table("EXP_ATTACHMENT")]
    public class ExpAttachment
    {
        public ExpAttachment() { }

        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(250)]
        public string FielName { get; set; }

        [ForeignKey("experiment")]
        public string ExpID { get; set; }
        public virtual Experiment experiment { get; set; }
    }
}

