using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using TSS.BLL;
using TSS.Models;

public partial class MaintenanceCycle_MaintenanceClass : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            BindData();
        if (!string.IsNullOrEmpty(Request.QueryString["id"]))
            Del();
    }

    private void BindData()
    {
        MaintenanceClassRepository repository = new MaintenanceClassRepository();
        IList<MaintenanceClass> maintenanceClasses = repository.GetAll();
        rptClass.DataSource = maintenanceClasses;
        rptClass.DataBind();
    }

    //是否删除成功要有提示
    protected void Del()
    {
        int maintenanceClassId = int.Parse(Request.QueryString["id"]);
        MaintenanceCycleRepository repostitory = new MaintenanceCycleRepository();
        if (!repostitory.IsExistOnMaintenancClass(maintenanceClassId))
            DelConfirm(repostitory.Delete(maintenanceClassId));
        else
            ExistChildConfirm();
        BindData();
    }
}