using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using TSS.Models;
namespace TSS.BLL
{
    public class ExpCategoryRepository:Repository<ExpCategory>
    {
        public IList<ExpCategory> GetRoots(string specialtyId) {
            //IList<ExpCategory> list = Context.ExpCategories
            //    .Where(p => p.SpecialtyId.Equals(specialtyId))
            //    .ToList();
            //IList<ExpCategory> roots = list.Where(p => p.ParentId.Equals(null)).ToList();
            ////foreach (var item in roots) {
            ////    BindChilds(item , list);
            ////}
            return Context.ExpCategories.Where(p => p.SpecialtyId.Equals(specialtyId) && !p.ParentId.HasValue).ToList();
            //return roots;
        }

        void BindChilds(ExpCategory parent , IList<ExpCategory> collection) {
            var childs = collection.Where(p => p.ParentId.Equals(parent.Id)).ToList();
            parent.SubCategories = childs;
            foreach (ExpCategory item in childs) {
                BindChilds(item , collection);
            }
        }

        public string GetRoots(string specialtyId,Formate formate) {
            IList<ExpCategory> list = GetRoots(specialtyId);
            System.Text.StringBuilder build = new System.Text.StringBuilder();
            //default json
            string begin = "{{\"id\":\"{0}\",\"text\":\"{1}\",\"children\":[";
            string end = "]},";

            switch (formate) {
                case Formate.Xml:
                    begin = "<category id=\"{0}\" name=\"{1}\">";
                    end = "</category>";
                    build.Append("<?xml version=\"1.0\"?>");
                    build.Append("<Categories>");
                    break;
                case Formate.Json:
                    build.Append("[");
                    break;
            }
            foreach (var item in list) {
                BuildHelp(item , build , begin , end);
            }
            switch (formate) {
                case Formate.Xml:
                    build.Append("</Categories>");
                    break;
                case Formate.Json:
                    build.Append("]");
                    build.Replace("},]" , "}]");
                    break;
            }
            return build.ToString();
        }

        void BuildHelp(ExpCategory parent,System.Text.StringBuilder build,string begin,string end) {
            build.AppendFormat(begin , parent.Id , parent.Name);
            foreach (var item in parent.SubCategories) {
                BuildHelp(item , build , begin , end);
            }
            
            build.Append(end);
        }
    }

    public enum Formate
    {
        Xml,
        Json
    }
}
