using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TSS.BLL;

public partial class SystemManagement_Experiment_ExpRecordDefault : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) {

            rptlist.DataSource = RepositoryFactory<ExpReocrdRepository>.Get().GetAll().OrderBy(p => p.SpecialtyId);
            rptlist.DataBind();
        }
    }
}