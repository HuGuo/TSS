using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Experiment_ExpList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) {
            hlSetTmp.NavigateUrl = string.Format("SetTemplate.aspx?cid={0}&sp={1}",Request.QueryString["cid"],Request.QueryString["sp"]);
            hlFillIn.NavigateUrl = string.Format("FillExperimentReport.aspx?tid={0}", Request.QueryString["cid"]);
        }
    }
}