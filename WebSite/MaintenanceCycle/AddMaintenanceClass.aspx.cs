using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using TSS.BLL;
using TSS.Models;

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
        foreach (Specialty specialty in new Specialties().GetAll())
            ddlSpecialty.Items.Add(new ListItem(specialty.Name, specialty.Id));
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        MaintenanceClassRepository repository = new MaintenanceClassRepository();
        bool result = repository.Add(new MaintenanceClass
          {
              equipmentClassName = tbClassNames.Text,
              SpecialtyId = ddlSpecialty.SelectedValue
          });
        AddConfirm(result, "MaintenanceClass.aspx");
    }
}