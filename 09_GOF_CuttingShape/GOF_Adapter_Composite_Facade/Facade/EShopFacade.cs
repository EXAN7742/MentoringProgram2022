using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facade
{
    internal class EShopFacade
    {
        IProductCatalog _productCatalog;
        IPaymentSystem _paymentSystem;
        IInvoiceSystem _invoiceSystem;

        public EShopFacade(IProductCatalog productCatalog, IPaymentSystem paymentSystem, IInvoiceSystem invoiceSystem)
        {
            _productCatalog = productCatalog;
            _paymentSystem = paymentSystem;
            _invoiceSystem = invoiceSystem;
        }

        public void MakeAnOrder(string productID, Payment payment)
        {
            _productCatalog.GetProductDetails(productID);
            _paymentSystem.MakePayment(payment);
            _invoiceSystem.SendInvoice(new Invoice { Amount = payment.Amount });
        }
    }
}
