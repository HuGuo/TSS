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
        ucSpecialtyControl.SpecialtyId = Request.QueryString["specialtyId"];
        BindMaintenanceClass();
    }

    protected void BindMaintenanceClass()
    {
        MaintenanceClassRepository repository = new MaintenanceClassRepository();
        MaintenanceClass maintenanceClass = repository.Get(
            int.Parse(Request.QueryString["id"]));
        tbClassNames.Text = maintenanceClass.equipmentClassName;
        ucSpecialtyControl.SpecialtyId = maintenanceClass.SpecialtyId;
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        MaintenanceClassRepository repository = new MaintenanceClassRepository();
        MaintenanceClass maintenanceClass = repository.Get(int.Parse(Request.QueryString["id"]));
        maintenanceClass.equipmentClassName = tbClassNames.Text;
        bool result = repository.Update(maintenanceClass);
        EditConfirm(result, string.Format("MaintenanceClass.aspx?sepcialtyId=", Request.QueryString["specialtyId"]));
    }
}