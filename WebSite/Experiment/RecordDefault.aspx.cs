using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TSS.BLL;

public partial class Experiment_RecordDefault : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) {
            string s = Server.UrlDecode(Request.QueryString[Helper.queryParam_specialty]);
            rptlist.DataSource = RepositoryFactory<ExpReocrdRepository>.Get().GetAll(s);
            rptlist.DataBind();
        }
    }
}