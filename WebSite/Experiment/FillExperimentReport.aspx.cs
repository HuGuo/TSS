using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TSS.BLL;
using TSS.Models;

public partial class Experiment_FillExperimentReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) {
            string templateID = Request.QueryString["tid"];
            string id = Request.QueryString["id"];
            if (!string.IsNullOrWhiteSpace(id)) {
                //edit model ,load data
                ExperimentRepository repository = new ExperimentRepository();
                Experiment experiment = repository.Get(System.Guid.Parse(id));
                if (null !=experiment) {
                    txt_tmpName.Value = experiment.Title;
                    ltHTML.Text = experiment.HTML;
                }
            }
            if (!string.IsNullOrWhiteSpace(templateID)) {
                System.Guid guid;

                if (Guid.TryParse(templateID, out guid)) {
                    //ExpCategoryRepository repository = new ExpCategoryRepository();
                    ExpTemplateRepository repository = new ExpTemplateRepository();
                    ExpTemplate template = repository.Get(guid);
                    if (null != template) {
                        txt_tmpName.Value = template.Title;
                        ltHTML.Text = template.HTML;
                    }
                }
            }
        }
    }
}