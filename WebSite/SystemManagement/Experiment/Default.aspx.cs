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
            string s = Request.QueryString[Helper.queryParam_specialty]; //专业id
            int p=PageIndex;
            int rowcount=0;

            ModuleId = 14;
            DefaultAction = Action.None;
            //
            IList<Specialty> list = RepositoryFactory<Specialties>.Get().GetAll();
            foreach (var item in list) {
                item.Name = item.Name + "(" + item.ExpTemlates.Count + ")";
            }
            list.Insert(0 , new Specialty { Id = "" , Name = "全部" });
            rptlistSpecialty.DataSource = list;
            rptlistSpecialty.DataBind();

            IList<ExpTemplate> list2 = RepositoryFactory<ExpTemplateRepository>.Get()
                .GetList(TemplateStatus.All , s , p , PageSize , out rowcount);

            Pagination.PageSize = PageSize;
            Pagination.RecordCount = rowcount;

            PageIndex = Helper.GetRealPageIndex(PageSize , rowcount , p);

            rptList.DataSource = list2.OrderBy(o => o.SpecialtyId).ThenBy(o => o.Title);
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