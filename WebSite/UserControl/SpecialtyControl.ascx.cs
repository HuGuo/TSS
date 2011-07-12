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

    private string specialtyId;
    public string SpecialtyId
    {
        get { return ddlSpecialty.SelectedValue; }
        set
        {
            ddlSpecialty.SelectedValue = value;
            specialtyId = value;
        }
    }

    private string specialtyName;
    public string SpecialtyName
    {
        get
        {
            return ddlSpecialty.SelectedItem.Text;
        }
        set
        {
            ddlSpecialty.SelectedItem.Text = value;
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