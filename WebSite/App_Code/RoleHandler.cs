using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TSS.BLL;
using TSS.Models;

public class RoleHandler:IHttpHandler
{
	public RoleHandler()
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
                case "list":
                    RolesListJSON(context);
                    break;
                case "right":
                    RightJSON(context);
                    break;
                case "add":
                    Add(context);
                    break;
                case "delete":
                    Delete(context);
                    break;
                case "update-n":
                    UpdateRoleName(context);
                    break;
                case "update-r":
                    UpdateRight(context);
                    break;
                case "del-employee":
                    RemoeEmployee(context);
                    break;
            }
        }
    }

    void RolesListJSON(HttpContext context) {
        context.Response.ContentType = "application/json";
        System.Web.Script.Serialization.JavaScriptSerializer jss = new System.Web.Script.Serialization.JavaScriptSerializer();
        IList<Role> list= RepositoryFactory<RolesRepository>.Get().GetAll();
        var tree = from p in list
                   select new { id = p.Id.ToString() , text = p.Name };
        System.Text.StringBuilder build = new System.Text.StringBuilder();
        build.Append("[{\"id\":\"\",\"text\":\"系统角色\",\"children\":");
        jss.Serialize(tree,build);

        build.Append("}]");
        context.Response.Write(build.ToString());
        context.Response.End();
    }
    
    void RightJSON(HttpContext context) {
        context.Response.ContentType = "application/json";
        string id = context.Request["id"];
        string msg = string.Empty;
        try {
            System.Web.Script.Serialization.JavaScriptSerializer jss = new System.Web.Script.Serialization.JavaScriptSerializer();
            Role obj = RepositoryFactory<RolesRepository>.Get().Get(new Guid(id));
            if (null!=obj) {
                System.Text.StringBuilder build = new System.Text.StringBuilder();
                build.Append("{");
                build.AppendFormat("\"id\":\"{0}\"",obj.Id);
                build.AppendFormat(",\"name\":\"{0}\"",obj.Name);
                build.Append(",\"employees\":");
                if (null!=obj.Employees) {
                    var arry1 = from p in obj.Employees
                                select new { id=p.Id.ToString(),name=p.Name};
                    jss.Serialize(arry1 , build);
                } else {
                    build.Append("[]");
                }
                build.Append(",\"mvs\":");
                if (null !=obj.Rights) {
                    var arry2 = from p in obj.Rights
                                select new { id=p.ModuleId,name=Convert.ToString(p.Permission,2)};
                    jss.Serialize(arry2 , build);
                } else {
                    build.Append("[]");
                }
                build.Append("}");
                context.Response.Write(build.ToString());
            } else {
                msg = "对象不存在";
            }
        } catch (Exception ex) {
            msg = ex.Message;
        }
        if (!string.IsNullOrWhiteSpace(msg)) {
            context.Response.Write("{\"msg\":\"" + msg + "\"}");
        }
    }

    void Add(HttpContext context) {
        context.Response.ContentType = "application/json";
        string name = context.Request["text"];
        string msg = string.Empty;
        if (!string.IsNullOrWhiteSpace(name)) {
            try {
                Guid id= System.Guid.NewGuid() ;
                IList<Module> modules = RepositoryFactory<Modules>.Get().GetAll();
                var query = from p in modules
                            select new Right { ModuleId = p.Id , Permission = 0 , RoleId = id };

                Role obj = new Role { Id =id, Name = context.Server.UrlDecode(name),Rights=query.ToList() };
                RepositoryFactory<RolesRepository>.Get().Add(obj);
                context.Response.Write("{\"id\":\"" + obj.Id.ToString() + "\"}");

                AppLog.Write("创建角色", AppLog.LogMessageType.Info,"name="+obj.Name,this.GetType());
            } catch (Exception ex) {

                AppLog.Write("创建角色 出错" , AppLog.LogMessageType.Error , "name=" + name , ex , this.GetType());

                msg = ex.Message;
            }
        } else {
            msg = "角色名不能为空";
        }
        if (!string.IsNullOrWhiteSpace(msg)) {
            context.Response.Write("{\"msg\":\""+msg+"\"}");
        }
    }

    void Delete(HttpContext context) {
        //context.Response.ContentType = "application/json";
        string id = context.Request["id"];
        string msg = string.Empty;
        try {
            RepositoryFactory<RolesRepository>.Get().Delete(new Guid(id));
            //context.Response.Write("{}");

            //log
            AppLog.Write("删除角色" , AppLog.LogMessageType.Info , "id="+id , this.GetType());
        } catch (Exception ex) {
            AppLog.Write("删除角色 出错" , AppLog.LogMessageType.Error , "id=" + id , ex , this.GetType());
            msg = ex.Message;            
        }
        if (!string.IsNullOrWhiteSpace(msg)) {
            context.Response.Write("{\"msg\":\"" + msg + "\"}");
        }
    }

    void UpdateRoleName(HttpContext context) {
        string id = context.Request["id"];
        string name = context.Request["text"];
        string msg = string.Empty;
        if (!string.IsNullOrWhiteSpace(id)) {
            try {
                Role obj = RepositoryFactory<RolesRepository>.Get().Get(new Guid(id));
                if (null !=obj) {
                    string log_oldName = obj.Name;
                    obj.Name = context.Server.UrlDecode(name);
                    RepositoryFactory<RolesRepository>.Get().Update(obj.Id , obj);

                    AppLog.Write(string.Format("更新角色 \"{0}\" 为 \"{1}\"" , log_oldName , obj.Name) , AppLog.LogMessageType.Info , "id=" + id , this.GetType());
                } else {
                    msg = Helper.NULLOBJECT;
                }
            } catch (Exception ex) {
                AppLog.Write("更新角色名称 出错", AppLog.LogMessageType.Error,"id="+id,ex,this.GetType());
                msg = ex.Message;
            }
        } else {
            msg = Helper.NULLOBJECT;
        }

        if (!string.IsNullOrWhiteSpace(msg)) {
            context.Response.Write("{\"msg\":\"" + msg + "\"}");
        }
    }

    void UpdateRight(HttpContext context) {
        string id = context.Request["id"];
        string mvs = context.Request["mvs"];
        string msg = string.Empty;
        try {
            Role obj = new Role {
                Id = new Guid(id) ,
                Rights = new List<Right>()
            };
            string[] arry1 = mvs.Split(',');
            foreach (var item in arry1) {
                string[] arry2 = item.Split('=');
                var newitem = new Right {
                    ModuleId = int.Parse(arry2[0]) ,
                    Permission = int.Parse(arry2[1])
                };
                obj.Rights.Add(newitem);
            }
            RepositoryFactory<RolesRepository>.Get().UpdateRight(obj);
            //log
            AppLog.Write("更新角色权限" , AppLog.LogMessageType.Info , "id=" + id , this.GetType());

            context.Response.End();
        } catch (Exception ex) {
            AppLog.Write("更新角色权限 出错" , AppLog.LogMessageType.Error , "id=" + id , ex , this.GetType());

            msg = ex.Message;
        }
        if (!string.IsNullOrWhiteSpace(msg)) {
            context.Response.Write("{\"msg\":\"" + msg + "\"}");
        }
    }

    void RemoeEmployee(HttpContext context) {
        string id = context.Request["id"];
        string emps = context.Request["emps"];
        string msg = string.Empty;
        try {
            Role obj = RepositoryFactory<RolesRepository>.Get().Get(new Guid(id));
            if (null !=obj) {
                string [] arry1 = emps.Split(',');
                var query = obj.Employees.Where(p => arry1.Contains(p.Id.ToString()));
                
                obj.Employees = obj.Employees.Except(query).ToList();
                RepositoryFactory<RolesRepository>.Get().Update(obj.Id , obj);
            } else {
                msg = Helper.NULLOBJECT;
            }
        } catch (Exception ex) {
            msg = ex.Message;
        }
        if (!string.IsNullOrWhiteSpace(msg)) {
            context.Response.Write("{\"msg\":\"" + msg + "\"}");
        }
    }
    #endregion
}