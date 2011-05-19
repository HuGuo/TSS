using System.Collections.Generic;
using System.Data;
using System.Linq;
using TSS.Models;
namespace TSS.BLL
{
    public class ExpTemplateRepository:Repository<ExpTemplate,int>
    {
        public ExpTemplateRepository() { }

        public override void Delete(int id)
        {
            using (Context db = new Context()) {
                ExpTemplate entity = db.Exptemplates.Find(id);
                if (null != entity) {
                    entity.IsDEL = 1;
                    db.SaveChanges();
                }
            }
        }
    }
}
