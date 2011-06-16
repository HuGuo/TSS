using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using TSS.BLL;
using TSS.Models;

public partial class MaintenanceCycle_EditMaintenanceExperiment : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            BindData();
    }

    protected void BindData()
    {
        MaintenanceExperimentRepository repository = new MaintenanceExperimentRepository();
        MaintenanceExperiment maintenanceExperiment = repository.Get(int.Parse(Request.QueryString["id"]));
        tbCycle.Text = maintenanceExperiment.CurrentCycle;
        tbExperimentTime.Text = maintenanceExperiment.ExperimentTime.ToString();
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        MaintenanceExperimentRepository repository = new MaintenanceExperimentRepository();
        bool result = repository.Update(new MaintenanceExperiment
        {
            Id = int.Parse(Request.QueryString["id"]),
            CurrentCycle = tbCycle.Text
        });
        EditConfirm(result, "MaintenanceCycle.aspx");
    }
}