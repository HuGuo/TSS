using System;
using System.Web.UI;
using TSS.BLL;

public partial class SystemManagement_Equipment_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) {
            CategoryTextBox.Text = Request.QueryString["category"];
        }
    }

    protected void AddEquipmentButton_Click(object sender, EventArgs e)
    {
        var equipments = RepositoryFactory<Equipments>.Get();
        equipments.Add(NameTextBox.Text, CodeTextBox.Text, CategoryTextBox.Text, SpecialtyDropDownList.SelectedValue);

        EquipmentListView.DataBind();
    }

    protected void EquipmentListView_SelectedIndexChanged(object sender, EventArgs e)
    {
        OpenDetialDialog();
    }

    protected void DetailListView_ItemInserted(object sender, System.Web.UI.WebControls.ListViewInsertedEventArgs e)
    {
        OpenDetialDialog();
    }

    private void OpenDetialDialog()
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "OpenDetailDialog",
            "openDialog('DetailDialog', '设备详情')", true);
    }
}