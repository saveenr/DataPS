namespace DataPS
{
    public class XMLElementNode : DocNode
    {
        private System.Xml.Linq.XElement el;

        public XMLElementNode(System.Xml.Linq.XElement el)
        {
            this.el = el;
        }

        public override void WriteXml(Context ctx)
        {
            var tw = new DataTableWriter();
            el.WriteTo(ctx.w);
        }
    }
}