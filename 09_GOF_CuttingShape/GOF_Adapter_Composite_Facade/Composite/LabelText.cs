using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Composite
{
    public class LabelText: Component
    {
        string _value;
        public LabelText(string value)
        {
            _value = value;
        }

        public override string ConvertToString()

        {
            return $"<label value='{_value}'/>\n\r";
        }

    }
}
