using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TSS.Models;
using TSS.BLL;

public partial class SystemManagement_Employee_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) {
            rptlist.DataSource= RepositoryFactory<Employees>.Get().GetAll();
            rptlist.DataBind();

            //bind specialty
            IList<Specialty> list = RepositoryFactory<Specialties>.Get().GetAll();
            foreach (var item in list) {
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
        rptlist.DataSource = RepositoryFactory<Employees>.Get().Search(txtKey.Text.Trim());
        rptlist.DataBind();
    }
}