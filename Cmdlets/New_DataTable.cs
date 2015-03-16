using SMA = System.Management.Automation;

namespace DataPS
{
    [SMA.Cmdlet("New", "DataTable")]
    public class New_DataTable : SMA.Cmdlet
    {
        [SMA.Parameter(Mandatory = true, Position = 0)]
        public string[] ColumnNames;

        [SMA.Parameter(Mandatory = false)]
        public System.Type[] ColumnTypes;

        protected override void ProcessRecord()
        {
            var dt = new System.Data.DataTable();
            for (int i = 0; i < this.ColumnNames.Length; i++)
            {
                if ( this.ColumnTypes != null && (i < this.ColumnTypes.Length) )
                {
                    var col = dt.Columns.Add(this.ColumnNames[i], this.ColumnTypes[i]);                        
                }
                else
                {
                    var col = dt.Columns.Add(this.ColumnNames[i]);
                }                
            }
            this.WriteObject(dt);
        }
    }
}
