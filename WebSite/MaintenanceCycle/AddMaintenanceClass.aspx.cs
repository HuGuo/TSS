using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using TSS.BLL;
using Tm = TSS.Models;

public partial class MaintenanceCycle_AddMaintenanceClass : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            BindData();

    }

    protected void BindData()
    {
        BindSpecialty();
    }

    protected void BindSpecialty()
    {
        foreach (Tm.Specialty specialty in new Specialties().GetAll())
            ddlSpecialty.Items.Add(new ListItem(specialty.Name, specialty.Id));
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        bool result = MaintenanceClass.Add(
            new Tm.MaintenanceClass
          {
              equipmentClassName = tbClassNames.Text,
              SpecialtyId = ddlSpecialty.SelectedValue
          });
        AddConfirm(result, "window.location.href='MaintenanceClass.aspx'");
    }
}