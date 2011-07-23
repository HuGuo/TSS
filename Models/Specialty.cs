using System;
using System.Collections.Generic;

namespace TSS.Models
{
    [Serializable]
    public class Specialty
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Equipment> Equipments { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<ExpTemplate> ExpTemlates { get; set; }
        public virtual ICollection<Certificate> Certificates { get; set; }
        public virtual ICollection<Document> Folders { get; set; }
        public virtual ICollection<MaintenanceClass> MaintenanceCalss { get; set; }
        public virtual ICollection<SpecialtyAnalysis> SpecialtyAnalysises { get; set; }
        public virtual ICollection<Indicator> Indicators { get; set; }
        /// 实验台帐
        /// </summary>
        public virtual ICollection<ExpRecord> ExpRecords { get; set; }
    }
}
