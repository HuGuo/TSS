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
    }

    public void BindData()
    {
        rptSpecialtyAnalysis.DataSource = new SpecialtyAnalysisRepository()
            .GetMuch(Request.QueryString["s"]);
        rptSpecialtyAnalysis.DataBind();
    }

    protected void lbtnDel_Click(object sender, EventArgs e)
    {
        using (var repository = RepositoryFactory<SpecialtyAnalysisRepository>.Get())
            repository.Delete(int.Parse(((LinkButton)sender).CommandArgument));
        BindData();
    }
}