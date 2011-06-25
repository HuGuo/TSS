using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using TSS.BLL;
using TSS.Models;

public partial class MaintenanceCycle_Default : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindData();
            BindClass();
        }
        if (!string.IsNullOrEmpty(Request.QueryString["id"]))
            Del();
    }

    protected void BindData()
    {
        MaintenanceCycleRepository reposittory = new MaintenanceCycleRepository();
        IList<MaintenanceCycle> maintenanceCycles = reposittory.GetMuchBySpecaity(Request.QueryString["s"]);
        rptCycle.DataSource = maintenanceCycles;
        rptCycle.DataBind();
    }

    protected void BindClass()
    {
        MaintenanceClassRepository repository = new MaintenanceClassRepository();
        IList<MaintenanceClass> maitenanceClasses = repository.GetMuchBySpecialty(Request.QueryString["s"]);
        foreach (MaintenanceClass maintenanceClass in maitenanceClasses)
        {
            ddlClass.Items.Add(new ListItem(maintenanceClass.equipmentClassName, maintenanceClass.Id.ToString()));
        }
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

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(ddlClass.SelectedValue))
            BindData();
        else
            SearchByMaintenaceClass();
    }

    protected void SearchByMaintenaceClass()
    {
        MaintenanceCycleRepository reposittory = new MaintenanceCycleRepository();
        IList<MaintenanceCycle> maintenanceCycles = reposittory.GetMuchByMaintenanceClass(int.Parse(ddlClass.SelectedValue));
        rptCycle.DataSource = maintenanceCycles;
        rptCycle.DataBind();
    }
}