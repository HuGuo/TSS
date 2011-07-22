using System;

namespace TSS.Models
{
    public class SupervisionNew
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Content { get; set; }

        public DateTime ReleaseTime { get; set; }

        public int SupervisionNewTypeId { get; set; }
        public virtual SupervisionNewType SupervisionNewType { get; set; }
    }
}
