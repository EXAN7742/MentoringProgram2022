using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Composite
{
    public class Form
    {
        String name;

        public Form(String name)
        {
            this.name = name;
        }

        public void AddComponent()
        {
        }

        public string ConvertToString()
        {
            return String.Empty;
        }
    }
}
