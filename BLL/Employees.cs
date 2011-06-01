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

        public bool ExistsCode(string p)
        {
            throw new NotImplementedException();
        }
    }
}
