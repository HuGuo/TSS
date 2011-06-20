using System;

namespace TSS.Models
{
    public class ExpRecord
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public Guid ExpTemplateId { get; set; }
        public string SpecialtyId { get; set; }
        public virtual Specialty Specialty { get; set; }
        public virtual ExpTemplate DataSourcesTemplate { get; set; }
    }
}
