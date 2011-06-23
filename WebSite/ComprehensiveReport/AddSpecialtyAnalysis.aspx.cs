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

    protected void BindData()
    {
        rptIndicator.DataSource = new IndicatorRepository().GetAll();
        rptIndicator.DataBind();
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        ComprehensiveReport comprehensiveReport = GetComprehensiveReport(YearAndMonControl1.Year, 
            YearAndMonControl1.Mon);
        if (comprehensiveReport == null)
        {
            AddComprehensiveReport();
        }
        else
        {
            if (!IsExistSpecialtyAnalysis())
                AddSpecialtyAnalysis();
            else
                ExistCurrentTimeRecord();
        }
    }

    protected void AddSpecialtyAnalysis()
    {
        SpecialtyAnalysisRepository repository = new SpecialtyAnalysisRepository();
        bool result= repository.Add(GetSpecialtyAnalysisOnComprehensiveReport());
        ConfirmAdd(result);
    }

    protected void ConfirmAdd(bool result)
    {
        AddConfirm(result,string.Format("specialtyAnalysis.aspx?specialtyId={0}", 
            Request.QueryString["specialtyId"]));
    }

    protected ComprehensiveReport GetComprehensiveReport(string year, string mon)
    {
        ComprehensiveReportRepository comprehensiveReportRepository = new ComprehensiveReportRepository();
        return comprehensiveReportRepository.Get(int.Parse(year), int.Parse(mon));
    }

    protected void AddComprehensiveReport()
    {
        ComprehensiveReportRepository repository = new ComprehensiveReportRepository();
        bool result = repository.Add(GetComprehensiveReport());
        ConfirmAdd(result);
    }

    protected ComprehensiveReport GetComprehensiveReport()
    {
        return new ComprehensiveReport
        {
            ReportYear = int.Parse(YearAndMonControl1.Year),
            ReportMonth = int.Parse(YearAndMonControl1.Mon),
            SpecialtyAnalysises = new List<SpecialtyAnalysis> { GetSpecialtyAnalysis() }
        };
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

    protected IList<IndicatorAnalysis> GetIndicatorAnalysis()
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

    protected bool IsExistSpecialtyAnalysis()
    {
        SpecialtyAnalysisRepository repositoy = new SpecialtyAnalysisRepository();
        return repositoy.IsExistWhileAdd(int.Parse(YearAndMonControl1.Year),
            int.Parse(YearAndMonControl1.Mon), Request.QueryString["specialtyId"]);
    }

    protected SpecialtyAnalysis GetSpecialtyAnalysisOnComprehensiveReport()
    {
        ComprehensiveReportRepository respository = new ComprehensiveReportRepository();
        ComprehensiveReport comprehensiveReport = respository.Get(
            int.Parse(YearAndMonControl1.Year),int.Parse(YearAndMonControl1.Mon));
        SpecialtyAnalysis specialtyAnalysis = GetSpecialtyAnalysis();
        specialtyAnalysis.ComprehensiveReportId = comprehensiveReport.Id;
        specialtyAnalysis.IndicatorAnalysises = GetIndicatorAnalysis();
        return specialtyAnalysis;
    }
}