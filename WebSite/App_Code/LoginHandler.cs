using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TSS.BLL;
using TSS.Models;
using System.Web.Security;

/// <summary>
/// Summary description for LoginHandler
/// </summary>
public class LoginHandler:IHttpHandler
{    
	public LoginHandler()
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
                case"login":
                    Validate(context);
                    break;
                case"initprofile":
                    InitProfile(context);
                    break;
            }
        }
    }
    #endregion

    void Validate(HttpContext context) {
        try {
            string name = context.Server.UrlDecode(context.Request["name"]);
            string pwd = context.Server.UrlDecode(context.Request["pwd"]);
            string remberme = context.Request["remember"];
            RightService server = new RightService();
            int result = server.Login(name , pwd.GetMD5());
            if (result==0) {
                UserDetail user = server.GetUserDetail(name);
                bool firstLogin = RepositoryFactory<Employees>.Get().ExistsCode(user.EmployeeCode);                
                //第一次登录系统
                if (!firstLogin) {
                    result = 4;
                } else {
                    //
                    SetAuthCookie(user.EmployeeCode , remberme == "1" , context);
                }
            }
            context.Response.Write(Helper.LoginResult[result]);
        } catch (Exception ex){
            context.Response.Write(ex.Message);
        }
    }

    void InitProfile(HttpContext context) {
        try {
            string name = context.Server.UrlDecode(context.Request["name"]);
            string spid = context.Request["spid"];
            string rememberme = context.Request["remember"];
            RightService server = new RightService();
            UserDetail user = server.GetUserDetail(name);
            Employee obj = new Employee {
                Id=System.Guid.NewGuid(),
                Name = name ,
                SpecialtyId = spid ,
                Code = user.EmployeeCode ,
                Roles = { }
            };

            RepositoryFactory<Employees>.Get().Add(obj);
            SetAuthCookie(user.EmployeeCode , rememberme == "1" , context);
        }catch( Exception ex){
            context.Response.Write(ex.Message);
        }
    }

    private void SetAuthCookie(string userCode,bool remberme,HttpContext context) {
        Employee entity = RepositoryFactory<Employees>.Get().GetByCode(userCode);
        var query = from p in entity.Roles
                    from m in p.Rights
                    select new { moduleId = m.ModuleId , permission = m.Permission };
        var result = from p in query
                     group p by p.moduleId into m
                     select m.Key + "=" + m.Max(p => p.permission);
        string ticketData = entity.Name + ";" + string.Join(";" , result.ToArray());
        FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1 , entity.Id.ToString() , DateTime.Now , DateTime.Now.AddHours(24) , false ,ticketData,"/");
        string EncTick = FormsAuthentication.Encrypt(ticket);

        HttpCookie userCookie = new HttpCookie(FormsAuthentication.FormsCookieName , EncTick);
        userCookie.Expires = DateTime.Now.AddHours(24);
        context.Response.Cookies.Add(userCookie);

        //rember me
        if (remberme) {
            HttpCookie cookieName = new HttpCookie("name" , entity.Name);
            cookieName.Expires = DateTime.Now.AddDays(7);
            context.Response.Cookies.Add(cookieName);
        }
    }
}