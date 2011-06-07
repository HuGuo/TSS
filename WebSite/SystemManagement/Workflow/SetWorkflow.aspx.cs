using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TSS.Models;
using TSS.BLL;

public partial class SystemManagement_Workflow_SetWorkflow : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) {
            //bind user tree
            string li = "<li id=\"{0}\" {1}><span>{2}</span>";
            System.Text.StringBuilder build = new System.Text.StringBuilder();

            IList<Specialty> list = RepositoryFactory<Specialties>.Get().GetAll();
            foreach (var item in list) {
                build.AppendFormat(li, "", "state=\"closed\"", item.Name);
                build.Append("<ul>");
                foreach (var obj in item.Employees) {
                    build.AppendFormat(li, obj.Id.ToString(), "", obj.Name);
                }
                build.Append("</ul></li>");
            }

            ltLI.Text = build.ToString();
        }
    }
}