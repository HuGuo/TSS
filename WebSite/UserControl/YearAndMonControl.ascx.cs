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
            year = DateTime.Now.Year.ToString();
        if (string.IsNullOrEmpty(mon))
            mon = DateTime.Now.Month.ToString();
        ddlYear.SelectedValue = year;
        ddlMon.SelectedValue = mon;
    }

    private string year;
    public string Year
    {
        get { return ddlYear.SelectedValue; }
        set
        {
            if (string.IsNullOrEmpty(value))
                year = DateTime.Now.Year.ToString();
            else
                year = value;
        }
    }

    private string mon;
    public string Mon
    {
        get { return ddlMon.SelectedValue; }
        set
        {
            if (string.IsNullOrEmpty(value))
                mon = DateTime.Now.Month.ToString();
            else
                mon = value;
        }
    }
}