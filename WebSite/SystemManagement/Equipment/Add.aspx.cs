using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SystemManagement_Equipment_Add : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void addButton_Click(object sender, EventArgs e)
    {
        using (var content = new TSSModel.TSSEntities())
        {
            content.AddToEQUIPMENT(new TSSModel.EQUIPMENT
            {
                ID = Guid.NewGuid().ToString(),
                NAME = TextBox1.Text,
                SP_CODE = "GHY-DC"
            });

            content.SaveChanges();
        }
    }
}