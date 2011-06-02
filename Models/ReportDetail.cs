using System;

namespace TSS.Models
{
    public class ReportDetail
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string URLView { get; set; }
        public string URLAdd { get; set; }
        public string URLEdit { get; set; }
        public string SpecialtyId { get; set; }
        public string WorkflowId { get; set; }
        public int disabled { get; set; }

    }
}
