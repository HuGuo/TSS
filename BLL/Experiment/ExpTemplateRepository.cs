using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using TSS.Models;
namespace TSS.BLL
{
    public class ExpTemplateRepository : Repository<ExpTemplate, Guid>
    {
        public ExpTemplateRepository() { }

        public ExpTemplate GetAndEquipments(Guid id)
        {
            using (Context db = new Context())
            {
                return null;
            }
        }
        public override bool Delete(Guid id)
        {
            using (Context db = new Context())
            {
                bool result = false;
                ExpTemplate entity = db.ExpTemplates.Find(id);
                if (null != entity)
                {
                    int exps = db.Experiments.Count(p => p.ExpTemplateID == entity.Id);
                    if (exps > 0)
                    {
                        entity.IsDEL = 1;
                    }
                    else
                    {
                        db.ExpTemplates.Remove(entity);
                    }
                    result = db.SaveChanges() > 0;
                }
                return result;
            }
        }

        public override IList<ExpTemplate> GetAll()
        {
            using (Context db = new Context())
            {
                return db.ExpTemplates.Where(p => p.IsDEL == 0)
                    .OrderBy(p => p.SpecialtyId)
                    .ToList();
            }
        }

        public IList<ExpTemplate> GetBySpecialty(string specialtyID)
        {
            using (Context db = new Context())
            {
                return db.ExpTemplates.Where(p => p.SpecialtyId == specialtyID)
                    .ToList();
            }
        }
    }
}
