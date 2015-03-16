namespace DataPS
{
    public class Link
    {
        public string Text;
        public string URL;

        public Link(string text, string URL)
        {
            this.Text = text;
            this.URL = URL;
        }

        public void Write(System.Xml.XmlWriter w)
        {
            w.WriteStartElement("a");
            w.WriteAttributeString("href",this.URL);
            w.WriteString(this.Text);
            w.WriteEndElement();
        }

        public override string ToString()
        {
            string msg = string.Format("{0}[{1}]", this.Text, this.URL);
            return msg;
        }

    }
}