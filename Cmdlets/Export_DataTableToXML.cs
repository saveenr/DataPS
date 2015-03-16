using System.Data;
using SMA = System.Management.Automation;

namespace DataPS
{
    [SMA.Cmdlet("Export", "DataTableToXML")]
    public class Export_DataTableToXML : SMA.Cmdlet
    {
        [SMA.Parameter(Mandatory = true, Position = 0)] public System.Data.DataTable DataTable;
        [SMA.Parameter(Mandatory = true, Position = 1)] public string Filename;
        
        protected override void ProcessRecord()
        {
            this.DataTable.WriteXml(this.Filename, XmlWriteMode.WriteSchema);
        }
    }
}