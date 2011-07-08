using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TSS.BLL;
using TSS.Models;

public partial class Experiment_experiment : System.Web.UI.Page
{
    public string res = "";
    protected void Page_Load(object sender , EventArgs e) {
        string id = Request.QueryString["id"];
        if (!string.IsNullOrWhiteSpace(id)) {
            Experiment obj = RepositoryFactory<ExperimentRepository>.Get().Get(new Guid(id));
            if (null != obj) {
                ltHTML.Text = obj.HTML;
                ltTitle.Text = obj.Title;
                ltRemark.Text = obj.Remark;
                ltResult.Text = obj.Result == 0 ? "不合格" : "合格";
                res = "res" + obj.Result;

                string attachments = string.Empty;
                string liStr="<li><a href=\"../exp.ashx?op=download&id={0}&filename={1}\">{2}</a></li>";
                foreach (var item in obj.Attachments) {
                    attachments += string.Format(liStr , item.Id , Server.UrlEncode(item.FileName) , item.FileName);
                }
                ltAttachment.Text = attachments;
            }
        }
    }
    
}