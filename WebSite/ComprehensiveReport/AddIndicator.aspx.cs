using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using TSS.BLL;
using TSS.Models;

public partial class ComprehensiveReport_AddIndicator : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            BindData();
    }

    public void BindData()
    {
        SpecialtyControl1.SpecialtyId = Request.QueryString["specialtyId"];
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        bool result = new IndicatorRepository().Add(
            new Indicator
            {
                SpecialtyId = SpecialtyControl1.SpecialtyId,
                IndicatorName = tbName.Text,
                IndivatorUnit = tbUnit.Text
            });
        AddConfirm(result,
            string.Format("Indicator.aspx?specialtyId={0}", Request.QueryString["specialtyId"]));
    }


}