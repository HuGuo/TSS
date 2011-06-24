using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TSS.Models;
using TSS.BLL;
public partial class Experiment_Record : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) {
            string id = Request.QueryString["id"];
            ExpRecord obj = RepositoryFactory<ExpReocrdRepository>.Get().Get(new Guid(id));
            if (null !=obj) {
                using (System.IO.StreamReader reader=new System.IO.StreamReader(Server.MapPath(obj.Path))) {
                    ltHTML.Text = reader.ReadToEnd();
                }
                this.Title = obj.Name;
            }
        }
    }
}