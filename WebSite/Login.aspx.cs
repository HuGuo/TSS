using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TSS.BLL;
using TSS.Models;
using System.Web.Security;
public partial class _Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack) {
            if (Request.QueryString["out"] != null) {
                CacheManager.RemoveCache("CACHE_" + User.Identity.Name);
                System.Web.Security.FormsAuthentication.SignOut();

            }
        }
    }
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        string loginName = txtLoginName.Text.Trim();
        string loginPwd = txtPassword.Text;
        RightService server = new RightService();
        int result= server.Login(loginName , loginPwd.GetMD5());
        if (result == 0) {
            UserDetail user = server.GetUserCompleteDetail(loginName);
            bool existsCode = RepositoryFactory<Employees>.Get().ExistsCode(user.EmployeeCode);
            string redirectUrl = Request.QueryString["ReturnUrl"] ?? "default.htm";
            if (existsCode) {
                //授权 登录
                Helper.SetAuthCookie(user.EmployeeCode , false , HttpContext.Current);
                Response.Redirect(redirectUrl , true);
            } else {
                //第一次登陆，跳转设置专业信息
                redirectUrl = string.Format("InitProfile.aspx?name={0}&uc={1}&ReturnUrl={2}" , Server.UrlEncode(loginName) , Server.UrlEncode(user.EmployeeCode) , Server.UrlEncode(redirectUrl));
                Response.Redirect(redirectUrl,true);
            }
        } else {
            ltmsg.Text = Helper.LoginResult[result];
        }
    }
}