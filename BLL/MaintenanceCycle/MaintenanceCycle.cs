using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.Entity;
using System.Data.EntityModel;
using Tm = TSS.Models;

namespace TSS.BLL
{
    public class MaintenanceCycle
    {
        public static IList<Tm.MaintenanceCycle> GetAll()
        {
            using (var dbContext = new Tm.Context())
                return dbContext.MaintenanceCycles
                    .Include(m => m.MaintenanceCalss)
                    .Include(m => m.MaintenanceExperiments)
                    .ToList();
        }

        public static Tm.MaintenanceCycle Get(int maintenanceCycleId)
        {
            using (var dbContext = new Tm.Context())
                return dbContext.MaintenanceCycles.Find(maintenanceCycleId);
        }

        public static Tm.MaintenanceCycle GetByEquipment(Guid equipmentId)
        {
            using (var dbContext = new Tm.Context())
                return dbContext.MaintenanceCycles
                    .Where(maintenanceCycle => maintenanceCycle.EquipmentId == equipmentId)
                    .SingleOrDefault();
        }

        public static bool IsExistOnMaintenancClass(int maintenanceClassId)
        {
            return GetByMaintenanceClass(maintenanceClassId).Count > 0;
        }

        public static IList<Tm.MaintenanceCycle> GetByMaintenanceClass(int maintenanceClassId)
        {
            using (var dbContext = new Tm.Context())
                return dbContext.MaintenanceCycles
                    .Where(maintenanceCycle => maintenanceCycle.MaintenanceClassId == maintenanceClassId)
                    .ToList();
        }

        public static bool Add(Tm.MaintenanceCycle maintenanceCycle)
        {
            using (var dbContext = new Tm.Context())
            {
                dbContext.MaintenanceCycles.Add(maintenanceCycle);
                return dbContext.SaveChanges() > 0;
            }
        }

        public static bool Update(Tm.MaintenanceCycle maintenanceCycle)
        {
            using (var dbContext = new Tm.Context())
            {
                dbContext.Entry(maintenanceCycle).State = EntityState.Modified;
                return dbContext.SaveChanges() > 0;
            }
        }

        public static bool Delete(int maintenanceCycleId)
        {
            using (var dbContex = new Tm.Context())
            {
                dbContex.MaintenanceCycles.Remove(
                    dbContex.MaintenanceCycles.Find(maintenanceCycleId));
                return dbContex.SaveChanges() > 0;
            }
        }
    }
}
