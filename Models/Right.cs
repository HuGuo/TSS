using System;

namespace TSS.Models
{
    public class Right
    {
        public int Id { get; set; }
        public int ModuleId { get; set; }
        public Guid RoleId { get; set; }
        public virtual Role Role { get; set; }

        public int Permission { get; set; }
    }
}
