using SMA = System.Management.Automation;

namespace DataPS
{
    // http://gallery.technet.microsoft.com/scriptcenter/4208a159-a52e-4b99-83d4-8048468d29dd

    [SMA.Cmdlet("Out", "DataTable")]
    public class Out_DataTable : SMA.PSCmdlet
    {
        [SMA.Parameter(Position = 0, Mandatory = true, ValueFromPipeline = true)] public SMA.PSObject[] InputObject;

        private System.Data.DataTable dt;
        bool first=true;

        protected override void BeginProcessing()
        {
            base.BeginProcessing();
            this.dt = new System.Data.DataTable();
            this.first = true;

        }
        protected override void ProcessRecord()
        {
            foreach (var o in this.InputObject)
            {

                // Create Schems if first row

                if (first)
                {
                    foreach (var prop in o.Properties)
                    {

                        this.WriteVerbose(string.Format("Adding Column {0}", prop.Name));
                        var col = new System.Data.DataColumn(prop.Name);
                        if (prop.Value != null)
                        {
                            var t = System.Type.GetType(prop.TypeNameOfValue);
                            this.WriteVerbose(string.Format("Setting DataType {0}", prop.TypeNameOfValue));
                            col.DataType = t;
                        }
                        dt.Columns.Add(col);

                        this.first = false;
                    }
                }

                // Put in the values
                var r = dt.NewRow();
                foreach (var prop in o.Properties)
                {
                    r[prop.Name] = prop.Value;
                }

                dt.Rows.Add(r);
            }
        }

        protected override void EndProcessing()
        {
            this.WriteObject(dt);
            base.EndProcessing();
        }

    }

}
