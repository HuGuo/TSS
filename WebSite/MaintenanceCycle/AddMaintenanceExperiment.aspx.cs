using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using TSS.BLL;
using TSS.Models;
using System.Globalization;

public partial class MaintenanceCycle_AddMaintenanceExperiment : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }


    protected void btnAdd_Click(object sender, EventArgs e)
    {
        MaintenanceExperimentRepository repository = new MaintenanceExperimentRepository();
        bool result = repository.Add(new MaintenanceExperiment
         {
             ExperimentId = Guid.NewGuid(),
             MaintenanceCycleId = int.Parse(Request.QueryString["maintenanceCycleId"]),
             NextExpTime = string.IsNullOrEmpty(tbExpectantTime.Text) ?
                (DateTime?)null : DateTime.Parse(tbExpectantTime.Text),
             ActualTime = DateTime.Parse(tbActualTime.Text),
             CurrentCycle = hfCycle.Value
         });
        AddConfirm(result,string.Format("MaintenanceExperiment.aspx?specialtyId={1}&MaintenanceCycleId={0}",
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