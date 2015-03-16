using System.Management.Automation;

namespace DataPS
{
    [System.Management.Automation.Cmdlet("New", "DGMLDocument")]
    public class New_DGMLDocument : PSCmdlet
    {

        protected override void ProcessRecord()
        {
            var db = new DGML.DGMLBuilder();
            this.WriteObject(db);
        }
    }
}