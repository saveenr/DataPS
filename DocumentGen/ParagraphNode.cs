namespace DataPS
{
    class ParagraphNode : DocNode
    {
        private string Text;

        public ParagraphNode(string text)
        {
            this.Text = text;
        }

        public override void WriteXml(Context ctx)
        {
            ctx.w.WriteStartElement("p");
            ctx.w.WriteString(this.Text);
            ctx.w.WriteEndElement();
        }
    }
}