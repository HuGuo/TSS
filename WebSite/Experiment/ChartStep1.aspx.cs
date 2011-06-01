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
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) {
            string templateId = Request.QueryString["tid"];
            if (!string.IsNullOrWhiteSpace(templateId)) {
                ExpTemplate template = ExpTemplateRepository.Repository.Get(new Guid(templateId));
                if (null !=template) {

                    ltHtml.Text = template.HTML;
                    ltTitle.Text = template.Title;
                    //foreach (var item in template.Equipments) {
                    //    ckblist.Items.Add(new ListItem(item.Name, item.Id.ToString()));
                    //}
                }

            }
        }
    }
}