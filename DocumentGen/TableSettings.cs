using System.Collections.Generic;

namespace DataPS
{
    public class TableSettings
    {
        public bool UpperCaseColumns;
        public List<TableColumn> Columns;

        public TableSettings()
        {
            this.Columns = new List<TableColumn>();
        }
    }

    public class TableColumn
    {
        public int Width = 0;
    }
}