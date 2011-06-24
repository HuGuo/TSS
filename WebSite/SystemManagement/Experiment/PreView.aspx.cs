using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TSS.BLL;
using TSS.Models;
public partial class SystemManagement_Experiment_PreView : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) {
            string id = Request.QueryString["id"];
            if (!string.IsNullOrWhiteSpace(id)) {
                ExpTemplate obj= RepositoryFactory<ExpTemplateRepository>.Get().Get(new Guid(id));
                if (obj!=null) {
                    ltHTML.Text = obj.HTML;
                    ltTitle.Text = obj.Title;
                }
            }
        }
    }
}