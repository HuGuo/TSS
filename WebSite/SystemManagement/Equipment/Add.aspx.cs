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
        using (var content = new TSS.Models.Context())
        {
            content.Equipments.Add(new TSS.Models.Equipment {
                Id = Guid.NewGuid(),
                Name = TextBox1.Text,
                SpecialtyId = DropDownList1.SelectedValue
            });

            content.SaveChanges();
        }
    }
}