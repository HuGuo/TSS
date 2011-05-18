using System.ComponentModel.DataAnnotations;
namespace TSS.Models
{
    [Table("SPECIALTY")]
    public class Specialty
    {
        [Key,DatabaseGenerated(System.ComponentModel.DataAnnotations.DatabaseGeneratedOption.None)]
        public string Code { get; set; }

        [MaxLength(250)]
        public string Name { get; set; }
    }
}
