using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using TSS.BLL;
using TSS.Models;

public partial class MaintenanceCycle_EditMaintenanceCycle : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            BindData();
    }

    protected void BindData()
    {
        using (var repository = RepositoryFactory<MaintenanceCycleRepository>.Get())
            MaintenanceCycleControl1.MaintenanceCycle = repository.Get(int.Parse(Request.QueryString["id"]));
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        bool result = false;
        using (var repository = RepositoryFactory<MaintenanceCycleRepository>.Get())
            result = repository.Update(MaintenanceCycleControl1.MaintenanceCycle);
        EditConfirm(result, string.Format("Default.aspx?s={0}", Request.QueryString["specialtyId"]));
    }
}