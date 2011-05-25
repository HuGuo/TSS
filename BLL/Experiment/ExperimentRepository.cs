using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TSS.Models;

namespace TSS.BLL
{
    public class ExperimentRepository:Repository<Experiment,Guid>
    {
        public ExperimentRepository() { }

        public override void Update(Experiment entity)
        {
            using (Context db = new Context()) {
                Experiment experiment = db.Experiments.Find(entity.Id);
                if (null !=experiment) {
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

        public override void Delete(Guid id)
        {
            using (Context db = new Context()) {
                Experiment entity = db.Experiments.Find(id);
                if (null != entity) {
                    entity.IsDEL = 1;
                    db.SaveChanges();
                }
            }
        }

        public IList<Experiment> GetMuch(Guid equipmentID) {
            return null;
        }

        public IList<Experiment> PageOf(int pageIndex, int pageSize,Guid equipmentID, out int rowCount) {
            rowCount = 0;
            return null;
        }

        public IList<Experiment> GetMuch(Guid equipmentID,Guid exptemplateID) 
        {
            return null;
        }

        public IList<Experiment> PageOf(int pageIndex, int pageSize, Guid equipmentID, Guid exptemplateID, out int rowCount)
        {
            rowCount = 0;
            return null;
        }

        public IList<Experiment> GetByEquipmentCategory (Guid equipmentCategoryId){
            using (Context db=new Context()) {
                return db.Experiments
                    .Where(p => p.Equipment.EquipmentCategoryId == equipmentCategoryId)
                    .OrderBy(p=>p.ExpDate)
                    .ToList();                
            }
        }
    }
}
