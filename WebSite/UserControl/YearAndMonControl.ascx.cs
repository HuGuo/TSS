using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserControl_YearAndMonControl : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            BindData();
    }

    public void BindData()
    {
        if (string.IsNullOrEmpty(year))
            ddlYear.SelectedValue = DateTime.Now.Year.ToString();
        else
            ddlYear.SelectedValue = year;
        if (string.IsNullOrEmpty(mon))
            ddlMon.SelectedValue = DateTime.Now.Month.ToString();
        else
            ddlMon.SelectedValue = mon;
    }

    private string year;
    public string Year
    {
        get { return ddlYear.SelectedValue; }
        set { year = value; }
    }

    private string mon;
    public string Mon
    {
        get { return ddlMon.SelectedValue; }
        set { mon = value; }
    }
}