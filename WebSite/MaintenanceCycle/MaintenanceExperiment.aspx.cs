using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using TSS.BLL;
using Tm = TSS.Models;

public partial class MaintenanceCycle_MaintenanceExperiment : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            BindData();
    }

    protected void BindData()
    {
        rptExperiment.DataSource = MaintenanceExperiment.GetAll();
        rptExperiment.DataBind();
    }

    protected void lbtnDel_Click(object sender, EventArgs e)
    {
        MaintenanceExperiment.Delete(
            ((LinkButton)sender).CommandArgument);
        BindData();
    }
}