namespace DataPS
{
    class CodeNode : DocNode
    {
        private string Text;

        public CodeNode(string text)
        {
            this.Text = text;
        }

        public override void WriteXml(Context ctx)
        {
            ctx.w.WriteStartElement("pre");
            ctx.w.WriteString(this.Text);
            ctx.w.WriteEndElement();
        }
    }
}