using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using TSS.BLL;
using TSS.Models;

public partial class ComprehensiveReport_SpecialtyAnalysis : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            BindData();
        string specialtyAnalysisId = Request.QueryString["id"];
        if (!string.IsNullOrEmpty(specialtyAnalysisId))
            Del(specialtyAnalysisId);
    }

    public void BindData()
    {
        rptSpecialtyAnalysis.DataSource = new SpecialtyAnalysisRepository()
            .GetMuch(Request.QueryString["specialtyId"]);
        rptSpecialtyAnalysis.DataBind();
    }

    public void Del(string specialtyAnalysisId)
    {
        SpecialtyAnalysisRepository reposigotry = new SpecialtyAnalysisRepository();
        bool result = reposigotry.Delete(reposigotry.Get(int.Parse(Request.QueryString["id"])));
        DelConfirm(result, string.Format("SpecialtyAnalysis.aspx?specialtyId={0}"
            , Request.QueryString["specialtyId"]));
    }
}