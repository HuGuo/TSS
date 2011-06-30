using System;
using System.Collections.Generic;
using System.Linq;
using TSS.Models;

namespace TSS.BLL
{
    public class ExpTemplateRepository : Repository<ExpTemplate>
    {
        public override bool Delete(object id) {
            ExpTemplate entity = Context.ExpTemplates.Find(id);
            if (null != entity) {
                int exps = Context.Experiments.Count(p => p.ExpTemplateID == entity.Id);
                if (exps > 0) {
                    entity.IsDEL = 1;
                } else {
                    Context.ExpTemplates.Remove(entity);
                }
                return Context.SaveChanges()>0;
            }
            return false;
        }

        public override IList<ExpTemplate> GetAll() {
            return Context.ExpTemplates.Where(p => p.IsDEL == 0)
                    .OrderBy(p => p.SpecialtyId)
                    .ToList();
        }

        public IList<ExpTemplate> GetBySpecialty(string specialtyID) {
            return Context.ExpTemplates.Where(p => p.SpecialtyId == specialtyID)
                    .ToList();
        }

        public void BindEquipment(Guid exptemplateId,params Guid[] equipmentId) {
            ExpTemplate obj = Context.ExpTemplates.Find(exptemplateId);
            if (null !=obj) {
                Context.Entry(obj).Collection(p => p.Equipments).Load();
                var query= Context.Equipments.Where(p => equipmentId.Contains(p.Id)).ToArray();
                EquipmentEqualityComparer equipmentEqc=new EquipmentEqualityComparer();
                foreach (var item in query) {
                    if (obj.Equipments.Contains(item , equipmentEqc)) {
                        continue;
                    }
                    obj.Equipments.Add(item);
                    item.ExpTemplates.Add(obj);
                }
                Context.SaveChanges();
            }
        }

        public void UnBindEquipment(Guid exptemplateId , params Guid[] equipmentId) {
            ExpTemplate obj = Context.ExpTemplates.Find(exptemplateId);
            if (null != obj) {
                Context.Entry(obj).Collection(p => p.Equipments).Load();
                var query = obj.Equipments.Where(p => equipmentId.Contains(p.Id)).ToArray();
                foreach (var item in query) {
                    obj.Equipments.Remove(item);
                    item.ExpTemplates.Remove(obj);
                }
                Context.SaveChanges();
            }
        }
    }
}
