using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TSS.Models;
using TSS.BLL;
public partial class document_Search : System.Web.UI.Page
{
    public static string ICON_PATH = System.Configuration.ConfigurationManager.AppSettings["FileTypeIcon"];
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) {
            string s = Request.QueryString["s"];
            string key = Server.UrlDecode(Request.QueryString["key"]);
            if (!string.IsNullOrWhiteSpace(key)) {
                txtKey.Text = key;
            }
            BindList( key);
        }
    }

    void BindList(string key) 
    {
        IList<Document> result=RepositoryFactory<DocumentRepository>.Get().Search(key , Request.QueryString["s"]);
        rptlist.DataSource = result;
        rptlist.DataBind();
        if (result.Count==0) {
            ltmsg.Text = "<div class='warning'>未找到相关文件，关键字：“" + key + "”</div>";
        } else {
            ltmsg.Text = "";
        }
    }
    protected void btnSearch_Click(object sender , EventArgs e) {
        BindList(txtKey.Text.Trim());
    }

    public string getIcon(string path) {
        string extension = System.IO.Path.GetExtension(path);
        if (extension == "" || extension.ToLower() == ".folder") {
            return "../scripts/images/folder.gif";
        } else {
            return ICON_PATH + extension;
        }
    }
}