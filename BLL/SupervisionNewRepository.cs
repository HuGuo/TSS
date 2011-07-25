using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using TSS.Models;

namespace TSS.BLL
{
    public class SupervisionNewRepository : Repository<SupervisionNew>
    {
        public override SupervisionNew Get(object id)
        {
            using (var dbContext = new Context()) {
                int tempId = int.Parse(id.ToString());
                return dbContext.SupervisionNews
                    .Where(s => s.Id == tempId)
                    .Include(s => s.SupervisionNewType)
                    .SingleOrDefault();
            }
        }

        public IList<SupervisionNew> GetByNewType(int supervisionTypeid)
        {
            using (var dbContext = new Context()) {
                return dbContext.SupervisionNews
                    .Where(s => s.SupervisionNewTypeId == supervisionTypeid)
                    .Include(s => s.SupervisionNewType)
                    .ToList();
            }
        }
    }
}
