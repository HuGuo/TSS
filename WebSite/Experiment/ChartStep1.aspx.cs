using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TSS.Models;
using TSS.BLL;

public partial class Experiment_ChartStep1 : System.Web.UI.Page
{
    public string templateTitle = string.Empty;
    protected void Page_Load(object sender , EventArgs e) {
        string templateId = Request.QueryString["tid"];
        if (!string.IsNullOrWhiteSpace(templateId)) {
            ExpTemplate template = RepositoryFactory<ExpTemplateRepository>.Get().Get(new Guid(templateId));
            if (null != template) {

                ltHtml.Text = template.HTML;
                templateTitle = template.Title;
                rptlist.DataSource = template.Equipments;
                rptlist.DataBind();
            }
        }
    }
}