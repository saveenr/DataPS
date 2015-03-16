using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DataPS
{

    internal static class DataUtil
    {

        internal static ExcelXMLWriter.DataType getdt(System.Type t)
        {
            var cdt = ExcelXMLWriter.DataType.String;
            if ((t == typeof (int))
                || t == typeof (double)
                || t == typeof (short)
                || t == typeof (long)
                || t == typeof (float)
                || t == typeof (byte)
                || t == typeof (sbyte)
                || t == typeof (ushort)
                || t == typeof (ulong)
                || t == typeof (ulong)
                || t == typeof (decimal))
            {
                cdt = ExcelXMLWriter.DataType.Number;
            }
            else if (t == typeof (string))
            {
                cdt = ExcelXMLWriter.DataType.String;
            }
            else if (t == typeof (System.DateTime))
            {
                cdt = ExcelXMLWriter.DataType.DateTime;
            }
            else if (t == typeof (System.DateTimeOffset))
            {
                cdt = ExcelXMLWriter.DataType.DateTime;
            }
            else
            {
                cdt = ExcelXMLWriter.DataType.String;
            }

            return cdt;
        }


        internal static string DataTableToExcelXML_get_cellstr(object item, ExcelXMLWriter.DataType excel_dt,
                                                               System.Data.DataColumn datatable_col)
        {
            string cell_str;
            if (excel_dt == ExcelXMLWriter.DataType.Number)
            {
                cell_str = (item != null) ? item.ToString() : System.String.Empty;
            }
            else if (excel_dt == ExcelXMLWriter.DataType.String)
            {
                cell_str = (item != null) ? item.ToString() : System.String.Empty;
            }
            else if (excel_dt == ExcelXMLWriter.DataType.DateTime)
            {
                cell_str = DataTableToExcelXML_get_cellstr_datetime(item, datatable_col);
            }
            else
            {
                cell_str = (item != null) ? item.ToString() : System.String.Empty;
            }

            return cell_str;
        }

        internal static string DataTableToExcelXML_get_cellstr_datetime(object item, System.Data.DataColumn col)
        {
            string cell_str = null;
            string datetime_fmt = "yyyy-MM-ddTHH:mm:ss.fff";
            if (col.DataType == typeof (System.DateTime))
            {
                if (item == null)
                {
                    cell_str = System.String.Empty;
                }
                else if (item is System.DBNull)
                {
                    cell_str = System.String.Empty;
                }
                else
                {
                    var datetime = (System.DateTime) item;
                    cell_str = datetime.ToString(datetime_fmt);
                }
            }
            else if (col.DataType == typeof (System.DateTimeOffset))
            {
                if (item == null)
                {
                    cell_str = System.String.Empty;
                }
                else if (item is System.DBNull)
                {
                    cell_str = System.String.Empty;
                }
                else
                {
                    var datetime = (System.DateTimeOffset) item;
                    var invariant_culture = System.Globalization.CultureInfo.InvariantCulture;
                    cell_str = datetime.ToString(datetime_fmt, invariant_culture);
                }
            }

            return cell_str;
        }


        internal static string DataTableToExcelXML_get_cellstr(object item, ExcelXMLWriter.DataType excel_dt,
                                                               Schema.Field datatable_col)
        {
            string cell_str;
            if (excel_dt == ExcelXMLWriter.DataType.Number)
            {
                cell_str = (item != null) ? item.ToString() : System.String.Empty;
            }
            else if (excel_dt == ExcelXMLWriter.DataType.String)
            {
                cell_str = (item != null) ? item.ToString() : System.String.Empty;
            }
            else if (excel_dt == ExcelXMLWriter.DataType.DateTime)
            {
                cell_str = DataTableToExcelXML_get_cellstr_datetime(item, datatable_col);
            }
            else
            {
                cell_str = (item != null) ? item.ToString() : System.String.Empty;
            }

            return cell_str;
        }

        internal static string DataTableToExcelXML_get_cellstr_datetime(object item, Schema.Field col)
        {
            string cell_str = null;
            string datetime_fmt = "yyyy-MM-ddTHH:mm:ss.fff";
            if (col.Type == typeof (System.DateTime))
            {
                if (item == null)
                {
                    cell_str = System.String.Empty;
                }
                else if (item is System.DBNull)
                {
                    cell_str = System.String.Empty;
                }
                else
                {
                    var datetime = (System.DateTime) item;
                    cell_str = datetime.ToString(datetime_fmt);
                }
            }
            else if (col.Type == typeof (System.DateTimeOffset))
            {
                if (item == null)
                {
                    cell_str = System.String.Empty;
                }
                else if (item is System.DBNull)
                {
                    cell_str = System.String.Empty;
                }
                else
                {
                    var datetime = (System.DateTimeOffset) item;
                    var invariant_culture = System.Globalization.CultureInfo.InvariantCulture;
                    cell_str = datetime.ToString(datetime_fmt, invariant_culture);
                }
            }

            return cell_str;
        }

        public static string NormalizeColumnName(string s)
        {
            string t = s.Replace(' ', '_');
            t = t.Replace('/', '_');
            t = t.Replace('\\', '_');
            t = t.Replace('\t', '_');
            return t;
        }

        public static string Join_ColName_Picker(System.Data.DataTable table_a, System.Data.DataTable table_b, string s)
        {
            if (table_a.Columns.Contains(s))
            {
                string prefix = table_b.TableName ?? "2.";
                return prefix + s;
            }
            else
            {
                return s;
            }
        }

        public static DataTable Join(DataSet dataset, string join_table_name, DataTable table_a, DataTable table_b,
                                     DataColumn[] join_columns_a,
                                     DataColumn[] join_columns_b)
        {
            // Create empty result table
            var result_table = new DataTable(join_table_name);


            // Create columns for the result table
            foreach (var col in table_a.Columns.AsEnumerable())
            {
                string colname = col.ColumnName;
                result_table.Columns.Add(colname, col.DataType);
            }

            foreach (var col in table_b.Columns.AsEnumerable())
            {
                string colname = Join_ColName_Picker(table_a, table_b, col.ColumnName);
                result_table.Columns.Add(colname, col.DataType);
            }

            //Create the Data Relation
            var datarelation = new DataRelation(string.Empty, join_columns_a, join_columns_b, false);
            dataset.Relations.Add(datarelation);

            // Load the data into the result table
            result_table.BeginLoadData();

            var grouped_arrays =
                from row_a in table_a.AsEnumerable()
                let rows_b = row_a.GetChildRows(datarelation)
                where (rows_b != null) && (rows_b.Length > 0)
                select new { row_a, rows_b };

            var merged_arrays =
                from item in grouped_arrays
                from row_b in item.rows_b
                select JoinArrays(item.row_a.ItemArray, row_b.ItemArray);

            foreach (var item in merged_arrays)
            {
                result_table.LoadDataRow(item, true);
            }

            result_table.EndLoadData();


            return result_table;
        }

        public static DataTable Join(DataTable table_a, DataTable table_b, DataColumn[] join_columns_a,
                                     DataColumn[] join_columns_b)
        {
            using (var ds = new DataSet())
            {
                var table_a_copy = table_a.Copy();
                var table_b_copy = table_b.Copy();

                ds.Tables.Add(table_a_copy);
                ds.Tables.Add(table_b_copy);

                var columns_a = join_columns_a.Select(col => table_a_copy.Columns[col.ColumnName]).ToArray();
                var columns_b = join_columns_b.Select(col => table_b_copy.Columns[col.ColumnName]).ToArray();


                var result_table = Join(ds, "Join", table_a_copy, table_b_copy, columns_a, columns_b);
                return result_table;
            }
        }

        public static T[] JoinArrays<T>(T[] array_a, T[] array_b)
        {
            int new_array_size = array_a.Length + array_b.Length;
            var new_array = new T[new_array_size];

            System.Array.Copy(array_a, 0, new_array, 0, array_a.Length);
            System.Array.Copy(array_b, 0, new_array, array_a.Length, array_b.Length);

            return new_array;
        }


        public static DataTable Join(DataTable table_a, DataTable table_b, DataColumn join_colum_a,
                                     DataColumn join_colum_b)
        {
            return Join(
                table_a,
                table_b,
                new DataColumn[] { join_colum_a },
                new DataColumn[] { join_colum_b });
        }

        public static DataTable Join(DataTable table_a, DataTable table_b, string join_colum_a, string join_colum_b)
        {
            return Join(
                table_a,
                table_b,
                new DataColumn[] { table_a.Columns[join_colum_a] },
                new DataColumn[] { table_b.Columns[join_colum_b] });
        }
    }
}