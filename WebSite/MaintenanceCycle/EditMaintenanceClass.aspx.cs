using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using TSS.BLL;
using Tm = TSS.Models;

public partial class MaintenanceCycle_EditMaintenanceClass : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            BindData();
    }

    protected void BindData()
    {
        BindSpecalty();
        Tm.MaintenanceClass maintenanceClass = MaintenanceClass.Get(int.Parse(Request.QueryString["id"]));
        tbClassNames.Text = maintenanceClass.equipmentClassName;
        ddlSpecialty.SelectedValue = maintenanceClass.SpecialtyId;
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Tm.MaintenanceClass maintenanceClass = MaintenanceClass.Get(int.Parse(Request.QueryString["id"]));
        maintenanceClass.equipmentClassName = tbClassNames.Text;
        MaintenanceClass.Update(maintenanceClass);
    }

    protected void BindSpecalty()
    {
        foreach (Tm.Specialty specialty in Specialty.GetAll())
        {
            ddlSpecialty.Items.Add(new ListItem(specialty.Name, specialty.Id));
        }
    }

    protected void btnCancle_Click(object sender, EventArgs e)
    {
        Response.Redirect("MaintenanceClass.aspx");
    }
}