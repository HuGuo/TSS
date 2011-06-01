using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TSS.BLL;
using TSS.Models;

public partial class SystemManagement_Experiment_setTemplate : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) {
            IList<TSS.Models.Specialty> list = (new TSS.BLL.Specialties()).GetAll();
            foreach (TSS.Models.Specialty item in list) {
                ddlSpecialty.Items.Add(new ListItem(item.Name, item.Id));
            }

            string tid = Request.QueryString["tid"];
            if (!string.IsNullOrWhiteSpace(tid)) {
                ExpTemplateRepository repository = new ExpTemplateRepository();
                ExpTemplate template= repository.Get(new Guid(tid));
                if (null !=template) {
                    ltHTML.Text = template.HTML;
                    txt_tmpName.Value = template.Title;
                    ddlSpecialty.SelectedValue = template.SpecialtyId;
                }
            }
        }
    }
}