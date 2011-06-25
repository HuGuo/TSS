using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using TSS.BLL;
using TSS.Models;

public partial class ComprehensiveReport_Default : BasePage
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
            .GetMuch(Request.QueryString["s"]);
        rptSpecialtyAnalysis.DataBind();
    }

    public void Del(string specialtyAnalysisId)
    {
        SpecialtyAnalysisRepository reposigotry = new SpecialtyAnalysisRepository();
        bool result = reposigotry.Delete(reposigotry.Get(int.Parse(Request.QueryString["id"])));
        DelConfirm(result, string.Format("Default.aspx?s={0}"
            , Request.QueryString["s"]));
    }
}