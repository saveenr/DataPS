using DataPS.DGML.Extensions;

namespace DataPS.DGML
{
    public class DGMLWriter
    {
        private System.Xml.XmlWriter _xmlwriter;

        private const string DGMLNamespace = "http://schemas.microsoft.com/vs/2009/dgml";

        public DGMLWriter(System.Xml.XmlWriter xmlwriter)
        {
            if (xmlwriter == null)
            {
                throw new System.ArgumentNullException("xmlwriter");
            }

            this._xmlwriter = xmlwriter;
        }

        public DGMLWriter(string filename)
        {
            if (filename == null)
            {
                throw new System.ArgumentNullException("filename");
            }

            var settings = new System.Xml.XmlWriterSettings();
            settings.Indent = true;
            this._xmlwriter = System.Xml.XmlWriter.Create(filename, settings);
        }

        public void Flush()
        {
            this._xmlwriter.Flush();
        }

        public void StartDocument()
        {
            this._xmlwriter.WriteStartDocument();
        }

        public void EndDocument()
        {
            this._xmlwriter.WriteEndDocument();
        }

        public void StartDirectedGraph()
        {
            this.StartDirectedGraph(null);
        }

        public void StartDirectedGraph(DGML.GraphAttributes attributes)
        {
            this._xmlwriter.WriteStartElement("DirectedGraph", DGMLNamespace);
            if (attributes != null)
            {
                attributes.Write(this._xmlwriter);
            }
        }

        public void EndDirectedGraph()
        {
            this._xmlwriter.WriteEndElement();
        }

        public void Close()
        {
            this._xmlwriter.Close();
        }

        public void StartNodes()
        {
            this._xmlwriter.WriteStartElement("Nodes");
        }

        public void EndNodes()
        {
            this._xmlwriter.WriteEndElement();
        }

        public void StartLinks()
        {
            this._xmlwriter.WriteStartElement("Links");
        }

        public void EndLinks()
        {
            this._xmlwriter.WriteEndElement();
        }

        public void StartCategories()
        {
            this._xmlwriter.WriteStartElement("Categories");
        }

        public void EndCategories()
        {
            this._xmlwriter.WriteEndElement();
        }

        public void StartProperties()
        {
            this._xmlwriter.WriteStartElement("Properties");
        }

        public void EndProperties()
        {
            this._xmlwriter.WriteEndElement();
        }

        public void Node(string id,string label)
        {
            this.Node(id, label, null);
        }

        public void Node(string id, string label, DGML.NodeAttributes attributes)
        {
            this._xmlwriter.WriteStartElement("Node");
            this._xmlwriter.WriteAttributeString("Id", id);

            if (label != null)
            {
                this._xmlwriter.WriteAttributeString("Label", label);                
            }
            if (attributes != null)
            {
                attributes.Write(this._xmlwriter);
            }

            this._xmlwriter.WriteEndElement();
        }

        public void Link(string source, string target)
        {
            this.Link(source, target, null);
        }

        public void Link(string source, string target, DGML.LinkAttributes attributes)
        {
            this._xmlwriter.WriteStartElement("Link");
            this._xmlwriter.WriteAttributeString("Source", source);
            this._xmlwriter.WriteAttributeString("Target", target);
            if (attributes != null)
            {
                attributes.Write(this._xmlwriter);
            }

            this._xmlwriter.WriteEndElement();
        }

        public void Category(Category cat)
        {
            this._xmlwriter.WriteStartElement("Category");
            this._xmlwriter.WriteAttributeString("Id", cat.ID);
            this._xmlwriter.WriteAttributeString("IsContainment", cat.IsContainment ? "True" : "False");
            this._xmlwriter.WriteEndElement();
        }

        public void Property(string id, string datatype, string label, string description, bool? isreference)
        {
            this._xmlwriter.WriteStartElement("Property");
            this._xmlwriter.WriteAttributeString("Id", id);
            this._xmlwriter.WriteAttributeString("DataType", datatype);
            this._xmlwriter.WriteAttributeStringIfNotNull("Label", label);
            this._xmlwriter.WriteAttributeStringIfNotNull("Description", description);
            this._xmlwriter.WriteAttributeStringIfHasValue("IsReference", isreference,
                                                           v =>
                                                           System.Globalization.CultureInfo.InvariantCulture.TextInfo.
                                                               ToTitleCase(v.ToString()));
            this._xmlwriter.WriteEndElement();
        }

        public void StartStyles()
        {
            this._xmlwriter.WriteStartElement("Styles");
        }

        public void EndStyles()
        {
            this._xmlwriter.WriteEndElement();
        }

        public void StartStyle(string targettype, string grouplabel, string valuelabel)
        {
            this._xmlwriter.WriteStartElement("Style");
            this._xmlwriter.WriteAttributeString("TargetType", targettype);
            this._xmlwriter.WriteAttributeString("GroupLabel", grouplabel);
            this._xmlwriter.WriteAttributeString("ValueLabel", valuelabel);
        }

        public void StyleCondition(string expression)
        {
            this._xmlwriter.WriteStartElement("Condition");
            this._xmlwriter.WriteAttributeString("Expression", expression);
            this._xmlwriter.WriteEndElement();
        }

        public void StyleSetter(string property, string expression, string value)
        {
            this._xmlwriter.WriteStartElement("Condition");
            this._xmlwriter.WriteAttributeString("Property", property);
            this._xmlwriter.WriteAttributeStringIfNotNull("Expression", expression);
            this._xmlwriter.WriteAttributeStringIfNotNull("Value", value);
            this._xmlwriter.WriteEndElement();
        }

        public void EndStyle()
        {
            this._xmlwriter.WriteEndElement();
        }
    }
}