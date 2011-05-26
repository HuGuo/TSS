﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.EntityModel;
using Tm = TSS.Models;

namespace TSS.BLL
{
    public class MaintenanceCycle
    {
        public static IList<Tm.MaintenanceCycle> GetAll()
        {
            using (var dbContext = new Tm.Context())
                return dbContext.MaintenanceCycles.ToList();

        }

        public static Tm.MaintenanceCycle Get(int maintenanceCycleId)
        {
            using (var dbContext = new Tm.Context())
                return dbContext.MaintenanceCycles.Find(maintenanceCycleId);
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

        public static bool Delete(Tm.MaintenanceCycle maintenanceCycle)
        {
            using (var dbContex = new Tm.Context())
            {
                dbContex.MaintenanceCycles.Remove(maintenanceCycle);
                return dbContex.SaveChanges() > 0;
            }
        }
    }
}
