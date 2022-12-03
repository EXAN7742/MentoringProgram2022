using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Adapter
{
    internal class MyAdapter<T>: IContainer<T>
    {
        private IElements<T> _myElements;

        public IEnumerable<T> Items {
            get
            {
                return _myElements.GetElements();    
            } }
        public int Count {
            get
            {
                return _myElements.GetElements().Count();
            }
        }

        public MyAdapter(IElements<T> myElements)
        {
            _myElements = myElements;    
        }
    }
}
