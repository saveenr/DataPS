using SMA = System.Management.Automation;

namespace DataPS
{
    [System.Management.Automation.Cmdlet("Join", "DataTable")]
    public class Join_DataTable : System.Management.Automation.Cmdlet
    {
        [System.Management.Automation.Parameter(Position = 1, Mandatory = true)]
        public System.Data.DataTable Table1 = null;

        [System.Management.Automation.Parameter(Position = 2, Mandatory = true)]
        public System.Data.DataTable Table2 = null;

        [System.Management.Automation.Parameter(Position = 3, Mandatory = true)]
        public string Field1 = null;

        [System.Management.Automation.Parameter(Position = 4, Mandatory = true)]
        public string Field2 = null;

        [System.Management.Automation.Parameter(Mandatory = false)]
        [System.Management.Automation.ValidateNotNullOrEmpty]
        public string ResultTableName = null;

        protected override void ProcessRecord()
        {
            var new_table = DataUtil.Join(Table1, Table2, Field1, Field2);
            new_table.TableName = this.ResultTableName;

            this.WriteObject(new_table);
        }
    }
}
