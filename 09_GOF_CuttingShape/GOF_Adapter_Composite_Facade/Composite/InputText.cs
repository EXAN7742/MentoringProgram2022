using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Composite
{
    public class InputText: Component
    {
        string _name;
        string _value;
        public InputText(string name, string value)
        {  
            _name = name;
            _value = value;
        }

        public override string ConvertToString()
        {
            return $"<inputText name='{_name}' value='{_value}'/>\n\r";
        }
    }
}
