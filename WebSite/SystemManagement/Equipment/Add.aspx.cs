using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SystemManagement_Equipment_Add : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) {
            XmlDataSource1.Data = TSS.BLL.EquipmentCategories.GetXml().ToString();
        }
    }

    protected void addButton_Click(object sender, EventArgs e)
    {
        TSS.BLL.Equipments.Add(TextBox1.Text, TreeView1.SelectedValue, DropDownList1.SelectedValue);
    }
}