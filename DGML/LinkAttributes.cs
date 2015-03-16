using DataPS.DGML.Extensions;

namespace DataPS.DGML
{
    public class LinkAttributes
    {
        public string Category { get; set; }
        public string Stroke { get; set; }
        public double? StrokeThickness { get; set; }
        public StrokeDashArray StrokeDashArray { get; set; }
        public string Background { get; set; }

        public void Write(System.Xml.XmlWriter xw)
        {
            xw.WriteAttributeStringIfNotNull("Category", this.Category);
            xw.WriteAttributeStringIfNotNull("Stroke", this.Stroke);
            xw.WriteAttributeStringIfHasValue("StrokeThickness", this.StrokeThickness);
            if (this.StrokeDashArray != null)
            {
                this.StrokeDashArray.Write(xw);                
            }
            xw.WriteAttributeStringIfNotNull("Background", this.Background);
        }
    }
}