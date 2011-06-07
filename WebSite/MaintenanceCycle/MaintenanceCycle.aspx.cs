using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using TSS.BLL;
using Tm = TSS.Models;

public partial class MaintenanceCycle_MaintenanceCycle : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            BindData();
        string id = Request.QueryString["id"];
        if (!string.IsNullOrEmpty(id))
            Del(id);
    }

    protected void BindData()
    {
        IList<Tm.MaintenanceCycle> maintenanceCycle = MaintenanceCycle.GetAll().ToList();
        rptCycle.DataSource = maintenanceCycle;
        rptCycle.DataBind();
    }


    public void Del(string id)
    {
        int maintenanceCycleId = int.Parse(id);
        if (!MaintenanceExperiment.IsExistOnMaintenanceCycle(maintenanceCycleId))
        {
            bool result = MaintenanceCycle.Delete(maintenanceCycleId);
            DelConfirm(result);
        }
        else
            ExistChildConfirm();
        BindData();
    }
}