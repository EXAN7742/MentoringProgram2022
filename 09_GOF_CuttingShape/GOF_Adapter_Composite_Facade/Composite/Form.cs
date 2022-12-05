using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Composite.Task1
{
    internal class Form: Component
    {
        private List<Component> components = new List<Component>();
        string _name;
        public Form(string name)
        {
            _name= name;
        }
        public override void Add(Component component)
        {
            components.Add(component);
        }

        public override string ConvertToString()
        {
            StringBuilder str = new StringBuilder($"<form name={_name}>\n\r");
            for (int i = 0; i < components.Count; i++)
            {
                str.Append(components[i].ConvertToString());
            }
            str.Append($"</form>\n\r");
            return str.ToString();
        }
    }
}
