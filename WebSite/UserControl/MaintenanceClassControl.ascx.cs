using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using TSS.BLL;
using TSS.Models;

public partial class UserControl_MaintenanceClassControl : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    private string specialtyId;
    public string SpecialtyId
    {
        get { return this.SpecialtyControl1.SpecialtyId; }
        set { this.SpecialtyControl1.SpecialtyId = value; }
    }

    public string EquipmentClassName
    {
        get { return this.tbClassNames.Text; }
        set { this.tbClassNames.Text = value; }
    }


    public MaintenanceClass MaintenanceClass
    {
        get
        {
            MaintenanceClass maintenanceClass = new MaintenanceClass();
            if (ViewState["maintenanceClass"] != null)
                maintenanceClass = (MaintenanceClass)ViewState["maintenanceClass"];
            maintenanceClass.equipmentClassName = tbClassNames.Text;
            maintenanceClass.SpecialtyId = this.SpecialtyControl1.SpecialtyId;
            return maintenanceClass;
        }
        set
        {
            this.tbClassNames.Text = value.equipmentClassName;
            specialtyId = value.SpecialtyId;
            this.SpecialtyControl1.SpecialtyId = value.SpecialtyId;
            ViewState["maintenanceClass"] = value;
        }
    }

    public void ReSet()
    {
        tbClassNames.Text = "";
    }
}