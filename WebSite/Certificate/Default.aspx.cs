using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TSS.Models;
using TSS.BLL;
public partial class Certificate_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) {
            string s = Request.QueryString["s"];
            rptList.DataSource = CertificateRepository.Repository.GetBySpecialty(s);
            rptList.DataBind();
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {        
        string key = txtKey.Text.Trim();
        string s = Request.QueryString["s"];
        rptList.DataSourceID = "";
        rptList.DataSource = CertificateRepository.Repository.Serach(key, s);
        rptList.DataBind();
    }
}