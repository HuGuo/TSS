using System.Collections.Generic;
using System.Linq;
using TSS.Models;

namespace TSS.BLL
{
    public class Employees : Repository<Employee>
    {
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

        public void UpdateRoles(Employee entity) {
            Employee obj = Context.Employees.Find(entity.Id);
                if (null !=obj) {
                    foreach (var item in entity.Roles) {
                        Role r= obj.Roles.FirstOrDefault(p => p.Id.Equals(item.Id));
                        if (null !=r) {
                            obj.Roles.Remove(r);
                            r.Employees.Remove(obj);
                        } else {
                            r = Context.Roles.Find(item.Id);
                            if (null !=r) {
                                r.Employees.Add(obj);
                                obj.Roles.Add(r);
                            }
                        }
                    }
                    Context.SaveChanges();
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
