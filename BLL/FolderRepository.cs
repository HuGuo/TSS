using System;
using System.Collections.Generic;
using System.Linq;
using TSS.Models;
namespace TSS.BLL
{
    public class FolderRepository:Repository<Folder,Guid>
    {
        public FolderRepository() { }

        public Folder GetRoot(string specialtyId) 
        {
            using (Context db=new Context()) {
                var all = db.Folders.Where(p => p.SpecialtyId == specialtyId).ToList();
                Folder root = all.FirstOrDefault(p => string.IsNullOrEmpty(p.SpecialtyId));
                BindChilds(root, all);
                return root;
            }
        }

        void BindChilds(Folder root,ICollection<Folder> childs) 
        {
            if (null != root) {
                if (null==root.Childs) {
                    root.Childs = new List<Folder>();
                }
                var query = childs.Where(p => p.ParentId == root.Id);
                foreach (Folder item in query) {
                    BindChilds(item, childs);
                    root.Childs.Add(item);
                }
            }
        }
    }

    public class FileRepository:Repository<File,Guid>
    {
        public FileRepository() { }

        public IList<File> Search(string key,string specialty="") {
            
            return null;
        }
    }
}
