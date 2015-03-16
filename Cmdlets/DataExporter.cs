using System.Collections.Generic;
using System.Linq;

namespace DataPS
{
    public static class DataExporter
    {
        public static void ToExcelXML(System.Data.DataSet dataset, string filename, bool write_column_headers)
        {
            var tables = dataset.Tables.AsEnumerable();
            ToExcelXML(tables, filename, write_column_headers);
        }

        public static void ToExcelXML(System.Data.DataTable datatable, string filename, string sheetname,
                                      bool write_column_headers)
        {
            var tables = new System.Data.DataTable[] {datatable};
            ToExcelXML(tables, filename, write_column_headers);
        }

        public static void ToExcelXML(IEnumerable<System.Data.DataTable> datatables, string filename,
                                      bool write_column_headers)
        {
            if (datatables == null)
            {
                throw new System.ArgumentNullException("datatables");
            }

            var datasources = datatables.Select(dt => (DataSource) new DataTableDataSource(dt.TableName, dt));
            ToExcelXML(datasources, filename, write_column_headers);
        }

        public static void ToExcelXML(IEnumerable<DataSource> datasources, string filename, bool write_column_headers)
        {
            if (datasources == null)
            {
                throw new System.ArgumentNullException("datasources");
            }

            var writer = new ExcelXMLWriter(filename);
            writer.StartDocument();
            writer.StartWorkBook();
            foreach (var datasource in datasources)
            {
                string sheetname = datasource.Name;

                if (string.IsNullOrEmpty(sheetname))
                {
                    string msg = string.Format("Datasource contains a null or blank Name for a worksheet");
                }
                write_worksheet(datasource, writer, sheetname, write_column_headers);
            }
            writer.EndWorkBook();
            writer.EndDocument();
            writer.Close();
        }

        private static void write_worksheet(DataSource datasource, ExcelXMLWriter writer, string sheetname,
                                            bool write_column_headers)
        {
            var schema = datasource.GetSchema();
            int num_cols = schema.Fields.Count;
            var datatypes = Enumerable.Repeat(ExcelXMLWriter.DataType.String, num_cols).ToArray();

            for (int i = 0; i < num_cols; i++)
            {
                var col = schema.Fields[i];
                if (col.Type == typeof (System.DateTime))
                {
                    datatypes[i] = ExcelXMLWriter.DataType.DateTime;
                }
            }

            writer.StartWorkSheet(sheetname, num_cols, datatypes);

            if (write_column_headers)
            {
                writer.StartRow();
                foreach (var col in schema.Fields)
                {
                    var cn = col.Name;
                    var cdt = ExcelXMLWriter.DataType.String;
                    writer.Cell(cn, cdt);
                }

                writer.EndRow();
            }

            var excel_datatypes = schema.Fields.AsEnumerable().Select(col => DataUtil.getdt(col.Type)).ToList();

            foreach (var row in datasource.Rows)
            {
                writer.StartRow();
                for (int i = 0; i < row.ItemArray.Length; i++)
                {
                    var datatable_col = schema.Fields[i];
                    ExcelXMLWriter.DataType excel_dt = excel_datatypes[i];
                    var item = row.ItemArray[i];

                    string cell_str = DataUtil.DataTableToExcelXML_get_cellstr(item, excel_dt, datatable_col);

                    writer.Cell(cell_str, excel_dt);
                }

                writer.EndRow();
            }

            writer.EndWorkSheet();
        }
    }
}