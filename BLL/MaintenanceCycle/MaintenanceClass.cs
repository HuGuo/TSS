using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.EntityModel;
using Tm = TSS.Models;

namespace TSS.BLL
{
    public class MaintenanceClass
    {
        public static IList<Tm.MaintenanceClass> GetAll()
        {
            using (var dbContext = new Tm.Context())
                return dbContext.MaintenanceClasses.ToList();
        }

        public static Tm.MaintenanceClass Get(int maintenanceClassId)
        {
            using (var dbContext = new Tm.Context())
                return dbContext.MaintenanceClasses.Find(maintenanceClassId);
        }

        public static bool Add(Tm.MaintenanceClass maintenanceClass)
        {
            using (var dbContext = new Tm.Context())
            {
                dbContext.MaintenanceClasses.Add(maintenanceClass);
                return dbContext.SaveChanges() > 0;
            }
        }

        public static bool Update(Tm.MaintenanceClass maintenanceClass)
        {
            using (var dbContext = new Tm.Context())
            {
                dbContext.Entry(maintenanceClass).State = EntityState.Modified;
                return dbContext.SaveChanges() > 0;
            }
        }

        public static bool Delete(int maintenanceClassId)
        {
            using (var dbContext = new Tm.Context())
            {
                var obj = dbContext.MaintenanceClasses.Find(maintenanceClassId);
                dbContext.MaintenanceClasses.Remove(obj);
                return dbContext.SaveChanges() > 0;
            }
        }

    }
}
