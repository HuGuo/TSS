using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using TSS.BLL;
using Tm = TSS.Models;

public partial class MaintenanceCycle_MaintenanceExperiment : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            BindData();
        string id = Request.QueryString["id"];
        if (!string.IsNullOrEmpty(id))
            Del(id);
    }

    protected void BindData()
    {
        rptExperiment.DataSource = MaintenanceExperiment.GetAll();
        rptExperiment.DataBind();
    }

    //是否删除成功要有提示
    protected void Del(string id)
    {
        bool result = MaintenanceExperiment.Delete(id);
        DelConfirm(result);
        BindData();
    }
}