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
    bool v = false;
    protected void Page_Load(object sender, EventArgs e)
    {
        string s = Request.QueryString["s"];
        v = !string.IsNullOrEmpty(s);
        if (!IsPostBack) {
            
            IList<Certificate> list = null;
            if (string.IsNullOrWhiteSpace(s)) {
                list=RepositoryFactory<CertificateRepository>.Get().GetAll();
                
            } else {
                list = RepositoryFactory<CertificateRepository>.Get().GetAll(CertificateStatus.All,s);
                linkAdd.NavigateUrl = "Add.aspx?s=" + s;
                linkAdd.Visible = true;
            }

            rptList.DataSource = list.OrderByDescending(p => p.ReceiveDateTime).ThenBy(p => p.EpmloyeeName);
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
        if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item) {
            var edit = e.Item.FindControl("linkEdit");
            if (null != edit) {
                edit.Visible = v;
            }
            var del = e.Item.FindControl("linkDel");
            if (null != del) {
                del.Visible = v;
            }
            RControls.Add(new RControl(Action.CUD , edit));
            RControls.Add(new RControl(Action.CUD , del));
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