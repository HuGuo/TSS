using System.Linq;
using TSS.Models;

namespace TSS.BLL
{
    public class EmployeeRepository:Repository<Employee,string>
    {
        public bool ExistsCode(string code) {
            using (Context db=new Context()) {
                int count = db.Employees.Count(p => p.Code == code);
                return count > 0;
            }
        }
    }
}
