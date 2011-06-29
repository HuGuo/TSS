using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using TSS.BLL;
using TSS.Models;

public partial class ComprehensiveReport_Indicator : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            BindData();
        string id = Request.QueryString["id"];
        if (!string.IsNullOrEmpty(id))
            Del(int.Parse(id));
    }

    public void BindData()
    {
        rptIndicator.DataSource = new IndicatorRepository()
            .GetBySpecialty(Request.QueryString["specialtyId"]);
        rptIndicator.DataBind();
    }

    public void Del(int indicatorId)
    {
        IndicatorAnalysisRepository repository = new IndicatorAnalysisRepository();
        if (repository.IsExistOnIndicator(indicatorId))
        {
            ExistChildConfirm();
        }
        else
        {
            bool resutl = new IndicatorRepository().Delete(indicatorId);
            DelConfirm(resutl);
        }
        BindData();
    }
}