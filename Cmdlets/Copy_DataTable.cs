using SMA = System.Management.Automation;

namespace DataPS
{
    [SMA.Cmdlet("Copy", "DataTable")]
    public class Copy_DataTable : SMA.Cmdlet
    {
        [SMA.Parameter(Mandatory = true, Position = 0)]
        public System.Data.DataTable DataTable;

        [SMA.Parameter(Mandatory = false)]
        public string Filter;

        [SMA.Parameter(Mandatory = false)]
        public string Sort;

        protected override void ProcessRecord()
        {
            if (Filter != null || this.Sort !=null)
            {
                this.WriteVerbose("Using Select");
                var dest_dt = this.DataTable.Clone();
                this.WriteVerbose("Input Rows "+this.DataTable.Rows.Count);
                var rows = this.DataTable.Select(this.Filter,this.Sort);
                this.WriteVerbose("Output Rows " + rows.Length);
                dest_dt.BeginLoadData();
                foreach (System.Data.DataRow row in rows)
                {
                    dest_dt.Rows.Add(row.ItemArray);
                }
                dest_dt.EndLoadData();
                this.WriteObject(dest_dt);
            }
            else
            {
                this.WriteVerbose("Using Copy");
                var dest_dt = this.DataTable.Copy();
                this.WriteObject(dest_dt);
            }
        }

    }
}
