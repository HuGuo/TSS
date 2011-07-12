using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using TSS.BLL;
using TSS.Models;

public partial class MaintenanceCycle_MaintenanceExperiment : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            BindData();
    }

    protected void BindData()
    {
        MaintenanceExpControl1.MaintenanceCycleId = Request.QueryString["maintenanceExperimentId"];
        MaintenanceExperimentRepository repository = new MaintenanceExperimentRepository();
        IList<MaintenanceExperiment> maintenanceExperiment = repository.GetByMaintenanceCycle(
            int.Parse(Request.QueryString["maintenanceExperimentId"]));
        rptExperiment.DataSource = maintenanceExperiment;
        rptExperiment.DataBind();
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        using (var repository = RepositoryFactory<MaintenanceExperimentRepository>.Get())
            repository.Add(MaintenanceExpControl1.MaintenanceExperiment);
        MaintenanceExpControl1.ReSet();
        BindData();
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        using (var repository = RepositoryFactory<MaintenanceExperimentRepository>.Get())
            repository.Update(MaintenanceExpControl2.MaintenanceExperiment);
        MaintenanceExpControl2.ReSet();
        BindData();
    }

    protected void lbtnDel_Click(object sender, EventArgs e)
    {
        using (var repository = RepositoryFactory<MaintenanceExperimentRepository>.Get())
            repository.Delete(int.Parse(((LinkButton)sender).CommandArgument));
        BindData();
    }
    protected void lbtnEdit_Click(object sender, EventArgs e)
    {
        using (var repository = RepositoryFactory<MaintenanceExperimentRepository>.Get())
            MaintenanceExpControl2.MaintenanceExperiment = repository.Get(int.Parse(((LinkButton)sender).CommandArgument));
    }

    protected void btnAddClose_Click(object sender, EventArgs e)
    {
        MaintenanceExpControl1.ReSet();
    }

    protected void btnEditClose_Click(object sender, EventArgs e)
    {
        MaintenanceExpControl2.ReSet();
    }

}