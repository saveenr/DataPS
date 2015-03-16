using System.Collections.Generic;
using System.Linq;

namespace DataPS.DGML
{
    public class StrokeDashArray
    {
        private readonly List<double> _items;

        public StrokeDashArray(params double[] items)
        {
            this._items = new List<double>(items);
        }

        public StrokeDashArray()
        {
            this._items = null;
        }

        public List<double> Items
        {
            get { return _items; }
        }

        private string double_to_str(double value)
        {
            return value.ToString(System.Globalization.CultureInfo.InvariantCulture);
        }

        public void Write(System.Xml.XmlWriter xw)
        {
            if (this._items.Count > 0)
            {
                var array = this._items.Select(d => double_to_str(d)).ToArray();
                string str_value = string.Join(" ", array);
                xw.WriteAttributeString("StrokeDashArray", str_value);
            }
        }
    }
}