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
    public class MaintenanceExperimentRepository : Repository<MaintenanceExperiment>
    {
        public override IList<MaintenanceExperiment> GetAll()
        {
            using (var dbContext = new Context())
                return dbContext.MaintenanceExperiments.ToList();
        }

        public  MaintenanceExperiment Get(int maintenanceExperimentId)
        {
            using (var dbContext = new Context())
                return dbContext.MaintenanceExperiments
                    .Where(m => m.Id == maintenanceExperimentId)
                    .Include(m => m.MaintenanceCycle.MaintenanceCalss)
                    .SingleOrDefault();

        }

        public IList<MaintenanceExperiment> GetByMaintenanceCycle(int maintenanceCycleId)
        {
            using (var dbContext = new Context())
                return dbContext.MaintenanceExperiments
                    .Where(o => o.MaintenanceCycleId == maintenanceCycleId)
                    .ToList();
        }

        public bool IsExistOnMaintenanceCycle(int maintenanceCycleId)
        {
            return GetByMaintenanceCycle(maintenanceCycleId).Count > 0;
        }
    }
}
