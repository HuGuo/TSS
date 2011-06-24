﻿using System;
using System.Web;
using TSS.BLL;

public class deleteHandler: IHttpHandler
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
                case "del-workflow":
                    DeleteWorkflow(context);
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
            RepositoryFactory<CertificateRepository>.Get().Delete(new Guid(id));
        } catch (Exception ex) {
            context.Response.Write(ex.Message);
        }
        context.Response.End();
    }

    void DeleteWorkflow(HttpContext context) {
        string id = context.Request["id"];
        try {
            workflow.WFRepository.Delete(id , true);
        } catch (Exception ex) {
            context.Response.Write(ex.Message);
        }
    }
}