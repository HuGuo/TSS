using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TSS.Models;
namespace TSS.BLL
{
    public class ExperimentRepository:Repository<Experiment,string>
    {
        public ExperimentRepository() { }

        public override void Update(Experiment entity)
        {
            using (Context db = new Context()) {
                Experiment experiment = db.Experiments.Find(entity.Id);
                if (null !=experiment) {
                    //foreach (ExpData item in experiment.Expdatas) {
                    //    db.Expdatas.Remove(item);
                    //}
                    int c = experiment.Expdatas.Count-1;
                    for (int i = c; i >=0; i--) {
                        db.ExpData.Remove(experiment.Expdatas.ElementAt(i));
                    }
                    experiment.Expdatas.Clear();
                    experiment.Expdatas = entity.Expdatas;
                    experiment.HTML = entity.HTML;
                    experiment.IsDEL = entity.IsDEL;
                    experiment.EquipmentID = entity.EquipmentID;
                    experiment.ExpDate = entity.ExpDate;
                    experiment.Title = entity.Title;
                    db.SaveChanges();
                }
            }
        }

        public override void Delete(string id)
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
