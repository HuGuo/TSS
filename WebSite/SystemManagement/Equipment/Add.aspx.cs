using System;

using TSS.BLL;

public partial class SystemManagement_Equipment_Add : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) {
            XmlDataSource1.Data = RepositoryFactory<EquipmentCategories>.Get().GetXml().ToString();
        }
    }

    protected void addButton_Click(object sender, EventArgs e)
    {
        RepositoryFactory<Equipments>.Get().Add(new TSS.Models.Equipment {
            Id = Guid.NewGuid(),
            Name = TextBox1.Text,
            EquipmentCategoryId = Guid.Parse(TreeView1.SelectedValue),
            SpecialtyId = DropDownList1.SelectedValue
        });
    }
}