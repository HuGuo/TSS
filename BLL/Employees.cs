using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TSS.Models;

namespace TSS.BLL
{
    public class Employees : Repository<Employee, Guid>
    {
        public void Add(string name, string specialtyId)
        {
            Add(new Employee {
                Name = name,
                SpecialtyId = specialtyId
            });
        }

        public bool ExistsCode(string code)
        {
            using (Context db = new Context()) {
                int count = db.Employees.Count(p => p.Code == code);
                return count > 0;
            }
        }
    }
}
