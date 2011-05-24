using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using TSS.Models;
namespace TSS.BLL
{
    public class ExpTemplateRepository:Repository<ExpTemplate,Guid>
    {
        public ExpTemplateRepository() { }

        public override void Delete(Guid id)
        {
            using (Context db = new Context()) {
                ExpTemplate entity = db.ExpTemplates.Find(id);
                if (null != entity) {
                    entity.IsDEL = 1;
                    db.SaveChanges();
                }
            }
        }

        public IList<ExpTemplate> GetBySpecialty(string specialtyID) 
        {
            return null;
        }
    }
}
