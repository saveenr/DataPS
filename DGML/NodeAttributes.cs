using DataPS.DGML.Extensions;

namespace DataPS.DGML
{
    public class NodeAttributes
    {
        public string Category { get; set; }
        public string Background { get; set; }
        public string FontFamily { get; set; }
        public GroupState? Group { get; set; }
        public string Reference { get; set; }
        public NodeVisibility? Visibility { get; set; }
        public string Icon { get; set; }
        public string Stroke { get; set; }
        public double? StrokeThickness { get; set; }
        public string Foreground { get; set; }
        public string FontSize { get; set; }
        public double? FontWeight { get; set; }
        public NodeStyle? Style { get; set; }
        public string Shape { get; set; }

        public void Write(System.Xml.XmlWriter xw)
        {
            xw.WriteAttributeStringIfNotNull("Category", this.Category);
            xw.WriteAttributeStringIfNotNull("Background", this.Background);
            xw.WriteAttributeStringIfNotNull("Foreground", this.Foreground);
            xw.WriteAttributeStringIfNotNull("FontFamily", this.FontFamily);
            xw.WriteAttributeStringIfNotNull("FontSize", this.FontSize);
            xw.WriteAttributeStringIfNotNull("FontWeight", this.FontWeight);
            xw.WriteAttributeStringIfHasValue("Group", this.Group);
            xw.WriteAttributeStringIfNotNull("Icon", this.Icon);
            xw.WriteAttributeStringIfNotNull("Reference", this.Reference);
            xw.WriteAttributeStringIfNotNull("Shape", this.Shape);
            xw.WriteAttributeStringIfNotNull("Stroke", this.Stroke);
            xw.WriteAttributeStringIfHasValue("StrokeThickness", this.StrokeThickness);
            xw.WriteAttributeStringIfHasValue("Style", this.Style);
            xw.WriteAttributeStringIfHasValue("Visibility", this.Visibility);
        }
    }
}