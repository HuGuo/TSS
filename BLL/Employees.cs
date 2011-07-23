using System.Collections.Generic;
using System.Linq;
using TSS.Models;
using System;

namespace TSS.BLL
{
    public class Employees : Repository<Employee>
    {
        public IList<Employee> GetAll(int pageIndex,int pageSize,out int rowCount) {
            rowCount = Context.Employees.Count();
            int skip = GetSkip(pageIndex , pageSize , rowCount);
            return Context.Employees
                .OrderBy(p => p.Id)
                .Skip(skip)
                .Take(pageSize)
                .ToList();
        }

        public bool ExistsCode(string code)
        {
            int count = Context.Employees.Count(p => p.Code.Equals(code));
                return count > 0;

        }

        public Employee GetByCode(string code) {
            return Context.Employees.FirstOrDefault(p => p.Code.Equals(code));
        }

        public override bool Add(Employee entity) {
            Role defaultRole = Context.Roles.First(p => p.Name.ToLower().Equals("guest"));
            if (null==entity.Roles) {
                entity.Roles = new List<Role>();
            }
            entity.Roles.Add(defaultRole);
            Context.Employees.Add(entity);
            defaultRole.Employees.Add(entity);
            return Context.SaveChanges()>0;
        }

        /// <summary>
        /// 移除用户角色（从角色中移除用户）
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="roleid"></param>
        public void RemoveUserFromRole(System.Guid userid , System.Guid roleid) {
            Employee obj = Context.Employees.Find(userid);
            if (null !=obj) {
                Role item = obj.Roles.FirstOrDefault(p => p.Id.Equals(roleid));
                if (null !=item) {
                    obj.Roles.Remove(item);
                    if (obj.Roles.Count==0) {
                        throw new Exception("最少保留一个角色，否则用户无法登录");
                    }
                    Context.SaveChanges();
                }
            }
        }

        /// <summary>
        /// 为用户添加角色（把用户加入角色）
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="roleid"></param>
        public void AddUserToRole(System.Guid userid , System.Guid roleid) {
            Employee obj = Context.Employees.Find(userid);
            
            if (null != obj) {
                Role item = obj.Roles.FirstOrDefault(p => p.Id.Equals(roleid));
                if (null ==item) { //角色中不包含用户

                    item = Context.Roles.Find(roleid);
                    if (null !=item) {
                        obj.Roles.Add(item);
                        Context.SaveChanges();
                    }
                }
            }
        }

        public IList<Employee> Search(string key) {
            if (string.IsNullOrWhiteSpace(key)) {
                return null;
            }
            return Context.Employees.Where(p => p.Name.Contains(key)).ToList();
        }

        
    }
}
