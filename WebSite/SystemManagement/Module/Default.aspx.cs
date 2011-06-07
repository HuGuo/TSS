using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using TSS.BLL;

public partial class SystemManagement_Module_Default : System.Web.UI.Page
{
    Specialties specilties = RepositoryFactory<Specialties>.Get();

    protected void DeleteButton_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(ListBox1.SelectedValue)) {
            specilties.RemoveModule(DropDownList1.SelectedValue,
                int.Parse(ListBox1.SelectedValue));

            ListBox1.DataBind();
            ListBox2.DataBind();
        }
    }

    protected void AddButton_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(ListBox2.SelectedValue)) {
            specilties.AddModule(DropDownList1.SelectedValue,
                int.Parse(ListBox2.SelectedValue));

            ListBox1.DataBind();
            ListBox2.DataBind();
        }
    }
}