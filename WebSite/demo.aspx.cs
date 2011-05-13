using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class demo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        RightService rs = new RightService();
        Response.Write(rs.Url);
        //rs.Url = "http://172.16.162.21/rightproject/webservices/rightservice.asmx ";
        UserDetail ud = rs.GetUserCompleteDetail("admin");
        Response.Write("<h1>["+ud.Password+"]</h1><br/>");
        UserDetail [] uds= rs.GetUserListByDptRoot(ud.DepartmentId);
        var data = from p in uds
                   select new { t = p.UserId, v = p.EmployeeCode };
        DropDownList1.DataSource = data;
        DropDownList1.DataTextField = "t";
        DropDownList1.DataValueField = "v";
        DropDownList1.DataBind();
    }
}