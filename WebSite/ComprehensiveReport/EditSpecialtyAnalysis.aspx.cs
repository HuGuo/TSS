using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using TSS.BLL;
using TSS.Models;

public partial class ComprehensiveReport_EditSpecialtyAnalysis : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            BindData();
    }

    public void BindData()
    {
        SpecialtyAnalysis specialtyAnalysis = new SpecialtyAnalysisRepository()
             .Get(int.Parse(Request.QueryString["id"]));
        rptIndicator.DataSource = specialtyAnalysis.IndicatorAnalysises;
        rptIndicator.DataBind();
        tbSpecialtyAnalysis.Text = TextDecode(specialtyAnalysis.Analysis);
        HfcomprehensiveReportId.Value = specialtyAnalysis.ComprehensiveReportId.ToString();
        YearAndMonControl1.Year = specialtyAnalysis.ComprehensiveReport.ReportYear.ToString();
        YearAndMonControl1.Mon = specialtyAnalysis.ComprehensiveReport.ReportMonth.ToString();
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        ComprehensiveReport comprehensiveReport = GetComprehensiveReport();
        if (comprehensiveReport == null)
        {
            AddCurrentTimeComprehensiveReport();
        }
        if (comprehensiveReport != null && IsExistExceptSelfSpecialty())
        {
            ExistCurrentTimeRecord();
            return;
        }
        bool reuslt = EditSpecialtyAnalysis();
        EditIsSccessfulPrompt(reuslt);
    }

    protected void EditIsSccessfulPrompt(bool reuslt)
    {
        EditConfirm(reuslt,
            string.Format("Default.aspx?s={0}", Request.QueryString["specialtyId"]));
    }

    protected ComprehensiveReport GetComprehensiveReport()
    {
        return new ComprehensiveReportRepository().Get(
            int.Parse(YearAndMonControl1.Year), int.Parse(YearAndMonControl1.Mon));
    }

    protected bool IsExistExceptSelfSpecialty()
    {
        SpecialtyAnalysisRepository repository = new SpecialtyAnalysisRepository();
        return repository.IsExistWhileEdit(int.Parse(YearAndMonControl1.Year), int.Parse(YearAndMonControl1.Mon),
            Request.QueryString["specialtyId"], int.Parse(Request.QueryString["id"]));
    }

    public bool AddCurrentTimeComprehensiveReport()
    {
        ComprehensiveReportRepository comprehensiveReportRepository = new ComprehensiveReportRepository();
        ComprehensiveReport comprehensiveReport = new ComprehensiveReport
        {
            ReportYear = int.Parse(YearAndMonControl1.Year),
            ReportMonth = int.Parse(YearAndMonControl1.Mon)
        };
        return comprehensiveReportRepository.Add(comprehensiveReport);
    }

    private bool EditSpecialtyAnalysis()
    {
        SpecialtyAnalysisRepository repository = new SpecialtyAnalysisRepository();
        SpecialtyAnalysis specialtyAnalysis = repository.Get(int.Parse(Request.QueryString["id"]));
        ComprehensiveReportRepository comprehensiveReportRepository = new ComprehensiveReportRepository();
        ComprehensiveReport comprehensiveReport = comprehensiveReportRepository.Get(int.Parse(YearAndMonControl1.Year),
            int.Parse(YearAndMonControl1.Mon));
        specialtyAnalysis.Analysis = TextEncode(tbSpecialtyAnalysis.Text);
        specialtyAnalysis.ComprehensiveReportId = comprehensiveReport.Id;
        foreach (IndicatorAnalysis indicatorAnalysis in specialtyAnalysis.IndicatorAnalysises)
            SetIndicatorAnalysis(indicatorAnalysis);
        return repository.Update(specialtyAnalysis);
    }

    private IndicatorAnalysis SetIndicatorAnalysis(IndicatorAnalysis indicatorAnalysis)
    {
        foreach (RepeaterItem rpti in rptIndicator.Items)
        {
            int indicatorAnalysisId = int.Parse(((HiddenField)rpti.FindControl("hdfId")).Value);
            if (indicatorAnalysis.Id == indicatorAnalysisId)
            {
                indicatorAnalysis.ActualValue = ((TextBox)rpti.FindControl("tbActualValue")).Text;
                indicatorAnalysis.StandardValue = ((TextBox)rpti.FindControl("tbStanderdValue")).Text;
                indicatorAnalysis.Analysis = TextEncode(((TextBox)rpti.FindControl("tbIndicatorAnalysis")).Text);
            }
        }
        return indicatorAnalysis;
    }
}