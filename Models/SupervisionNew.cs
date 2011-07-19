using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TSS.Models
{
    public class SupervisionNew
    {
        public int Id;
        public string Title;
        public string Author;
        public string Content;
        public DateTime ReleaseTime;

        public int SupervisionNewId;
        public SupervisionNewType SupervisionNewType;
    }
}
