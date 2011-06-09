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
        var equipments = RepositoryFactory<Equipments>.Get();
        equipments.Add(TextBox1.Text, TextBox2.Text, TreeView1.SelectedValue, DropDownList1.SelectedValue);
    }
}