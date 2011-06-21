using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TSS.BLL;
using TSS.Models;
public partial class SystemManagement_Experiment_SetExpRecord : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //string id = "b7064ba8-1585-4733-bc2a-1f295c97b150";
        string id = Request.QueryString["id"];
        ExpRecord obj = RepositoryFactory<ExpReocrdRepository>.Get().Get(new Guid(id));
        if (null != obj) {
            if (null != obj.DataSourcesTemplate) {
                ltExpTemplate.Text = obj.DataSourcesTemplate.HTML;
                exptemplate.Value = obj.ExpTemplateId.ToString();
            }
            using (System.IO.StreamReader fs = System.IO.File.OpenText(Server.MapPath(obj.Path))) {
                ltExpReocrd.Text = fs.ReadToEnd();
                fs.Close();
            }
        }
    }
}