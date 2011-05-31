using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        RightService rs = new RightService();
        Response.Write(rs.Url+"<br/>");
        string dptid=rs.GetDptIdByUser("guest");
        Response.Write("dptid:"+dptid+"<br/>");
        UserDetail[] list = rs.GetUserListByDpt(dptid);
        foreach (var item in list) {
            UserDetail tmp = rs.GetUserCompleteDetail(item.UserId);
            Response.Write(string.Format("{0}---{1}---{2}<br/>", tmp.UserId, tmp.Password, tmp.EmployeeCode));

        }

        //string pwd = rs.GetUserPasssword("guest");
        //Response.Write(pwd);
    }
}