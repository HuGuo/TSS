using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using TSS.BLL;
using TSS.Models;
using System.Text;

public partial class MaintenanceCycle_MaintenanceClass : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            BindData();
    }

    private void BindData()
    {
        InitMaintenanceClassControlAdd();
        BindRpt();
    }

    protected void InitMaintenanceClassControlAdd()
    {
        this.MaintenanceClassControl1.SpecialtyId = Request.QueryString["specialtyId"];
        MaintenanceClassControl1.EquipmentClassName = "";
    }

    public void BindRpt()
    {
        MaintenanceClassRepository repository = new MaintenanceClassRepository();
        IList<MaintenanceClass> maintenanceClasses = repository.GetMuchBySpecialty(
            Request.QueryString["specialtyId"]);
        rptClass.DataSource = maintenanceClasses;
        rptClass.DataBind();
    }

    protected void lbtnEdit_Click(object sender, EventArgs e)
    {
        BindEdit(int.Parse(((LinkButton)sender).CommandArgument));
    }

    protected void BindEdit(int id)
    {
        using (var repository = RepositoryFactory<MaintenanceClassRepository>.Get())
            MaintenanceClassControl2.MaintenanceClass = repository.Get(id);
    }

    protected void InitMaintenanceClassControlEdit()
    {
        this.MaintenanceClassControl2.SpecialtyId = Request.QueryString["specialtyId"];
        MaintenanceClassControl2.EquipmentClassName = "";
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Add();
        InitMaintenanceClassControlAdd();
        BindData();
    }

    protected void Add()
    {
        using (var repository = RepositoryFactory<MaintenanceClassRepository>.Get())
            repository.Add(MaintenanceClassControl1.MaintenanceClass);
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        Edit();
        BindData();
        InitMaintenanceClassControlEdit();
    }

    public void Edit()
    {
        using (var repository = RepositoryFactory<MaintenanceClassRepository>.Get())
            repository.Update(MaintenanceClassControl2.MaintenanceClass);
    }

    protected void lbtnDel_Click(object sender, EventArgs e)
    {
        int maintenanceClassId = int.Parse(((LinkButton)sender).CommandArgument);
        MaintenanceCycleRepository repostitory = new MaintenanceCycleRepository();
        if (!repostitory.IsExistOnMaintenancClass(maintenanceClassId))
            new MaintenanceClassRepository().Delete(maintenanceClassId);
        else
            ExistChildConfirm();
        BindData();
    }


    protected void btnAddClose_Click(object sender, EventArgs e)
    {
        InitMaintenanceClassControlAdd();
    }
    protected void btnEditClose_Click(object sender, EventArgs e)
    {
        InitMaintenanceClassControlEdit();
    }
}