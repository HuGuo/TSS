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
                case "delte":
                    DeleteCategory(context);
                    break;
                case "rename":
                    RenameCategory(context);
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
        } catch (Exception ex) {
            context.Response.Write(ex.Message);
        }
    }

    void RenameCategory(HttpContext context) {
        try {
            string id = context.Request["id"];
            string name = context.Server.UrlDecode(context.Request["name"]);
            ExpCategory obj= RepositoryFactory<ExpCategoryRepository>.Get().Get(new Guid(id));
            if (null !=obj) {
                obj.Name = name;
                RepositoryFactory<ExpCategoryRepository>.Get().Update(obj);
            } else {
                context.Response.Write(Helper.NULLOBJECT);
            }
        } catch (Exception ex) {
            context.Response.Write(ex.Message);
        }
    }
    #endregion
}