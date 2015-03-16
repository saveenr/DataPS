using System.Collections.Generic;
using SMA = System.Management.Automation;

namespace DataPS
{
    [SMA.Cmdlet("Export", "DataTableToFeed")]
    public class Export_DataTableToFeed : SMA.Cmdlet
    {

        [System.Management.Automation.Parameter(Position = 0,
            Mandatory = true,
            HelpMessage = "DataTable")]
        [System.Management.Automation.ValidateNotNull]
        public System.Data.DataTable DataTable;

        [System.Management.Automation.Parameter(Position = 1,
            Mandatory = true,
            HelpMessage = "OutputFilename")]
        [System.Management.Automation.ValidateNotNullOrEmpty]
        public string OutputFilename;

        [System.Management.Automation.Parameter(Mandatory = true,
            HelpMessage = "Namespace")]
        [System.Management.Automation.ValidateNotNullOrEmpty]
        public string Namespace;

        [System.Management.Automation.Parameter(Mandatory = true,
            HelpMessage = "NamespacePrefix")]
        [System.Management.Automation.ValidateNotNullOrEmpty]
        public string NamespacePrefix;

        [System.Management.Automation.Parameter(Mandatory = false,
            HelpMessage = "FeedTitle")]
        [System.Management.Automation.ValidateNotNullOrEmpty]
        public string FeedTitle = "Untitled";

        [System.Management.Automation.Parameter(Mandatory = false,
            HelpMessage = "FeedID")]
        [System.Management.Automation.ValidateNotNullOrEmpty]
        public string FeedID = "http://tempuri.org/feed";

        [System.Management.Automation.Parameter(Mandatory = false,
            HelpMessage = "FeedUpdateTime")]
        public System.DateTimeOffset FeedUpdateTime = System.DateTimeOffset.UtcNow;

        [System.Management.Automation.Parameter(Mandatory = false,
            HelpMessage = "EntryUpdateTime")]
        public System.DateTimeOffset EntryUpdateTime = System.DateTimeOffset.UtcNow;

        [System.Management.Automation.Parameter(Mandatory = false,
            HelpMessage = "LinkSelf")]
        [System.Management.Automation.ValidateNotNullOrEmpty]
        public string LinkSelf = "http://tempuri.org/feed";

        protected override void ProcessRecord()
        {
            string feedid = this.FeedID;
            string title = this.FeedTitle;
            string selflink = this.LinkSelf;

            var feed_updated = this.FeedUpdateTime;
            string fmt = "yyyy-MM-ddTHH:mm:ss.fffZ";
            string feed_updated_str = feed_updated.ToString(fmt);
            string entry_updated_str = feed_updated.ToString(fmt);
            string entry_author_name = "Unknown";
            string entry_content_type = "html";

            var encoding = System.Text.Encoding.UTF8;
            var xtw = new System.Xml.XmlTextWriter(this.OutputFilename, encoding);
            var xs = new System.Xml.XmlWriterSettings();
            xs.Indent = true;
            var x = System.Xml.XmlWriter.Create(xtw, xs);


            x.WriteStartDocument();
            x.WriteStartElement("feed");
            x.WriteAttributeString("xmlns", "http://www.w3.org/2005/Atom");
            x.WriteAttributeString("xmlns:" + this.NamespacePrefix, this.Namespace);

            x.WriteElementString("id", feedid);
            x.WriteElementString("updated", feed_updated_str);
            x.WriteElementString("title", title);

            x.WriteStartElement("link");
            x.WriteAttributeString("rel", "self");
            x.WriteAttributeString("type", "application/atom+xml");
            x.WriteAttributeString("href", selflink);
            x.WriteEndElement();//link

            var raw_colnames = new List<string>(this.DataTable.Columns.Count);
            foreach (System.Data.DataColumn col in this.DataTable.Columns)
            {
                raw_colnames.Add(col.ColumnName);
            }
            var fixed_colnames = new List<string>(this.DataTable.Columns.Count);
            foreach (string colname in this.DataTable.Columns)
            {
                string norm_col = colname.Replace(" ", "");
                fixed_colnames.Add(norm_col);
            }


            int entry_count = 0;

            foreach (System.Data.DataRow row in this.DataTable.Rows)
            {
                x.WriteStartElement("entry");
                x.WriteStartElement("author");
                x.WriteElementString("name", entry_author_name);
                x.WriteEndElement();//author
                x.WriteElementString("updated", entry_updated_str);

                string entry_id = feedid + "/" + entry_count.ToString();
                string entry_title = entry_id;
                if (string.IsNullOrEmpty(entry_title))
                {
                    entry_title = "Untitled";
                }
                x.WriteElementString("id", entry_id);
                x.WriteElementString("title", entry_title);

                x.WriteStartElement("content");
                x.WriteAttributeString("type", entry_content_type);
                x.WriteString(entry_title);
                x.WriteEndElement();//content

                foreach (int i in System.Linq.Enumerable.Range(0, fixed_colnames.Count))
                {
                    var cn = fixed_colnames[i];
                    object cvo = row.ItemArray[i];
                    string cv = (cvo == null) ? "" : cvo.ToString();
                    string elname = this.NamespacePrefix + ":" + cn;
                    x.WriteElementString(elname, cv);
                }

                x.WriteEndElement();//entry


                entry_count++;

            }

            x.WriteEndElement(); //feed
            x.WriteEndDocument();
            x.Flush();
            x.Close();
        }
    }
}