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
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) {
            string id = Request.QueryString["id"];
            if (!string.IsNullOrWhiteSpace(id)) {
                ExperimentRepository repository = new ExperimentRepository();
                Experiment obj= repository.Get(new Guid(id));
                if (null !=obj) {
                    ltHTML.Text = obj.HTML;
                    ltTitle.Text = obj.Title;
                    ltResult.Text = obj.Result == 0 ? "不合格" : "合格";
                }
            }
        }
    }
}