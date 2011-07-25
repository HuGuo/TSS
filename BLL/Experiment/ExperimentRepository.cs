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
        /// 删除试验报告附件
        /// </summary>
        /// <param name="id"></param>
        /// <param name="attachment"></param>
        /// <param name="onRemove"></param>
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

        /// <summary>
        /// 添加试验报告个附件
        /// </summary>
        /// <param name="id"></param>
        /// <param name="items"></param>
        /// <param name="onError"></param>
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
        
        public IList<Experiment> GetList(Guid? exptemplateId,Guid? equipmentId) {
            int rowcount=0;
            return GetList(exptemplateId , equipmentId , -1 , 0 , out rowcount);
        }

        public IList<Experiment> GetList(Guid? exptemplateId , Guid? equipmentId,int pageIndex,int pageSize,out   int rowCount) {
            
            var query = BuildQuery(Context , exptemplateId , equipmentId);
            rowCount = query.Count();
            int skip = GetSkip(pageIndex , pageSize , rowCount);
            query = query.OrderByDescending(p => p.ExpDate);
            if (pageIndex<0) {
                return query.ToList();
            } else {
                return query
                    .OrderBy(p => p.Id).Skip(skip).Take(pageSize).ToList();
            }
        }

        //help method
        private IQueryable<Experiment> BuildQuery(Context db , Guid? exptemplateId, Guid? equipmentId) {
            var query = Context.Experiments.AsQueryable();
            if (exptemplateId.HasValue) {
                query = query.Where(p => p.ExpTemplateID.Equals(exptemplateId.Value));
            }
            if (equipmentId.HasValue) {
                query = query.Where(p => p.EquipmentID.Equals(equipmentId.Value));
            }

            return query;
        }

        public IList<Experiment> GetByEquipmentCategory(Guid equipmentCategoryId , string specialtyId) {

            return Context.Experiments
                    .Where(p => p.Equipment.SpecialtyId.Equals(specialtyId) && p.Equipment.EquipmentCategoryId.Equals(equipmentCategoryId))
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
