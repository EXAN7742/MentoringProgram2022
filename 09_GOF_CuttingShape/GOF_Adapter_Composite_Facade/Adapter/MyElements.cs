using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adapter
{
    internal class MyElements<T>: IElements<T>
    {
        public IEnumerable<T> MyItems { get; set; }
        
        public IEnumerable<T>  GetElements() 
        {
            return MyItems;
        }
    }
}
