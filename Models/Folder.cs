using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TSS.Models
{
    public class Folder
    {
        public Folder() { }

        public Guid Id { get; set; }
        public Guid ParentId { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public string SpecialtyId { get; set; }
        public virtual Specialty Specialty { get; set; }
        
        public ICollection<Folder> Childs { get; set; }
        public virtual ICollection<File> Files { get; set; }
    }

    public class File
    {
        public File() { }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public DateTime UploadDate { get; set; }

        public Guid ParentId { get; set; }
        public virtual Folder Folder { get; set; }
    }
}
