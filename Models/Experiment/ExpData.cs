using System.ComponentModel.DataAnnotations;

namespace TSS.Models
{
    [Table("EXP_DATA")]
    public class ExpData
    {
        public ExpData() { }
        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string GUID { get; set; }
        public string Value { get; set; }

        [ForeignKey("experiment"), Column("EXP_ID")]
        public int EXPId { get; set; }
        public virtual Experiment experiment { get; set; }
    }
}
