using SMA = System.Management.Automation;

namespace DataPS
{
    [SMA.Cmdlet("Export", "DataTableToDelimitedText")]
    public class Export_DataTableToCSV: SMA.Cmdlet
    {
        [SMA.Parameter(Mandatory = true, Position =0)]
        public System.Data.DataTable DataTable;

        [SMA.Parameter(Mandatory = true, Position=1)]
        public string Filename;

        [SMA.Parameter(Mandatory = false)]
        public SMA.SwitchParameter NoHeaders;

        [SMA.Parameter(Mandatory = false)]
        public string Delimiter = ",";

        [SMA.Parameter(Mandatory = false)]
        public string DelimiterReplacement = "_";

        [SMA.Parameter(Mandatory = false)]
        public SMA.SwitchParameter ErrorIfDelimiterFound;

        [SMA.Parameter(Mandatory = false)]
        public SMA.SwitchParameter WarnIfDelimiterFound;

        [SMA.Parameter(Mandatory = false)]
        public string NULLReplacement = "";

        [SMA.Parameter(Mandatory = false)]
        public SMA.SwitchParameter ErrorIfNullFound;

        [SMA.Parameter(Mandatory = false)]
        public SMA.SwitchParameter WarnIfNullFound;

        [SMA.Parameter(Mandatory = false)]
        public SMA.SwitchParameter UseISODateFormat;

        [SMA.Parameter(Mandatory = false)]
        public SMA.SwitchParameter BoolToInt;

        protected override void ProcessRecord()
        {
            var tw = System.IO.File.CreateText(this.Filename);
            tw.AutoFlush = true;

            using (var w = new CsvHelper.CsvWriter(tw))
            {
                if (!this.NoHeaders)
                {
                    foreach (System.Data.DataColumn col in this.DataTable.Columns)
                    {
                        w.WriteField(col.ColumnName);
                    }
                    w.NextRecord();
                }

                int rowcount = 0;
                foreach (System.Data.DataRow row in this.DataTable.Rows)
                {
                    for (int i = 0; i < row.ItemArray.Length; i++)
                    {
                        object o = row.ItemArray[i];
                        string s;

                        if (o == null || o is System.DBNull)
                        {
                            string msg = string.Format("NULL found in Row {0} Col = {1}", rowcount, i);
                            
                            if (this.WarnIfNullFound)
                            {
                                this.WriteWarning(msg);
                            }

                            if (this.ErrorIfNullFound)
                            {
                                throw new System.ArgumentException(msg);                                                                        
                            }

                            s = this.NULLReplacement;
                        }
                        else
                        {
                            s = this.object_to_string(row.ItemArray[i]);

                            if (s.Contains(this.Delimiter))
                            {
                                if (this.WarnIfDelimiterFound || this.ErrorIfDelimiterFound)
                                {
                                    string msg = string.Format("Delimiter {0} found in Row {1} Col = {2} Content ={3}",
                                                               this.Delimiter, rowcount, i, s);                                  
 
                                    if (this.WarnIfDelimiterFound)
                                    {
                                        this.WriteWarning(msg);
                                    }

                                    if (this.ErrorIfDelimiterFound)
                                    {
                                        throw new System.ArgumentException(msg);                                        
                                    }
                                    
                                }

                                bool ReplaceDelimiter = true;
                                if (ReplaceDelimiter)
                                {
                                    s = s.Replace(this.Delimiter, this.DelimiterReplacement);
                                }
                            }

                        }
                        w.WriteField(s);
                    }
                    w.NextRecord();
                    rowcount++;
                }                
            }
        }

        private string object_to_string(object o)
        {
            string s;
            if (this.UseISODateFormat &&  o is System.DateTime)
            {
                var dt = (System.DateTime) o;
                s = dt.ToString("s", System.Globalization.CultureInfo.InvariantCulture);
            }
            else if (this.BoolToInt && o is bool)
            {
                var b = (bool) o;
                s = b ? "1" : "0";
            }
            else
            {
                s = o.ToString();
            }
            return s;
        }
    }
}
