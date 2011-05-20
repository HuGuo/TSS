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
            XmlDataSource1.Data = TSS.BLL.EquipmentCategory.GetXml().ToString();
        }

    }

    protected void Button1_Click(object sender, EventArgs e)
    {

        TSS.BLL.EquipmentCategory.Add(TextBox1.Text, TreeView1.SelectedValue);
    }
}