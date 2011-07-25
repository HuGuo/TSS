using System;
using System.Linq;
using System.Web.UI.WebControls;

using TSS.BLL;
using TSS.Models;
using System.Configuration;
using System.Collections.Generic;

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
                e.Item.Visible = HasRight(module);
            }
        }
    }

    private bool HasRight(Module module)
    {
        if (module.Submodules.Count == 0) {
            return HasRight(module.Id, Action.View);
        } else {
            foreach (var m in module.Submodules) {
               return HasRight(m);
            }
        }

        return false;
    }

    protected string GetUrl(int moduleId)
    {
        string result = null;

        var module = modules.Get(moduleId);
        if (module != null) {
            if (module.Submodules.Count == 0 &&
                !string.IsNullOrEmpty(module.Path)) {
                foreach (string rule in
                    ConfigurationManager.AppSettings["UrlRules"].Split(';')) {
                    result = System.Text.RegularExpressions.Regex.Replace(
                        modules.GetFullPath(module.Id),
                        rule.Split(',')[0],
                        rule.Split(',')[1]);
                }

                result = result.TrimStart('/');
            }
        }

        return result;
    }
}