using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facade
{
    internal class ProductCatalog: IProductCatalog
    {
        List<Product> products;
        public ProductCatalog()
        {
            products = new List<Product>() { new Product { ID = "1", Name = "Product 1"}, new Product { ID = "2", Name = "Product 2" } };
        }

        public Product GetProductDetails(string productId)
        {
            Product product = products.Where(x => x.ID == productId).First();
            Console.WriteLine($"{product.Name} successfully selected");
            return product;
        }
    }
}
