using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TSS.BLL;
using TSS.Models;
public partial class _Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["ajax"]!=null) {
            string name = Server.UrlDecode(Request.QueryString["name"]);
            string pwd = Server.UrlDecode(Request.QueryString["pwd"]);
            RightService server = new RightService();
            int result= server.Login(name, pwd);
            if (result==0) {
                //检查是否第一次登陆系统
                UserDetail user = server.GetUserDetail(name);
                var repository = new Employees();
                bool firstLogin=repository.ExistsCode(user.EmployeeCode);
                Response.Write("-1");
            } else {
                Response.Write(result);
            }
            Response.End();
            return;
        }
        if (!IsPostBack) {
            IList<TSS.Models.Specialty> list = (new TSS.BLL.Specialties()).GetAll();
            foreach (var item in list) {
                ddlSpecialty.Items.Add(new ListItem(item.Name, item.Id));
            }
        }
    }

    void InitProfile(string userid) 
    {
        RightService rs = new RightService();

    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        
        Response.Redirect("default.aspx",true);
    }
}