using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Composite
{
    public abstract class Component
    {
        public virtual void Add(Component component) { }

        public abstract string ConvertToString();

    }
}
