namespace DataPS
{
    class HeaderNode : DocNode
    {
        private string Text;

        public HeaderNode(string text)
        {
            this.Text = text;
        }

        public override void WriteXml(Context ctx)
        {
            ctx.w.WriteStartElement("h1");
            ctx.w.WriteString(this.Text);
            ctx.w.WriteEndElement();
        }
    }
}