using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TSS.BLL;
using TSS.Models;

public partial class Experiment_CategoryList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) {
            string categoryId = Request.QueryString["category"];
            string s = Request.QueryString["s"];
            ExpCategory obj= RepositoryFactory<ExpCategoryRepository>.Get().Get(new Guid(categoryId));
            if (null !=obj) {
                rptlist.DataSource = obj.ExpTemplates.Where(p=>p.Enable==1);
                rptlist.DataBind();
            }

            linkAdd.HRef = string.Format("../SystemManagement/Experiment/setTemplate.aspx?s={0}&category={1}" , s , categoryId);
        }
    }
}