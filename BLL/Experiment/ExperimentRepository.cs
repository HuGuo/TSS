﻿using System;
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
                    experiment.EquipmentID = entity.EquipmentID;
                    experiment.ExpDate = entity.ExpDate;
                    experiment.Title = entity.Title;
                    db.SaveChanges();
                }
            }
        }
        
        public IList<Experiment> GetMuch(Guid equipmentID) {
            using (Context db=new Context()) {
                return db.Experiments.Where(p => p.EquipmentID == equipmentID)
                    .OrderByDescending(p => p.ExpDate).ToList();
            }
        }

        public IList<Experiment> PageOf(int pageIndex, int pageSize,Guid equipmentID, out int rowCount) {            
            using (Context db=new Context()) {
                int skip = pageSize * (pageIndex - 1);
                rowCount = db.Experiments.Count(p => p.EquipmentID == equipmentID);
                var query = db.Experiments.Where(p => p.EquipmentID == equipmentID)
                    .OrderByDescending(p => p.ExpDate)
                    .Skip(skip)
                    .Take(pageSize);
                return query.ToList();
            }
        }

        public IList<Experiment> GetMuch(Guid equipmentID,Guid exptemplateID) 
        {
            using (Context db = new Context()) {
                return db.Experiments.Where(p => p.EquipmentID == equipmentID && p.ExpTemplateID==exptemplateID)
                    .OrderByDescending(p => p.ExpDate).ToList();
            }
        }

        public IList<Experiment> PageOf(int pageIndex, int pageSize, Guid equipmentID, Guid exptemplateID, out int rowCount)
        {
            using (Context db = new Context()) {
                int skip = pageSize * (pageIndex - 1);
                rowCount = db.Experiments.Count(p => p.EquipmentID == equipmentID && p.ExpTemplateID == exptemplateID);
                return db.Experiments.Where(p => p.EquipmentID == equipmentID && p.ExpTemplateID==exptemplateID)
                    .OrderByDescending(p => p.ExpDate)
                    .Skip(skip)
                    .Take(pageSize)
                    .ToList();
            }
        }

        public IList<Experiment> GetByEquipmentCategory (Guid equipmentCategoryId,string specialtyId){
            using (Context db=new Context()) {
                return db.Experiments
                    .Where(p => p.Equipment.SpecialtyId == specialtyId && p.Equipment.EquipmentCategoryId == equipmentCategoryId)
                    .OrderBy(p => p.ExpDate)
                    .ToList();
            }
        }

        public IList<ChartData> GetChartData(string coord,DateTime? startTime,DateTime? endTime,params string[] equipmentId) {
            List<Guid> equipments = (from v in equipmentId
                                     select new Guid(v)).ToList();
            using (Context db=new Context()) {
                var query = db.ExpData.Where(p => p.GUID == coord);

                if (equipments.Count > 0) {
                    query = query.Where(p => equipments.Contains(p.Experiment.EquipmentID));
                }

                if (startTime.HasValue && endTime.HasValue) {
                    query = query.Where(p => p.Experiment.ExpDate >= startTime && p.Experiment.ExpDate <= endTime);
                } else if (startTime.HasValue) {
                    query = query.Where(p => p.Experiment.ExpDate >= startTime);
                } else if (endTime.HasValue) {
                    query = query.Where(p => p.Experiment.ExpDate <= endTime);
                }

                var result = from p in query
                             select new ChartData {
                                 Coord = p.GUID,
                                 CoordValue = p.Value,
                                 EquipmentId = p.Experiment.EquipmentID,
                                 ExpDate = p.Experiment.ExpDate,
                                 ExperimentId = p.Experiment.Id,
                                 ExpResult = p.Experiment.Result
                             };
                return result.OrderBy(p => p.ExpDate).ToList();
                //IQueryable<Experiment> KT = db.Experiments.Where(p => p.ExpTemplateID == tmpId);
                //IQueryable<ExpData> FT = db.ExpData.Where(m => m.GUID == coord && m.Value.HasValue);
                //var query = KT.Join(FT,
                //    p => p,
                //    v => v.Experiment,
                //    (p, v) => new ChartData {
                //        ExperimentId = p.Id,
                //        ExpResult = p.Result,
                //        ExpDate = p.ExpDate,
                //        EquipmentId = p.EquipmentID,
                //        Coord = v.GUID,
                //        CoordValue = v.Value.Value
                //    });
                //return query.OrderBy(p=>p.ExpDate).ToList();
            }
        }
    }
}
