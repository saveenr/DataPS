using System;
using SMA=System.Management.Automation;

namespace DataPS
{
    public class DataTableWriter
    {
        public void WriteTableElement(System.Data.DataTable dt , System.Xml.XmlWriter w, TableSettings _tableSettings)
        {
            w.WriteStartElement("table");
            w.WriteStartElement("tr");

            int i = 0;
            foreach (System.Data.DataColumn col in dt.Columns)
            {
                w.WriteStartElement("th");
                if (i < _tableSettings.Columns.Count)
                {
                    var tcol = _tableSettings.Columns[i];
                    if (tcol.Width > 0)
                    {
                        w.WriteAttributeString("width", _tableSettings.Columns[i].Width.ToString());                        
                    }
                }

                string cn = col.ColumnName;
                if (_tableSettings.UpperCaseColumns)
                {
                    cn = cn.ToUpper();
                }
                w.WriteString(cn);
                w.WriteEndElement(); //th

                i++;
            }
            w.WriteEndElement(); //tr

            foreach (System.Data.DataRow row in dt.Rows)
            {
                w.WriteStartElement("tr");
                var items = row.ItemArray;
                foreach (var item in items)
                {
                    w.WriteStartElement("td");
                    if (item.GetType() == typeof (Link))
                    {
                        var link = (Link) item;
                        link.Write(w);
                    }
                    else
                    {
                        w.WriteString(item.ToString());
                        
                    }
                    w.WriteEndElement(); //td                    
                }
                w.WriteEndElement(); //tr
            }

            w.WriteEndElement(); //table

            //WriteEndDocument(w);
            //w.Close();
        }

        private static void WriteEndDocument(System.Xml.XmlWriter w)
        {
            w.WriteEndElement(); // </body>
            w.WriteEndElement(); // </html>
            w.WriteEndDocument();
        }

        private static void WriteStartDocument(System.Xml.XmlWriter w, string style)
        {
            w.WriteStartDocument();
            w.WriteDocType("html", null, null, null);
            w.WriteStartElement("html");
            w.WriteStartElement("head");
            w.WriteStartElement("style");
            w.WriteAttributeString("type", "text/css");
            w.WriteString(style);
            w.WriteEndElement(); // </head>
            w.WriteStartElement("body");
            w.WriteStartElement("p");
        }
    }
}