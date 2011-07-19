using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TSS.Models;
using TSS.BLL;
public partial class Certificate_Default : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) {
            string s = Request.QueryString["s"];
            IList<Certificate> list = null;
            if (string.IsNullOrWhiteSpace(s)) {
                list=RepositoryFactory<CertificateRepository>.Get().GetAll();
                
            } else {
                list = RepositoryFactory<CertificateRepository>.Get().GetAll(CertificateStatus.All,s);
                linkAdd.NavigateUrl = "Add.aspx?s=" + s;
                linkAdd.Visible = true;
            }
            
            rptList.DataSource = list;
            rptList.DataBind();
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {        
        string key = txtKey.Text.Trim();
        string s = Request.QueryString["s"];
        rptList.DataSourceID = "";
        rptList.DataSource = RepositoryFactory<CertificateRepository>.Get().Serach(key, s);
        rptList.DataBind();
    }
    protected void rptList_ItemDataBound(object sender , RepeaterItemEventArgs e) {
        if (e.Item.ItemType== ListItemType.AlternatingItem || e.Item.ItemType== ListItemType.Item) {
            RControls.Add(new RControl(Action.CUD , e.Item.FindControl("linkEdit")));   
            RControls.Add(new RControl(Action.CUD , e.Item.FindControl("linkDel")));
        }
    }

    DateTime now = DateTime.Now;
    public string SetColor(DateTime dt) {
        string cls = "normal";
        if (now.CompareTo(dt) > 0) {
            cls = "color1";
        } else {
            TimeSpan span = dt - now;
            if (span.Days<30) {
                cls = "color2";
            }
        }
        return cls;
    }
}