using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using TSS.BLL;
using TSS.Models;

public partial class MaintenanceCycle_MaintenanceCycle : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            BindData();
        if (!string.IsNullOrEmpty(Request.QueryString["id"]))
            Del();
    }

    protected void BindData()
    {
        MaintenanceCycleRepository reposittory = new MaintenanceCycleRepository();
        IList<MaintenanceCycle> maintenanceCycles = reposittory.GetAll();
        rptCycle.DataSource = maintenanceCycles;
        rptCycle.DataBind();
    }



    public void Del()
    {
        int maintenanceCycleId = int.Parse(Request.QueryString["id"]);
        MaintenanceExperimentRepository experimentRepository = new MaintenanceExperimentRepository();
        if (!experimentRepository.IsExistOnMaintenanceCycle(maintenanceCycleId))
        {
            MaintenanceCycleRepository maintenanceCycleRepository = new MaintenanceCycleRepository();
            bool result = maintenanceCycleRepository.Delete(maintenanceCycleRepository.Get(maintenanceCycleId));
            DelConfirm(result);
        }
        else
            ExistChildConfirm();
        BindData();
    }
}