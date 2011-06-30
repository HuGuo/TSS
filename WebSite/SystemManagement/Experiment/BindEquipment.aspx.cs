using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TSS.Models;
using TSS.BLL;

public partial class SystemManagement_Experiment_BindEquipment : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        IList<Specialty> list=TSS.BLL.RepositoryFactory<TSS.BLL.Specialties>.Get().GetAll();
        foreach (var item in list) {
            item.Name = item.Name + "("+item.ExpTemlates.Count+")";
        }

        IList<ExpTemplate> list2=TSS.BLL.RepositoryFactory<TSS.BLL.ExpTemplateRepository>.Get().GetAll();

        list.Insert(0 , new Specialty { Id = "ALL" , Name = "全部("+list2.Count+")" });
        rptlistSpecialty.DataSource = list;
        rptlistSpecialty.DataBind();

        rptlistET.DataSource = list2;
        rptlistET.DataBind();
    }
}