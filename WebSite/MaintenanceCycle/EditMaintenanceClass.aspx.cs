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
        using (var repository = RepositoryFactory<MaintenanceClassRepository>.Get())
            this.MaintenanceClassControl1.MaintenanceClass = repository.Get(int.Parse(Request.QueryString["id"]));
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        bool result = false;
        using (var repository = RepositoryFactory<MaintenanceClassRepository>.Get())
            repository.Update(this.MaintenanceClassControl1.MaintenanceClass);
        EditConfirm(result, string.Format("MaintenanceClass.aspx?sepcialtyId=", Request.QueryString["specialtyId"]));
    }
}