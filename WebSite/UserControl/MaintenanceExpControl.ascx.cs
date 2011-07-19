using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using TSS.BLL;
using TSS.Models;

public partial class UserControl_MaintenanceExpControl : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public string LatsTime
    {
        get { return tbLastTime.Text; }
        set { tbLastTime.Text = value; }
    }

    public string NextTime
    {
        get { return tbNextTime.Text; }
        set { tbNextTime.Text = value; }
    }



    public MaintenanceExperiment MaintenanceExperiment
    {
        get
        {
            MaintenanceExperiment maintenanceExperiment = new MaintenanceExperiment();
            if (ViewState["maintenanceExperiment"] != null)
                maintenanceExperiment = (MaintenanceExperiment)ViewState["maintenanceExperiment"];
            else
                using (var repository = RepositoryFactory<MaintenanceCycleRepository>.Get())
                    maintenanceExperiment.CurrentCycle = repository.Get(int.Parse(hdMaintenanceCycleId.Value)).Cycle;
            maintenanceExperiment.LastExpTime = string.IsNullOrEmpty(tbLastTime.Text) ?
                (DateTime?)null : DateTime.Parse(tbLastTime.Text);
            maintenanceExperiment.NextExpTime = DateTime.Parse(tbNextTime.Text);
            maintenanceExperiment.MaintenanceCycleId = int.Parse(hdMaintenanceCycleId.Value);
            maintenanceExperiment.ExperimentId = new Guid();//绑定试验报告，绑定未确定
            return maintenanceExperiment;
        }
        set
        {
            tbNextTime.Text = value.NextExpTime.Value.ToString("yyyy-MM-dd");
            hdMaintenanceCycleId.Value = value.MaintenanceCycleId.ToString();
            tbLastTime.Text = value.LastExpTime.HasValue ? value.LastExpTime.Value.ToString("yyyy-MM-dd") : "";
            ddlExperiment.SelectedValue = value.ExperimentId.ToString();//绑定试验报告，绑定未确定
            ViewState["maintenanceExperiment"] = value;
        }
    }

    public void ReSet()
    {
        tbLastTime.Text = "";
        tbNextTime.Text = "";
    }

    public string MaintenanceCycleId
    {
        get { return hdMaintenanceCycleId.Value; }
        set { hdMaintenanceCycleId.Value = value; }
    }

    protected void tbActualTime_TextChanged(object sender, EventArgs e)
    {
        using (var repository = RepositoryFactory<MaintenanceCycleRepository>.Get())
        {
            MaintenanceCycle maintenaceCycle = repository.Get(int.Parse(hdMaintenanceCycleId.Value));
            if (!string.IsNullOrEmpty(tbLastTime.Text))
                tbNextTime.Text = DateTime.Parse(tbLastTime.Text)
                    .AddMonths(ParseYearToMonth(maintenaceCycle.Cycle))
                    .Date.ToString("yyyy-MM-dd");
        }
    }

    public int ParseYearToMonth(string interval)
    {
        int monthCount = 0;
        string[] timeStr = interval.Split('.');
        monthCount = int.Parse(timeStr[0]) * 12;
        if (timeStr.Length > 1)
            monthCount += int.Parse(timeStr[1]);
        return monthCount;
    }
}