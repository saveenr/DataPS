using DataPS.DGML.Extensions;

namespace DataPS.DGML
{
    public class GraphAttributes
    {
        public string Title { get; set; }
        public string Background { get; set; }
        public string BackgroundImage { get; set; }
        public string ButterflyMode { get; set; }
        public GraphDirectionType? GraphDirection { get; set; }
        public GraphLayoutType? Layout { get; set; }
        public int? NeighborhoodDistance { get; set; }
        public string ZoomLevel { get; set; }

        public void Write(System.Xml.XmlWriter xw)
        {
            xw.WriteAttributeStringIfNotNull("Title", this.Title);
            xw.WriteAttributeStringIfNotNull("Background", this.Background);
            xw.WriteAttributeStringIfNotNull("BackgroundImage", this.BackgroundImage);
            xw.WriteAttributeStringIfNotNull("ButterflyMode", this.ButterflyMode);
            xw.WriteAttributeStringIfHasValue("GraphDirection", this.GraphDirection);
            xw.WriteAttributeStringIfHasValue("Layout", this.Layout);
            xw.WriteAttributeStringIfHasValue("NeighborhoodDistance", this.NeighborhoodDistance);
            xw.WriteAttributeStringIfNotNull("ZoomLevel", this.ZoomLevel);
        }
    }
}