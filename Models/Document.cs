using System;
using System.Collections.Generic;

namespace TSS.Models
{
    public class Document
    {
        public Guid Id { get; set; }
        public Guid? ParentId { get; set; }

        public string Name { get; set; }
        public string Path { get; set; }
        public DateTime UploadDate { get; set; }

        public string SpecialtyId { get; set; }
        public virtual Specialty Specialty { get; set; }

        public int IsFolder { get; set; }

        public ICollection<Document> Childs { get; set; }
    }
}
