﻿using System;
using System.Collections.Generic;
using System.Linq;
using TSS.Models;
namespace TSS.BLL
{
    public class ExpReocrdRepository:Repository<TSS.Models.ExpRecord>
    {
        public IList<ExpRecord> GetAll(string specialtyId) {
            return Context.ExpRecords.Where(p => p.SpecialtyId.Equals(specialtyId)).ToList();
        }

        public IEnumerable<Equipment> GetEquipmentsByExpTemplate(Guid exptemplateId) {
            ExpTemplate obj= Context.ExpTemplates.Find(exptemplateId);
            if (null!=obj) {
                Context.Entry(obj).Collection(p => p.Equipments).Load();
                return obj.Equipments;
            }
            return null;
        }
    }
}
