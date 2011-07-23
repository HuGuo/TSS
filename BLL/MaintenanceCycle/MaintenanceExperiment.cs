using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using TSS.Models;

namespace TSS.BLL
{
    public class MaintenanceExperimentRepository : Repository<MaintenanceExperiment>
    {
        public override IList<MaintenanceExperiment> GetAll()
        {
            using (var dbContext = new Context())
                return dbContext.MaintenanceExperiments
                    .Include(m => m.MaintenanceCycle.MaintenanceCalss)
                    .ToList();
        }

        public MaintenanceExperiment Get(int maintenanceExperimentId)
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
                     .Include(m => m.MaintenanceCycle.MaintenanceCalss)
                    .ToList();
        }

        public bool IsExistOnMaintenanceCycle(int maintenanceCycleId)
        {
            return GetByMaintenanceCycle(maintenanceCycleId).Count > 0;
        }
    }
}
