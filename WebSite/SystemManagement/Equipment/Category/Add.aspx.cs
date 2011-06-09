using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TSS.BLL;

public partial class SystemManagement_Equipment_Category_Add : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) {
            XmlDataSource1.Data = RepositoryFactory<EquipmentCategories>.Get().GetXml().ToString();
        }
    }

    protected void addButton_Click(object sender, EventArgs e)
    {
        var categories = RepositoryFactory<EquipmentCategories>.Get();
        categories.Add(new TSS.Models.EquipmentCategory {
            Id = Guid.NewGuid(),
            Name = TextBox1.Text,
            ParentCategoryId = string.IsNullOrEmpty(TreeView1.SelectedValue) ? (Guid?)null : Guid.Parse(TreeView1.SelectedValue)
        });

        XmlDataSource1.Data = RepositoryFactory<EquipmentCategories>.Get().GetXml().ToString();
    }
}