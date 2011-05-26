using System;
using System.Collections.Generic;
using System.Linq;
using TSS.Models;
namespace TSS.BLL
{
    public class DocumentRepository:Repository<Document,Guid>
    {
        public DocumentRepository() { }

        public IList<Document> GetChildItems(Guid  parentId) 
        {
            using (Context DBNull=new Context()) {
                var query = DBNull.Documents.Where(p=>p.ParentId==parentId)
                    .OrderBy(p=>p.IsFolder);

                return query.ToList();
            }
        }

        public Document GetRoot(string specialtyId) 
        {
            using (Context db=new Context()) {
                var all = db.Documents
                    .Where(p => p.SpecialtyId == specialtyId && p.IsFolder==1)
                    .OrderBy(p=>p.Name)
                    .ToList();
                Document root = all.FirstOrDefault(p => string.IsNullOrEmpty(p.SpecialtyId));
                BindChilds(root, all);
                return root;
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
            Document root = GetRoot(specialtyId);
            build.Append("<Categories>");
            BindChilds(root, build);
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

        public IList<Document> Search(string key, string specialty = "")
        {
            using (Context db = new Context()) {
                var query = db.Documents
                    .Where(p => p.IsFolder == 0 && p.Name.ToLower().Contains(key.ToLower()) && (string.IsNullOrWhiteSpace(specialty) ? true : p.SpecialtyId == specialty));
                return query.ToList();
            }
        }
    }

}
