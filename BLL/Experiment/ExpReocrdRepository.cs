using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TSS.Models;
using System.Data.Entity;
namespace TSS.BLL
{
    public class ExpReocrdRepository:Repository<TSS.Models.ExpRecord,Guid>
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
