using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TSS.Models;
using System.Data.Entity;

namespace TSS.BLL
{
    public class SupervisionNewRepository : Repository<SupervisionNew>
    {
        public override SupervisionNew Get(object id)
        {
            using (var dbContext = new Context())
            {
                int tempId = (int)id;
                return dbContext.SupervisionNews
                    .Where(s => s.Id == tempId)
                    .Include(s => s.SupervisionNewType)
                    .SingleOrDefault();   
            }
        }

        public IList<SupervisionNew> GetByNewType(int supervisionTypeid)
        {
            using (var dbContext = new Context())
            {
                return dbContext.SupervisionNews
                    .Where(s => s.SupervisionNewId == supervisionTypeid)
                    .Include(s => s.SupervisionNewType)
                    .ToList();
            }
        }
    }
}
