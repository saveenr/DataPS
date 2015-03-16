using SMA=System.Management.Automation;
using MSCHART = System.Windows.Forms.DataVisualization.Charting;

namespace DataPS.Cmdlets
{
    [SMA.Cmdlet("New", "Chart")]
    public class NewChart: SMA.PSCmdlet
    {
        [SMA.Parameter(Mandatory = true)]
        public System.Data.DataTable DataTable { get; set; }

        [SMA.Parameter(Mandatory = false)]
        public int? Width { get; set; }

        [SMA.Parameter(Mandatory = false)]
        public int? Height { get; set; }

        [SMA.Parameter(Mandatory = false)]
        public MSChartStylesheet.ChartType? ChartType { get; set; }

        [SMA.Parameter(Mandatory = false)]
        public string Name { get; set; }

        [SMA.Parameter(Mandatory = false)]
        public MSChartStylesheet.Stylesheet StyleSheet { get; set; }

        [SMA.Parameter(Mandatory = true)]
        public string XColumn { get; set; }

        [SMA.Parameter(Mandatory = true)]
        public string YColumn { get; set; }

        [SMA.Parameter(Mandatory = false)]
        public string SeriesColumn { get; set; }

        protected override void ProcessRecord()
        {
            var builder = new MSChartStylesheet.ChartBuilder();
            builder.DataTable = this.DataTable;
            builder.Width = this.Width;
            builder.Height = this.Height;
            builder.ChartType = this.ChartType;
            builder.Name = this.Name;
            builder.StyleSheet = this.StyleSheet;
            builder.XColumn = this.XColumn;
            builder.YColumn = this.YColumn;
            builder.SeriesColumn = this.SeriesColumn;

            this.WriteObject(builder);
        }
    }
}
