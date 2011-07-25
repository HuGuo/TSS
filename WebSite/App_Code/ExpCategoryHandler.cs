using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TSS.Models;
using TSS.BLL;
using System.Xml;
using System.IO;

public class ExpCategoryHandler:IHttpHandler
{
    #region IHttpHandler Members

    public bool IsReusable {
        get { return false; }
    }

    public void ProcessRequest(HttpContext context) {
        string op = context.Request["op"];
        if (!string.IsNullOrWhiteSpace(op)) {
            switch (op.ToLower()) {
                case "json":
                    ResponseTreeJson(context);
                    break;
                case "xml":
                    ResponseTreeHTML(context);
                    break;
                case "delete":
                    DeleteCategory(context);
                    break;
                case "rename":
                    RenameCategory(context);
                    break;
                case "add":
                    Add(context);
                    break;
                default:
                    context.Response.Write("{\"msg\":\"无效参数\"}");
                    break;
            }
        }
    }

    #endregion

    #region methods
    void ResponseTreeHTML(HttpContext context) {
        string s = context.Request[Helper.queryParam_specialty];
        XmlTextReader reader = new XmlTextReader(new StringReader(RepositoryFactory<ExpCategoryRepository>.Get().GetRoots(s, Formate.Xml)));
        Helper.ResponseEasyuiTreeHTML(reader , context);
    }

    void ResponseTreeJson(HttpContext context) {
        context.Response.ContentType = "application/json; charset=utf-8";
        string s = context.Request[Helper.queryParam_specialty];
        context.Response.Write(RepositoryFactory<ExpCategoryRepository>.Get().GetRoots(s , Formate.Json));
    }

    void DeleteCategory(HttpContext context) {
        string id = context.Request["id"];
        try {
            RepositoryFactory<ExpCategoryRepository>.Get().Delete(new Guid(id));
            //log
            AppLog.Write("删除试验报告分类" , AppLog.LogMessageType.Info , "id=" + id , this.GetType());
        } catch (Exception ex) {
            AppLog.Write("删除试验报告分类 出错" , AppLog.LogMessageType.Error , "id=" + id , ex , this.GetType());
            context.Response.Write(ex.Message);
        }
    }

    void RenameCategory(HttpContext context) {
        string id = context.Request["id"];
        try {
            string name = context.Server.UrlDecode(context.Request["name"]);            
            ExpCategory obj= RepositoryFactory<ExpCategoryRepository>.Get().Get(new Guid(id));
            
            if (null !=obj) {
                string log_oldName = obj.Name;
                obj.Name = name;
                RepositoryFactory<ExpCategoryRepository>.Get().Update(obj.Id,obj);
                //log
                AppLog.Write(string.Format("重命名实验报告分类 \"{0}\" 为 \"{1}\"",log_oldName,name) , AppLog.LogMessageType.Info , "id=" + id , this.GetType());
            } else {
                context.Response.Write(Helper.NULLOBJECT);
            }
        } catch (Exception ex) {
            AppLog.Write("重命名实验报告分类 出错" , AppLog.LogMessageType.Error , "id=" + id , ex , this.GetType());
            context.Response.Write(ex.Message);
        }
    }

    void Add(HttpContext context) {
        context.Response.ContentType = "application/json; charset=utf-8";
        string pid = context.Request["pid"];
        string name = context.Request["name"];
        string s = context.Request["s"];
        try {
            ExpCategory obj = new ExpCategory {
                Id = new Guid() ,
                Name = context.Server.UrlDecode(name) ,
                ParentId = string.IsNullOrWhiteSpace(pid) ? null : (Guid?)new Guid(pid) ,
                SpecialtyId = s
            };
            RepositoryFactory<ExpCategoryRepository>.Get().Add(obj);
            //log
            AppLog.Write("添加试验报告分类 \"" + obj.Name + "\"" , AppLog.LogMessageType.Info , "id=" + obj.Id.ToString() , this.GetType());

            context.Response.Write("{\"id\":\"" + obj.Id.ToString() + "\"}");
        } catch (Exception ex) {
            AppLog.Write("添加试验报告分类 出错" , AppLog.LogMessageType.Error , "" , ex , this.GetType());

            context.Response.Write("{\"msg\":\""+ex.Message+"\"}");
        }
    }
    #endregion
}