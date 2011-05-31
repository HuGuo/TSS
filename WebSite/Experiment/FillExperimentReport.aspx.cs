using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TSS.BLL;
using TSS.Models;
using System.Text.RegularExpressions;

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
                    txt_expdate.Text = experiment.ExpDate.ToString("yyyy-MM-dd");
                }
            }
            else if (!string.IsNullOrWhiteSpace(templateID)) {
                System.Guid guid;

                if (Guid.TryParse(templateID, out guid)) {
                    string input = "<input type=\"text\" style=\"width:0;height:0;\" class=\"{0}\" {1} />";
                    string textarea = "<textarea style=\"width:0;height:0;\" class=\"text\"></textarea>";
                    ExpTemplateRepository repository = new ExpTemplateRepository();
                    ExpTemplate template = repository.Get(guid);
                    if (null != template) {
                        txt_tmpName.Value = template.Title;
                        ltHTML.Text = template.HTML.Replace("{d}", string.Format(input, "number", ""))
                            .Replace("{time}", string.Format(input, "time", "onClick=\"WdatePicker()\""))
                            .Replace("{#}",string.Format(input,"text",""))
                            .Replace("{##}",textarea); 
                    }
                }
            }
        }
    }

   
}