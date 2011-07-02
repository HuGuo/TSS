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
        if (Request.QueryString["out"]!=null) {
            CacheManager.RemoveCache("CACHE_" + User.Identity.Name);
            System.Web.Security.FormsAuthentication.SignOut();
            
        }
        if (!IsPostBack) {
            IList<TSS.Models.Specialty> list = new TSS.BLL.Specialties().GetAll();
            foreach (var item in list) {
                ddlSpecialty.Items.Add(new ListItem(item.Name, item.Id));
            }
        }
    }

    void InitProfile(Employee obj) 
    {
        RightService rs = new RightService();

    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        
        Response.Redirect("default.aspx",true);
    }
}