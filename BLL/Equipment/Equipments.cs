using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using TSS.Models;

namespace TSS.BLL
{
    public class Equipments : Repository<Equipment>
    {
        public IList<Equipment> GetList(string categoryId, string specialtyId)
        {
            IList<Equipment> result = null;

            Guid cId;
            if (Guid.TryParse(categoryId, out cId) && !string.IsNullOrEmpty(specialtyId)) {
                var c = Context.EquipmentCategories.Find(cId);
                var s = Context.Specialties.Find(specialtyId);
                if (c != null && s != null) {
                    result = Context.Equipments.Where(e =>
                        e.EquipmentCategoryId == c.Id &&
                        e.SpecialtyId == s.Id)
                        .ToList();
                }
            } else if (Guid.TryParse(categoryId, out cId)) {
                var c = Context.EquipmentCategories.Find(cId);
                if (c != null) {
                    result = Context.Equipments.Where(e =>
                        e.EquipmentCategory.Id == c.Id)
                        .Include("Specialty")
                        .ToList();
                }
            } else if (!string.IsNullOrEmpty(specialtyId)) {
                var s = Context.Specialties.Find(specialtyId);
                if (s != null) {
                    result = Context.Equipments.Where(e =>
                        e.Specialty.Id == s.Id)
                        //.Include("EquipmentDetails")
                        .ToList();
                }
            } else {
                result = Context.Equipments
                    //.Include("EquipmentDetails")
                    .Include("EquipmentCategory")
                    .Include("Specialty")
                    .ToList();
            }

            return result;
        }

        public void Add(string name, string code, string categoryId, string specialtyId)
        {
            Guid cId;
            if (Guid.TryParse(categoryId, out cId) &&
                !string.IsNullOrEmpty(specialtyId)) {
                var c = Context.EquipmentCategories.Find(cId);
                var s = Context.Specialties.Find(specialtyId);
                if (c != null && s != null) {
                    Add(new Equipment {
                        Id = Guid.NewGuid(),
                        Name = name,
                        Code = code,
                        EquipmentCategory = c,
                        Specialty = s
                    });
                }
            }
        }

        private Equipment Get(string id)
        {
            Equipment result = null;

            Guid equipmentId;
            if (Guid.TryParse(id, out equipmentId)) {
                return Get(equipmentId);
            }

            return result;
        }

        #region EquipmentDetail
        public IList<EquipmentDetail> GetDetails(Guid equipmentId)
        {
            IList<EquipmentDetail> result = null;

            var e = Get(equipmentId);
            if (e != null && e.EquipmentDetails != null) {
                result = e.EquipmentDetails.ToList();
            }

            return result;
        }

        public void AddDetail(Guid equipmentId, string lable, string value)
        {
            var e = Get(equipmentId);
            if (e != null) {
                e.EquipmentDetails.Add(new EquipmentDetail {
                    Lable = lable,
                    Value = value
                });

                Update(e);
            }
        }

        public void RemoveDetail(Guid equipmentId, int id)
        {
            var e = Get(equipmentId);
            if (e != null && e.EquipmentDetails != null) {
                var d = e.EquipmentDetails.SingleOrDefault(o => o.Id == id);
                if (d != null) {
                    e.EquipmentDetails.Remove(d);

                    Update(e);
                }
            }
        }

        public void UpdateDetail(Guid equipmentId, int id, string lable, string value)
        {
            var e = Get(equipmentId);
            if (e != null && e.EquipmentDetails != null) {
                var d = e.EquipmentDetails.SingleOrDefault(o => o.Id == id);
                if (d != null) {
                    d.Lable = lable;
                    d.Value = value;

                    Update(e);
                }
            }
        }

        public string GetDetailValue(string equipmentId, string lable)
        {
            string result = string.Empty;

            var e = Get(equipmentId);
            if (e != null && e.EquipmentDetails != null) {
                var d = e.EquipmentDetails.FirstOrDefault(o => o.Lable == lable);
                if (d != null) {
                    result = d.Value;
                }
            }

            return result;
        }
        #endregion
    }
}
