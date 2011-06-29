using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using TSS.BLL;
using TSS.Models;

public partial class ComprehensiveReport_SpecialtyAnalysisDetail : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            BindData();
    }

    public void BindData()
    {
        rptIndicator.DataSource = new IndicatorAnalysisRepository()
            .GetMuchBySpecialtyAnalysisId(int.Parse(Request.QueryString["id"]));
        rptIndicator.DataBind();
        SpecialtyAnalysis specialtyAnalysis = new SpecialtyAnalysisRepository()
            .Get(int.Parse(Request.QueryString["id"]));
        lbSpecialtyAnalysis.Text = specialtyAnalysis.Analysis;
        lbTitle.Text = string.Format("{0}年{1}月{2}专业综合月报",
            specialtyAnalysis.ComprehensiveReport.ReportYear.ToString(), 
            specialtyAnalysis.ComprehensiveReport.ReportMonth.ToString(),
            specialtyAnalysis.Specialty.Name);
    }
}