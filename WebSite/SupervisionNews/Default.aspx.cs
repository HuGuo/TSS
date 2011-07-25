using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using TSS.BLL;
using TSS.Models;

public partial class SupervisionNews_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            BindData();
    }

    protected void BindData()
    {
        using (var repository = RepositoryFactory<SupervisionNewRepository>.Get())
            rptNews.DataSource = repository.GetByNewType(int.Parse(Request.QueryString["s"]));
        rptNews.DataBind();
    }

    protected void lbtnDel_Click(object sender, EventArgs e)
    {
        using (var repository = RepositoryFactory<SupervisionNewRepository>.Get())
            repository.Delete(((LinkButton)sender).CommandArgument);
    }
}