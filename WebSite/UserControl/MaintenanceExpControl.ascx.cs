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

    public string ActualTime
    {
        get { return tbActualTime.Text; }
        set { tbActualTime.Text = value; }
    }

    public string Experiment
    {
        get { return tbExpectantTime.Text; }
        set { tbExpectantTime.Text = value; }
    }

    public MaintenanceExperiment MaintenanceExperiment
    {
        get
        {
            MaintenanceExperiment maintenanceExperiment = new MaintenanceExperiment();
            if (ViewState["maintenanceExperiment"] != null)
                maintenanceExperiment = (MaintenanceExperiment)ViewState["maintenanceExperiment"];
            maintenanceExperiment.ActualTime = DateTime.Parse(tbActualTime.Text);
            maintenanceExperiment.ExpectantTime = DateTime.Parse(tbExpectantTime.Text);
            maintenanceExperiment.MaintenanceCycleId = int.Parse(hdMaintenanceCycleId.Value);
            maintenanceExperiment.ExperimentId = new Guid();//绑定试验报告，绑定未确定
            return maintenanceExperiment;
        }
        set
        {
            tbActualTime.Text = value.ActualTime.ToString("yyyy-MM-dd");
            hdMaintenanceCycleId.Value = value.MaintenanceCycleId.ToString();
            tbExpectantTime.Text = value.ExpectantTime.HasValue ? value.ExpectantTime.Value.ToString("yyyy-MM-dd") : "";
            ddlExperiment.SelectedValue = value.ExperimentId.ToString();//绑定试验报告，绑定未确定
            ViewState["maintenanceExperiment"] = value;
        }
    }

    public void ReSet()
    {
        tbActualTime.Text = "";
        tbExpectantTime.Text = "";
    }

    public string MaintenanceCycleId
    {
        get { return hdMaintenanceCycleId.Value; }
        set { hdMaintenanceCycleId.Value = value; }
    }

    protected void tbActualTime_TextChanged(object sender, EventArgs e)
    {
        MaintenanceCycleRepository repository = new MaintenanceCycleRepository();
        MaintenanceCycle maintenaceCycle = repository.Get(int.Parse(hdMaintenanceCycleId.Value));
        tbExpectantTime.Text = DateTime.Parse(tbActualTime.Text)
            .AddYears(int.Parse(maintenaceCycle.Cycle))
            .Date.ToString("yyyy-MM-dd");
    }
}