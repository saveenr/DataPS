using DataPS.DGML.Extensions;

namespace DataPS.DGML
{
    public class CategoryAttributes
    {
        public string Label { get; set; }
        public string BasedOn { get; set; }
        public string Background { get; set; }
        public string Stroke { get; set; }

        public void Write(System.Xml.XmlWriter xw)
        {
            xw.WriteAttributeStringIfNotNull("Label", this.Label);
            xw.WriteAttributeStringIfNotNull("BasedOn", this.BasedOn);
            xw.WriteAttributeStringIfNotNull("Background", this.Background);
            xw.WriteAttributeStringIfNotNull("Stroke", this.Stroke);
        }
    }
}