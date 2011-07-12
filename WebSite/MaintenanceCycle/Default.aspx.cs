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


    protected void btnAdd_Click(object sender, EventArgs e)
    {
        using (var repository = RepositoryFactory<MaintenanceCycleRepository>.Get())
            repository.Add(MaintenanceCycleControl1.MaintenanceCycle);
        MaintenanceCycleControl1.ReSet();
        BindData();
    }

    protected void lbtnEdit_Click(object sender, EventArgs e)
    {
        using (var repository = RepositoryFactory<MaintenanceCycleRepository>.Get())
            MaintenanceCycleControl2.MaintenanceCycle = repository.Get(int.Parse(((LinkButton)sender).CommandArgument));
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        using (var repository = RepositoryFactory<MaintenanceCycleRepository>.Get())
            repository.Update(MaintenanceCycleControl2.MaintenanceCycle);
        MaintenanceCycleControl2.ReSet();
        BindData();
    }

    protected void lbtnDel_Click(object sender, EventArgs e)
    {
        using (var repository = RepositoryFactory<MaintenanceCycleRepository>.Get())
            repository.Delete(int.Parse(((LinkButton)sender).CommandArgument));
        BindData();
    }

    protected void btnAddClose_Click(object sender, EventArgs e)
    {
        MaintenanceCycleControl1.ReSet();
    }
    protected void btnEditClose_Click(object sender, EventArgs e)
    {
        MaintenanceCycleControl2.ReSet();
    }

    public string GetLastExpTime(Object obj)
    {
        MaintenanceCycle maintenanceCycle = (MaintenanceCycle)obj;
        if (maintenanceCycle != null && maintenanceCycle.MaintenanceExperiments.Count > 0)
            return maintenanceCycle.MaintenanceExperiments.Last().ActualTime.ToString("yyyy-MM-dd");
        else
            return "";
    }

    public string GetNextExpTime(Object obj)
    {
        MaintenanceCycle maintenanceCycle = (MaintenanceCycle)obj;
        if (maintenanceCycle != null && maintenanceCycle.MaintenanceExperiments.Count > 0)
            return maintenanceCycle.MaintenanceExperiments.Last().ExpectantTime.Value.ToString("yyyy-MM-dd");
        else
            return "";
    }
}