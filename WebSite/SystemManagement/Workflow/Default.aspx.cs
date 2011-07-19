using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using workflow;
public partial class SystemManagement_Workflow_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) {
            using (WFContext db = new WFContext()) {
                rptlist.DataSource = WFRepository.GetAll(db);
                rptlist.DataBind();
            }
        }
    }
}