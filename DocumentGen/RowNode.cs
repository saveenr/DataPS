using System.Collections.Generic;

namespace DataPS
{
    public class RowNode : DocNode
    {
        public List<DocNode> Cells;

        public RowNode()
        {
            this.Cells = new List<DocNode>();
        }

        public override void WriteXml(Context ctx)
        {
            ctx.w.WriteStartElement("table");

            ctx.w.WriteStartElement("tr");
            foreach (var cells in this.Cells)
            {
                ctx.w.WriteStartElement("td");
                cells.WriteXml(ctx);
                ctx.w.WriteEndElement(); //td
            }
            ctx.w.WriteEndElement(); //tr
            ctx.w.WriteEndElement(); //table
        }

        public void Add( DocNode node)
        {
            this.Cells.Add(node);
        }
    }
}