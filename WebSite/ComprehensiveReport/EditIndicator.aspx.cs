using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using TSS.BLL;
using TSS.Models;

public partial class ComprehensiveReport_EditIndicator : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            BindData();
    }

    public void BindData()
    {
        Indicator indicator = new IndicatorRepository().Get(
            int.Parse(Request.QueryString["id"]));
        tbName.Text = indicator.IndicatorName;
        tbUnit.Text = indicator.IndivatorUnit;
        SpecialtyControl1.SpecialtyId = indicator.SpecialtyId;
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        bool result = new IndicatorRepository().Add(
            new Indicator
            {
                IndicatorName = tbName.Text,
                IndivatorUnit = tbUnit.Text
            });
        EditConfirm(result, 
            string.Format("Indicator.aspx?specialtyId={0}", Request.QueryString["specialtyId"]));
    }
}