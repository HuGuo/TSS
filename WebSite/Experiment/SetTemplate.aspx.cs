using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TSS.Models;
using TSS.BLL;

public partial class Experiment_SetTemplate : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) {
            //
            string categoryID = Request.QueryString["cid"];
            if (!string.IsNullOrWhiteSpace(categoryID)) {
                int id = 0;
                if (int.TryParse(categoryID, out id)) {
                    //ExpCategoryRepository repository = new ExpCategoryRepository();
                    ExpTemplateRepository repository = new ExpTemplateRepository();
                    ExpTemplate template = repository.Get(id);
                    if (null!=template) {
                        txt_tmpName.Value = template.Title;
                        ltHTML.Text = template.HTML;
                    }
                }
            }
        }
    }
}