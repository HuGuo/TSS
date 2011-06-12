using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using TSS.BLL;

public partial class SystemManagement_Equipment_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) {
            EquipmentCategoryTextBox.Text = ParentCategoryTextBox.Text =
                Request.QueryString["category"];
        }
    }

    protected void AddEquipmentButton_Click(object sender, EventArgs e)
    {
        EquipmentListView.DataBind();
    }

    protected void AddCategoryButton_Click(object sender, EventArgs e)
    {
        var categories = RepositoryFactory<EquipmentCategories>.Get();
        categories.Add(CategoryNameTextBox.Text, ParentCategoryTextBox.Text);
    }

    protected void EquipmentListView_SelectedIndexChanged(object sender, EventArgs e)
    {
        OpenDetialDialog();
    }

    protected void DetailListView_ItemDeleting(object sender, ListViewDeleteEventArgs e)
    {
        OpenDetialDialog();
    }

    protected void DetailListView_ItemInserting(object sender, ListViewInsertEventArgs e)
    {
        e.Values.Add("Id", Guid.NewGuid());
        e.Values.Add("EquipmentId", (Guid)EquipmentListView.SelectedDataKey["Id"]);

        OpenDetialDialog();
    }

    protected void DetailListView_ItemUpdating(object sender, ListViewUpdateEventArgs e)
    {
        OpenDetialDialog();
    }

    protected void DetailListView_ItemEditing(object sender, ListViewEditEventArgs e)
    {
        OpenDetialDialog();
    }

    private void OpenDetialDialog()
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "OpenDetailDialog",
            "openDialog('DetailDialog', '设备详情')", true);
    }

    [System.Web.Services.WebMethod]
    public static string RemoveCategory(string id)
    {
        string result = string.Empty;

        var categories = RepositoryFactory<EquipmentCategories>.Get();
        if (categories.HasSubcategories(id)) {
            result = "NOT_EMPTY";
        } else {
            if (categories.Remove(id)) {
                result = "COMPLETED";
            } else {
                result = "NOT_EMPTY";
            }
        }

        return result;
    }

    [System.Web.Services.WebMethod]
    public static string RenameCategory(string id, string name)
    {
        string result = string.Empty;

        var categories = RepositoryFactory<EquipmentCategories>.Get();
        if (categories.Update(id, name)) {
            result = "COMPLETED";
        }

        return result;
    }
}