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
                default:
                    break;
            }
        }
    }

    #endregion

    #region methods
    void ResponseTreeHTML(HttpContext context) {
        string s = context.Request["s"];
        XmlTextReader reader = new XmlTextReader(new StringReader(RepositoryFactory<ExpCategoryRepository>.Get().GetRoots(s, Formate.Xml)));
        Helper.ResponseEasyuiTreeHTML(reader , context);
    }

    void ResponseTreeJson(HttpContext context) {
        context.Response.ContentType = "application/json; charset=utf-8";
        string s = context.Request["s"];
        context.Response.Write(RepositoryFactory<ExpCategoryRepository>.Get().GetRoots(s , Formate.Json));
    }
    #endregion
}