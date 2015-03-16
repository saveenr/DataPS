using SMA = System.Management.Automation;

namespace DataPS
{
    [SMA.Cmdlet("Import", "DataTableFromDelimitedText")]
    public class Import_DataTableFromDelimitedText : SMA.Cmdlet
    {
        [SMA.Parameter(Mandatory = true, Position = 0)]
        public string Filename;

        [SMA.Parameter(Mandatory = false)]
        public SMA.SwitchParameter HeadersInFirstRow;

        [SMA.Parameter(Mandatory = false)]
        public string Delimiter;

        protected override void ProcessRecord()
        {
            var ext = System.IO.Path.GetExtension(this.Filename).ToLower();

            var dt = new System.Data.DataTable();

            var reader = new CsvHelper.CsvReader(new System.IO.StreamReader(this.Filename));

            var csvcon = reader.Configuration;
            csvcon.HasHeaderRecord = this.HeadersInFirstRow;
            if (this.Delimiter != null)
            {
                csvcon.Delimiter = this.Delimiter;

                this.WriteVerbose(string.Format("Delimiter: \"{0}\" ", this.Delimiter));
            }

            int count = 0;

            object[] arr = null;

            while (reader.Read())
            {
                string[] currec = reader.CurrentRecord;
                if (arr == null)
                {
                    arr = new object[currec.Length];
                }

                if (count == 0)
                {

                    if (this.HeadersInFirstRow)
                    {
                        foreach (var item in reader.FieldHeaders)
                        {
                            this.WriteVerbose("HEADER " + item);
                            dt.Columns.Add(item, typeof(string));
                        }
                    }
                    else
                    {
                        foreach (var item in currec)
                        {
                            dt.Columns.Add();
                        }
                    }

                }
                else
                {
                    for (int i = 0; i < arr.Length; i++)
                    {
                        arr[i] = currec[i];
                    }
                    dt.Rows.Add(arr);
                }
                count++;
            }

            this.WriteObject(dt);
        }
    }

}
