using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using TSS.Models;
namespace TSS.BLL
{
    public class DocumentRepository:Repository<Document,Guid>
    {
        public DocumentRepository() { }

        public void Delete(Guid id, Action<string> onDeleted) {
            using (Context db=new Context()) {
                Document obj = db.Documents.Find(id);
                if (null!=obj) {
                    db.Documents.Where(p => p.ParentId == obj.Id).ToList()
                        .ForEach(m => db.Documents.Remove(m));
                    
                    db.Documents.Remove(obj);
                    db.SaveChanges();
                    onDeleted(obj.Path);
                }
            }
        }

        public IList<Document> GetChildItems(Guid? parentId,string specialtyId=null) 
        {
            using (Context db=new Context()) {
                if (!parentId.HasValue) {
                    return db.Documents.Where(p => p.ParentId.Equals(null) && p.SpecialtyId.Equals(specialtyId))
                        .OrderByDescending(p => p.IsFolder).ToList();
                }
                return db.Documents.Where(p => p.ParentId == parentId && p.SpecialtyId.Equals(specialtyId))
                    .OrderByDescending(p => p.IsFolder)
                    .ToList();
            }
        }

        public IList<Document> GetRoot(string specialtyId) 
        {
            using (Context db=new Context()) {
                var all = db.Documents
                    .Where(p => p.SpecialtyId == specialtyId && p.IsFolder==1)
                    .OrderBy(p=>p.Name)
                    .ToList();
                IEnumerable<Document> list = from p in all
                                             where p.SpecialtyId == null
                                             select p;
                foreach (var item in list) {
                    BindChilds(item, all);
                }                
                return list.ToList();
            }
        }

        void BindChilds(Document root, ICollection<Document> childs) 
        {
            if (null != root) {
                if (null==root.Childs) {
                    root.Childs = new List<Document>();
                }
                var query = childs.Where(p => p.ParentId == root.Id);
                foreach (Document item in query) {
                    BindChilds(item, childs);
                    root.Childs.Add(item);
                }
            }
        }

        public System.Text.StringBuilder GetDirectoryXML(string specialtyId) 
        {
            System.Text.StringBuilder build = new System.Text.StringBuilder();
            IList<Document> root = GetRoot(specialtyId);
            build.Append("<Categories>");
            foreach (var item in root) {
                BindChilds(item, build);
            }
            build.Append("<Categories>");            
            return build;
        }
        void BindChilds(Document obj,System.Text.StringBuilder build) 
        {
            foreach (var item in obj.Childs) {
                build.AppendFormat("<category id=\"{0}\" name=\"{1}\">", item.Id, item.Name);
                BindChilds(item, build);
                build.Append("</category>");
            }
        }

        public IList<Document> Search(string key, string specialty = null)
        {
            using (Context db = new Context()) {
                var query = db.Documents
                    .Where(p => p.IsFolder == 0 && p.Name.ToLower().Contains(key.ToLower()) && (string.IsNullOrWhiteSpace(specialty) ? true : p.SpecialtyId == specialty));
                return query.ToList();
            }
        }
    }

}
