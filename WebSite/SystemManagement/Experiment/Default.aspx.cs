using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using TSS.BLL;
public partial class SystemManagement_Experiment_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) {
            rptList.DataSource = RepositoryFactory<ExpTemplateRepository>.Get().GetAll();
            rptList.DataBind();
        }
    }
}