using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using workflow;

public partial class Report_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) {
            string specialtyId = Request.QueryString[Helper.queryParam_specialty];
            using (WFContext db=new WFContext()) {
                rptlist.DataSource= ReportDetailRepository.GetAll(specialtyId , db);
                rptlist.DataBind();
            }
        }
    }
}