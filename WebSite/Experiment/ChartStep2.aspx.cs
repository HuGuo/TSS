using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using TSS.Models;
using TSS.BLL;

public partial class Experiment_ChartStep2 : System.Web.UI.Page
{
    public string ChartString = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) {
            string coord = Request.QueryString["coord"];
            string eqs = Server.UrlDecode(Request.QueryString["eqs"]);
            if (!string.IsNullOrWhiteSpace(coord)) {

            }
        }
    }
}