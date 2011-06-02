using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using TSS.BLL;
using Tm = TSS.Models;

public partial class MaintenanceCycle_AddMaintenanceExperiment : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    //提示是否添加成功
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Tm.MaintenanceExperiment maintenanceExperiment = new Tm.MaintenanceExperiment();
        maintenanceExperiment.ExperimentId = Guid.NewGuid();
        maintenanceExperiment.CurrentCycle = tbExperimentTime.Text;
        maintenanceExperiment.MaintenanceCycleId = int.Parse(Request.QueryString["id"]);
        maintenanceExperiment.ExperimentTime = DateTime.Parse(tbExperimentTime.Text);
        MaintenanceExperiment.Add(maintenanceExperiment);
    }
}