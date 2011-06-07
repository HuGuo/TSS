using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using TSS.BLL;
using Tm = TSS.Models;

public partial class MaintenanceCycle_MaintenanceClass : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            BindData();
        string id = Request.QueryString["id"];
        if (!string.IsNullOrEmpty(id))
            Del(id);
    }

    private void BindData()
    {
        rptClass.DataSource = MaintenanceClass.GetAll();
        rptClass.DataBind();
    }

    //是否删除成功要有提示
    protected void Del(string id)
    {
        int maintenanceClassId = int.Parse(id);
        if (!MaintenanceCycle.IsExistOnMaintenancClass(maintenanceClassId))
            DelConfirm(MaintenanceClass.Delete(maintenanceClassId));
        else
            ExistChildConfirm();
        BindData();
    }
}