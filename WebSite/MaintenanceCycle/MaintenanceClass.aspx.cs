using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using TSS.BLL;
using Tm = TSS.Models;

public partial class MaintenanceCycle_MaintenanceClass : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            BindData();
    }

    private void BindData()
    {
        rptClass.DataSource = MaintenanceClass.GetAll();
        rptClass.DataBind();
    }

    //是否删除成功要有提示
    protected void lbtnDel_Click(object sender, EventArgs e)
    {
        int maintenanceClassId = int.Parse(((LinkButton)sender).CommandArgument);
        if (MaintenanceCycle.IsExistOnMaintenancClass(maintenanceClassId))
            MaintenanceClass.Delete(maintenanceClassId);
        else { }
        BindData();
    }
}