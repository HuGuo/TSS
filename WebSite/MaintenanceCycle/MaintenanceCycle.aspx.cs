using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using TSS.BLL;
using Tm = TSS.Models;

public partial class MaintenanceCycle_MaintenanceCycle : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            BindData();
    }

    protected void BindData()
    {
        IList<Tm.MaintenanceCycle> maintenanceCycle = MaintenanceCycle.GetAll().ToList();
        rptCycle.DataSource = maintenanceCycle;
        rptCycle.DataBind();
    }

    protected void btnClass_Click(object sender, EventArgs e)
    {
        Response.Redirect(string.Format("MaintenanceClass.aspx?specialty={0}"
            , Request.QueryString["specialty"]));
    }

    protected void lbtnDel_Click(object sender, EventArgs e)
    {
        int maintenanceCycleId = int.Parse(((LinkButton)sender).CommandArgument);
        if (MaintenanceExperiment.IsExistOnMaintenanceCycle(maintenanceCycleId))
            MaintenanceClass.Delete(maintenanceCycleId);
        else//提示该类别下存在设备周期无法删除
        { };
    }
}