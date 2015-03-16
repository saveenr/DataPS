using System.Collections.Generic;

namespace DataPS
{
    public class TableNode : DocNode
    {
        private System.Data.DataTable datatable;
        public TableSettings TableSettings;

        public TableNode(System.Data.DataTable datatable)
        {
            this.datatable= datatable;
            this.TableSettings = new TableSettings();
        }

        public override void WriteXml(Context ctx)
        {
            var tw = new DataTableWriter();
            tw.WriteTableElement(this.datatable,ctx.w,this.TableSettings);
        }
    }
}