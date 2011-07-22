using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TSS.Models;
using TSS.BLL;

public partial class SystemManagement_Employee_Default : BasePage
{
    protected void Page_Load(object sender, EventArgs e) {
        if (!IsPostBack) {
            int rowcount = 0;
            IList<Employee> list1 = RepositoryFactory<Employees>.Get().GetAll(PageIndex , PageSize , out rowcount);
            PageIndex = Helper.GetRealPageIndex(PageSize , rowcount , PageIndex);

            rptlist.DataSource = list1;
            rptlist.DataBind();
            //pagination
            Pager1.RecordCount = rowcount;
            Pager1.PageSize = PageSize;

            //bind specialty
            IList<Specialty> list2 = RepositoryFactory<Specialties>.Get().GetAll();
            foreach (var item in list2) {
                ddlSpecialty.Items.Add(new ListItem(item.Name,item.Id));
            }

            //bind all roles
            IList<Role> all = RepositoryFactory<RolesRepository>.Get().GetAll();
            foreach (var item in all) {
                selRight.Items.Add(new ListItem(item.Name , item.Id.ToString()));
            }
        }
    }
    protected void btnSearch_Click(object sender , EventArgs e) {
        Pager1.Visible = false;
        rptlist.DataSource = RepositoryFactory<Employees>.Get().Search(txtKey.Text.Trim());
        rptlist.DataBind();
    }
}