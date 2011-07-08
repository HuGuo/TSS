using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TSS.Models;
using TSS.BLL;
using System.Data;

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

            //bind workflow
            string id = Request.QueryString["id"];
            if (!string.IsNullOrWhiteSpace(id)) {
                using (workflow.WFContext db = new workflow.WFContext()) {

                    workflow.Workflow wf = workflow.WFRepository.Get(id , db);
                    if (null != wf) {
                        wf.DesrializeFromXML();
                        txtName.Text = wf.Name;
                        stepsCount.Value = wf.Actives.Count.ToString();
                        DataTable dt = new DataTable();
                        dt.Columns.Add("Id");
                        dt.Columns.Add("hours");
                        dt.Columns.Add("t");
                        System.Text.StringBuilder build2 = new System.Text.StringBuilder();
                        foreach (var item in wf.Actives) {
                            build2.Clear();
                            foreach (var signer in item.Signers) {
                                build2.AppendFormat("<a class=\"{0} button\" sid=\"{1}\"><span class=\"user icon\"></span>{2}</a> " , signer.IsWeight ? "primary negative" : "" , signer.Id , signer.NameCN);
                            }
                            dt.Rows.Add(item.Id , item.IntervalHours , build2.ToString());
                        }

                        rptlist.DataSource = dt;
                        rptlist.DataBind();
                    }
                }
                //end using
            }
            //end if
        }
    }
}