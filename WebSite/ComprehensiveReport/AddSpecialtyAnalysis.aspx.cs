using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using TSS.BLL;
using TSS.Models;

public partial class ComprehensiveReport_AddSpecialtyAnalysis : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            BindData();
    }

    public void BindData()
    {
        rptIndicator.DataSource = new IndicatorRepository().GetAll();
        rptIndicator.DataBind();
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        SpecialtyAnalysisRepository specialtyAnalysisRepository = new SpecialtyAnalysisRepository();
        ComprehensiveReportRepository comprehensiveReportRepository = new ComprehensiveReportRepository();
        ComprehensiveReport comprehensiveReport = comprehensiveReportRepository.Get(
            int.Parse(YearAndMonControl1.Year), int.Parse(YearAndMonControl1.Mon));
        if (comprehensiveReport == null)
            AddConfirm(comprehensiveReportRepository.Add(GetComprehensiveReport()),
                string.Format("specialtyAnalysis.aspx?specialtyId={0}", Request.QueryString["specialtyId"]));
        else
            if (!specialtyAnalysisRepository.IsExistWhileAdd(GetSpecialtyAnalysis(comprehensiveReport)))
                AddConfirm(specialtyAnalysisRepository.Add(GetSpecialtyAnalysis(comprehensiveReport)),
               string.Format("specialtyAnalysis.aspx?specialtyId={0}", Request.QueryString["specialtyId"]));
            else
                ExistCurrentTimeRecord();
    }

    public ComprehensiveReport GetComprehensiveReport()
    {
        return new ComprehensiveReport
        {
            ReportYear = int.Parse(YearAndMonControl1.Year),
            ReportMonth = int.Parse(YearAndMonControl1.Mon),
            SpecialtyAnalysises = new List<SpecialtyAnalysis> { GetSpecialtyAnalysis() }
        };
    }

    public SpecialtyAnalysis GetSpecialtyAnalysis(ComprehensiveReport comprehensiveReport)
    {
        SpecialtyAnalysis specialtyAnalysis = GetSpecialtyAnalysis();
        specialtyAnalysis.ComprehensiveReportId = comprehensiveReport.Id;
        specialtyAnalysis.ComprehensiveReport = new ComprehensiveReport
        {
            Id = comprehensiveReport.Id,
            ReportMonth = comprehensiveReport.ReportMonth,
            ReportYear = comprehensiveReport.ReportYear
        };
        return specialtyAnalysis;
    }

    public SpecialtyAnalysis GetSpecialtyAnalysis()
    {
        return new SpecialtyAnalysis
        {
            SpecialtyId = Request.QueryString["specialtyId"].ToUpper(),
            Analysis = TextEncode(tbSpecialtyAnalysis.Text),
            IndicatorAnalysises = GetIndicatorAnalysis()
        };
    }

    public IList<IndicatorAnalysis> GetIndicatorAnalysis()
    {
        IList<IndicatorAnalysis> listIndicatorAnalysis = new List<IndicatorAnalysis>();
        foreach (RepeaterItem rpti in rptIndicator.Items)
        {
            listIndicatorAnalysis.Add(new IndicatorAnalysis()
            {
                ActualValue = ((TextBox)rpti.FindControl("tbActualValue")).Text,
                StandardValue = ((TextBox)rpti.FindControl("tbStanderdValue")).Text,
                IndicatorId = int.Parse(((HiddenField)rpti.FindControl("tbIndicatorId")).Value),
                Analysis = TextEncode(((TextBox)rpti.FindControl("tbIndicatorAnalysis")).Text)
            }
            );
        }
        return listIndicatorAnalysis;
    }

}