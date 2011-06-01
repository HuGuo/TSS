using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SystemManagement_Equipment_Category_Add : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) {
            XmlDataSource1.Data = (new TSS.BLL.EquipmentCategories()).GetXml().ToString();
        }

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        var categories = new TSS.BLL.EquipmentCategories();
        categories.Add(new TSS.Models.EquipmentCategory {
            
        });
    }
}