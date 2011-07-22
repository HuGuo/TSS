using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using TSS.BLL;
using TSS.Models;

public partial class SupervisionNews_SupervisionNewsAdd : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        using (var repository = RepositoryFactory<SupervisionNewRepository>.Get())
            repository.Add(new SupervisionNew
            {
                Author = "",
                ReleaseTime = DateTime.Now,
                SupervisionNewTypeId = int.Parse(Request.QueryString["s"]),
                Title = tbTitle.Text,
                Content = ckeContent.Text
            });
        Response.Redirect("default.aspx");
    }
}