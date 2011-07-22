using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TSS.BLL;
using TSS.Models;
public partial class document_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) {
            string s = Request.QueryString[Helper.queryParam_specialty];
            string pid = Request.QueryString["pid"];
            Guid? parentId = null;
            if (!string.IsNullOrWhiteSpace(pid)) {
                parentId = new Guid(pid);
                Document obj = RepositoryFactory<DocumentRepository>.Get().Get(parentId.Value);
                if (null !=obj) {
                    backto.HRef = string.Format("default.aspx?s={0}&pid={1}" , Request.QueryString[Helper.queryParam_specialty] , obj.ParentId.HasValue ? obj.ParentId.Value.ToString() : "");
                }
            }
            IList<Document> list = RepositoryFactory<DocumentRepository>.Get().GetChildItems(parentId, s);
            rptlist.DataSource = list;
            rptlist.DataBind();
        }
    }

}