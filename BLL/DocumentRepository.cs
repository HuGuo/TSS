using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using TSS.Models;
namespace TSS.BLL
{
    public class DocumentRepository : Repository<Document , Guid>
    {
        public DocumentRepository() { }

        public void Delete(Guid id , Action<string> onDeleted) {
            Document obj = Context.Documents.Find(id);
            if (null != obj) {
                if (obj.IsFolder == 1) {
                    Context.Documents.Where(p => p.ParentId == obj.Id).ToList()
                    .ForEach(m => Context.Documents.Remove(m));
                }

                Context.Documents.Remove(obj);
                Context.SaveChanges();
                onDeleted(obj.Path);
            }
        }

        public IList<Document> GetChildItems(Guid? parentId , string specialtyId = null) {
            if (!parentId.HasValue) {
                return Context.Documents.Where(p => p.ParentId.Equals(null) && p.SpecialtyId.Equals(specialtyId))
                    .OrderByDescending(p => p.IsFolder).ToList();
            }
            return Context.Documents.Where(p => p.ParentId == parentId && p.SpecialtyId.Equals(specialtyId))
                .OrderByDescending(p => p.IsFolder)
                .ToList();
        }

        public IList<Document> GetRoot(string specialtyId) {
            var all = Context.Documents
                    .Where(p => p.SpecialtyId == specialtyId && p.IsFolder == 1)
                    .OrderBy(p => p.Name)
                    .ToList();
            IEnumerable<Document> list = from p in all
                                         where p.SpecialtyId == null
                                         select p;
            foreach (var item in list) {
                BindChilds(item , all);
            }
            return list.ToList();
        }

        void BindChilds(Document root , ICollection<Document> childs) {
            if (null != root) {
                if (null == root.Childs) {
                    root.Childs = new List<Document>();
                }
                var query = childs.Where(p => p.ParentId == root.Id);
                foreach (Document item in query) {
                    BindChilds(item , childs);
                    root.Childs.Add(item);
                }
            }
        }

        public System.Text.StringBuilder GetDirectoryXML(string specialtyId) {
            System.Text.StringBuilder build = new System.Text.StringBuilder();
            IList<Document> root = GetRoot(specialtyId);
            build.Append("<Categories>");
            foreach (var item in root) {
                BindChilds(item , build);
            }
            build.Append("<Categories>");
            return build;
        }
        void BindChilds(Document obj , System.Text.StringBuilder build) {
            foreach (var item in obj.Childs) {
                build.AppendFormat("<category id=\"{0}\" name=\"{1}\">" , item.Id , item.Name);
                BindChilds(item , build);
                build.Append("</category>");
            }
        }

        public IList<Document> Search(string key , string specialty = null) {
            if (string.IsNullOrWhiteSpace(key)) {
                return null;
            }
            var query = Context.Documents
                    .Where(p => p.IsFolder == 0 && p.Name.ToLower().Contains(key.ToLower()));
            if (null != specialty) {
                query = query.Where(p => p.SpecialtyId.Equals(specialty));
            }
            return query.ToList();

        }
    }

}
