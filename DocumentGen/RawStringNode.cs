namespace DataPS
{
    public class RawStringNode : DocNode
    {
        private string s;

        public RawStringNode(string s)
        {
            this.s = s;
        }

        public override void WriteXml(Context ctx)
        {
            ctx.w.WriteRaw(this.s);
        }
    }
}