using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SystemManagement_Equipment_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            using (var context = new TSS.Models.Context())
            {
                GridView1.DataSource = (from p in context.Equipments
                                        select new { Name = p.Name, SpecialtyName = p.Specialty.Name }).ToList();
            }
        }
    }
}