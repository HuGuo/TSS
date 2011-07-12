using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using TSS.Models;

public partial class UserControl_IndicatorControl : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public string SpecialtyId
    {
        get { return SpecialtyControl1.SpecialtyId; }
        set { SpecialtyControl1.SpecialtyId = value; }
    }

    public Indicator Indicator
    {
        get
        {
            Indicator indicator = new Indicator();
            if (ViewState["indicator"] != null)
                indicator = (Indicator)ViewState["indicator"];
            indicator.IndicatorName = tbName.Text;
            indicator.IndivatorUnit = tbUnit.Text;
            indicator.SpecialtyId = SpecialtyControl1.SpecialtyId;
            return indicator;
        }
        set
        {
            tbName.Text = value.IndicatorName;
            tbUnit.Text = value.IndivatorUnit;
            SpecialtyControl1.SpecialtyId = value.SpecialtyId;
            ViewState["indicator"] = value;
        }
    }

    public void ReSet()
    {
        SpecialtyControl1.SpecialtyId = "";
        tbName.Text = "";
        tbUnit.Text = "";
    }
}