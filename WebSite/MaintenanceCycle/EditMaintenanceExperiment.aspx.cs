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
        tbActualTime.Text = maintenanceExperiment.ActualTime.Date.ToString("yyyy-MM-dd");
        tbExpectantTime.Text = maintenanceExperiment.ExpectantTime.Value.Date.ToString("yyyy-MM-dd");
        hfCycle.Value = maintenanceExperiment.MaintenanceCycle.Cycle;
        hfMaintenanceCycleId.Value = maintenanceExperiment.MaintenanceCycleId.ToString();
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        MaintenanceExperimentRepository repository = new MaintenanceExperimentRepository();
        bool result = repository.Update(new MaintenanceExperiment
        {
            Id = int.Parse(Request.QueryString["id"]),
            CurrentCycle = hfCycle.Value,
            ActualTime = DateTime.Parse(tbActualTime.Text),
            ExpectantTime = DateTime.Parse(tbExpectantTime.Text),
            MaintenanceCycleId = int.Parse(hfMaintenanceCycleId.Value)
        });
        EditConfirm(result, 
            string.Format("MaintenanceExperiment.aspx?specialtyId={1}&MaintenanceCycleId={0}",
            Request.QueryString["MaintenanceCycleId"],
            Request.QueryString["specialtyId"]));
    }

    protected void tbActualTime_TextChanged(object sender, EventArgs e)
    {
        MaintenanceCycleRepository repository = new MaintenanceCycleRepository();
        MaintenanceCycle maintenaceCycle = repository.Get(
            int.Parse(Request.QueryString["maintenanceCycleId"]));
        tbExpectantTime.Text = DateTime.Parse(tbActualTime.Text)
            .AddYears(int.Parse(maintenaceCycle.Cycle))
            .Date.ToString("yyyy-MM-dd");
        hfCycle.Value = maintenaceCycle.Cycle;
    }
}