using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.Entity;
using  TSS.Models;

namespace TSS.BLL
{
    public class MaintenanceClassRepository:Repository<MaintenanceClass>
    {
        public override IList<MaintenanceClass> GetAll()
        {
            using (var dbContext = new Context())
                return dbContext.MaintenanceClasses
                    .Include(m => m.MaintenanceCycles)
                    .ToList();
        }

        public MaintenanceClass Get(int maintenanceClassId)
        {
            using (var dbContext = new Context())
                return dbContext.MaintenanceClasses
                    .Where(m => m.Id == maintenanceClassId)
                    .SingleOrDefault();
        }
    }
}
