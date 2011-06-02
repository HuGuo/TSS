using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TSS.Models
{
    public class Module
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }

        public virtual ICollection<Specialty> Specialties { get; set; }
    }
}
