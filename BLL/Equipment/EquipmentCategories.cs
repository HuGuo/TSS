using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TSS.Models;

namespace TSS.BLL
{
    public class EquipmentCategories : Repository<EquipmentCategory>
    {
        public void Add(string name, string parentId)
        {
            if (!string.IsNullOrEmpty(name)) {
                EquipmentCategory parent = null;
                Guid pid;
                if (Guid.TryParse(parentId, out pid)) {
                    parent = Get(pid);
                }

                Add(new TSS.Models.EquipmentCategory {
                    Id = Guid.NewGuid(),
                    Name = name,
                    ParentCategory = parent
                });
            }
        }

        public bool Remove(string id)
        {
            bool result = false;

            Guid categoryId;
            if (Guid.TryParse(id, out categoryId)) {
                var category = Get(categoryId);
                if (category != null && (category.Equipments == null || category.Equipments.Count == 0)) {
                    return Delete(category);
                }
            }

            return result;
        }

        public bool HasSubcategories(string id)
        {
            bool result = false;

            Guid categoryId;
            if (Guid.TryParse(id, out categoryId)) {
                var category = Get(categoryId);
                if (category != null && category.Subcategories != null && category.Subcategories.Count != 0) {
                    result = true;
                }
            }

            return result;
        }

        public bool Update(string id, string name)
        {
            bool result = false;

            Guid categoryId;
            if (!string.IsNullOrEmpty(name) && Guid.TryParse(id, out categoryId)) {
                var category = Get(categoryId);
                if (category != null) {
                    category.Name = name;

                    return Update(category);
                }
            }

            return result;
        }

        public string GetXml()
        {
            var result = new StringBuilder();

            result.Append("<?xml version=\"1.0\"?>\n\n");

            result.Append("<Categories>");
            result.Append(Walking(GetAll(), null, "<category id=\"{0}\" name=\"{1}\">", "</category>"));
            result.Append("</Categories>");

            return result.ToString();
        }

        public string GetJson()
        {
            var result = new StringBuilder();

            result.Append("[");
            result.Append(Walking(GetAll(), null, "{{\"id\":\"{0}\",\"text\":\"{1}\",\"children\":[", "]},").ToString().Replace(",]", "]").TrimEnd(','));
            result.Append("]");

            return result.ToString();
        }

        private static StringBuilder Walking(IList<EquipmentCategory> categories, EquipmentCategory parent, string begin, string end)
        {
            StringBuilder result = new StringBuilder();

            foreach (var i in categories.Where(c => c.ParentCategory == parent)) {
                result.Append(string.Format(begin, i.Id, i.Name));
                result.Append(Walking(categories, i, begin, end));
                result.Append(end);
            }

            return result;
        }
    }
}
