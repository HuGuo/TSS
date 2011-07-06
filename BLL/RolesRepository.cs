﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
                foreach (var item in old.Rights) {
                    item.Permission = entity.Rights.First(p => p.Id.Equals(item.Id)).Permission;
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
