using System;
using System.ComponentModel.DataAnnotations;

namespace TSS.Models
{
    public class Employee
    {
        [Key]
        public Guid Id { get; set; }

        public string LoginName { get; set; }

        public virtual Specialty Specialty { get; set; }
    }
}
