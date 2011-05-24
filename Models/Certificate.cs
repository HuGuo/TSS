using System;

namespace TSS.Models
{
    public class Certificate
    {
        public Guid Id { get; set; }

        public string Title { get; set; }
        public string Number { get; set; }
        public string CretificationAuthority { get; set; }
        public DateTime ReceiveDateTime { get; set; }
        public DateTime ExpireDateTime { get; set; }
        public string ScanFilePath { get; set; }
        public string Remark { get; set; }

        public string EmployeeId { get; set; }
        public string EpmloyeeName { get; set; }
        public string Gender { get; set; }

        public string SpecialtyId { get; set; }
        public virtual Specialty Specialty { get; set; }
    }
}
