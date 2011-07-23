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
    CertificateStatus status = CertificateStatus.All;

    protected void Page_Load(object sender , EventArgs e) {
        string s = Request.QueryString[Helper.queryParam_specialty]; //专业id
        status = GetStatus();
        int p = PageIndex; //当前页码
        v = !string.IsNullOrEmpty(s);

        int rowcount = 0; // 记录数
        if (!IsPostBack) {

            IList<Certificate> list = RepositoryFactory<CertificateRepository>.Get().GetList(status , s , p , PageSize , out rowcount);

            if (string.IsNullOrWhiteSpace(s)) {
            } else {
                linkAdd.NavigateUrl = "Add.aspx?s=" + s;
                linkAdd.Visible = true;
            }
            Pager1.RecordCount = rowcount;
            Pager1.PageSize = PageSize;

            PageIndex = Helper.GetRealPageIndex(PageSize , rowcount , p);

            rptList.DataSource = list.OrderByDescending(P => P.ReceiveDateTime);
            rptList.DataBind();
        }
    }

    protected void btnSearch_Click(object sender , EventArgs e) {
        string key = txtKey.Text.Trim();
        string s = Request.QueryString[Helper.queryParam_specialty];
        rptList.DataSourceID = "";
        rptList.DataSource = RepositoryFactory<CertificateRepository>.Get().Serach(key , s);
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
            if (span.Days < 30) {
                cls = "color2";
            }
        }
        return cls;
    }

    CertificateStatus GetStatus() {
        string status = Request.QueryString["status"];
        int val = 0;
        int.TryParse(status , out val);
        if (val>3 || val <0) {
            val = 0;
        }
        return (CertificateStatus)val;
    }
}