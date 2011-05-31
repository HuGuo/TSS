using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TSS.Models;
using TSS.BLL;
/// <summary>
/// Summary description for deleteEntity
/// </summary>
public class deleteEntity:IHttpHandler
{
    #region IHttpHandler Members

    public bool IsReusable
    {
        get { return false; }
    }

    public void ProcessRequest(HttpContext context)
    {
        string op = context.Request.QueryString["op"];
        if (!string.IsNullOrWhiteSpace(op)) {
            switch (op.ToLower()) {
                case "del-certificate":
                    DeleteCertificate(context);
                    break;
                default:
                    break;
            }
        } else {
            context.Response.Write("参数错误");
        }
    }
    #endregion

    void DeleteCertificate(HttpContext context) {
        string id = context.Request.QueryString["id"];
        try {
            CertificateRepository repository = new CertificateRepository();
            repository.Delete(new Guid(id));
        } catch (Exception ex) {
            context.Response.Write(ex.Message);
        }
        context.Response.End();
    }
}