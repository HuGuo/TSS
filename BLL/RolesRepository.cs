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
                //foreach (var item in old.Rights) {
                //    var right = entity.Rights.FirstOrDefault(p => p.Id.Equals(item.Id));
                //    if (null!=right) {
                //        item.Permission = right.Permission;
                //    }
                //}
                if (null != entity.Rights) {
                    foreach (var item in entity.Rights) {
                        var right = old.Rights.FirstOrDefault(p => p.Id.Equals(item.Id));
                        if (null != right) {
                            right.Permission = item.Permission;
                        } else {
                            var newitem = new Right {
                                ModuleId = item.Id ,
                                Permission = item.Permission ,
                                Role = old
                            };
                            Context.Rights.Add(newitem);
                            old.Rights.Add(newitem);
                        }
                    }
                }
                Context.SaveChanges();
                
            }
        }
        public override bool Update(Role entity) {
            Update(entity.Id , entity);
            return true;
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
