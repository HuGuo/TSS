using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using TSS.BLL;
using Tm = TSS.Models;

public partial class MaintenanceCycle_EditMaintenanceExperiment : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            BindData();
    }

    protected void BindData()
    {
        Tm.MaintenanceExperiment maintenanceExperiment = MaintenanceExperiment.Get(
            int.Parse(Request.QueryString["id"]));
        tbCycle.Text = maintenanceExperiment.CurrentCycle;
        tbExperimentTime.Text = maintenanceExperiment.ExperimentTime.ToString();
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        Tm.MaintenanceExperiment maintenanceExperiment = MaintenanceExperiment.Get(
            int.Parse(Request.QueryString["id"]));
        maintenanceExperiment.CurrentCycle = tbCycle.Text;
        maintenanceExperiment.ExperimentTime = DateTime.Parse(tbExperimentTime.Text);
        MaintenanceExperiment.Update(maintenanceExperiment);
    }

    protected void btnCancle_Click(object sender, EventArgs e)
    {
        Response.Redirect("MaintenanceCycle.apsx");
    }
}