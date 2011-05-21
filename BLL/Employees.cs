using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TSS.Models;

namespace TSS.BLL
{
    static class Employees
    {
        public static Employee Get(Guid id)
        {
            using (var context = new Context()) {
                return context.Employees.Find(id);
            }
        }

        public static IList<Employee> GetAll()
        {
            using (var context = new Context()) {
                return context.Employees.ToList();
            }
        }

        public static void Add(string name, string specialty)
        {
            using (var context = new Context()) {
                context.Employees.Add(new Employee {
                    Name = name,
                    SpecialtyId = specialty
                });

                context.SaveChanges();
            }
        }

        public static void Update(Employee employee)
        {
            using (var context = new Context()) {
                var o = context.Employees.Find(employee.Id);
                if (o != null) {
                    o.Name = employee.Name;
                    o.SpecialtyId = employee.SpecialtyId;

                    context.SaveChanges();
                }
            }
        }

        public static void Delete(Employee employee)
        {
            throw new NotImplementedException();
        }
    }
}
