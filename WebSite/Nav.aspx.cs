using System;
using System.Linq;
using System.Web.UI.WebControls;

using TSS.BLL;
using TSS.Models;

public partial class Nav : BasePage
{
    private Modules modules = RepositoryFactory<Modules>.Get();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) {
            ListView1.DataSource = modules.GetRootModules();
            ListView1.DataBind();
        }
    }

    protected void ListView_ItemCreated(object sender, ListViewItemEventArgs e)
    {
        var currentItem = e.Item as ListViewDataItem;
        if (currentItem != null) {
            var module = currentItem.DataItem as Module;
            if (module != null) {
                if (module.Submodules.Count > 0 && module.Submodules.All(m =>
                    !HasRight(m.Id, Action.View))) {
                    e.Item.Visible = false;
                } else if (module.Submodules.Any(m =>
                    HasRight(m.Id, Action.View))) {
                    e.Item.Visible = true;
                } else {
                    e.Item.Visible = HasRight(module.Id, Action.View);
                }
            }
        }
    }

    protected string GetUrl(int moduleId)
    {
        string result = null;

        var module = modules.Get(moduleId);
        if (module != null) {
            if (module.Submodules.Count == 0 &&
                !string.IsNullOrEmpty(module.Path)) {
                result = System.Text.RegularExpressions.Regex.Replace(
                    modules.GetFullPath(module.Id),
                    @"^Specialty/([\w-]*)/(\w*)", @"$2/?s=$1");
                result = result.TrimStart('/');
            }
        }

        return result;
    }
}