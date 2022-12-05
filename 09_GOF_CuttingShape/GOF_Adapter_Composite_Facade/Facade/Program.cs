// See https://aka.ms/new-console-template for more information
using Facade;

ProductCatalog productCatalog= new ProductCatalog();
PaymentSystem paymentSystem = new PaymentSystem();
InvoiceSystem invoiceSystem = new InvoiceSystem();

EShopFacade eShop = new EShopFacade(productCatalog, paymentSystem, invoiceSystem);

eShop.MakeAnOrder("1", new Payment { Amount = 100, PaymentType = "Cash" });
