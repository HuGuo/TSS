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
            string eqId=Request.QueryString["eqmId"];
            //设备信息
            if (!string.IsNullOrWhiteSpace(eqId)) {
                Equipment obj = RepositoryFactory<Equipments>.Get().Get(new Guid(eqId));
                if (null !=obj) {
                    rptEquipment.DataSource = obj.EquipmentDetails;
                    rptEquipment.DataBind();
                }

            }
            if (!string.IsNullOrWhiteSpace(id)) {
                //edit model ,load data                
                Experiment experiment = RepositoryFactory<ExperimentRepository>.Get().Get(System.Guid.Parse(id));
                if (null !=experiment) {
                    txt_tmpName.Value = experiment.Title;
                    ltHTML.Text = experiment.HTML;
                    txt_expdate.Text = experiment.ExpDate.ToString("yyyy-MM-dd");
                    txt_remark.Value = experiment.Remark.HtmlDecode();
                    rptlistAttachment.DataSource = experiment.Attachments;
                    rptlistAttachment.DataBind();
                }
            }
            else if (!string.IsNullOrWhiteSpace(templateID)) {
                System.Guid guid;

                if (Guid.TryParse(templateID, out guid)) {
                    string input = "<input type=\"text\" style=\"width:75px;\" class=\"{0}\" {1} />";
                    string textarea = "<textarea style=\"width:0;height:0;\" class=\"text\"></textarea>";

                    ExpTemplate template = RepositoryFactory<ExpTemplateRepository>.Get().Get(guid);
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