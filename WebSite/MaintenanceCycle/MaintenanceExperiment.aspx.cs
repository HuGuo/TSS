using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using TSS.BLL;
using TSS.Models;

public partial class MaintenanceCycle_MaintenanceExperiment : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            BindData();
        if (!string.IsNullOrEmpty(Request.QueryString["id"]))
            Del();
    }

    protected void BindData()
    {
        MaintenanceExperimentRepository repository = new MaintenanceExperimentRepository();
        IList<MaintenanceExperiment> maintenanceExperiment = repository.GetAll();
        rptExperiment.DataSource = maintenanceExperiment;
        rptExperiment.DataBind();
    }

    //是否删除成功要有提示
    protected void Del()
    {
        MaintenanceExperimentRepository repository = new MaintenanceExperimentRepository();
        bool result = repository.Delete(int.Parse(Request.QueryString["id"]));
        DelConfirm(result);
        BindData();
    }
}