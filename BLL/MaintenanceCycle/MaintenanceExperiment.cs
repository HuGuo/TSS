using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.EntityModel;
using Tm = TSS.Models;

namespace TSS.BLL
{
    public class MaintenanceExperiment
    {
        public static IList<Tm.MaintenanceExperiment> GetAll()
        {
            using (var dbContext = new Tm.Context())
                return dbContext.MaintenanceExperiments.ToList();
        }

        public static Tm.MaintenanceExperiment Get(int maintenanceExperimentId)
        {
            using (var dbContext = new Tm.Context())
                return dbContext.MaintenanceExperiments.Find(maintenanceExperimentId);
        }

        public static bool Add(Tm.MaintenanceExperiment maintenanceExperiment)
        {
            using (var dbContext = new Tm.Context())
            {
                dbContext.MaintenanceExperiments.Add(maintenanceExperiment);
                return dbContext.SaveChanges() > 0;
            }
        }

        public static bool Update(Tm.MaintenanceExperiment maintenanceExperiment)
        {
            using (var dbContext = new Tm.Context())
            {
                dbContext.Entry(maintenanceExperiment).State = EntityState.Modified;
                return dbContext.SaveChanges() > 0;
            }
        }

        public static bool Delete(string  maintenanceExperimentId)
        {
            using (var dbContex = new Tm.Context())
            {
                dbContex.MaintenanceExperiments.Remove(dbContex.MaintenanceExperiments
                    .Find(int.Parse(maintenanceExperimentId)));
                return dbContex.SaveChanges() > 0;
            }
        }
    }
}
