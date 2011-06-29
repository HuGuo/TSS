using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.Entity;
using System.Data.EntityModel;
using TSS.Models;

namespace TSS.BLL
{
    public class MaintenanceCycleRepository : Repository<MaintenanceCycle>
    {
        public override IList<MaintenanceCycle> GetAll()
        {
            using (var dbContext = new Context())
                return dbContext.MaintenanceCycles
                    .Include(m => m.MaintenanceCalss)
                    .Include(m => m.MaintenanceExperiments)
                    .ToList();
        }

        //public override MaintenanceCycle Get(int maintenanceCycleId)
        //{
        //    using (var dbContext = new Context())
        //        return dbContext.MaintenanceCycles
        //           .Where(m => m.Id == maintenanceCycleId)
        //            .Include(m => m.MaintenanceCalss)
        //            .Include(m => m.MaintenanceExperiments)
        //            .SingleOrDefault();
        //}

        public  MaintenanceCycle GetByEquipment(Guid equipmentId)
        {
            using (var dbContext = new Context())
                return dbContext.MaintenanceCycles
                    .Where(maintenanceCycle => maintenanceCycle.EquipmentId == equipmentId)
                    .Include(m => m.MaintenanceCalss)
                    .Include(m => m.MaintenanceExperiments)
                    .SingleOrDefault();
        }

        public  bool IsExistOnMaintenancClass(int maintenanceClassId)
        {
            return GetByMaintenanceClass(maintenanceClassId).Count > 0;
        }

        public  IList<MaintenanceCycle> GetByMaintenanceClass(int maintenanceClassId)
        {
            using (var dbContext = new Context())
                return dbContext.MaintenanceCycles
                    .Where(maintenanceCycle => maintenanceCycle.MaintenanceClassId == maintenanceClassId)
                    .ToList();
        }
    }
}
