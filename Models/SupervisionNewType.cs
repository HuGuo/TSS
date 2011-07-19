using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ComponentModel.DataAnnotations;

namespace TSS.Models
{
    public class SupervisionNewType
    {
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.DatabaseGeneratedOption.None)]
        public int Id;
        public string TypeName;

        public ICollection<SupervisionNew> SupervisionNews;
    }
}
