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
    public static string ICON_PATH = "http://192.168.0.100/icons/geticon.axd?file=";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) {
            string s = Request.QueryString["s"];
            string pid = Request.QueryString["pid"];
            Guid? parentId = null;
            if (!string.IsNullOrWhiteSpace(pid)) {
                parentId = new Guid(pid);
            }
            IList<Document> list = DocumentRepository.Repository.GetChildItems(parentId, s);
            rptlist.DataSource = list;
            rptlist.DataBind();
        }
    }

    public string getIcon(string path) 
    {
        string extension=System.IO.Path.GetExtension(path);
        if (extension=="") {
            return "../scripts/images/folder.gif";
        } else {
            return ICON_PATH + extension;
        }
    }
}