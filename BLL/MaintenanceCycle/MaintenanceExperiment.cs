using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.EntityModel;
using Tm = TSS.Models;

namespace TSS.BLL.MaintenanceExperiment
{
    public class MaintenanceExperiment
    {
        public IList<Tm.MaintenanceExperiment> GetAll()
        {
            using (var dbContext = new Tm.Context())
                return dbContext.MaintenanceExperiments.ToList();
        }

        public Tm.MaintenanceExperiment Get(int maintenanceExperimentId)
        {
            using (var dbContext = new Tm.Context())
                return dbContext.MaintenanceExperiments.Find(maintenanceExperimentId);
        }

        public bool Add(Tm.MaintenanceExperiment maintenanceExperiment)
        {
            using (var dbContext = new Tm.Context())
            {
                dbContext.MaintenanceExperiments.Add(maintenanceExperiment);
                return dbContext.SaveChanges() > 0;
            }
        }

        public bool Update(Tm.MaintenanceExperiment maintenanceExperiment)
        {
            using (var dbContext = new Tm.Context())
            {
                dbContext.Entry(maintenanceExperiment).State = EntityState.Modified;
                return dbContext.SaveChanges() > 0;
            }
        }

        public bool Delete(Tm.MaintenanceExperiment maintenanceExperiment)
        {
            using (var dbContex = new Tm.Context())
            {
                dbContex.MaintenanceExperiments.Remove(maintenanceExperiment);
                return dbContex.SaveChanges() > 0;
            }
        }


    }
}
