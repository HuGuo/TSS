using System.Linq;
using System.Data.Entity;
using TSS.Models;
using System.Collections;
using System.Collections.Generic;

namespace TSS.BLL
{
    public class ExpCategoryRepository:Repository<ExpCategory,int>
    {
        public ExpCategoryRepository() { }

        public ExpCategory GetRoot(string specialtyID)
        {
            using (Context db = new Context()) {

                var query = db.Expcategory.AsNoTracking().Where(p => p.SP_Code == specialtyID).ToList();
                ExpCategory root = query.FirstOrDefault(p => p.ParentId == null);
                if (null != root) {
                    BindChilds(root, query);
                }
                return root;
            }
        }

        void BindChilds(ExpCategory root,IList<ExpCategory> collection) 
        {
            var childs = (from p in collection
                               where p.ParentId == root.Id
                               select p).ToList();
            root.Childs = childs;
            foreach (ExpCategory item in childs) {
                //root.Childs.Add(item);
                //collection.Remove(item);
                BindChilds(item, collection);
            }
        }
    }
}
