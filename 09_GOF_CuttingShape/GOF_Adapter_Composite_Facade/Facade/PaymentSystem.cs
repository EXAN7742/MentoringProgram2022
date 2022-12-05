using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facade
{
    internal class PaymentSystem: IPaymentSystem
    {
        public bool MakePayment(Payment payment)
        {
            Console.WriteLine("Payment was successful ");
            return true;
        }
    }
}
