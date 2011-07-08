using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using TSS.BLL;
using TSS.Models;
public partial class SystemManagement_Experiment_Default : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) {
            ModuleId = 14;
            DefaultAction = Action.None;
            //
            IList<Specialty> list = RepositoryFactory<Specialties>.Get().GetAll();
            foreach (var item in list) {
                item.Name = item.Name + "(" + item.ExpTemlates.Count + ")";
            }

            IList<ExpTemplate> list2 = RepositoryFactory<ExpTemplateRepository>.Get().GetAll();

            list.Insert(0 , new Specialty { Id = "ALL" , Name = "全部(" + list2.Count + ")" });
            rptList.DataSource = list2;
            rptList.DataBind();

            rptlistSpecialty.DataSource = list;
            rptlistSpecialty.DataBind();
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