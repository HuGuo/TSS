using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TSS.Models;
using TSS.BLL;

public class EmployeeHandler:IHttpHandler
{
	public EmployeeHandler()
	{
	}

    #region IHttpHandler Members

    public bool IsReusable {
        get { return false; }
    }

    public void ProcessRequest(HttpContext context) {
        string op = context.Request["op"];
        if (!string.IsNullOrWhiteSpace(op)) {
            switch (op.ToLower()) {
                case "get":
                    GetUserJson(context);
                    break;
                case "us":
                    ChangeSpecialty(context);
                    break;
                case "addrole":
                    AddUserToRole(context);
                    break;
                case "removerole":
                    RemoveUserFromRole(context);
                    break;
            }
        }
    }

    private void RemoveUserFromRole(HttpContext context) {
        string id = context.Request["id"];
        string rid = context.Request["rid"];
        try {
            RepositoryFactory<Employees>.Get().RemoveUserFromRole(new Guid(id) , new Guid(rid));
        } catch (Exception ex) {
            context.Response.Write(ex.Message);
        }
    }

    private void AddUserToRole(HttpContext context) {
        string id = context.Request["id"];
        string rid = context.Request["rid"];
        try {
            RepositoryFactory<Employees>.Get().AddUserToRole(new Guid(id) , new Guid(rid));
        } catch (Exception ex) {
            context.Response.Write(ex.Message);
        }
    }

    void GetUserJson(HttpContext context) {
        context.Response.ContentType = "application/json";
        string id = context.Request["id"];
        string msg = string.Empty;
        try {
            Employee obj = RepositoryFactory<Employees>.Get().Get(new Guid(id));
            if (obj!=null) {
                System.Text.StringBuilder build = new System.Text.StringBuilder();
                build.Append("{");
                build.AppendFormat("\"id\":\"{0}\"",obj.Id);
                build.AppendFormat(",\"name\":\"{0}\"",obj.Name);
                build.AppendFormat(",\"specialtyId\":\"{0}\"" , obj.SpecialtyId);
                build.AppendFormat(",\"specialtyName\":\"{0}\"" , obj.Specialty.Name);
                build.Append(",\"roles\":[{}");
                if (null !=obj.Roles) {
                    foreach (var item in obj.Roles) {
                        build.Append(",{");
                        build.AppendFormat("\"id\":\"{0}\"",item.Id);
                        build.AppendFormat(",\"name\":\"{0}\"",item.Name);
                        build.Append("}");
                    }
                }
                build.Append("]}");
                context.Response.Write(build.ToString());
            } else {
                msg = "对象不存在";
            }
        } catch (Exception ex) {
            msg = ex.Message;
        }
        if (!string.IsNullOrEmpty(msg)) {
            context.Response.Write("{\"msg\":\"" + msg + "\"}");
        }
    }

    void ChangeSpecialty(HttpContext context) {
        string id = context.Request["id"];
        string sp = context.Request["sp"];
        string msg = string.Empty;

        try {
            Employee obj = RepositoryFactory<Employees>.Get().Get(new Guid(id));
            if (obj != null) {
                obj.SpecialtyId = sp;
                RepositoryFactory<Employees>.Get().Update(obj.Id,obj);
            } else {
                msg = "对象不存在";
            }
        } catch (Exception ex) {
            msg = ex.Message;
        }
        if (!string.IsNullOrEmpty(msg)) {
            context.Response.Write("error:" + msg + "");
        }
    }
    #endregion
}