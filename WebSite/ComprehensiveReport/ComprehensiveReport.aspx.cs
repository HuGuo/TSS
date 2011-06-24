using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using TSS.BLL;
using TSS.Models;

public partial class ComprehensiveReport_ComprehensiveReport : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            BindData();
        string comprehensiveReportId = Request.QueryString["id"];
        if (!string.IsNullOrEmpty(comprehensiveReportId))
            Del(int.Parse(comprehensiveReportId));
    }

    protected void BindData()
    {
        rptReport.DataSource = new ComprehensiveReportRepository().GetAll();
        rptReport.DataBind();
    }

    public void Del(int comprehensiveReportId)
    {
        ComprehensiveReportRepository repository = new ComprehensiveReportRepository();
        ComprehensiveReport report = repository.Get(comprehensiveReportId);
        if (report.SpecialtyAnalysises.Count == 0)
        {
            bool result = repository.Delete(repository.Get(comprehensiveReportId));
            DelConfirm(result);
            BindData();
        }
        else
        {
            ExistChildConfirm();
        }
    }
}