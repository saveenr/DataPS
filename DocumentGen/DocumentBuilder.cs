using MSChartStylesheet;
using MSCHART = System.Windows.Forms.DataVisualization.Charting;
using System.Collections.Generic;

namespace DataPS
{
    public class DocumentBuilder
    {
        private List<DocNode> nodes;
        private int chartcount = 0;
        public string Style;

        public DocumentBuilder()
        {
            this.nodes = new List<DocNode>();
            this.Style = @"

body, tbody { 
    font-family: Segoe UI; 
    font-size: 10pt; 
}

tbody
{
    vertical-align:top;
}
th {text-align:left}
h1 { font-family: segoe ui light; font-size: 20pt; font-weight: normal;}
h2 { font-family: segoe ui light; font-size: 16pt; font-weight: normal;}
h3 { font-family: segoe ui light; font-size: 12pt; font-weight: normal;}
p,table {font-family: segoe ui;}
pre { font-family: consolas; }
";

        }

        public Link CreateLink(string test, string url)
        {
            return new Link(test,url);
        }

        public void Save(string filename)
        {
            var settings = new System.Xml.XmlWriterSettings();
            settings.OmitXmlDeclaration = true;
            settings.Indent = true;

            var w = System.Xml.XmlWriter.Create(filename,settings);

            var ctx = new Context();
            ctx.w = w;
            ctx.filename = filename;
            ctx.folder = System.IO.Path.GetDirectoryName(filename);
            ctx.files_folder = System.IO.Path.Combine(ctx.folder,
                                                System.IO.Path.GetFileNameWithoutExtension(filename) + "_files");
            w.WriteStartDocument();
            w.WriteDocType("html",null,null,null);
            w.WriteStartElement("html");
            w.WriteStartElement("head");
            w.WriteStartElement("style");
            w.WriteString(this.Style);
            w.WriteEndElement(); // style
            w.WriteEndElement(); // head
            w.WriteStartElement("body");
            foreach (var node in this.nodes)
            {
                node.WriteXml(ctx);
            }
            w.WriteEndElement(); // body
            w.WriteEndElement(); // html
            w.WriteEndDocument();
            w.Close();

        }

        public void Header(string title)
        {
            var h = new HeaderNode(title);
            this.nodes.Add(h);
        }

        public void Paragraph(string title)
        {
            var h = new ParagraphNode(title);
            this.nodes.Add(h);
        }

        public void Code(string title)
        {
            var h = new CodeNode(title);
            this.nodes.Add(h);
        }

        public void Chart(MSChartStylesheet.ChartBuilder cb)
        {
            var c = newchart(cb);
            this.nodes.Add(c);
            this.chartcount++;
        }

        private ChartNode newchart(ChartBuilder cb)
        {
            var c = new ChartNode(cb, this.chartcount);
            return c;
        }

        public TableNode Table(System.Data.DataTable tb)
        {
            var tablenode = new TableNode(tb);
            foreach (System.Data.DataColumn col in tb.Columns)
            {
                var tcol = new TableColumn();
                tablenode.TableSettings.Columns.Add(tcol);
            }
            this.nodes.Add(tablenode);
            return tablenode;
        }

        public void XML(System.Xml.Linq.XElement el)
        {
            var node = new XMLElementNode(el);
            this.nodes.Add(node);
        }

        public void RawString(string s)
        {
            var node = new RawStringNode(s);
            this.nodes.Add(node);
        }

        public void Show()
        {
            string filename = System.IO.Path.Combine( System.IO.Path.GetTempPath(), System.IO.Path.GetRandomFileName()) + ".htm";
            this.Save(filename);
            System.Diagnostics.Process.Start(filename);
        }        
    }
}
