using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using TSS.BLL;

public partial class ComprehensiveReport_Indicator : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            BindData();
        string id = Request.QueryString["id"];
        if (!string.IsNullOrEmpty(id))
            Del(id);
    }

    public void BindData()
    {
        rptIndicator.DataSource = new IndicatorRepository()
            .GetBySpecialty(Request.QueryString["specialtyId"]);
        rptIndicator.DataBind();
    }

    public void Del(string id)
    {
        IndicatorRepository repository = new IndicatorRepository();
        bool resutl = repository.Delete(repository.Get(int.Parse(id)));
        DelConfirm(resutl);
        DataBind();
    }
}