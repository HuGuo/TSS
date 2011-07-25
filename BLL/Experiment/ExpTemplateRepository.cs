using System;
using System.Collections.Generic;
using System.Linq;
using TSS.Models;

namespace TSS.BLL
{
    public class ExpTemplateRepository : Repository<ExpTemplate>
    {
        public override bool Delete(object id) {
            ExpTemplate entity = Context.ExpTemplates.Find(id);
            if (null != entity) {
                int exps = Context.Experiments.Count(p => p.ExpTemplateID == entity.Id);
                if (exps > 0) {
                    entity.Enable = 0;
                } else {
                    Context.ExpTemplates.Remove(entity);
                }
                return Context.SaveChanges()>0;
            }
            return false;
        }

        public override IList<ExpTemplate> GetAll() {
            return GetList(TemplateStatus.All);
        }

        public IList<ExpTemplate> GetList(TemplateStatus status) {
            return GetList(status , null);
        }

        public IList<ExpTemplate> GetList(TemplateStatus status , int pageIndex , int pageSize , out int rowCount) {
            return GetList(status , null , pageIndex , pageSize , out rowCount);
        }

        public IList<ExpTemplate> GetList(TemplateStatus status,string specialtyId) {
            int rowcount = 0;
            return GetList(status , specialtyId , -1 , 0 , out rowcount);
        }

        public IList<ExpTemplate> GetList(TemplateStatus status,string specialtyId,int pageIndex,int pageSize,out int rowCount) {
            
            var query = BuildQuery(Context , status , specialtyId);
            rowCount = query.Count();
            int skip = GetSkip(pageIndex , pageSize , rowCount);
            if (pageIndex<0) {
                return query.ToList();
            } else {
                return query
                    .OrderBy(p => p.Id)
                    .Skip(skip)
                    .Take(pageSize)
                    .ToList();
            }
        }

        //help method
        private IQueryable<ExpTemplate> BuildQuery(Context db,TemplateStatus status,string specialtyId) {
            var query = db.ExpTemplates.AsQueryable();
            if (!string.IsNullOrEmpty(specialtyId)) {
                query = query.Where(p=>p.SpecialtyId.Equals(specialtyId));
            }
            switch (status) {
                case TemplateStatus.All:
                    break;
                case TemplateStatus.Enable:
                    query = query.Where(p=>p.Enable==1);
                    break;
                case TemplateStatus.Disabled:
                    query = query.Where(p => p.Enable == 0);
                    break;
            }
            return query;
        }

        /// <summary>
        /// 关联设备到试验报告模板
        /// </summary>
        /// <param name="exptemplateId"></param>
        /// <param name="equipmentId"></param>
        public void BindEquipment(Guid exptemplateId,params Guid[] equipmentId) {
            ExpTemplate obj = Context.ExpTemplates.Find(exptemplateId);
            if (null !=obj) {
                Context.Entry(obj).Collection(p => p.Equipments).Load();
                var query= Context.Equipments.Where(p => equipmentId.Contains(p.Id)).ToArray();
                EquipmentEqualityComparer equipmentEqc=new EquipmentEqualityComparer();
                foreach (var item in query) {
                    if (obj.Equipments.Contains(item , equipmentEqc)) {
                        continue;
                    }
                    obj.Equipments.Add(item);
                    item.ExpTemplates.Add(obj);
                }
                Context.SaveChanges();
            }
        }

        /// <summary>
        /// 解除设备与试验报告关联
        /// </summary>
        /// <param name="exptemplateId"></param>
        /// <param name="equipmentId"></param>
        public void UnBindEquipment(Guid exptemplateId , params Guid[] equipmentId) {
            ExpTemplate obj = Context.ExpTemplates.Find(exptemplateId);
            if (null != obj) {
                Context.Entry(obj).Collection(p => p.Equipments).Load();
                var query = obj.Equipments.Where(p => equipmentId.Contains(p.Id)).ToArray();
                foreach (var item in query) {
                    obj.Equipments.Remove(item);
                    item.ExpTemplates.Remove(obj);
                }
                Context.SaveChanges();
            }
        }

        /// <summary>
        /// 根据ID，标题搜索试验报告模板
        /// </summary>
        /// <param name="idOrTitle"></param>
        /// <returns></returns>
        public IList<ExpTemplate> Search(string idOrTitle) {
            return Context.ExpTemplates
                .Where(p => p.Id.ToString().Equals(idOrTitle) || p.Title.Contains(idOrTitle))
                .ToList();
        }
    }

    public enum TemplateStatus
    {
        /// <summary>
        /// 全部
        /// </summary>
        All,
        /// <summary>
        /// 正常启用
        /// </summary>
        Enable,
        /// <summary>
        /// 不可用
        /// </summary>
        Disabled
    }
}
