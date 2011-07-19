using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TSS.Models;
using TSS.BLL;

public partial class InitProfile : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) {
            if (Request.QueryString["uc"]==null || Request.QueryString["name"]==null) {
                Response.End();
                return;
            }
            IList<Specialty> list = RepositoryFactory<Specialties>.Get().GetAll();
            foreach (var item in list) {
                ddlSpecialty.Items.Add(new ListItem(item.Name , item.Id));
            }
        }
    }

    protected void lbtnOk_Click(object sender , EventArgs e) {
        string userCode = Server.UrlDecode(Request.QueryString["uc"]);
        string name = Server.UrlDecode(Request.QueryString["name"]);
        string redirectUrl = Server.UrlDecode(Request.QueryString["ReturnUrl"]);
        Employee obj = new Employee {
            Id = System.Guid.NewGuid() ,
            Name = name ,
            SpecialtyId = ddlSpecialty.SelectedValue ,
            Code = userCode ,
            Roles = { }
        };

        RepositoryFactory<Employees>.Get().Add(obj);
        Helper.SetAuthCookie(userCode , false , HttpContext.Current);
        Response.Redirect(redirectUrl);
    }
}