using System;
using System.Collections.Generic;
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
                IList<bool> rights = new List<bool>();
                GetRights(module, rights);
                e.Item.Visible = rights.Contains(true);
            }
        }
    }

    private void GetRights(Module module, IList<bool> rights)
    {
        if (module.Submodules.Count == 0) {
            rights.Add(HasRight(module.Id, Action.View));
        } else {
            foreach (var m in module.Submodules) {
                GetRights(m, rights);
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
                result = modules.GetFullPath(module.Id).TrimStart('/');

                foreach (string rule in System.Configuration
                    .ConfigurationManager.AppSettings["UrlRules"].Split(';')) {
                    result = System.Text.RegularExpressions.Regex.Replace(
                        result,
                        rule.Split(',')[0],
                        rule.Split(',')[1]);
                }
            }
        }

        return result;
    }
}