using System.Xml;
using SMA = System.Management.Automation;

namespace DataPS
{
    [SMA.Cmdlet("Export", "DataTableToHtml")]
    public class Export_DataTableToHtml : SMA.Cmdlet
    {
        [SMA.Parameter(Mandatory = true, Position =0)]
        public System.Data.DataTable DataTable;

        [SMA.Parameter(Mandatory = true, Position=1)]
        public string Filename;

        [SMA.Parameter(Mandatory = false)]
        public string Font="Segoe UI";

        [SMA.Parameter(Mandatory = false)]
        public string FontSize = "10pt";

        [SMA.Parameter(Mandatory = false)]
        public int[] ColumnWidths;

        [SMA.Parameter(Mandatory = false)]
        public SMA.SwitchParameter UpperCaseColumns;

        protected override void ProcessRecord()
        {
            string style = @"

body, tbody { 
    font-family: ##FONT##; 
    font-size: ##FONTSIZE##; 
}

tbody
{
    vertical-align:top;
}
th {text-align:left}

";

            style = style.Replace("##FONT##", this.Font);
            style = style.Replace("##FONTSIZE##", this.FontSize);

            var settings = new System.Xml.XmlWriterSettings();
            settings.OmitXmlDeclaration = true;
            settings.Indent = true;

            var w = System.Xml.XmlWriter.Create(this.Filename, settings);

            WriteStartDocument(w, style);

            var ts = new TableSettings();
            ts.UpperCaseColumns = this.UpperCaseColumns;

            var tablewriter = new DataTableWriter();
            tablewriter.WriteTableElement(this.DataTable, w, ts);
            
            WriteEndDocument(w);
            w.Close();
        }

        private static void WriteEndDocument(XmlWriter w)
        {
            w.WriteEndElement(); // </body>
            w.WriteEndElement(); // </html>
            w.WriteEndDocument();
        }

        private static void WriteStartDocument(XmlWriter w, string style)
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
