using SMA=System.Management.Automation;
using MSCHART = System.Windows.Forms.DataVisualization.Charting;

namespace DataPS.Cmdlets
{
    [SMA.Cmdlet("New", "ChartStylesheet")]
    public class NewChartStylesheet : SMA.PSCmdlet
    {
        protected override void ProcessRecord()
        {
            var stylesheet = new MSChartStylesheet.Stylesheet();
            stylesheet.RadialLabelStyle = MSChartStylesheet.RadialLabelStyle.Outside;
            this.WriteObject(stylesheet);
        }
    }
}
