using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using TSS.BLL;
using TSS.Models;

public partial class SupervisionNews_SupervisionNewsEdit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            BindData();
    }

    public void BindData()
    {
        using (var repository = RepositoryFactory<SupervisionNewRepository>.Get())
        {
            SupervisionNew supervisionNew = repository.Get(int.Parse(Request.QueryString["id"]));
            tbTitle.Text = supervisionNew.Title;
            ckeContent.Text = supervisionNew.Content;
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        using (var repository = RepositoryFactory<SupervisionNewRepository>.Get())
        {
            SupervisionNew supervisionNew = repository.Get(int.Parse(Request.QueryString["id"]));
            supervisionNew.Title = tbTitle.Text;
            supervisionNew.Content = ckeContent.Text;
            repository.Update(supervisionNew);
        }
        Response.Redirect("default.aspx");
    }
}