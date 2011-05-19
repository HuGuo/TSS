using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TSS.Models;
namespace TSS.BLL
{
    public class ExperimentRepository:Repository<Experiment,int>
    {
        public ExperimentRepository() { }

        public override void Delete(int id)
        {
            using (Context db = new Context()) {
                Experiment entity = db.Experiments.Find(id);
                if (null != entity) {
                    entity.IsDEL = 1;
                    db.SaveChanges();
                }
            }
        }
    }
}
