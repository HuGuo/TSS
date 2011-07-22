using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using TSS.BLL;
using TSS.Models;

public partial class SupervisionNews_SupervisionNewsDetail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            BindData();
    }

    protected void BindData()
    {
        using (var repository = RepositoryFactory<SupervisionNewRepository>.Get())
        {
            SupervisionNew supervisionNew = repository.Get(int.Parse(Request.QueryString["id"]));
            lbTitle.Text = supervisionNew.Title;
            lbReleaseTime.Text = supervisionNew.ReleaseTime.ToString();
            plContent.GroupingText = supervisionNew.Content;
        }
    }
}