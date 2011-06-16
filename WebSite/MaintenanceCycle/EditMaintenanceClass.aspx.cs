using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using TSS.BLL;
using TSS.Models;

public partial class MaintenanceCycle_EditMaintenanceClass : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            BindData();
    }

    protected void BindData()
    {
        BindSpecalty();
        BindMaintenanceClass();
    }

    protected void BindMaintenanceClass()
    {
        MaintenanceClassRepository repository = new MaintenanceClassRepository();
        MaintenanceClass maintenanceClass = repository.Get(
            int.Parse(Request.QueryString["id"]));
        tbClassNames.Text = maintenanceClass.equipmentClassName;
        ddlSpecialty.SelectedValue = maintenanceClass.SpecialtyId;
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        MaintenanceClassRepository repository = new MaintenanceClassRepository();
        MaintenanceClass maintenanceClass = repository.Get(int.Parse(Request.QueryString["id"]));
        maintenanceClass.equipmentClassName = tbClassNames.Text;
        bool result = repository.Update(maintenanceClass);
        EditConfirm(result, "MaintenanceClass.aspx");
    }

    protected void BindSpecalty()
    {
        foreach (Specialty specialty in new Specialties().GetAll())
        {
            ddlSpecialty.Items.Add(new ListItem(specialty.Name, specialty.Id));
        }
    }
}