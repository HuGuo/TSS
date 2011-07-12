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
            this.MaintenanceClassControl1.SpecialtyId = Request.QueryString["specialtyId"];
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        bool result = false;
        using (var repository = RepositoryFactory<MaintenanceClassRepository>.Get())
            result = repository.Add(this.MaintenanceClassControl1.MaintenanceClass);
        AddConfirm(result, string.Format("MaintenanceClass.aspx?sepcialtyId=", Request.QueryString["specialtyId"]));
    }
}