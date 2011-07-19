using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TSS.Models
{
    /// <summary>
    /// 试验报告分类
    /// </summary>
    public class ExpCategory
    {
        public Guid Id { get; set; }
        public Guid? ParentId { get; set; }

        public string Name { get; set; }

        public string SpecialtyId { get; set; }
        public virtual ExpCategory Parent { get; set; }
        public virtual ICollection<ExpCategory> SubCategories { get; set; }
        public virtual ICollection<ExpTemplate> ExpTemplates { get; set; }
    }
}
