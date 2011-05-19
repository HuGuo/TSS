using System.Linq;
using TSS.Models;

namespace TSS.BLL
{
    class ExpCategoryRepository:Repository<ExpCategory,int>
    {
        public ExpCategoryRepository() { }

        public ExpCategory GetRoot(string specialtyID)
        {
            using (Context db = new Context()) {
                ExpCategory entity = db.Expcategory.FirstOrDefault(p => p.SP_Code == specialtyID && p.ParentId == null);
                return entity;
            }
        }
    }
}
