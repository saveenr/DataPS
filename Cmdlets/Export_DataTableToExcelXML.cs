using SMA = System.Management.Automation;

namespace DataPS
{
    [SMA.Cmdlet("Export", "DataTableToExcelXML")]
    public class Export_DataTableToExcelXML : SMA.Cmdlet
    {
        [SMA.Parameter(Mandatory = true, Position = 0)] public System.Data.DataTable DataTable;

        [SMA.Parameter(Mandatory = true, Position = 1)] public string Filename;

        [SMA.Parameter(Mandatory = false)]
        public SMA.SwitchParameter NoHeaders;
        
        protected override void ProcessRecord()
        {
            DataExporter.ToExcelXML(new [] { this.DataTable}, this.Filename,this.NoHeaders);
        }
    }
}