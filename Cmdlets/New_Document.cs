using SMA=System.Management.Automation;
using MSCHART = System.Windows.Forms.DataVisualization.Charting;

namespace DataPS
{
    [SMA.Cmdlet("New", "Document")]
    public class NewDocument: SMA.PSCmdlet
    {

        protected override void ProcessRecord()
        {
            var db = new DocumentBuilder();
            this.WriteObject(db);
        }
    }
}
