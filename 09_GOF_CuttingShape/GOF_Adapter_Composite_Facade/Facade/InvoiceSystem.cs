using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facade
{
    internal class InvoiceSystem: IInvoiceSystem
    {
        public void SendInvoice(Invoice invoice)
        {
            Console.WriteLine("Invoice successfully sended");
        }
    }
}
