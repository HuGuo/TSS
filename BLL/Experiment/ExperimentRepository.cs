using System;
using System.Collections.Generic;
using System.Linq;
using TSS.Models;

namespace TSS.BLL
{
    public class ExperimentRepository : Repository<Experiment>
    {
        public ExperimentRepository() { }
        public void Add(Experiment entity,Action<Experiment> onAdd=null) {
            base.Add(entity);
            if (null !=onAdd) {
                onAdd(entity);
            }
        }

        public void RemoveAttachment(Guid id,Guid attachment,Action<string> onRemove) {
            Experiment obj = Context.Experiments.Find(id);
            if (null !=obj) {
                ExpAttachment item=obj.Attachments.Single(p => p.Id.Equals(attachment));
                if (null !=item) {
                    Context.ExpAttachments.Remove(item);
                    obj.Attachments.Remove(item);
                    Context.SaveChanges();
                    onRemove(item.Id + System.IO.Path.GetExtension(item.FileName));
                }
            }
        }

        public void AddAttachment(Guid id , ICollection<ExpAttachment> items , Action<ICollection<ExpAttachment>> onError) {
            try {
                Experiment obj = Context.Experiments.Find(id);
                if (null != obj) {
                    foreach (var item in items) {
                        item.Experiment = obj;
                        obj.Attachments.Add(item);
                    }
                    Context.SaveChanges();
                }
            } catch (Exception) {
                onError(items);
            }
        }
        public Experiment GetLastExperiment(Guid templateId,Guid equipmentId) {
            return Context.Experiments.Where(p => p.EquipmentID.Equals(equipmentId) && p.ExpTemplateID.Equals(templateId))
                .OrderByDescending(p => p.ExpDate)
                .FirstOrDefault();
        }

        public override bool Update(Experiment entity) {
            Experiment experiment = Context.Experiments.Find(entity.Id);
            if (null != experiment) {
                int c = experiment.Expdatas.Count - 1;
                for (int i = c; i >= 0; i--) {
                    Context.ExpData.Remove(experiment.Expdatas.ElementAt(i));
                }
                experiment.Expdatas.Clear();
                //experiment.Expdatas = entity.Expdatas;
                foreach (var item in entity.Expdatas) {
                    experiment.Expdatas.Add(item);
                }
                experiment.HTML = entity.HTML;
                experiment.EquipmentID = entity.EquipmentID;
                experiment.ExpDate = entity.ExpDate;
                experiment.Title = entity.Title;
                return Context.SaveChanges() > 0;

            }
            return false;
        }

        /// <summary>
        /// 指定试验报告模板的试验记录列表
        /// </summary>
        /// <param name="exptemplateId"></param>
        /// <param name="stime"></param>
        /// <param name="etime"></param>
        /// <returns></returns>
        public IList<Experiment> GetMuch(Guid exptemplateId,DateTime? stime=null,DateTime? etime=null) {
            return Context.Experiments.Where(p => p.ExpTemplateID.Equals(exptemplateId))
                    .OrderByDescending(p => p.ExpDate).ToList();
        }

        public IList<Experiment> PageOf(int pageIndex , int pageSize , Guid equipmentID , out int rowCount) {

            int skip = pageSize * (pageIndex - 1);
            rowCount = Context.Experiments.Count(p => p.EquipmentID == equipmentID);
            var query = Context.Experiments.Where(p => p.EquipmentID == equipmentID)
                .OrderByDescending(p => p.ExpDate)
                .Skip(skip)
                .Take(pageSize);
            return query.ToList();

        }
        
        /// <summary>
        /// 指定设备 指定试验报告模板的试验记录列表
        /// </summary>
        /// <param name="equipmentID"></param>
        /// <param name="exptemplateID"></param>
        /// <returns></returns>
        public IList<Experiment> GetMuch(Guid equipmentID , Guid exptemplateID) {
            return Context.Experiments.Where(p => p.EquipmentID == equipmentID && p.ExpTemplateID == exptemplateID)
                    .OrderByDescending(p => p.ExpDate).ToList();

        }

        public IList<Experiment> PageOf(int pageIndex , int pageSize , Guid equipmentID , Guid exptemplateID , out int rowCount) {
            int skip = pageSize * (pageIndex - 1);
            rowCount = Context.Experiments.Count(p => p.EquipmentID == equipmentID && p.ExpTemplateID == exptemplateID);
            return Context.Experiments.Where(p => p.EquipmentID == equipmentID && p.ExpTemplateID == exptemplateID)
                .OrderByDescending(p => p.ExpDate)
                .Skip(skip)
                .Take(pageSize)
                .ToList();

        }

        public IList<Experiment> GetByEquipmentCategory(Guid equipmentCategoryId , string specialtyId) {

            return Context.Experiments
                    .Where(p => p.Equipment.SpecialtyId == specialtyId && p.Equipment.EquipmentCategoryId == equipmentCategoryId)
                    .OrderBy(p => p.ExpDate)
                    .ToList();

        }

        public IList<ChartData> GetChartData(string coord , DateTime? startTime , DateTime? endTime , params string[] equipmentId) {
            var query = Context.ExpData.Where(p => p.GUID == coord);
            if (null!=equipmentId) {
                List<Guid> equipments = (from v in equipmentId
                                         select new Guid(v)).ToList();
                if (equipments.Count > 0) {
                    query = query.Where(p => equipments.Contains(p.Experiment.EquipmentID));
                }
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
                             Coord = p.GUID ,
                             CoordValue = p.Value ,
                             EquipmentId = p.Experiment.EquipmentID ,
                             ExpDate = p.Experiment.ExpDate ,
                             ExperimentId = p.Experiment.Id ,
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
