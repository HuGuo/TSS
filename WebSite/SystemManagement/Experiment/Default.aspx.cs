using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using TSS.BLL;
public partial class SystemManagement_Experiment_Default : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) {
            ModuleId = 14;
            DefaultAction = Action.View;

            rptList.DataSource = RepositoryFactory<ExpTemplateRepository>.Get().GetAll();
            rptList.DataBind();
        }
    }
    protected void rptList_ItemDataBound(object sender , RepeaterItemEventArgs e) {
        if (e.Item.ItemType== ListItemType.Item || e.Item.ItemType== ListItemType.AlternatingItem) {
            RControls.Add(new RControl(Action.CUD , e.Item.FindControl("linkEdit")));
            RControls.Add(new RControl(Action.CUD , e.Item.FindControl("linkDel")));
            RControls.Add(new RControl(Action.CUD , e.Item.FindControl("linkBindEQ")));
        }
    }
}