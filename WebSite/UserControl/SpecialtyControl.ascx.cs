using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using TSS.BLL;
using TSS.Models;

public partial class UserController_SpecialtyControl : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            BindData();
    }

    public string specialtyId;
    public string SpecialtyId
    {
        get
        {
            specialtyId = ddlSpecialty.SelectedValue;
            return specialtyId;
        }
        set
        {
            specialtyId = value;
        }
    }

    public string specialtyName;
    public string SpecialtyName
    {
        get
        {
            specialtyName = ddlSpecialty.SelectedItem.Text;
            return specialtyName;
        }
        set
        {
            specialtyName = value;
        }
    }



    protected void BindData()
    {
        foreach (Specialty specialty in new Specialties().GetAll())
        {
            ddlSpecialty.Items.Add(new ListItem(specialty.Name, specialty.Id));
        }
        ddlSpecialty.SelectedValue = specialtyId;
    }
}