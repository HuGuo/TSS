using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using TSS.BLL;
using TSS.Models;

public partial class ComprehensiveReport_ComprehensiveReportDetail : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            BindData();
    }

    public void BindData()
    {
        ComprehensiveReportRepository comprehensiveReportRepository =
            new ComprehensiveReportRepository();
        ComprehensiveReport comprehensiveReport = comprehensiveReportRepository
            .Get(int.Parse(Request.QueryString["id"]));
        rptComprehensiveReport.DataSource = comprehensiveReport.SpecialtyAnalysises;
        rptComprehensiveReport.DataBind();
        BindComprehensiveReport(comprehensiveReport);
    }

    public void BindComprehensiveReport(ComprehensiveReport comprehensiveReport)
    {
        lbTitle.Text = string.Format("隔河岩水电站{0}年{1}月技术监督月报",
            comprehensiveReport.ReportYear.ToString(),
            comprehensiveReport.ReportMonth.ToString());
        lbComment.Text = comprehensiveReport.Comment;
    }

    protected void rptComprehensiveReport_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item)
        {
            SpecialtyAnalysis specialtyAnalysis = (SpecialtyAnalysis)e.Item.DataItem;
            Repeater repeater = (Repeater)e.Item.FindControl("rptSpecialty");
            repeater.DataSource = specialtyAnalysis.IndicatorAnalysises;
            repeater.DataBind();
        }
    }
}