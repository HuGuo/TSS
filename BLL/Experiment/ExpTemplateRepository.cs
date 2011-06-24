using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using TSS.Models;
namespace TSS.BLL
{
    public class ExpTemplateRepository : Repository<ExpTemplate>
    {
        public ExpTemplate GetAndEquipments(Guid id) {

            return null;

        }

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
    }
}
