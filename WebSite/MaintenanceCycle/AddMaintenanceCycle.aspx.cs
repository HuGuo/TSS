using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using TSS.BLL;
using TSS.Models;

public partial class MaintenanceCycle_AddMaintenanceCycle : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        bool result = false;
        using (var repository = RepositoryFactory<MaintenanceCycleRepository>.Get())
            result = repository.Add(this.MaintenanceCycleControl1.MaintenanceCycle);
        AddConfirm(result, string.Format("Default.aspx?s={0}", Request.QueryString["specialtyId"]));
    }
}