namespace DataPS
{
    class ChartNode : DocNode
    {
        private int chart_id;
        private MSChartStylesheet.ChartBuilder chart;

        public ChartNode(MSChartStylesheet.ChartBuilder chart,int id)
        {
            this.chart = chart;
            this.chart_id = id;
        }

        public override void WriteXml(Context ctx)
        {
            if (!System.IO.Directory.Exists(ctx.files_folder))
            {
                System.IO.Directory.CreateDirectory(ctx.files_folder);
            }
            string chart_filename = System.IO.Path.Combine(ctx.files_folder,"chart" + this.chart_id.ToString() + ".png");
            ctx.w.WriteStartElement("p");
            ctx.w.WriteStartElement("img");
            ctx.w.WriteAttributeString("src", chart_filename);
            ctx.w.WriteEndElement();
            ctx.w.WriteEndElement();

            this.chart.Save(chart_filename);
        }
    }
}