using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using TSS.BLL;
using Tm = TSS.Models;

public partial class MaintenanceCycle_AddMaintenanceExperiment : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    //提示是否添加成功
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        bool result = MaintenanceExperiment.Add(new Tm.MaintenanceExperiment
         {
             ExperimentId = Guid.NewGuid(),
             CurrentCycle = tbExperimentTime.Text,
             MaintenanceCycleId = int.Parse(Request.QueryString["id"]),
             ExperimentTime = DateTime.Parse(tbExperimentTime.Text),
         });
        AddConfirm(result, "window.location.href='MaintenanceCycle.aspx'");
    }
}