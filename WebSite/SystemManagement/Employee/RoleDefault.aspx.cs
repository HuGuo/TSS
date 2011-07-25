using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TSS.BLL;

public partial class SystemManagement_Employee_RoleDefault : System.Web.UI.Page
{
    protected Modules modules = RepositoryFactory<Modules>.Get();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) {
            //bind modules
            rptlist.DataSource = modules.GetLeafModules();
            rptlist.DataBind();
        }
    }
}