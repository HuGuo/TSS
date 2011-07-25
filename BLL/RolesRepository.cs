using System;
using System.Linq;
using TSS.Models;

namespace TSS.BLL
{
    public class RolesRepository : Repository<Role>
    {
        public Role GetDefaultRole() {
            return Context.Roles.First(p => p.Name.Equals("guest"));
        }

        public override void Update(object key , Role entity) {
            Role old = Context.Roles.Find(entity.Id);
            if (old != null) {
                if (old.Name.ToLower()=="guest" && entity.Name.ToLower() != "guest") {
                    throw new Exception("“guest”为系统默认角色名，请更换其他角色名称");
                }
                
                Context.Entry(old).CurrentValues.SetValues(entity);
                
                Context.SaveChanges();
                
            }
        }
        public override bool Update(Role entity) {
            Update(entity.Id , entity);
            return true;
        }

        public void UpdateRight(Role entity) {
            Role old = Context.Roles.Find(entity.Id);
            if (old != null) {
                int c = old.Rights.Count-1;
                for (int i = c; i >= 0; i--) {
                    Context.Rights.Remove(old.Rights.ElementAt(i));
                }
                old.Rights.Clear();
                foreach (var item in entity.Rights) {
                    item.Role = old;
                    old.Rights.Add(item);
                }
                Context.SaveChanges();
            }
        }
        public override bool Delete(object id) {
            Role entity = Get(id);
            if (entity.Name.ToLower() == "guest") {
                throw new Exception("“guest”为系统默认角色，无法删除");
            }
            return base.Delete(id);
        }
    }
}
