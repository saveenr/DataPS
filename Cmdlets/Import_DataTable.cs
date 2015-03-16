using SMA = System.Management.Automation;

namespace DataPS
{
    [SMA.Cmdlet("Import", "DataTable")]
    public class Import_DataTable : SMA.Cmdlet
    {
        [SMA.Parameter(Mandatory = true, Position =0)]
        public string Filename;

        protected override void ProcessRecord()
        {
            var ext = System.IO.Path.GetExtension(this.Filename).ToLower();

            var dt = new System.Data.DataTable();
            dt.ReadXml(this.Filename);
            this.WriteObject(dt);                
        }
    }
}
